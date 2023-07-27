using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Request
{
    public class EmployeeEntityReqDTO
    {
        public string EmployeeId { get; set; } = string.Empty;

        public string AccountId { get; set; } = string.Empty;

        public string Password { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "单位Id必须大于0")]
        public long OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        /// <summary>
        /// 单位类型 0：公司；1：门店；2：仓库
        /// </summary>
        public EmployeeType Type { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; } = string.Empty;

        public EmployeeGenderEnum Gender { get; set; }

        public string Identity { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public EmployeeLevelEnum Level { get; set; }

        public int WorkKindId { get; set; }

        //[Required(ErrorMessage = "岗位不能为空")]
        public int JobId { get; set; }

        public string WeChat { get; set; } = string.Empty;

        public string QQ { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public List<string> QualificationCertificate { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsNeedLogin { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 操作者Id
        /// </summary>
        //[Required(ErrorMessage = "操作者Id不能为空")]
        public string UserId { get; set; } = string.Empty;

        public List<long> RoleIds { get; set; } = new List<long>();

        public List<OrganizationRangeDTO> OrgRanges { get; set; } = new List<OrganizationRangeDTO>();
    }

    public class OrganizationRangeDTO
    {
        public long OrganizationId { get; set; }

        /// <summary>
        /// 单位类型 0：公司；1：门店；2：仓库
        /// </summary>
        public EmployeeType Type { get; set; }

        public List<long> RoleId { get; set; } = new List<long>();
    }

    //public class EmployeeOrganizationRangeDTO
    //{
    //    public string EmployeeId { get; set; }

    //    public long OrganizationId { get; set; }

    //    public EmployeeType Type { get; set; }

    //    public List<long> RoleId { get; set; } = new List<long>();

    //    public string CreateBy { get; set; } = string.Empty;

    //    /// <summary>
    //    /// 操作者Id
    //    /// </summary>
    //    public string UserId { get; set; } = string.Empty;
    //}

}
