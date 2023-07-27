using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Account.Api.Client.Model
{
    public class AuthorityDTO
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public List<long> ParentIdList { get; set; }

        public string Name { get; set; }

        public AuthorityType Type { get; set; }

        public string TypeName { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        /// <summary>
        /// 权限路由， 前端路由URL。例如：@/views/accountauthority/application/index
        /// </summary>
        public string Route { get; set; }

        public string RouteParameter { get; set; }

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
