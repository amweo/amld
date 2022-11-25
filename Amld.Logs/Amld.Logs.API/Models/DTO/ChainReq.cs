namespace Amld.Logs.API.Models.DTO
{
    public class ChainReq
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
        /// 链路ID
        /// </summary>
        public string ChainId { get; set; } = string.Empty;
    }
}
