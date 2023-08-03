using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Sign
{
    /// <summary>
    ///  今日签收的包裹请求对象
    /// </summary>
    public class GetTodaySignPackageRequest: BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
