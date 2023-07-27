using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Enums;
using Ae.AccountAuthority.Service.Core.Model;

namespace Ae.AccountAuthority.Service.Core.Response
{
    public class RoleResDTO { }

    public class RolePageListResDTO
    {
        public List<RolePageResDTO> RoleList { get; set; }

        public int TotalItems { get; set; }
    }

    public class RolePageResDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型 0：公司；1：门店；2：仓库；
        /// </summary>
        public RoleType Type { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long OrganizationId { get; set; }
        /// <summary>
        /// 门店类型 0公司 1合作店 2直营店 4上门养车 8认证店
        /// </summary>
        public sbyte ShopType { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 角色属性门店的0：精简 1：旗舰 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        public int Features { get; set; }
    }

    public class OrgRangeRolesDTO
    {
        public long OrganizationId { get; set; }

        public string OrganizationName { get; set; } = string.Empty;

        public RoleType Type { get; set; }

        public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    }

}
