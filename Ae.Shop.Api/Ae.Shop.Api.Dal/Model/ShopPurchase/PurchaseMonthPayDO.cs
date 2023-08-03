using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{

    [Table("purchase_month_pay")]
    public class PurchaseMonthPayDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }



        [Column("vender_id")]
        public long VenderId { get; set; }


        [Column("vender_name")]
        public string VenderName { get; set; } = string.Empty;


        [Column("bank_name")]
        public string BankName { get; set; } = string.Empty;

        [Column("account_no")]
        public string AccountNo { get; set; } = string.Empty;

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("pay_method")]
        public string PayMethod { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("audit_time")]
        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>

        [Column("audit_user")]
        public string AuditUser { get; set; } = string.Empty;

        [Column("serial_number")]
        public string SerialNumber { get; set; } = string.Empty;

        [Column("payer")]
        public string Payer { get; set; } = string.Empty;

        [Column("pay_time")]
        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>

        [Column("cancle_time")]
        public DateTime CancleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>

        [Column("cancle_user")]
        public string CancleUser { get; set; } = string.Empty;





        /// <summary>
        /// 备注说明
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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



        [Column("relation_purchaseIds")]
        public string RelationPurchaseIds { get; set; } = string.Empty;

        [Column("opening_bank")]
        public string OpeningBank { get; set; } = string.Empty;

        [Column("receive_bank_name")]
        public string ReceiveBankName { get; set; } = string.Empty;

    }
}
