using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Model.Order;
using System.Collections.Generic;

namespace Ae.Order.Service.Core.Response
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
        /// <summary>
        /// 预约ID
        /// </summary>
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
