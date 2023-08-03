using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    /// <summary>
    /// 得到订单基本信息
    /// </summary>
    public class GetOrderBaseInfoRequest 
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 订单Ids
        /// </summary>
        public List<int> OrderIds { get; set; }

        /// <summary>
        /// 安装码集合
        /// </summary>
        public List<string> InstallCodes { get; set; }
    }
}
