using System.Collections.Concurrent;

namespace Amld.Extensions.Logging
{
    public class Enhancer
    {
        private static readonly AsyncLocal<Enhancer> _local = new AsyncLocal<Enhancer>();
        internal ConcurrentDictionary<object,object> keyValues= new();

        private Enhancer() { }

        public static Enhancer Current => _local.Value ??= new Enhancer();


        public string AppId { get; set; }
        public string ChainId { get; set; }
        public string TraceId { get; set; }
        public string ParentTraceId { get; set; }
    }
}