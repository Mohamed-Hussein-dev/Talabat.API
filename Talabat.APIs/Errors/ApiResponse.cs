namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int statusCode { get; set; }
        public string? message { get; set; }

        public ApiResponse(int _statusCode , string ?_massage = null)
        {
            statusCode = _statusCode;
            message = _massage?? getMassage(_statusCode);
        }

        private string? getMassage(int statusCode)
        {
            return statusCode switch
            {
                400 => "BadRequest",
                401 => "You Are Not Authrized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            }; 
        }
    }
    
}
