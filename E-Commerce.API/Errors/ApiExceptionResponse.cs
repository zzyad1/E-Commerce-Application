namespace E_Commerce.API.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? errorMassage = null, string? details = null) 
            : base(statusCode, errorMassage)
        {
            Details = details;
        }
    }
}
