namespace Amld.Logs.API.Models.Entities
{
    public class LogMessage
    {
        /// <summary>
        /// 应用程序ID
        /// </summary>

        public string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 日志级别
        /// </summary>
        public int LogLevel { get; set; }

        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}
