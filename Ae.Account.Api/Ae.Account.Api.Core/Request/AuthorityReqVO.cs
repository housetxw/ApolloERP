using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Core.Model;

namespace Ae.Account.Api.Core.Request
{
    public class AuthorityReqVO
    {
    }

    public class AuthorityListReqVO : BasePageRequest
    {
        public long Id { get; set; }

        public long ApplicationId { get; set; }

        public AuthorityType Type { get; set; } = AuthorityType.None;

        public int IsDeleted { get; set; } = -1;
    }


}
