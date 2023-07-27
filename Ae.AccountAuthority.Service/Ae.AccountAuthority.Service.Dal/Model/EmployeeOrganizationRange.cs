using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.AccountAuthority.Service.Dal.Model
{
    [Table("employee_organization_range")]
    public class EmployeeOrganizationRange
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("employee_id")]
        public string EmployeeId { get; set; }

        [Column("organization_id")]
        public long OrganizationId { get; set; }

        [Column("type")]
        public int Type { get; set; }

        [Column("role_ids")]
        public string RoleIds { get; set; }



        /// <summary>
        /// 是否删除
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
