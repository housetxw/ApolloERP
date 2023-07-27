using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Account.Api.Core.Model
{
    public class RoleVO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "请输入权限名称")]
        [MaxLength(50, ErrorMessage = "权限名称长度不允许超过50")]
        public string Name { get; set; }

        /// <summary>
        /// 0：公司；1：门店；2：仓库；
        /// </summary>
        public RoleType Type { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "请选择正确的所属单位")]
        public long OrganizationId { get; set; }
        /// <summary>
        /// 门店类型 0公司 1合作店 2直营店 4上门养车 8认证店
        /// </summary>
        public sbyte ShopType { get; set; }

        [MaxLength(255, ErrorMessage = "权限描述长度不允许超过255")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 角色属性门店的0：精简 1：旗舰 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司，9-默认角色
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
