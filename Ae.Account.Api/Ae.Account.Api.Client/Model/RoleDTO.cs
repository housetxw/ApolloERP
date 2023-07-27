using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Account.Api.Client.Model
{
    public class RoleDTO
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
        Warehouse = 2
    }
}
