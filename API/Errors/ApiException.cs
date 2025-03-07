namespace API.Errors;

// esta es la respuesta q manda mi ExceptionMiddleware cuando 
// hay una excepcion
public class ApiException(int statusCode, string message, string? details)
{
    //public ApiException(int statusCode, string message = null,
    //                    string details = null)
    //{
    //    StatusCode = statusCode;
    //    Message = message;
    //    Details = details;
    //}

    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
