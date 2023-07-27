using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Base.Request;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Request
{
    public class AuthorityReqDTO { }

    public class AuthorityListReqDTO : BasePageRequest
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        /// <summary>
        /// 所属应用Id
        /// </summary>
        public long ApplicationId { get; set; }
        /// <summary>
        /// 所属应用简称
        /// </summary>
        public string Initialism { get; set; }

        public AuthorityType Type { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AuthorityListInternalReqDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 所属应用Id
        /// </summary>
        public long ApplicationId { get; set; }
        /// <summary>
        /// 所属应用简称
        /// </summary>
        public string Initialism { get; set; }

        public string Route { get; set; }
    }

}
