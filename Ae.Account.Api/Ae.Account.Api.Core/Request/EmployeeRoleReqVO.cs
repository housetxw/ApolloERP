using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Core.Request
{
    #region ！！！授权接口Model！！！

    public class AuthorizationReqVO : BaseRequest
    {
        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "请输入员工类型")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        [Required(ErrorMessage = "请输入员工所属单位Id")]
        [Range(1, long.MaxValue, ErrorMessage = "请输入正确的员工所属单位Id")]
        public int OrganizationId { get; set; }

        //标识取员工角色，还是范围角色
        [Required(ErrorMessage = "请输入IsOrganizationRange")]
        public bool IsOrganizationRange { get; set; }
    }

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleReqVO { }

    public class EmployeePageReqVO : BasePageRequest
    {
        public string Id { get; set; }

        public List<string> AccountId { get; set; } = new List<string>();

        public string EmployeeName { get; set; }

        public int OrganizationId { get; set; } = CommonConstant.NegativeOne;

        public EmployeeType Type { get; set; } = EmployeeType.None;

        public int IsDeleted { get; set; } = CommonConstant.NegativeOne;
    }

    public class EmployeeListReqVO
    {
        public string Id { get; set; }

        public int OrganizationId { get; set; }

        public int IsDeleted { get; set; } = CommonConstant.NegativeOne;

        public EmployeeType Type { get; set; } = EmployeeType.None;
    }

    public class EmployeeRoleListReqVO
    {
        public string EmployeeId { get; set; }

    }

    public class EmployeeRoleDialogListReqVO
    {
        public long OrganizationId { get; set; }

        public long RoleId { get; set; }

        public RoleType Type { get; set; } = RoleType.None;

        public int IsDeleted { get; set; } = CommonConstant.NegativeOne;

        public int? Features { get; set; }

    }

    public class EmployeeRoleSaveReqVO
    {
        [Required(ErrorMessage = "请输入正确的EmployeeId")]
        public string EmployeeId { get; set; }

        public List<long> RoleIdList { get; set; }

        public string Operator { get; set; }
    }

}
