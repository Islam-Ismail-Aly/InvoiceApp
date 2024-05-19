namespace InvoiceApp.API.Utilities
{
    public class APIResponseResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public APIResponseResult(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public APIResponseResult(string message)
        {
            Success = false;
            Message = message;
        }

        public APIResponseResult()
        {
            
        }
    }
}
