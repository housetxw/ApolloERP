using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Core.Request
{
    public class AccountAuthorityReqVO { }

    public class RoleListReqVO
    {
        public long OrganizationId { get; set; }

        public RoleType Type { get; set; } = RoleType.None;

    }

}
