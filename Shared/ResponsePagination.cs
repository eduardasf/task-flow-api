using TaskFlow_API.Domains;

namespace TaskFlow_API.Shared
{
    public class ResponsePagination
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public List<Tarefa> Data { get; set; }
        public PageEvent PageEvent { get; set; }

        public ResponsePagination() { }

        public ResponsePagination(bool success, string message,
            List<Tarefa> data, PageEvent pageEvent)
        {
            Success = success;
            Message = message;
            Data = data;
            PageEvent = pageEvent;
        }
    }
}
