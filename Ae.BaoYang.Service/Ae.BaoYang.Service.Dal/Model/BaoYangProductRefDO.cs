using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_product_ref")]
    public class BaoYangProductRefDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 配件类型
        /// </summary>
        [Column("part_type")]
        public string PartType { get; set; }
        /// <summary>
        /// 配件号
        /// </summary>
        [Column("part_code")]
        public string PartCode { get; set; }
        /// <summary>
        /// 产品pid
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
