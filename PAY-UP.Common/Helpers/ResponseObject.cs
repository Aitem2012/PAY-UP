namespace PAY_UP.Common.Helpers
{
    public class ResponseObject<T>
    {
        public string Message { get; set; }
        public bool IsSuccessfull { get; set; }
        public T Data { get; set; }

        public ResponseObject<T> CreateResponse(string message, bool isSuccessful, T data)
        {
            return new ResponseObject<T>
            {
                Message = message,
                IsSuccessfull = isSuccessful,
                Data = data
            };
        }
    }
}
