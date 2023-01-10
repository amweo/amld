namespace Amld.Extensions.Logging.AspNetCore
{
    internal class TraceEntry:LogEntry
    {
        public Request Request { get; set; }

        public Response Response { get; set; }

        public int TimeSpan { get; set; }
    }

    public class Request
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public int ContentLength { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }

    public class Response
    {
        public string Body { get; set; }
        public long ContentLength { get; set; }
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
