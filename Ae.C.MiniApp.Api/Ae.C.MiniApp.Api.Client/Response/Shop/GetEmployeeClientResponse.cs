using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetEmployeeClientResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Identity { get; set; }

        public long OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        public EmployeeGenderEnum Gender { get; set; }

        public EmployeeType Type { get; set; }

        /// <summary>
        /// 是否技师
        /// </summary>
        public bool IsTechnican { get; set; }

        public string WeChat { get; set; }

        public string QQ { get; set; }

        public EmployeeLevelEnum Level { get; set; }

        public string WorkKindName { get; set; }

        public int WorkKindId { get; set; }

        public string JobName { get; set; }

        public long JobId { get; set; }

        public string Avatar { get; set; }

        public string QualificationCertificate { get; set; }

        public decimal Score { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }
    }
}
