using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Model
{
    public class EmployeeDTO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        public int OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public string Identity { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public decimal Score { get; set; }

        /// <summary>
        /// 离职类型；0：自动离职；1：辞退 等等
        /// </summary>
        public DimissionType DimissionType { get; set; }

        public string DimissionCause { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class EmployeePageDTO
    {
        public string Id { get; set; }
        /// <summary>
        /// 账户ID
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 性别 （0保密 1男 2女）
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 员工号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType EmployeeType { get; set; }
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 单位简称
        /// </summary>
        public string OrganizationSimpleName { get; set; }

        public bool IsOrganizationRange { get; set; }
        /// <summary>
        /// 单位类型
        /// </summary>
        public int ShopType { get; set; }

        public List<ShopTypeDTO> ShopTypeList { get; set; } = new List<ShopTypeDTO>();

        public string OrganizationImage { get; set; }

        public string OrganizationAddress { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public int JobId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        public int WorkKindId { get; set; }
        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkKindName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 离职类型；0：自动离职；1：辞退 等等
        /// </summary>
        public DimissionType DimissionType { get; set; }
        /// <summary>
        /// 离职原因
        /// </summary>
        public string DimissionCause { get; set; }
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 员工角色
        /// </summary>
        public string RoleName { get; set; }
    }

    public class EmployeeListDTO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationSimpleName { get; set; }

        public string OrganizationImage { get; set; }

        public string OrganizationAddress { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }

        public string Identity { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public decimal Score { get; set; }

        /// <summary>
        /// 离职类型；0：自动离职；1：辞退 等等
        /// </summary>
        public DimissionType DimissionType { get; set; }

        public string DimissionCause { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class OrgRangeRoleListForLoginResDTO
    {
        public string EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public string RoleIds { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }



}
