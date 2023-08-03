using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Receive
{
    public class CanReserveOrderDTO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public decimal OrderTotalAmount { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 可预约产品列表
        /// </summary>
        public List<RebookReserveProductDTO> ReserveProductVOs { get; set; }
    }
}
