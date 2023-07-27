using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Client.Request
{
    public class EmployeeReqDTO { }

    public class EmployeePageReqDTO : BasePageRequest
    {
        public string Id { get; set; }

        public List<string> AccountId { get; set; }

        public string EmployeeName { get; set; }

        public int OrganizationId { get; set; }

        public EmployeeType Type { get; set; } = EmployeeType.None;

        public int IsDeleted { get; set; } = -1;
    }

    public class EmployeeListReqDTO
    {
        public string Id { get; set; }

        public int OrganizationId { get; set; }

        public int IsDeleted { get; set; } = -1;

        public EmployeeType Type { get; set; } = EmployeeType.None;
    }

    public class OrgRangeRoleListForLoginReqDTO
    {
        public string EmployeeId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public int OrganizationId { get; set; }
    }

}
