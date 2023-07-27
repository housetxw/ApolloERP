using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Core.Model;

namespace Ae.Account.Api.Core.Request
{
    public class RoleReqVO { }

    public class RoleListReqVO : BasePageRequest
    {
        public long Id { get; set; }

        public long OrganizationId { get; set; }

        public RoleType Type { get; set; } = RoleType.None;

        public int IsDeleted { get; set; } = -1;
    }

    public class RoleAuthorityListReqByRoleIdVO
    {
        public long RoleId { get; set; }
    }

    public class RoleAuthorityReqVO
    {
        public long RoleId { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public List<long> HalfCheckedList { get; set; }

        public List<long> CheckedList { get; set; }

        public List<RoleAuthorityVO> AddList { get; set; } = new List<RoleAuthorityVO>();

        //public List<RoleAuthorityVO> DeleteList { get; set; }
    }

}
