using Ae.C.MiniApp.Api.Client.Model.Order;
using System.Collections.Generic;

namespace Ae.C.MiniApp.Api.Client.Response.Order
{
    public class GetOrderConfirmClientResponse : OrderCalcPriceInfoDTO
    {
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        public byte DeliveryType { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; }
        /// <summary>
        /// 收货人地址Id（仅当配送类型为到家时有值）
        /// </summary>
        public long ReceiverAddressId { get; set; }
        /// <summary>
        /// 完整收货人地址（仅当配送类型为到家时有值）
        /// </summary>
        public string FullReceiverAddress { get; set; }

        /// <summary>
        /// 选择的套餐或单品集合
        /// </summary>
        public List<OrderConfirmPackageProductDTO> Products { get; set; }

        /// <summary>
        /// 选购的服务集合
        /// </summary>
        public List<OrderConfirmProductDTO> Services { get; set; }

        /// <summary>
        /// 支付方式（0未设置 1微信支付 2到店支付）
        /// </summary>
        public byte Payment { get; set; }
        /// <summary>
        /// 选择使用的用户优惠券Id（0未选择）
        /// </summary>
        public long UserCouponId { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public bool IsNeedInvoice { get; set; }


        /// <summary>
        ///钻石积分优惠金额
        /// </summary>
        public decimal DiamondsDiscountAmount { get; set; }
    }
}
