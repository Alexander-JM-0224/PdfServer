using Microsoft.AspNetCore.Components;

namespace PdfServer.Components.Pages
{
    public partial class Home
    {

        public HtmlRenderService HtmlRenderService { get; set; }



        private void SavePdf2()
        {
            var result = HtmlRenderService.TakeScreenshot("www.google.nl");
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
