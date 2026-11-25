using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Book.DTOs;
using FileSageAPI.Entities.Url.DTOs;
using FileSageAPI.Entities.Url.Layers;
using FileSageAPI.Scraping.Interface;
using FileSageAPI.Types.Enums;
using HtmlAgilityPack;
using Microsoft.Playwright;

namespace FileSageAPI.Scraping.Scrapers
{
    public class Ao3Scrapping : IScraping
    {
        public async Task<BookInsertDTO> ScrapBook(IPage page, string url)
        {

            await page.GotoAsync( url,
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded
                });

            var pagehtml = await page.ContentAsync();


            var html = new HtmlDocument();
            html.LoadHtml(pagehtml);

            Console.WriteLine(pagehtml);

            var title = html.DocumentNode.SelectSingleNode("//h2[contains(@class,'title heading')]")?.InnerText.Trim();
            var words = html.DocumentNode.SelectSingleNode("//dd[contains(@class,'words')]")?.InnerText.Replace(",", "").Trim();
            var chapters = html.DocumentNode.SelectSingleNode("//dd[contains(@class,'chapters')]")?.InnerText.Replace(",", "").Trim().Split("/");
            var summary = html.DocumentNode.SelectSingleNode("//blockquote[contains(@class,'userstuff')]")?.InnerText.Replace("\n", "").Trim();
            var published = html.DocumentNode.SelectSingleNode("//dd[contains(@class,'published')]")?.InnerText.Trim();
            var totalChapters = chapters != null && chapters.Length > 1 ? chapters[1] : null;

            var actualChapter = chapters != null && chapters.Length > 1 ? chapters[0] : null;


            WritingStatus? writingStatus = null;

            if (totalChapters != null && totalChapters != "" & actualChapter != null)
            {
                writingStatus = totalChapters == actualChapter ? WritingStatus.Finished : WritingStatus.InProgress;
            }


            var book = new BookInsertDTO()
            {
                Title = title ?? "",
                Summary = summary ?? "",
                Words = int.TryParse(words, out int parsedWords) ? parsedWords : null,
                TotalChapters = int.TryParse(totalChapters, out int parsedChapters) ? parsedChapters : null,
                Published = DateOnly.TryParse(published, out DateOnly parsedPublished) ? parsedPublished : null,
                WritingStatus = writingStatus,
                Urls = [new UrlInsertNestedDTO { Name= UrlFunctions.GenerateUrlName(url), Content = url }]

            };

            return book;
        }

        public async Task<AuthorModel> ScrapAuthor(IPage page, string url)
        {
            return new AuthorModel();

        }


    }
}
