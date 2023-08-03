using Ae.Shop.Api.Client.Enums;
using Ae.Shop.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Response
{
    public class QueryOrderDetailUseStockResponse
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单渠道（0未设置 1C端 2门店）
        /// </summary>
        public ChannelEnum OrderChannel { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已取消 40已完成）
        /// </summary>
        public OrderStatusEnum OrderStatus { get; set; }
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }

        public long ShopId { get; set; }

        /// <summary>
        /// 套餐或单商品信息集合
        /// </summary>
        public List<UseStockOrderDetailProductDTO> Products { get; set; }


        public int ProduceType { get; set; }
    }
}
