using Ae.B.Order.Api.Core.Model.OrderDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response.OrderQuery
{
    public class GetOrderConfirmResponse : OrderCalcPriceInfoVO
    {
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        public byte DeliveryType { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; } = string.Empty;
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; } = string.Empty;
        /// <summary>
        /// 收货人地址Id（仅当配送类型为到家时有值）
        /// </summary>
        public long ReceiverAddressId { get; set; }
        /// <summary>
        /// 完整收货人地址（仅当配送类型为到家时有值）
        /// </summary>
        public string FullReceiverAddress { get; set; } = string.Empty;
        /// <summary>
        /// 选择的套餐或单品集合
        /// </summary>
        public List<OrderConfirmPackageProductVO> Products { get; set; }
        /// <summary>
        /// 选购的服务集合
        /// </summary>
        public List<OrderConfirmProductVO> Services { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1微信支付 2到店支付）
        /// </summary>
        public byte Payment { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public bool IsNeedInvoice { get; set; }

        /// <summary>
        /// 上门安装服务
        /// </summary>
        public OrderConfirmProductVO DoorToDoorService { get; set; }
    }
}
