using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Common.Exceptions;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Enums;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Imp.OperationLog;

namespace Ae.BasicData.Service.Filters
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class FilterAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="categoryName"></param>
        public FilterAttribute(string categoryName) : base(typeof(FilterImpl))
        {
            Arguments = new object[] { categoryName };
        }

        private class FilterImpl : IAsyncActionFilter, IAsyncResultFilter, IAsyncExceptionFilter
        {
            private IDictionary<string, object> actionArguments;

            private readonly ILogger logger;

            private readonly IBaseLogRepository logRepo;

            private readonly Stopwatch stopwatch;

            public FilterImpl(IBaseLogRepository logRepo, ILoggerFactory loggerFactory, string categoryName)
            {
                logger = loggerFactory.CreateLogger(categoryName);
                this.logRepo = logRepo;
                stopwatch = new Stopwatch();
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                stopwatch.Start();

                // 如果参数验证不通过，返回参数错误的信息
                if (!context.ModelState.IsValid)
                {
                    var errorMessages = new List<string>();
                    foreach (var key in context.ModelState.Keys)
                    {
                        var state = context.ModelState[key];
                        var errorModel = state?.Errors?.FirstOrDefault();
                        if (errorModel != null)
                            errorMessages.Add($"{key}: {errorModel.ErrorMessage}");
                    }

                    var val = new ApiResult
                    {
                        Code = ResultCode.ArgumentError,
                        Message = string.Join(',', errorMessages)
                    };
                    context.Result = new ObjectResult(val);
                    return;
                }

                // 临时保存一下，为了如果异常的时候可以使用
                actionArguments = context.ActionArguments;

                await next();
            }

            public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
            {
                await next();

                stopwatch.Stop();

                var time = stopwatch.ElapsedMilliseconds;
                //if (time > CommonConstant.FiveHundred) RecordElapsedTime(context, time);
                RecordElapsedTime(context, time);
            }

            public async Task OnExceptionAsync(ExceptionContext context)
            {
                var type = context.Exception.GetType();

                if (type == typeof(CustomException)) // 如果是自定义异常，直接返回失败状态和错误信息
                {
                    context.Result = new ObjectResult(Result.Failed(context.Exception.Message));
                }
                else // 异常
                {
                    context.Result = new ObjectResult(Result.Exception($"{context.ActionDescriptor.RouteValues["controller"]} {context.ActionDescriptor.RouteValues["action"]} Exception"));

                    RecordExceptionLog(context);
                }

                //logger.LogError(context.Exception, "方法异常自动抛出");

                await Task.CompletedTask;
            }

            private void RecordExceptionLog(ExceptionContext ctx)
            {
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
                var modelParam = JsonConvert.SerializeObject(actionArguments);

                var msgDetail = "\n【过滤器异常自动抛出】" +
                                $"\n【RequestType】：{reqType}，【RouteValue】：{routeValue}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterReqDetail}】：{reqParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterModelDetail}】：{modelParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.DetailedInformation}】：\n  ";
                logger.LogError(ctx.Exception, msgDetail);
            }

            private void RecordElapsedTime(ResultExecutingContext context, long time)
            {
                Task.Run(() =>
                {
                    string res = JsonConvert.SerializeObject(context.Result);
                    byte[] tmp = Encoding.Default.GetBytes(res ?? "");
                    if (tmp.Length > 5000)
                    {
                        res = Encoding.Default.GetString(tmp.Take(4999).ToArray());
                    }

                    logRepo.InsertLog(new LogOperationReqDTO
                    {
                        LogType = TableNameEnum.ElapsedTime.ToString(),
                        BizType = TableNameEnum.ElapsedTime.GetEnumDescription(),
                        ReqParam = JsonConvert.SerializeObject(actionArguments),
                        OperatedBeforeContent = $"{time.ToString()} {CommonConstant.MilliUnit}",
                        OperatedAfterContent = res,
                        Comment = $"{context.ActionDescriptor.RouteValues["controller"]}.{context.ActionDescriptor.RouteValues["action"]}"
                    });
                });
            }

        }
    }
}
