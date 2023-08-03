using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.ShopPurchase
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
        /// 采购类型 1 商品内采 2 商品外采
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
        /// 状态(1新建 2待发货 3已取消 4已发货 5部分收货 6已收货)
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 发票类型(0 无需发票 2 普通发票 3 增值税发票)
        /// </summary>
        [Column("pinvoice_type")]
        public int PinvoiceType { get; set; }
        /// <summary>
        /// 结算方式(1 现金 2 账期 )
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
    }
}
