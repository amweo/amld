namespace Amld.Logs.API.Models
{

    public class GR<T>: GR
    {
        public T? Data { get; set; }
        public Page? Page { get; set; }
    }

    public class GR
    {
        public string? Code { get; set; }
        public string? Msg { get; set; }

        public static GR Success(string code = "0", string msg = "")
        {
            return new GR { Code = code, Msg = msg };
        }

        public static GR<T> Success<T>(T data)
        {
            return Success(data);
        }

        public static GR<T> Success<T>(T data, string code = "0", string msg = "", Page? page = null)
        {
            return new GR<T> { Data = data, Code = code, Msg = msg, Page = page };
        }

        public static GR Fail(string code, string msg)
        {
            return new GR { Code = code, Msg = msg };
        }
        public static GR<T> Fail<T>(string code, string msg)
        {
            return new GR<T> { Code = code, Msg = msg };
        }
    }
    public class Page
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页条目数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        public Page(int pageIndex, int pageSize, int total)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
        }
    }
}
