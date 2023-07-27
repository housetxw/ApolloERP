using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Core.Base.Request;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Request
{
    public class RoleReqDTO { }

    public class RoleListReqDTO : BasePageRequest
    {
        public long Id { get; set; }

        public long OrganizationId { get; set; }

        public RoleType Type { get; set; } = RoleType.None;
        /// <summary>
        /// 门店的0：普通 1：高级 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        public int? Features { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class RoleListReqsDTO
    {
        public List<OrgEntityReqDTO> Items { get; set; } = new List<OrgEntityReqDTO>();
    }
    public class OrgEntityReqDTO
    {
        public long OrganizationId { get; set; }

        public RoleType Type { get; set; }
        public int SystemType { get; set; }
    }

    public class RoleListInternalReqDTO
    {
        public long OrganizationId { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }
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

}
