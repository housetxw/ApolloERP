using Ae.Receive.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    /// <summary>
    /// 得到订单基本信息
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
        public List<int> OrderIds { get; set; }

        /// <summary>
        /// 安装码集合
        /// </summary>
        public List<string> InstallCodes { get; set; }
    }
}
