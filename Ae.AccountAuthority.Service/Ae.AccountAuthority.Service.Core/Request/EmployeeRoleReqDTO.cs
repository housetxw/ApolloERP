using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Core.Base.Request;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Request
{
    #region ！！！授权接口Model！！！

    public class AuthorizationReqDTO : BaseRequest
    {
        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "请输入员工所属单位类型")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        [Required(ErrorMessage = "请输入员工所属单位Id")]
        [Range(0, long.MaxValue, ErrorMessage = "请输入正确的员工所属单位Id")]
        public long OrganizationId { get; set; }
    }

    public class RangeRoleAuthorityReqDTO : BaseRequest
    {
        public string EmployeeId { get; set; }

        public List<long> RoleIds { get; set; }
    }

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleReqDTO { }

    public class EmployeeRoleListReqDTO
    {
        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }
    }

    public class EmployeeRoleListByEmpIdsReqDTO
    {
        public List<string> EmployeeIds { get; set; }
    }

    public class EmployeeRoleSaveComReqDTO
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 操作者中文名称
        /// </summary>
        [Required(ErrorMessage = "请输入操作者中文名称")]
        public string Operator { get; set; }
    }

    public class EmployeeRoleSaveReqDTO : EmployeeRoleSaveComReqDTO
    {
        public List<EmployeeRoleEntityDTO> EmployeeRoleList { get; set; }
    }

    public class EmployeeRoleSaveReqWithRoleIdDTO : EmployeeRoleSaveComReqDTO
    {
        /// <summary>
        /// 操作者EmployeeId
        /// </summary>
        [Required(ErrorMessage = "请输入操作者EmployeeId")]
        public string OperatorId { get; set; }

        public List<long> RoleIds { get; set; } = new List<long>();
    }

    public class EmployeeDefaultRoleReqDTO : EmployeeRoleSaveComReqDTO
    {
        /// <summary>
        /// 操作者EmployeeId
        /// </summary>
        [Required(ErrorMessage = "请输入操作者EmployeeId")]
        public string OperatorId { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public int Type { get; set; }
    }

    public enum EmployeeType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,
        [Description("拓展")]
        Extend = 3,
        [Description("供应商")]
        Supplier = 4,

        [Description("所有类型")]
        All = 9999
    }

}
