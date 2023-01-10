using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var logContext = BuildLogContext(httpContext);
            using var _ = LogContext.SetContxt(logContext);
            await _next(httpContext);
            _logger.LogTrace(LogLevel.Information, new TraceEntry
            {
                Request = new Request
                {
                    Path = $"{httpContext.Request.Path}{httpContext.Request.QueryString.Value}",
                    Method = httpContext.Request.Method
                },
                Response = new Response
                {
                    
                }
            });
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