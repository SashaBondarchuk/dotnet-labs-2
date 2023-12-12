using Restaurant.Common.Enums;
using Restaurant.Common.Exceptions;
using System.Net;

namespace Restaurant.Common.Extensions
{
    public static class ExceptionFilterExtensions
    {
        public static (HttpStatusCode statusCode, ErrorCode errorCode) ParseException(this Exception exception)
        {
            return exception switch
            {
                NotFoundException _ => (HttpStatusCode.NotFound, ErrorCode.NotFound),
                BadOperationException _ => (HttpStatusCode.BadRequest, ErrorCode.BadRequest),
                _ => (HttpStatusCode.InternalServerError, ErrorCode.General),
            };
        }
    }
}