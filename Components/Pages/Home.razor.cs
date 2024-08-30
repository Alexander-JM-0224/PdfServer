using Microsoft.AspNetCore.Components;

namespace PdfServer.Components.Pages
{
    public partial class Home
    {

        public HtmlRenderService HtmlRenderService { get; set; }

        private async void SavePdf()
        {
            var abu = "abu";
            var result = await HtmlRenderService.RenderServiceCheckReport(0);
        }

        private void Henk()
        {
            var h = "Henk";
        }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}
