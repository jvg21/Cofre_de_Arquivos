using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    public class UnknownException: HttpException
    {
        public UnknownException(string message = "Unknown Exception") : base(message, HttpStatusCode.InternalServerError)
        { }
    }
}
