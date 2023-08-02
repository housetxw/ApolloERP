using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    [Table("service_type_enum")]
    public class ServiceTypeEnumDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        public string ServiceType { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// icon图片地址
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 跳转路由
        /// </summary>
        [Column("route_url")]
        public string RouteUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
        /// 类目ID集合
        /// </summary>
        [Column("category_ids")]
        public string CategoryIds { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("display_index")]
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
