using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("sign")]
    public class SignDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("relation_no")]
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 来源类型（0：未设置 1：订单)
        /// </summary>
        [Column("source_type")]
        public sbyte SourceType { get; set; }
        /// <summary>
        /// 包裹数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }

        /// <summary>
        /// 已签收包裹的数量
        /// </summary>
        [Column("have_sign_num")]
        public int HaveSignNum { get; set; }
        /// <summary>
        /// 签收状态(0:设置 1：未签收 2: 部分签收 1:全部签收）
        /// </summary>
        [Column("sign_status")]
        public sbyte SignStatus { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        [Column("package_nos")]
        public string PackageNos { get; set; } = string.Empty;
        /// <summary>
        /// 订单备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
