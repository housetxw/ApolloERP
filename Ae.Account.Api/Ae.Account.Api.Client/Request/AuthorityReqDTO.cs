using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Client.Request
{

    public class AuthorityReqDTO { }

    public class AuthorityListReqDTO : BasePageRequest
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public long ApplicationId { get; set; }

        public AuthorityType Type { get; set; }

        public int IsDeleted { get; set; }
    }

    public class AuthorityListInternalReqDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ApplicationId { get; set; }
        /// <summary>
        /// 所属应用简称
        /// </summary>
        public string Initialism { get; set; }

        public string Route { get; set; }
    }

}
