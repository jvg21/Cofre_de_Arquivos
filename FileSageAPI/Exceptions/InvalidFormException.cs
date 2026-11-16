using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    ///used  when the data for a api call contains invalid data, missing required fields, or incorrect data format.
    public class InvalidFormException:HttpException
    {
        public InvalidFormException(string message = "Input Form Have Invalid Data") : base(message, HttpStatusCode.BadRequest)
        { }
    }
}
