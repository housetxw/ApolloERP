using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request
{
    /// <summary>
    /// 得到订单基本信息请求对象
    /// </summary>
    public class GetOrderBaseInfoRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 订单Ids
        /// </summary>
        public List<long> OrderIds { get; set; }

        /// <summary>
        /// 安装码集合
        /// </summary>
        public List<string> InstallCodes { get; set; }
    }
}
