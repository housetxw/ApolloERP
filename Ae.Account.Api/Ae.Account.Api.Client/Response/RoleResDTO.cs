using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Account.Api.Client.Response
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
        /// 0：公司；1：门店；2：仓库；
        /// </summary>
        public RoleType Type { get; set; }

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

    public enum RoleType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,
        [Description("拓展")]
        Extend = 3,
        [Description("供应商")]
        Supplier = 4
    }

}
