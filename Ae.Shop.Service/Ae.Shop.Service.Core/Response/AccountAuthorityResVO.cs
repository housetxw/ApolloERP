using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Core.Response
{
    public class AccountAuthorityResVO { }

    public class RoleListResVO
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class RoleVO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Features { get; set; }

        /// <summary>
        /// 0：公司；1：门店；2：仓库；
        /// </summary>
        public RoleType Type { get; set; }

        

        //public long OrganizationId { get; set; }
    }
    
    public class OrgRangeRolesVO
    {
        public long OrganizationId { get; set; }

        public string OrganizationName { get; set; } = string.Empty;

        public RoleType Type { get; set; }

        public List<RoleVO> Roles { get; set; } = new List<RoleVO>();
    }

}
