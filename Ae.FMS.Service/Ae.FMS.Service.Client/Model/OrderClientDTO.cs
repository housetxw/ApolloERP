using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Model
{
    public class OrderClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1总部C端 2总部门店 3电销下单 4第三方订单 5大客户）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public sbyte TerminalType { get; set; }
        /// <summary>
        /// 应用版本号
        /// </summary>
        public string AppVersion { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生）
        /// </summary>
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已取消 40已完成）
        /// </summary>
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 下单人用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 下单人电话
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 联系人用户Id
        /// </summary>
        public string ContactId { get; set; } = string.Empty;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; } = string.Empty;
        /// <summary>
        /// 商品总数
        /// </summary>
        public int TotalProductNum { get; set; }
        /// <summary>
        /// 商品总价
        /// </summary>
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 总优惠金额
        /// </summary>
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款 3月结）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝 3 美团 4线下 9代付款）
        /// </summary>
        public sbyte PayChannel { get; set; }
        /// <summary>
        /// 支付状态（未支付 已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 到账状态（未到账 已到账）
        /// </summary>
        public sbyte MoneyArriveStatus { get; set; }
        /// <summary>
        /// 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 是否需要开发票
        /// </summary>
        public sbyte IsNeedInvoice { get; set; }
        /// <summary>
        /// 是否需要配送
        /// </summary>
        public sbyte IsNeedDelivery { get; set; }
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        public sbyte DeliveryType { get; set; }
        /// <summary>
        /// 配送方式（0未设置 1自配 2快递）
        /// </summary>
        public sbyte DeliveryMethod { get; set; }
        /// <summary>
        /// 配送状态（未配送 已配送）
        /// </summary>
        public sbyte DeliveryStatus { get; set; }
        /// <summary>
        /// 签收状态（未签收 已签收）
        /// </summary>
        public sbyte SignStatus { get; set; }
        /// <summary>
        /// 是否需要安装服务
        /// </summary>
        public sbyte IsNeedInstall { get; set; }
        /// <summary>
        /// 安装码
        /// </summary>
        public string InstallCode { get; set; } = string.Empty;
        /// <summary>
        /// 公司Id
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 派工状态（未派工 已派工）
        /// </summary>
        public sbyte DispatchStatus { get; set; }
        /// <summary>
        /// 安装服务状态（未安装 已安装）
        /// </summary>
        public sbyte InstallStatus { get; set; }
        /// <summary>
        /// 点评状态（0待客户点评 1已客户点评 2已回复点评 3已客户追评 4已回复追评）
        /// </summary>
        public sbyte CommentStatus { get; set; }
        /// <summary>
        /// 是否发生退订
        /// </summary>
        public sbyte IsOccurRefund { get; set; }
        /// <summary>
        /// 退款状态（未退款 已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
        /// <summary>
        /// 订单对账状态（0:未设置 1：未对账 2：异常 3：已对账
        /// </summary>
        public sbyte ReconciliationStatus { get; set; }
        /// <summary>
        /// 结算状态（未结算 已结算）
        /// </summary>
        public sbyte SettleStatus { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SignTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 派工时间
        /// </summary>
        public DateTime DispatchTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始配送的时间
        /// </summary>
        public DateTime DeliveryTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 整个订单取消的时间
        /// </summary>
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);
    }

}
