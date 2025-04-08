namespace DragonBall.Application.DTOs.SwaggerResponses
{
    public class Response
    {
        public required string Message { get; set; }
        public required int StatusCode { get; set; }
        public required string Details { get; set; }
    }
}
