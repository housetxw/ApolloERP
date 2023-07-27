using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Common.Constant;

namespace Ae.Shop.Service.Client.Request
{
    public class AccountAuthorityReqDTO { }

    public class RoleListReqDTO
    {
        public long OrganizationId { get; set; }

        public RoleType Type { get; set; } = RoleType.None;

        public int? Features { get; set; }
        public int IsDeleted { get; set; } = 0;
    }

    public class EmployeeRoleSaveReqWithRoleIdDTO
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Required(ErrorMessage = "请输入员工Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "员工Id格式不正确")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 操作者中文名称
        /// </summary>
        [Required(ErrorMessage = "请输入操作者中文名称")]
        public string Operator { get; set; }

        /// <summary>
        /// 操作者EmployeeId
        /// </summary>
        [Required(ErrorMessage = "请输入操作者EmployeeId"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "操作者EmployeeId格式不正确")]
        public string OperatorId { get; set; }

        public List<long> RoleIds { get; set; } = new List<long>();
    }

    public class RoleListReqsDTO
    {
        public List<OrgEntityReqDTO> Items { get; set; } = new List<OrgEntityReqDTO>();
    }
    public class OrgEntityReqDTO
    {
        public long OrganizationId { get; set; }

        public RoleType Type { get; set; }
        public int SystemType { get; set; }
    }


    public class EmployeeRoleListReqDTO
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Required(ErrorMessage = "请输入员工Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "员工Id格式不正确")]
        public string EmployeeId { get; set; }
    }
    /// <summary>
    /// 获取员工角色列表
    /// </summary>
    public class EmployeeRoleListByEmpIdsReqDTO
    {
        public List<string> EmployeeIds { get; set; }
    }

}
