namespace RequestTracker.Common.Models
{
    public record RequestInfo
    {
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
    }
}
