using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Model.Order;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    /// <summary>
    /// 不适配返回信息
    /// </summary>
    public class GetOrderNotAdapterInfoResponse
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 是否有历史记录
        /// </summary>
        public bool IsHistory { get; set; }


        /// <summary>
        /// 车辆不适配
        /// </summary>
        public List<CarInfoVO> CarInfoList { get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        public List<ProductNotAdapterVO> ProductList { get; set; }
    }
}
