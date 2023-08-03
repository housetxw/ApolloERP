using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.ShopCost
{
    [Table("shop_cost")]
    public class ShopCostDO
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 费用日期
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// 费用类别
        /// </summary>
        [Column("cost_type")]
        public long CostType { get; set; }
        /// <summary>
        /// 费用类别中文
        /// </summary>
        public string CostTypeLabel { get; set; }
        /// <summary>
        /// 费用金额
        /// </summary>
        [Column("money")]
        public decimal Money { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        [Column("pay_channel")]
        public string PayChannel { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        [Column("invoice_type")]
        public int InvoiceType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public string CreateTime { get; set; } = DateTime.Now.ToString();
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public string UpdateTime { get; set; } = DateTime.Now.ToString();
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("is_deleted")]
        public int IsDeleted { get; set; }
    }
}
