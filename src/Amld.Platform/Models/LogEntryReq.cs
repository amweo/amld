namespace Amld.Platform.Models
{
    public class LogEntryReq
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
    }
}
