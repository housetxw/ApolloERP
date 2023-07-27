using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Core.Response
{
    public class EmployeeResDTO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

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

        public int JobId { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class EmployeeEntityResDTO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        public int OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class EmployeePageResDTO
    {
        public List<EmployeePageDTO> Items { get; set; }

        public int TotalItems { get; set; }
    }

    public class EmployeeLevelListResDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TechnicianEntityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public EmployeeLevelEnum Level { get; set; }
        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkKindName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
    }
    public class TechnicianPageDTO : TechnicianEntityDTO { }


}
