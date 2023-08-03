using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("home_care_record")]
    public class HomeCareRecordDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 技师编号
        /// </summary>
        [Column("tech_id")]
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称
        /// </summary>
        [Column("tech_name")]
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 领取人
        /// </summary>
        [Column("receive_name")]
        public string ReceiveName { get; set; } = string.Empty;
        /// <summary>
        /// 领取时间
        /// </summary>
        [Column("receive_time")]
        public DateTime ReceiveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 品类数
        /// </summary>
        [Column("category_num")]
        public int CategoryNum { get; set; }
        /// <summary>
        /// 单品数
        /// </summary>
        [Column("sum_product_num")]
        public int SumProductNum { get; set; }
        /// <summary>
        /// 状态(新建/部分退货/已退货)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
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

        [NotMapped]
        public List<string> StatusList { get; set; } = new List<string>();
    }
}