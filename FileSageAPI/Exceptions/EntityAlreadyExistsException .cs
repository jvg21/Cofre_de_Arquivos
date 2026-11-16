using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    /// <summary>
    ///     Used when trying to create an entity that already exists in the database
    /// </summary>
    public class EntityAlreadyExistsException : HttpException
    {
        public EntityAlreadyExistsException(string message = "Intended Entity Already Exists") : base(message, HttpStatusCode.Conflict)
        { }
    }
}
