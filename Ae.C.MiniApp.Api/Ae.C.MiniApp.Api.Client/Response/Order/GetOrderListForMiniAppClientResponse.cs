using Ae.C.MiniApp.Api.Client.Model.Order;
using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Order
{
    public class GetOrderListForMiniAppClientResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        public long ReserveId { get; set; }
        /// <summary>
        /// 订单渠道
        /// </summary>
        public ChannelEnum Channel { get; set; }
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
        /// 是否折叠显示商品集合
        /// </summary>
        public bool IsCollapseShowProducts { get; set; } = false;
        /// <summary>
        /// 商品信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Products { get; set; }
        /// <summary>
        /// 订单用户当前可执行操作集合
        /// </summary>
        public List<OrderUserOperationDTO> OrderUserOperations { get; set; }
    }
}
