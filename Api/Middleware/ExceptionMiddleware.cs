using Application.DTOs;

namespace BankApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    { 
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Paso 1: Indico que la respuesta siempre será un JSON
            httpContext.Response.ContentType = "application/json";
            var message = string.Empty;

            // Paso 2: Evaluamos el tipo de excepción para asignar el StatusCode correcto
            if (ex is InvalidOperationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad Request (Regla de negocio rota)
                message = ex.Message;
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; // Internal Server Error (Error inesperado)
                message = "Ocurrió un error inesperado.";
            }
            
            // Paso 3: Creamos una respuesta con el mensaje adecuado
            var errorResponse = new ErrorResponseDto
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            };
            
            // Paso 4: Escribir el DTO serializado como JSON en el cuerpo de la respuesta
            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}