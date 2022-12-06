using Microsoft.Extensions.Logging;

namespace Amld.Extensions.Logging
{
    public class LoggerOption
    {
        /// <summary>
        /// 是否前台打印
        /// </summary>
        public bool Console { get; set; } = false;
        /// <summary>
        /// 日志级别
        /// </summary>
        public IDictionary<string, LogLevel> LogLevel { get; set; } = new Dictionary<string, LogLevel>();
        /// <summary>
        /// 日志队列长度
        /// </summary>
        public int MaxQueneCount { get; internal set; } = 5000;
    }
}
