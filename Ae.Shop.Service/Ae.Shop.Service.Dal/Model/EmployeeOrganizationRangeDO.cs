using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("employee_organization_range")]
    public class EmployeeOrganizationRangeDO
    {
        /// <summary>
        /// 应主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>
        /// 管辖范围的单位Id
        /// </summary>
        [Column("organization_id")]
        public long OrganizationId { get; set; }
         
        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        [Column("type")]
        public EmployeeType Type { get; set; }

        /// <summary>
        /// 管辖范围的角色
        /// </summary>
        [Column("role_ids")]
        public string RoleIds { get; set; } = string.Empty;

        /// <summary>
        /// 是否禁用，可用
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
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
