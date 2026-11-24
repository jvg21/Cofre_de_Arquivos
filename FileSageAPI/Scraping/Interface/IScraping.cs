using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Book.DTOs;
using Microsoft.Playwright;

namespace FileSageAPI.Scraping.Interface
{
    public interface IScraping
    {
        Task<BookInsertDTO> ScrapBook(IPage page, string url);
        Task<AuthorModel> ScrapAuthor(IPage page, string url);
    }
}
