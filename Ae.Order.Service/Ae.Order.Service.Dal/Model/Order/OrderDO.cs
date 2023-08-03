using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Order.Service.Dal.Model
{
    [Table("order")]
    public class OrderDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        [Column("channel")]
        public sbyte Channel { get; set; }
        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        [Column("terminal_type")]
        public sbyte TerminalType { get; set; }
        /// <summary>
        /// 应用版本号
        /// </summary>
        [Column("app_version")]
        public string AppVersion { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        [Column("order_type")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        [Column("produce_type")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        [Column("order_status")]
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        [Column("order_time")]
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 下单人用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 下单人姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 下单人电话
        /// </summary>
        [Column("user_phone")]
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 联系人用户Id
        /// </summary>
        [Column("contact_id")]
        public string ContactId { get; set; } = string.Empty;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Column("contact_name")]
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// 联系人电话
        /// </summary>
        [Column("contact_phone")]
        public string ContactPhone { get; set; } = string.Empty;
        /// <summary>
        /// 商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        [Column("total_product_num")]
        public int TotalProductNum { get; set; }
        /// <summary>
        /// 商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        [Column("total_product_amount")]
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务数量（指带出的安装服务，不含套餐内安装服务）
        /// </summary>
        [Column("service_num")]
        public int ServiceNum { get; set; }
        /// <summary>
        /// 服务费（指带出的安装服务，不含套餐内安装服务）
        /// </summary>
        [Column("service_fee")]
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        [Column("delivery_fee")]
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 总优惠金额
        /// </summary>
        [Column("total_coupon_amount")]
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 手动优惠金额
        /// </summary>
        [Column("other_coupon_amount")]
        public decimal OtherCouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        [Column("actual_amount")]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        [Column("pay_method")]
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝）
        /// </summary>
        [Column("pay_channel")]
        public sbyte PayChannel { get; set; }
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        [Column("pay_status")]
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 到账状态（0未到账 1已到账）
        /// </summary>
        [Column("money_arrive_status")]
        public sbyte MoneyArriveStatus { get; set; }
        /// <summary>
        /// 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        [Column("stock_status")]
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 是否需要开发票（0否 1是）
        /// </summary>
        [Column("is_need_invoice")]
        public sbyte IsNeedInvoice { get; set; }
        /// <summary>
        /// 是否需要配送（0否 1是）
        /// </summary>
        [Column("is_need_delivery")]
        public sbyte IsNeedDelivery { get; set; }
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        [Column("delivery_type")]
        public sbyte DeliveryType { get; set; }
        /// <summary>
        /// 配送方式（0未设置 1自配 2快递）
        /// </summary>
        [Column("delivery_method")]
        public sbyte DeliveryMethod { get; set; }
        /// <summary>
        /// 配送状态（0未配送 1已配送）
        /// </summary>
        [Column("delivery_status")]
        public sbyte DeliveryStatus { get; set; }
        /// <summary>
        /// 签收状态（0未签收 1已签收）
        /// </summary>
        [Column("sign_status")]
        public sbyte SignStatus { get; set; }
        /// <summary>
        /// 是否需要安装服务（0否 1是）
        /// </summary>
        [Column("is_need_install")]
        public sbyte IsNeedInstall { get; set; }
        /// <summary>
        /// 安装码
        /// </summary>
        [Column("install_code")]
        public string InstallCode { get; set; } = string.Empty;
        /// <summary>
        /// 公司Id
        /// </summary>
        [Column("company_id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 预约状态（0未预约 1已预约）
        /// </summary>
        [Column("reserve_status")]
        public sbyte ReserveStatus { get; set; }
        /// <summary>
        /// 到店状态（0未到店 1已到店）
        /// </summary>
        [Column("receive_status")]
        public sbyte ReceiveStatus { get; set; }
        /// <summary>
        /// 派工状态（0未派工 1已派工）
        /// </summary>
        [Column("dispatch_status")]
        public sbyte DispatchStatus { get; set; }
        /// <summary>
        /// 安装服务状态（0未安装 1已安装）
        /// </summary>
        [Column("install_status")]
        public sbyte InstallStatus { get; set; }
        /// <summary>
        /// 点评状态（0待客户点评  1 客户已点评 2 客户已追评）
        /// </summary>
        [Column("comment_status")]
        public sbyte CommentStatus { get; set; }
        /// <summary>
        /// 是否发生过逆向申请（0否 1是）
        /// </summary>
        [Column("is_occur_reverse")]
        public sbyte IsOccurReverse { get; set; }
        /// <summary>
        /// 逆向申请单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        [Column("reverse_status")]
        public sbyte ReverseStatus { get; set; }
        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        [Column("refund_status")]
        public sbyte RefundStatus { get; set; }
        /// <summary>
        /// 总部对账状态（0未对账 1异常 2已对账）
        /// </summary>
        [Column("reconciliation_status")]
        public sbyte ReconciliationStatus { get; set; }
        /// <summary>
        /// 结算状态（0未结算 1已结算）
        /// </summary>
        [Column("settle_status")]
        public sbyte SettleStatus { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识（0否 1是）
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 支付时间
        /// </summary>
        [Column("pay_time")]
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 确认时间
        /// </summary>
        [Column("confirm_time")]
        public DateTime ConfirmTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 开始配送的时间
        /// </summary>
        [Column("delivery_time")]
        public DateTime DeliveryTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 签收时间
        /// </summary>
        [Column("sign_time")]
        public DateTime SignTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 预约时间
        /// </summary>
        [Column("reserve_time")]
        public DateTime ReserveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 到店时间
        /// </summary>
        [Column("receive_time")]
        public DateTime ReceiveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 派工时间
        /// </summary>
        [Column("dispatch_time")]
        public DateTime DispatchTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 安装时间
        /// </summary>
        [Column("install_time")]
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 对账的时间
        /// </summary>
        [Column("reconciliation_time")]
        public DateTime ReconciliationTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 结算时间
        /// </summary>
        [Column("settle_time")]
        public DateTime SettleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 整单取消的时间
        /// </summary>
        [Column("cancel_time")]
        public DateTime CancelTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 安装方式(0:未设置 1:到店安装 2：上门安装 3：无需安装）
        /// </summary>
        [Column("install_type")]
        public sbyte InstallType { get; set; }
        /// <summary>
        /// 退款时间
        /// </summary>
        [Column("refund_time")]
        public DateTime RefundTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 退款金额
        /// </summary>
        [Column("refund_amount")]
        public decimal RefundAmount { get; set; }
    }
}
