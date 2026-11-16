using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    public class InvalidFormFile: HttpException
    {
        public InvalidFormFile(string message = "Form File Invalid") : base(message, HttpStatusCode.BadRequest)
        { }
    }
}
