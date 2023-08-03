using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Web.WebApi;

namespace Ae.ShopOrder.Service.Core.Response.Sign
{
    /// <summary>
    /// 今日包裹签收
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TodaySignPackageApiPagedResult<T> : ApiPagedResultData<T>
    {
        /// <summary>
        /// 今日签收包裹数量
        /// </summary>
        public int TitleNum { get; set; }
    }
}
