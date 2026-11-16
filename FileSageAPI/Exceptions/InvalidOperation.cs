using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    /// <summary>
    /// Used when the repository encounters an invalid operation, such as trying to link an archive to a tag that doesn't exist
    /// or trying to insert a url with an invalid archive id
    /// </summary>
    public class InvalidOperation:HttpException
    {
        public InvalidOperation(string message = "Invalid Database Operation") : base(message, HttpStatusCode.BadRequest)
        { }
    }
}
