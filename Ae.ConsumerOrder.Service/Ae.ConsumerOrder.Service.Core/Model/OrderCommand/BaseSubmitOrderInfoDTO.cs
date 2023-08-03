using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 提交订单基本信息
    /// </summary>
    public class BaseSubmitOrderInfoDTO
    {
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        [Required(ErrorMessage = "入口类型不能为空")]
        //[Range(1, 3, ErrorMessage = "入口类型范围错误")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生  5 上门服务）
        /// </summary>
        //[Range(0, 4, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 下单人用户Id
        /// </summary>
        [Required(ErrorMessage = "下单人用户Id不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 下单车辆Id
        /// </summary>
        //[Required(ErrorMessage = "下单车辆Id不能为空")]
        public string CarId { get; set; }
        /// <summary>
        /// 选择的门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        //[Required(ErrorMessage = "配送类型不能为空")]
        //[Range(1, 2, ErrorMessage = "配送类型范围错误")]
        public sbyte DeliveryType { get; set; }
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
        /// 选择购买的产品信息集合
        /// </summary>
        [Required(ErrorMessage = "请选择商品")]
        [MinLength(1, ErrorMessage = "请至少选购一个商品")]
        public List<SelectedProductInfoDTO> ProductInfos { get; set; }

        /// <summary>
        /// 支付方式（0未设置 1微信支付 2到店支付）
        /// </summary>
        //[Required(ErrorMessage = "支付方式不能为空")]
        //[Range(1, 2, ErrorMessage = "支付方式范围错误")]
        public sbyte Payment { get; set; }
        /// <summary>
        /// 选择使用的用户优惠券Id（0未选择）
        /// </summary>
        public long UserCouponId { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public bool IsNeedInvoice { get; set; } = false;
    }
}
