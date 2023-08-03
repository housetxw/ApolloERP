using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    public class OrderDTO
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
        /// 渠道（0未设置 1C端 2门店）
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
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
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
        /// 商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        public int TotalProductNum { get; set; }
        /// <summary>
        /// 商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务数量（指带出的安装服务，不含套餐内安装服务）
        /// </summary>
        public int ServiceNum { get; set; }
        /// <summary>
        /// 服务费（指带出的安装服务，不含套餐内安装服务）
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
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝）
        /// </summary>
        public sbyte PayChannel { get; set; }
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 到账状态（0未到账 1已到账）
        /// </summary>
        public sbyte MoneyArriveStatus { get; set; }
        /// <summary>
        /// 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 是否需要开发票（0否 1是）
        /// </summary>
        public sbyte IsNeedInvoice { get; set; }
        /// <summary>
        /// 是否需要配送（0否 1是）
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
        /// 配送状态（0未配送 1已配送）
        /// </summary>
        public sbyte DeliveryStatus { get; set; }
        /// <summary>
        /// 签收状态（0未签收 1已签收）
        /// </summary>
        public sbyte SignStatus { get; set; }
        /// <summary>
        /// 是否需要安装服务（0否 1是）
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
        /// 预约状态（0未预约 1已预约）
        /// </summary>
        public sbyte ReserveStatus { get; set; }
        /// <summary>
        /// 到店状态（0未到店 1已到店）
        /// </summary>
        public sbyte ReceiveStatus { get; set; }
        /// <summary>
        /// 派工状态（0未派工 1已派工）
        /// </summary>
        public sbyte DispatchStatus { get; set; }
        /// <summary>
        /// 安装服务状态（0未安装 1已安装）
        /// </summary>
        public sbyte InstallStatus { get; set; }
        /// <summary>
        /// 点评状态（0待客户点评  1 客户已点评 2 客户已追评）
        /// </summary>
        public sbyte CommentStatus { get; set; }
        /// <summary>
        /// 是否发生过逆向申请（0否 1是）
        /// </summary>
        public sbyte IsOccurReverse { get; set; }
        /// <summary>
        /// 逆向申请单状态（10已提交 20已确认 30已取消 40已完成）
        /// </summary>
        public sbyte ReverseStatus { get; set; }
        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
        /// <summary>
        /// 订单对账状态（0未对账 1异常 2已对账）
        /// </summary>
        public sbyte ReconciliationStatus { get; set; }
        /// <summary>
        /// 结算状态（0未结算 1已结算）
        /// </summary>
        public sbyte SettleStatus { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识（0否 1是）
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
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime ConfirmTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始配送的时间
        /// </summary>
        public DateTime DeliveryTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SignTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime ReserveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 到店时间
        /// </summary>
        public DateTime ReceiveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 派工时间
        /// </summary>
        public DateTime DispatchTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 对账的时间
        /// </summary>
        public DateTime ReconciliationTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 整单取消的时间
        /// </summary>
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
