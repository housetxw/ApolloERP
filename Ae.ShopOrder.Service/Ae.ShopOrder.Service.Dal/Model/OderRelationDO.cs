using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("oder_relation")]
    public class OderRelationDO
    {
        /// <summary>
        /// 
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
        /// 关联关系
        /// </summary>
        [Column("refer")]
        public string Refer { get; set; } = string.Empty;
        /// <summary>
        /// 关联订单
        /// </summary>
        [Column("refer_order")]
        public string ReferOrder { get; set; } = string.Empty;

        /// <summary>
        /// 关联交易号
        /// </summary>
        [Column("refer_transfer_no")]
        public string ReferTransferNo { get; set; } = string.Empty;
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
    }
}
