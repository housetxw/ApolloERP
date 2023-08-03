using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.ShopOrder.Service.Core.Model.Order;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 提交订单请求
    /// </summary>
    public class SubmitOrderRequest
    {
        /// <summary>
        /// 渠道 1C端 2门店
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网 6 boss）
        /// </summary>
        public sbyte TerminalType { get; set; }
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        [Required(ErrorMessage = "入口类型不能为空")]
        //[Range(0, 6, ErrorMessage = "入口类型范围错误")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        //[Range(0, 4, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        //[Required(ErrorMessage = "下单人用户Id不能为空")]
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
        /// 选择购买的产品信息集合
        /// </summary>
        //[Required(ErrorMessage = "请选择商品")]
        //[MinLength(1, ErrorMessage = "请至少选购一个商品")]
        public List<SelectedProductInfoDTO> ProductInfos { get; set; }

        /// <summary>
        /// 待更新的对象
        /// </summary>
        public List<UpdateProductInfo> UpdateProductInfos { get; set; } = null;

        /// <summary>
        ///  1在线支付 2到店付款
        /// </summary>
        //[Required(ErrorMessage = "支付方式不能为空")]
        public sbyte PayMethod { get; set; }

        /// <summary>
        /// 支付方式（0未设置 1微信支付 2支付宝 3 美团 4线下 9代付款）
        /// </summary>
        //[Required(ErrorMessage = "支付渠道不能为空")]
        public sbyte PayChannel { get; set; }

        /// <summary>
        /// 下单操作人
        /// </summary>
        //[Required(ErrorMessage = "下单操作人不能为空")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 促销类型
        /// </summary>
        public string PromotionChannel { get; set; }

        /// <summary>
        /// 选择使用的用户优惠券Id（0未选择）
        /// </summary>
        public long UserCouponId { get; set; }
        /// <summary>
        /// 是否需要发票
        /// </summary>
        public bool IsNeedInvoice { get; set; } = false;

        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        //[Required(ErrorMessage = "配送类型不能为空")]
        //[Range(1, 2, ErrorMessage = "配送类型范围错误")]
        public sbyte DeliveryType { get; set; }

        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ArrivalId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 安装方式(0:未设置 1:到店安装 2：上门安装 3：无需安装）
        /// </summary>
        public sbyte InstallType { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserverTime { get; set; }

        /// <summary>
        /// 其他优惠金额
        /// </summary>
        public decimal OtherCouponAmount { get; set; } = 0;

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
        /// 代理者UserId
        /// </summary>
        public string DelegateUserId { get; set; }

        /// <summary>
        /// 代理者名称
        /// </summary>
        public string DelegateUserName { get; set; }

        /// <summary>
        /// 客户下单的门店
        /// </summary>
        public long DelegateUserShopId { get; set; }

        /// <summary>
        /// 押金单分类  A、B、C
        /// </summary>
        public string DepositOrderCategory { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        [MaxLength(100)]
        public string InsuranceName { get; set; }

        /// <summary>
        /// 保险关联单号
        /// </summary>
        [MaxLength(100)]
        public string InsuranceRefNo { get; set; }

        /// <summary>
        /// 已有订单号
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 美团订单号
        /// </summary>
        public string MeiTuanOrder { get; set; }

        /// <summary>
        /// 美团交易单号
        /// </summary>
        public string MeiTuanTransferOrder { get; set; }

    }

    public class UpdateProductInfo
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 商品的自营属性(0:自营，1：shop)
        /// </summary>
        public int ProductOwnAttri { get; set; }

        /// <summary>
        /// 支付押金价格
        /// </summary>
        public decimal Price { get; set; }


        public string OldPid { get; set; }

        public List<UpdateProductInfo> Child { get; set; }
    }
}
