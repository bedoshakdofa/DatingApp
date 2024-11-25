namespace DatingApp.Errors
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string ErrorStack { get; set; }
        public string Message { get; set; }
        public ApiException(int statusCode, string message, string errorStack)
        {
            StatusCode = statusCode;
            ErrorStack = errorStack;
            Message = message;
            
        }


       
    }
}
