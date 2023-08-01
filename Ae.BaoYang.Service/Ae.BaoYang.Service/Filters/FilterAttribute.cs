using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Common.Exceptions;
using System.Text;
using Newtonsoft.Json;
using Ae.BaoYang.Service.Common.Constant;

namespace Ae.BaoYang.Service.Filters
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

        private class FilterImpl : IAsyncActionFilter, IAsyncExceptionFilter
        {
            private IDictionary<string, object> _actionArguments;

            private readonly ILogger _logger;

            public FilterImpl(ILoggerFactory loggerFactory, string categoryName)
            {
                _logger = loggerFactory.CreateLogger(categoryName);
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // 如果参数验证不通过，返回参数错误的信息
                if (!context.ModelState.IsValid)
                {
                    var errorMessages = new List<string>();
                    foreach (var key in context.ModelState.Keys)
                    {
                        var state = context.ModelState[key];
                        var errorModel = state?.Errors?.FirstOrDefault();
                        if (errorModel != null)
                            errorMessages.Add($"{key}:{errorModel.ErrorMessage}");
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

                await next();
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
                    context.Result = new ObjectResult(Result.Exception(
                        $"{context.ActionDescriptor.RouteValues["controller"]} {context.ActionDescriptor.RouteValues["action"]} Exception"));
                    RecordExceptionLog(context);
                }

                await Task.CompletedTask;
            }

            private void RecordExceptionLog(ExceptionContext ctx)
            {
                var sb = new StringBuilder();
                var req = ctx.HttpContext.Request;
                foreach (var data in ctx.RouteData.Values)
                {
                    sb.Append(sb.Length <= 0 ? $"{data.Key.ToUpper()}: {data.Value}" : $" | {data.Key.ToUpper()}: {data.Value}");
                }
                var routeValue = sb.ToString();

                var reqType = req.Method;
                var queryStr = req.QueryString;
                var reqParam = queryStr.HasValue ? JsonConvert.SerializeObject(queryStr.Value) : CommonConstant.ParameterNone;
                var modelParam = JsonConvert.SerializeObject(_actionArguments);

                var msgDetail = "\n【过滤器异常自动抛出】" +
                                $"\n【RequestType】：{reqType}，【RouteValue】：{routeValue}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterReqDetail}】：{reqParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.ParameterModelDetail}】：{modelParam}" +
                                $"\n【{CommonConstant.ErrorOccured}, {CommonConstant.DetailedInformation}】：{ctx.Exception.Message}\n  ";
                _logger.LogError(ctx.Exception, msgDetail);
            }
        }
    }
}
