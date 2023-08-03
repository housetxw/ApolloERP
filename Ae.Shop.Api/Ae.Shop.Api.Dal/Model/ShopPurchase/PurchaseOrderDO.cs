using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.ShopPurchase
{

    [Table("purchase_order")]
    public class PurchaseOrderDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 采购类型 类型 1 公司采购 2 门店采购，3内销单，4外销单
        /// </summary>
        [Column("purchase_type")]
        public int PurchaseType { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column("vender_id")]
        public long VenderId { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        [Column("vender_short_name")]
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 供客仓库Id
        /// </summary>
        [Column("vender_warehouse_id")]
        public long VenderWarehouseId { get; set; }
        /// <summary>
        /// 供客仓库简称
        /// </summary>
        [Column("vender_warehouse_name")]
        public string VenderWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }
        /// <summary>
        /// 仓库简称
        /// </summary>
        [Column("warehouse_name")]
        public string WarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2待发货 3已取消 4已发货 5部分收货 6已收货 )
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 发票类型(0 无需发票 2 普通发票 3 增值税发票)
        /// </summary>
        [Column("pinvoice_type")]
        public int PinvoiceType { get; set; }
        /// <summary>
        /// 结算方式(1 现金 2 账期 3月结)
        /// </summary>
        [Column("pay_type")]
        public int PayType { get; set; }
        /// <summary>
        /// 支付状态(1未付款 2部分付款 3已付款)
        /// </summary>
        [Column("pay_status")]
        public int PayStatus { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        [Column("coupon_amount")]
        public decimal CouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        [Column("actual_amount")]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        [Column("buyer")]
        public string Buyer { get; set; } = string.Empty;
        /// <summary>
        /// 运单号
        /// </summary>
        [Column("waybill_number")]
        public string WaybillNumber { get; set; } = string.Empty;
        /// <summary>
        /// 自负运费
        /// </summary>
        [Column("own_freight")]
        public decimal OwnFreight { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店简称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public int IsDeleted { get; set; }
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

        [Column("pay_method")]
        public string PayMethod { get; set; } = string.Empty;

        [Column("serial_number")]
        public string SerialNumber { get; set; } = string.Empty;

        [Column("payer")]
        public string Payer { get; set; } = string.Empty;

        [Column("pay_time")]
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 月结标识
        /// </summary>
        [Column("mouth_pay_flag")]
        public string MouthPayFlag { get; set; } = string.Empty;

        /// <summary>
        /// 关联红冲采购单
        /// </summary>
        [Column("relation_purchase_id")]
        public long RelationPurchaseId { get; set; }

        /// <summary>
        /// 关联红冲采购单
        /// </summary>
        [Column("hc_type")]
        public string HcType { get; set; } = string.Empty;

    }
}