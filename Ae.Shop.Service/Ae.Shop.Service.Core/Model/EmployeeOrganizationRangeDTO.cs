using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Model
{
    public class EmployeeOrganizationRangeDTO
    {

        /// <summary>
        /// 应主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>
        /// 管辖范围的单位Id
        /// </summary>
        public long OrganizationId { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        /// <summary>
        /// 管辖范围的角色
        /// </summary>
        public string RoleIds { get; set; } = string.Empty;

        /// <summary>
        /// 是否禁用，可用
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
