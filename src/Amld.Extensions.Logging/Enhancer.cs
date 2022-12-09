namespace Amld.Extensions.Logging
{
    public class Enhancer
    {
        public static string AppId { get; set; }

        public static Enhancer Current => _local.Value ??= new Enhancer();
        private static readonly AsyncLocal<Enhancer> _local = new();
        private Enhancer() { }

        public string ChainId { get; set; }
        public string TraceId { get; set; }
        public string ParentTraceId { get; set; }
    }
}