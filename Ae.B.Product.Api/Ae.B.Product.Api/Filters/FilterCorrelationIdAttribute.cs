using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Common.Constant;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Filters
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class FilterCorrelationIdAttribute: TypeFilterAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="categoryName"></param>
        public FilterCorrelationIdAttribute(string categoryName) : base(typeof(FilterImpl))
        {
            Arguments = new object[] { categoryName };
        }

        private class FilterImpl : IAsyncActionFilter, IAsyncExceptionFilter, IAsyncResultFilter
        {
            private IDictionary<string, object> _actionArguments;
            private readonly ILogger _logger;
            private readonly ICorrelationContextAccessor _correlationContext;
            private LoggerWatch _loggerWatch;
            private Stopwatch stopwatch;
            private string correlationId;

            public FilterImpl(ILoggerFactory loggerFactory, string categoryName,
                ICorrelationContextAccessor correlationContext)
            {
                _logger = loggerFactory.CreateLogger(categoryName);
                _correlationContext = correlationContext;
            }

            //方法执行前
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                _loggerWatch = new LoggerWatch();
                string controlname = string.Empty;
                context.ActionDescriptor?.RouteValues?.TryGetValue("controller", out controlname);
                if (string.IsNullOrEmpty(controlname) || controlname.Equals("Home"))
                {
                    await next();
                }
                else
                {
                    _loggerWatch.StartTime = DateTime.Now.ToLocalTime().ToString();
                    _loggerWatch.Action = context.ActionDescriptor?.AttributeRouteInfo?.Template;
                    correlationId = _correlationContext?.CorrelationContext?.CorrelationId;
                    _loggerWatch.CorrelationId = correlationId ?? string.Empty;

                    // 如果参数验证不通过，返回参数错误的信息
                    if (!context.ModelState.IsValid)
                    {
                        var errorMessages = new List<string>();
                        foreach (var key in context.ModelState.Keys)
                        {
                            var state = context.ModelState[key];
                            var errorModel = state?.Errors?.FirstOrDefault();
                            if (errorModel != null)
                                //errorMessages.Add($"{key}:{errorModel.ErrorMessage}");
                                errorMessages.Add($"{errorModel.ErrorMessage}");
                        }

                        var val = new ApiResult()
                        {
                            Code = ResultCode.ArgumentError,
                            Message = string.Join(',', errorMessages)
                        };
                        context.Result = new ObjectResult(val);
                        return;
                    }

                    // 临时保存一下，为了如果异常的时候可以使用
                    _actionArguments = context.ActionArguments;
                    _loggerWatch.RequestArguments = JsonConvert.SerializeObject(_actionArguments);

                    await next();
                }
            }

            //方法执行后
            public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
            {
                if (stopwatch == null) { stopwatch = Stopwatch.StartNew(); }
                if (_loggerWatch == null) { _loggerWatch = new LoggerWatch(); }
                var objectResult = context.Result as ObjectResult;
                stopwatch?.Stop();
                _loggerWatch.ResponseResult = JsonConvert.SerializeObject(objectResult?.Value);
                _loggerWatch.TotalTime = stopwatch.ElapsedMilliseconds;
                string result = $"traceid:{JsonConvert.SerializeObject(_loggerWatch)}";
                _logger.LogInformation(result);
                await next();
            }

            //方法执行失败
            public async Task OnExceptionAsync(ExceptionContext context)
            {
                var type = context.Exception.GetType();
                // 如果是自定义业务异常，直接返回失败状态和错误信息
                if (type == typeof(CustomException))
                {
                    context.Result = new ObjectResult(Result.Failed(context.Exception.Message));
                    RecordCustomExceptionLog(context);
                }
                else
                {
                    //系统异常
                    string serviceName =
                        context.ActionDescriptor?.DisplayName?.Substring(
                            context.ActionDescriptor.DisplayName.IndexOf("("));
                    string msg = $"{CommonConstant.NotCatchException}{serviceName}";
                    context.Result = new ObjectResult(Result.Exception(msg));
                    RecordExceptionLog(context);
                }

                await Task.CompletedTask;
            }

            //记录逻辑异常信息到日志
            private void RecordCustomExceptionLog(ExceptionContext ctx)
            {
                if (stopwatch == null) { stopwatch = Stopwatch.StartNew(); }
                if (_loggerWatch == null) { _loggerWatch = new LoggerWatch(); }
                stopwatch?.Stop();
                _loggerWatch.TotalTime = stopwatch.ElapsedMilliseconds;
                var sb = new StringBuilder();
                var req = ctx.HttpContext.Request;
                foreach (var data in ctx.RouteData.Values)
                {
                    sb.Append(sb.Length <= 0
                        ? $"{data.Key.ToUpper()}: {data.Value}"
                        : $" | {data.Key.ToUpper()}: {data.Value}");
                }

                var routeValue = sb.ToString();

                var reqType = req.Method;
                var queryStr = req.QueryString;
                var reqParam = queryStr.HasValue
                    ? JsonConvert.SerializeObject(queryStr.Value)
                    : CommonConstant.ParameterNone;
                var modelParam = JsonConvert.SerializeObject(_actionArguments);

                var msgDetail = "\n【过滤器业务异常自动抛出】" +
                                $"\n【RequestType】：{reqType}，【RouteValue】：{routeValue}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterReqDetail}】：{reqParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterModelDetail}】：{modelParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.DetailedInformation}】：\n{ctx.Exception.Message} ";

                _loggerWatch.ErrorMessage = msgDetail;
                string result = $"traceid:{JsonConvert.SerializeObject(_loggerWatch)}";
                //_logger.LogInformation(ctx.Exception, result);
                _logger.LogWarning(ctx.Exception, result);
            }

            //记录系统异常信息到日志
            private void RecordExceptionLog(ExceptionContext ctx)
            {
                if (stopwatch == null) { stopwatch = Stopwatch.StartNew(); }
                if (_loggerWatch == null) { _loggerWatch = new LoggerWatch(); }
                stopwatch?.Stop();
                _loggerWatch.TotalTime = stopwatch.ElapsedMilliseconds;
                var sb = new StringBuilder();
                var req = ctx.HttpContext.Request;
                foreach (var data in ctx.RouteData.Values)
                {
                    sb.Append(sb.Length <= 0
                        ? $"{data.Key.ToUpper()}: {data.Value}"
                        : $" | {data.Key.ToUpper()}: {data.Value}");
                }

                var routeValue = sb.ToString();

                var reqType = req.Method;
                var queryStr = req.QueryString;
                var reqParam = queryStr.HasValue
                    ? JsonConvert.SerializeObject(queryStr.Value)
                    : CommonConstant.ParameterNone;
                var modelParam = JsonConvert.SerializeObject(_actionArguments);

                var msgDetail = "\n【过滤器异常自动抛出】" +
                                $"\n【RequestType】：{reqType}，【RouteValue】：{routeValue}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterReqDetail}】：{reqParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterModelDetail}】：{modelParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.DetailedInformation}】：\n{ctx.Exception.Message} ";

                _loggerWatch.ErrorMessage = msgDetail;
                string result = $"traceid:{JsonConvert.SerializeObject(_loggerWatch)}";
                _logger.LogError(ctx.Exception, result);
            }
        }

        private class LoggerWatch
        {
            public string CorrelationId { get; set; }
            public string Action { get; set; }
            public long TotalTime { get; set; }
            public string StartTime { get; set; }
            public string RequestArguments { get; set; }
            public string ResponseResult { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
