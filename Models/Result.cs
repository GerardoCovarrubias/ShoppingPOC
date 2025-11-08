namespace Models
{
   
    public class Result
    {
        public ResultStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public Result(ResultStatusCode statusCode, string errorMessage = "")
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

    }
}