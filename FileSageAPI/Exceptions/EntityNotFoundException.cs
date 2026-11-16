using System.Net;

namespace FileSageAPI.Types.Exceptions
{
    /// <summary>
    /// Used when trying to access an entity that does not exist in the database
    /// </summary>
    public class EntityNotFoundException : HttpException
    {
        public EntityNotFoundException(string message = "Intended Entity Not Found") : base(message, HttpStatusCode.NotFound)
        { }
    }
}
