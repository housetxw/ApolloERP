using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Client.Response
{
    #region ！！！授权接口Model！！！

    public class AuthorizationResDTO
    {
        public string EmployeeId { get; set; }

        public long AuthorityId { get; set; }

        public string ParentId { get; set; }

        public string AuthorityName { get; set; }

        public string Route { get; set; }

        public string MenuIcon { get; set; }

        public string RouteParameter { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }


        public AuthorityType AuthorityType { get; set; }
    }

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleResDTO { }

}
