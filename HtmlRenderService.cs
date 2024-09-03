using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using PdfServer.Components.Pages;
using PuppeteerSharp;
using PuppeteerSharp.BrowserData;

namespace PdfServer
{
    public class HtmlRenderService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;
        private string ChromiumPath => Path.Combine(Path.GetTempPath());

        public HtmlRenderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        }

        public async Task<string> RenderComponentAsync<TComponent>(Dictionary<string, object?> parameters) where TComponent : IComponent
        {
            try
            {
                await using var htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);

                var parameterView = ParameterView.FromDictionary(parameters);

                var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
                {
                    var output = await htmlRenderer.RenderComponentAsync<TComponent>(parameterView);
                    return output.ToHtmlString();
                });

                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> RenderComponentObjectAsync<TComponent>(Dictionary<string, object?> parameters) where TComponent : IComponent
        {
            try
            {
                await using var htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);

                var parameterView = ParameterView.FromDictionary(parameters);

                var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
                {
                    var output = await htmlRenderer.RenderComponentAsync<TComponent>(parameterView);
                    return output.ToHtmlString();
                });

                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<byte[]> RenderServiceCheckReport(int serviceActionId)
        {
            try
            {
                var dictionary = new Dictionary<string, object?>
                {
                    { "ServiceactionId", serviceActionId },
                };
                await using var htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);

                var parameterView = ParameterView.FromDictionary(dictionary);

                var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
                {

                    var output = await htmlRenderer.RenderComponentAsync<Container>();
                    return output.ToHtmlString();
                });

                var result = await GenerateBlazor(html);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<byte[]> GenerateBlazor(string content)
        {
            var bf = await InitializeBrowser();
            byte[] pdfBytes = null;
            await using var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(
            new LaunchOptions
            {
                Headless = true,
                ExecutablePath = bf.GetExecutablePath(Chrome.DefaultBuildId),
            });
            await using (var page = await browser.NewPageAsync())
            {
                await page.SetContentAsync(@$"{content}");

                pdfBytes = await page.PdfDataAsync(new PdfOptions()
                {
                    PrintBackground = true,
                });
            };

            return pdfBytes;
        }

        public async Task<string> TakePdfScreenshot()
        {
            try
            {
                using var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
                //var browserFetcher = InitializeBrowser();
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                var page = await browser.NewPageAsync();
                await page.GoToAsync("http://www.google.com");
                //await page.GoToAsync("http://www.jama.nl");
                string filename = $@"C:\Temp\ggl_{DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HHmmss")}.pdf";
                await page.ScreenshotAsync(filename);
                return filename;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<byte[]> TakeScreenshot(string url)
        {
            try
            {
                var bf = await InitializeBrowser();
                byte[] pdfBytes = null;
                await using var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(
                    new LaunchOptions
                    {
                        Headless = true,
                    });
                await using var page = await browser.NewPageAsync();
                await page.GoToAsync($"{url}");
                await page.ScreenshotAsync(@"C:\Temp\Pdfpdfddfd.pdf");
                pdfBytes = await page.ScreenshotDataAsync(new ScreenshotOptions()
                {
                    FullPage = true,
                });
                return pdfBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async Task<BrowserFetcher> InitializeBrowser()
        {
            BrowserFetcher bf = null;
            var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions { Path = ChromiumPath });
            await browserFetcher.DownloadAsync(Chrome.DefaultBuildId);
            bf = browserFetcher;
            return bf;
        }
    }
}
