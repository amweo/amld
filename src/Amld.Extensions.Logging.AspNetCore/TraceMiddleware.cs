using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Amld.Extensions.Logging.AspNetCore
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        public ILogger<TraceMiddleware> _logger;


        public TraceMiddleware(ILogger<TraceMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogContext logContext = BuildLogContext(context);
            using var _ = LogContext.SetContxt(logContext);
            //记录请求URL
            //记录请求信息
            //记录响应信息
            //记录完成时间
            await _next(context);
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
                ParentSpanId = pSpanId,
            };
            return logContext;
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