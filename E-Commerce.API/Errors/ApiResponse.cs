
namespace E_Commerce.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? errorMassage = null)
        {
            StatusCode = statusCode;
            ErrorMassage = errorMassage ?? GetStatusMassageForStatusCode(statusCode);
        }

        private string? GetStatusMassageForStatusCode(int statusCode)
        => statusCode switch 
        {
            500 => "Internal Server Error",
            404 => "Not Found",
            401 => "UnAuthorized",
            400 => "BadRequest",
            _ =>""
        };
        public int StatusCode { get; set; }
        public string? ErrorMassage { get; set; }
    }
}
