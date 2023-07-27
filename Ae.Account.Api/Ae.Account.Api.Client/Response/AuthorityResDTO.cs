using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ae.Account.Api.Client.Model;

namespace Ae.Account.Api.Client.Response
{
    public class AuthorityPageListResDTO
    {
        public List<AuthorityPageResDTO> AuthorityList { get; set; }

        public int TotalItems { get; set; }
    }

    public class AuthorityPageResDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ParentId { get; set; }

        public string ParentName { get; set; }

        public AuthorityType Type { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        /// <summary>
        /// 权限路由， 前端路由URL。例如：@/views/accountauthority/application/index
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 路由参数；目前基于前端路由，此参数暂时用不到；
        /// </summary>
        public string RouteParameter { get; set; }

        /// <summary>
        /// 菜单图标Unicode或是Url
        /// </summary>
        public string MenuIcon { get; set; }

        public int Sort { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }
    }


    public enum AuthorityType
    {
        [Description("模块")]
        Module = 0,

        [Description("页面")]
        Page = 1,

        [Description("按钮")]
        Button = 2
    }

}
