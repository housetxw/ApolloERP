using Ae.Receive.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Response
{
    public class GetOrderListForMiniAppResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        public long ReserveId { get; set; }
        /// <summary>
        /// 显示订单状态（结合多个状态计算产生）
        /// </summary>
        public string DisplayOrderStatus { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 列表商品总数（套餐数+单服务数）
        /// </summary>
        public int ListTotalProductNum { get; set; }
        /// <summary>
        /// 商品信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Products { get; set; } = new List<OrderDetailProductDTO>();
    }
}
