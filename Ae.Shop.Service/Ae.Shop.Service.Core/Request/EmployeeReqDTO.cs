using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Core.Request
{
    public class EmployeeReqDTO { }

    #region ！！！登录接口相关，请勿轻易对其修改！！！

    public class EmployeePageForLoginListReqDTO : BasePageRequest
    {
        public string AccountId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class EmployeePageForLoginListByTokenReqDTO : BasePageRequest
    {
        public string EmployeeId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class OrgRangeRoleListForLoginReqDTO
    {
        [Required(ErrorMessage = "请输入EmployeeId")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "请输入员工类型")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        [Required(ErrorMessage = "请输入员工所属单位Id")]
        [Range(1, long.MaxValue, ErrorMessage = "请输入正确的员工所属单位Id")]
        public int OrganizationId { get; set; }
    }


    #endregion ！！！登录接口相关，请勿轻易对其修改！！！

    
    public class EmployeePageReqDTO : BasePageRequest
    {
        public string Id { get; set; }

        /// <summary>
        /// 用于“账号管理”获取员工是否禁用的信息
        /// </summary>
        public List<string> AccountId { get; set; } = new List<string>();
        /// <summary>
        /// 名称
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 单位ID
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrganizationName { get; set; }

        public EmployeeType Type { get; set; } = EmployeeType.None;

        public int IsDeleted { get; set; } = -1;
    }

    public class EmployeeListReqDTO
    {
        //public string Id { get; set; }

        public string AccountId { get; set; }

        public int OrganizationId { get; set; }

        public int IsDeleted { get; set; } = -1;

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }

    public class EmployeeListReqByEmpIdDTO
    {
        public List<string> EmployeeId { get; set; }
    }

}
