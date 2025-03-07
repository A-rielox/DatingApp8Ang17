using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware;
// necesito darlo de alta en program.cs
public class ExceptionMiddleware(   RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env )
{
    // los middlewares necesitan el delegate para pasar al next
    // IHostEnvironment env --> p' ver en q ambiente estoy

    // con "HttpContext context" es q tengo acceso al req
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message); // p' ver el error en la terminal
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // ApiException es la clase q yo cree
            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
