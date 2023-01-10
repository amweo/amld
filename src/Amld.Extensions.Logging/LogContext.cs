namespace Amld.Extensions.Logging
{
    public class LogContext
    {
        private static readonly AsyncLocal<LogContext?> _local = new();
        public static LogContext? Current => _local.Value;
        public static void Clear() => _local.Value = null;

        public static IDisposable SetContxt(LogContext context)
        {
            _local.Value = context;
            return new LogContextDispose();
        }

        public string? TraceId { get; set; }
        public string? SpanId { get; set; }
        public string? ParentId { get; set; }
    }

    public class LogContextDispose : IDisposable
    {
        public void Dispose() => LogContext.Clear();
    }

}