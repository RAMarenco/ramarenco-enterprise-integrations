using System.Net;
using System.Text.Json;
using DragonBall.Application.CustomExceptions;

namespace DragonBall.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context);
            }
        }

        private static async Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var details = string.Empty;
            switch (ex)
            {
                case ConflictException conflict:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                case NoContentException noContent:
                    statusCode = HttpStatusCode.NoContent;
                    break;
                default:
                    details = ex.StackTrace ?? "";
                    break;
            }

            var response = new { message = ex.Message, statusCode, details };
            
            context.Response.StatusCode = (int)statusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                return;
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}