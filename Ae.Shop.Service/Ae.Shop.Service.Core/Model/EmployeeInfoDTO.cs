using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Core.Request;

namespace Ae.Shop.Service.Core.Model
{
    public class EmployeeInfoDTO
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

        public bool IsDeleted { get; set; }

        public List<long> RoleIds { get; set; } = new List<long>();

        public List<OrganizationRangeDTO> OrgRanges { get; set; } = new List<OrganizationRangeDTO>();
    }

}
