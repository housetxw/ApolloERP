using Ae.Account.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Core.Response
{
    public class AuthorityResVO
    {
    }

    public class AuthorityPageListResVO
    {
        public List<AuthorityPageResVO> AuthorityList { get; set; }

        public int TotalItems { get; set; }
    }

    public class AuthorityPageResVO
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
        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }
        public string UpdateId { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public class AuthoritySelectResVO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public bool IsDeleted { get; set; }

        public AuthorityType Type { get; set; }

        public string TypeName { get; set; }
    }

    /// <summary>
    /// 构建权限树模型
    /// 注：若无父级权限，则默认其父及权限为应用
    /// </summary>
    public class AuthorityTreeVO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ParentId { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public List<AuthorityTreeVO> Children { get; set; } = new List<AuthorityTreeVO>();

        public bool IsDeleted { get; set; }
    }


}
