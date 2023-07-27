using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Common.Http;
using RoleType = Ae.Account.Api.Client.Response.RoleType;

namespace Ae.Account.Api.Client.Request
{
    public class RoleReqDTO { }

    public class RoleListReqDTO : BasePageRequest
    {
        public long Id { get; set; }

        public long OrganizationId { get; set; }

        public RoleType Type { get; set; }

        public int IsDeleted { get; set; } = -1;

        public int? Features { get; set; }
    }

    public class RoleListInternalReqDTO
    {
        public long OrganizationId { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 角色属性门店的0：精简 1：旗舰 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        public int Features { get; set; }
    }

    public class RoleAuthorityListReqByRoleIdDTO
    {
        public long RoleId { get; set; }
    }

    public class RoleAuthorityReqDTO
    {
        public long RoleId { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public List<RoleAuthorityDTO> AddList { get; set; }

        //public List<RoleAuthorityDTO> DeleteList { get; set; }
    }


    public class OrganizationReqDTO : BasePageRequest
    {
        public string Name { get; set; }
    }

}
