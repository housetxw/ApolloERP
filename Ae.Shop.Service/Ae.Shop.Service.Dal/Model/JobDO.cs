using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("job")]
    public class JobDO
    {
        /// <summary>
        /// 职位id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 职位编号
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 所属单位Id
        /// </summary>
        [Column("organization_id")]
        public int OrganizationId { get; set; }
        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        [Column("type")]
        public JobType Type { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
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