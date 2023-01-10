using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace Amld.Extensions.Logging.AspNetCore
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        public ILogger<TraceMiddleware> _logger;
        private static readonly List<string> DefaultContents = new List<string>() {
         "image",
         "video",
         "audio",
         "excel",
         "world",
         "stream"
        };

        public TraceMiddleware(ILogger<TraceMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //设置日志上下文
            var logContext = BuildLogContext(httpContext);
            using var _ = LogContext.SetContxt(logContext);

            //读取请求体
            var requestBody = await ReadRequestBodyAsync(httpContext.Request);
            //响应流
            using var currentStream = new MemoryStream();
            var originalBody = httpContext.Response.Body;
            httpContext.Response.Body = currentStream;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(httpContext);
            stopwatch.Stop();
            //读取响应内容
            var responseBody = await ReadResponseBodyAsync(httpContext.Response);

            //请求记录
            var req = new Request
            {
                Path = $"{httpContext.Request.Path}{httpContext.Request.QueryString.Value}",
                Method = httpContext.Request.Method,
                Body = requestBody,
            };
            //请求头
            foreach (var item in httpContext.Request.Headers)
            {
                req.Headers.Add(item.Key, item.Value.ToString());
            }
            //响应记录
            var res = new Response
            {
                Body = responseBody,
                StatusCode = httpContext.Response.StatusCode,
            };
            //响应头
            foreach (var item in httpContext.Response.Headers)
            {
                res.Headers.Add(item.Key, item.Value.ToString());
            }

            _logger.LogTrace(LogLevel.Information, new TraceEntry { Request = req, Response = res,TimeSpan= stopwatch.Elapsed.Milliseconds });
            await currentStream.CopyToAsync(originalBody);
        }

        /// <summary>
        /// 构建logContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static LogContext BuildLogContext(HttpContext context)
        {
            context.Request.Headers.TryGetValue("trace-id", out var traceId);
            if (string.IsNullOrEmpty(traceId))
            {
                traceId = Guid.NewGuid().ToString("N");
            }
            var pSpanId = string.Empty;
            context.Request.Headers.TryGetValue("span-id", out var spanId);
            if (string.IsNullOrEmpty(spanId))
            {
                spanId = Guid.NewGuid().ToString("N");
            }
            else
            {
                pSpanId = spanId.ToString();
                spanId = Guid.NewGuid().ToString("N");
            }
            var logContext = new LogContext
            {
                TraceId = traceId.ToString(),
                SpanId = spanId.ToString(),
                ParentId = pSpanId,
            };
            return logContext;
        }
        /// <summary>
        /// 读取请求体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            try
            {
                var requestBody = string.Empty;
                if (request.ContentType != null && request.ContentType.ToLower().Contains("multipart/form-data"))
                    return "文件详情忽略";

                if (request.ContentLength.HasValue && request.ContentLength.Value > 0)
                {
                    request.EnableBuffering();

                    //最多读取2048字节
                    var length = 2048;
                    if (request.ContentLength < length)
                        length = (int)request.ContentLength.Value;
                    var readBuffer = new char[length];

                    using var reader = new StreamReader(request.Body, Encoding.UTF8, false, length, true);
                    var readLength = await reader.ReadAsync(readBuffer, 0, length);

                    if (readLength > 0)
                        requestBody = new string(readBuffer, 0, readLength);

                    //重置
                    request.Body.Position = 0;
                    return requestBody;
                }

                return requestBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Request Body 错误");
                throw;
            }
        }
        /// <summary>
        /// 读取响应体
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            if (response.ContentType != null)
            {
                foreach (var item in DefaultContents)
                {
                    if (response.ContentType.Contains(item))
                    {
                        return "检查到文件流，内容不记录";
                    }
                }
            }

            try
            {
      
                var responseStr = string.Empty;
          
                var length = 2048;
                if (response.Body.Length < length)
                    length = (int)response.Body.Length;

                //读取指定长度
                response.Body.Position = 0;
                var readBuffer = new char[length];
                using var reader = new StreamReader(response.Body, Encoding.UTF8, false, length, true);
                var readLength = await reader.ReadAsync(readBuffer, 0, length);

                if (readLength > 0)
                    responseStr = new string(readBuffer, 0, readLength);
                //重置
                response.Body.Position = 0;
                return responseStr;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Response Body 错误");
                throw;
            }
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogTrace(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceMiddleware>();
        }
    }
}