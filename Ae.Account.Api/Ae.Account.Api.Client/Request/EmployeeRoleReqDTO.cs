using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Client.Request
{
    #region ！！！授权接口Model！！！

    public class AuthorizationReqDTO : BaseRequest
    {
        public string EmployeeId { get; set; }

        public int OrganizationId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }

    public class RangeRoleAuthorityReqDTO : BaseRequest
    {
        public string EmployeeId { get; set; }

        public List<long> RoleIds { get; set; }
    }

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleReqDTO { }

    public class EmployeeRoleListReqDTO
    {
        public string EmployeeId { get; set; }
    }

    public class EmployeeRoleSaveReqDTO
    {
        public string EmployeeId { get; set; }

        public string Operator { get; set; }

        public List<EmployeeRoleEntityDTO> EmployeeRoleList { get; set; }
    }


}
