namespace Amld.Logs.API.Models.DTO
{
    public class AllReq
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 1;

        /// <summary>
        /// 页面码
        /// </summary>
        public int PageIndex { get; set; } = 20;

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
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
