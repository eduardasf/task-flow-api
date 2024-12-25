namespace TaskFlow_API.Shared
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }

        public Response() { }

        public Response(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
