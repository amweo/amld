namespace Amld.Platform.Models
{
    public class LogEntry
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 链路ID
        /// </summary>
        public string ChainId { get; set; }
        /// <summary>
        /// 当前TraceId
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 父级TraceId
        /// </summary>
        public string ParentTraceId { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 事件Id
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string? Exception { get; set; }
        /// <summary>
        /// 日志消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
