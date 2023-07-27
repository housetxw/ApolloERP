using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Common.Http;

namespace Ae.B.Login.Api.Client.Request.ShopManage.Employee
{
    public class EmployeeReqDTO { }

    public class EmployeeListReqDTO
    {
        public string AccountId { get; set; }

        public int IsDeleted { get; set; } = 0;
    }

    public class EmployeePageForLoginListReqDTO : BasePageRequest
    {
        public string AccountId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public string OrganizationName { get; set; }

        public int IsDeleted { get; set; } = 0;
    }

    public class EmployeePageForLoginListByTokenReqDTO : BasePageRequest
    {
        public string EmployeeId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public int IsDeleted { get; set; } = 0;
    }

}
