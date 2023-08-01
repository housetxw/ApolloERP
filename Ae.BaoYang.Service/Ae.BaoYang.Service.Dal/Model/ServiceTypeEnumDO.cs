using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("service_type_enum")]
    public class ServiceTypeEnumDO
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
        /// 排序
        /// </summary>
        [Column("display_index")]
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 押金额度限制
        /// </summary>
        [Column("deposit_amount")]
        public decimal DepositAmount { get; set; }

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
