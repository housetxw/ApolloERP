using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Dal.Model
{
    [Table("authority")]
    public class AuthorityDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 父级权限id
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 类型；0：模块；1：页面；2：按钮；
        /// </summary>
        [Column("type")]
        public AuthorityType Type { get; set; }

        /// <summary>
        /// 所属应用Id
        /// </summary>
        [Column("application_id")]
        public long ApplicationId { get; set; }

        /// <summary>
        /// 权限路由， 前端路由URL。例如：@/views/accountauthority/application/index
        /// </summary>
        [Column("route")]
        public string Route { get; set; } = string.Empty;

        /// <summary>
        /// 路由参数；目前基于前端路由，此参数暂时用不到；
        /// </summary>
        [Column("route_parameter")]
        public string RouteParameter { get; set; } = string.Empty;

        /// <summary>
        /// 菜单图标Unicode或是Url
        /// </summary>
        [Column("menu_icon")]
        public string MenuIcon { get; set; } = string.Empty;

        /// <summary>
        /// 菜单排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 权限的简单描述，前台页面非必填项
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
