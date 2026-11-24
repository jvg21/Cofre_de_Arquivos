
using FileSageAPI.Entities.Url.Layers;
using FileSageAPI.Scraping.Interface;
using Microsoft.Playwright;

namespace API.Scraping
{
    public static class ScrapingFunctions
    {

        public static IScraping? IdentifyScrapper(string url)
        {
            var urlSite = UrlFunctions.GenerateUrlName(url);
            if (urlSite == "AO3")
            {
                return new Ao3Scrapping();
            }
            return null;

        }

        public static async Task<BrowserSession> OpenBrowser()
        {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Firefox.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = true
                });

            var context = await browser.NewContextAsync(
                new BrowserNewContextOptions
                {
                    Locale = "pt-BR",
                    TimezoneId = "America/Sao_Paulo",
                    ViewportSize = new ViewportSize
                    {
                        Width = 1366,
                        Height = 768
                    },
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                        "AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/140.0.0.0 Safari/537.36",
                    ColorScheme = ColorScheme.Dark
                });

            return new BrowserSession(
                playwright,
                browser,
                context);
        }
    }
}