using Microsoft.Playwright;

namespace API.Scraping
{
    public class BrowserSession : IAsyncDisposable
    {
        public IPlaywright Playwright { get; }
        public IBrowser Browser { get; }
        public IBrowserContext Context { get; }

        public BrowserSession(IPlaywright playwright, IBrowser browser, IBrowserContext context)
        {
            Playwright = playwright;
            Browser = browser;
            Context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}