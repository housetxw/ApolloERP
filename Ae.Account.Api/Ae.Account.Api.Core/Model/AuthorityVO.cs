using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Account.Api.Core.Model
{
    public class AuthorityVO
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        /// <summary>
        /// 此字段，主要是在新建或禁用权限时，用于解决 权限树 中，父级节点CheckBox是半选中还是全选中的问题；
        /// 新建或禁用一个权限时，如其有父级，需要获取其父级及其所有的上级，并更新role_authority的half_check = 1;
        /// </summary>
        public List<long> ParentIdList { get; set; }

        [Required(ErrorMessage = "请输入权限名称")]
        [MaxLength(50, ErrorMessage = "权限名称长度不允许超过50")]
        public string Name { get; set; }

        /// <summary>
        /// 类型；0：模块；1：页面；2：按钮；
        /// </summary>
        [Required(ErrorMessage = "请输入权限类型")]
        public AuthorityType Type { get; set; }

        [Required(ErrorMessage = "请输入系统Id")]
        public long ApplicationId { get; set; }

        /// <summary>
        /// 权限路由， 前端路由URL。例如：@/views/accountauthority/application/index
        /// </summary>
        [MaxLength(100, ErrorMessage = "权限路由长度不允许超过100")]
        public string Route { get; set; }

        /// <summary>
        /// 路由参数；目前基于前端路由，此参数暂时用不到；
        /// </summary>
        [MaxLength(100, ErrorMessage = "路由参数长度不允许超过100")]
        public string RouteParameter { get; set; }

        /// <summary>
        /// 菜单图标Unicode或是Url
        /// </summary>
        [MaxLength(100, ErrorMessage = "图标长度不允许超过100")]
        public string MenuIcon { get; set; }

        public int Sort { get; set; }

        [MaxLength(255, ErrorMessage = "权限描述长度不允许超过255")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public enum AuthorityType
    {
        None = -1,

        [Description("模块")]
        Module = 0,

        [Description("页面")]
        Page = 1,

        [Description("按钮")]
        Button = 2
    }

    public class ElementAuthorityTree
    {
        public long Id { get; set; }

        public string Label { get; set; }

        public List<ElementAuthorityTree> Children { get; set; } = new List<ElementAuthorityTree>();

    }

}
