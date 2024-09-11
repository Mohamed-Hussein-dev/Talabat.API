namespace Talabat.APIs.Errors
{
    public class ApiExciptionError : ApiResponse
    {
        public string ? Details { get; set; }
        public ApiExciptionError(int _statusCode, string? _massage = null , string ? details = null) : base(_statusCode, _massage)
        {
            Details = details;
        }
    }
}
