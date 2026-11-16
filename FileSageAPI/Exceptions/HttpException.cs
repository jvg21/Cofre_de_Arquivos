using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    public class HttpException:Exception
    {
        public HttpStatusCode StatusCode { get; }
        protected HttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }

}
