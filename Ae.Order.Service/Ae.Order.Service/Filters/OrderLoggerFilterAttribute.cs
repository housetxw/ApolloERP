using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ae.Order.Service.Filters
{

    /// <summary>
    /// 订单日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OrderLoggerFilterAttribute : ResultFilterAttribute
    {
        private long OrderId { get; set; }
        //   public OrderLoggerFilterAttribute()
        /// <summary>
        /// 操作完成之后
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {

            base.OnResultExecuted(context);
        }
    }
}
