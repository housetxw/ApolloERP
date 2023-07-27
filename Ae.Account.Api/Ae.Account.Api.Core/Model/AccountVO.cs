using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Core.Model
{
    public class AccountVO { }

    public class AccountPageResVO
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        public string OrganizationId { get; set; }

        public string OrganizationName { get; set; }

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
        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }
        public string UpdateId { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
        

        public int State { get; set; }

        public int ErrorCount { get; set; }

    }


}
