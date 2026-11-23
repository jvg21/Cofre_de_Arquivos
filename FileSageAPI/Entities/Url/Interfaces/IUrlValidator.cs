using FileSageAPI.Database.Models;

namespace FileSageAPI.Entities.Url.Interfaces
{
    public interface IUrlValidator
    {
        Task ValidateInsert(UrlModel url);
        Task ValidateInsert(List<UrlModel> urls);

        Task ValidateNestedInsert(UrlModel url);
        Task ValidateNestedInsert(List<UrlModel> urls);
    }
}
