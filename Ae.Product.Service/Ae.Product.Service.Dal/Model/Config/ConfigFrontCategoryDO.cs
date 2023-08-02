using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Config
{
    /// <summary>
    /// 前台类目配置
    /// </summary>
    [Table("config_front_category")]
    public class ConfigFrontCategoryDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Column("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 显示
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 展示图url
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 跳转路由url
        /// </summary>
        [Column("route_url")]
        public string RouteUrl { get; set; } = string.Empty;

        /// <summary>
        /// 二级标题
        /// </summary>
        [Column("sub_title")]
        public string SubTitle { get; set; } = string.Empty;

        /// <summary>
        /// 父级Id
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        [Column("terminal_type")]
        public string TerminalType { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Column("version")]
        public int Version { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        [Column("extend_param")]
        public string ExtendParam { get; set; }

        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
