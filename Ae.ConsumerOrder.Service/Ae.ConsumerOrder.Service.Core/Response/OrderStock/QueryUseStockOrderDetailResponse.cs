using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class QueryUseStockOrderDetailResponse
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
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
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

        /// <summary>
        /// 套餐或单商品信息集合
        /// </summary>
        public List<UseStockOrderDetailPackageProductDTO> Products { get; set; }

        /// <summary>
        /// 订单地址信息
        /// </summary>
        public OrderAddressDTO OrderAddress { get; set; }
    }
}
