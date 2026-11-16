using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    public class InvalidHeaderException:HttpException
    {
        public InvalidHeaderException(string message = "Input Header Have Invalid Data") : base(message, HttpStatusCode.BadRequest)
        { }
    }
}
