using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Login.Api.Client.Response.ShopManage.Employee
{
    public class EmployeeResDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AccountId { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationSimpleName { get; set; }
        
        public string OrganizationImage { get; set; }

        public string OrganizationAddress { get; set; }
    }

    public class EmployeePageDTO
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

        public bool IsOrganizationRange { get; set; }

        public int ShopType { get; set; }

        public List<ShopTypeDTO> ShopTypeList { get; set; } = new List<ShopTypeDTO>();

        public string OrganizationAddress { get; set; }

        public string OrganizationImage { get; set; }

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

    public enum EmployeeType
    {
        [Description("未设置")]
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,

        //[Description("所有类型")]
        //All = 9999
    }

    public enum DimissionType
    {
        [Description("未设置")]
        None = -1,

        [Description("自动离职")]
        VoluntaryTurnover = 0,

        [Description("辞退")]
        Dismiss = 1
    }

    public class ShopTypeDTO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

}
