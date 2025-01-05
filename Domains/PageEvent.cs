namespace TaskFlow_API.Domains
{
    public class PageEvent
    {
        public int? First { get; set; }
        public int Rows { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public int? PageCount { get; set; }
        public string? GlobalFilter { get; set; }
        public Guid UserId { get; set; }
        public StatusTarefa? Status { get; set; }
    }
}
