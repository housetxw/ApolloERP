using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_package_config")]
    public class BaoYangPackageConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// PackageType
        /// </summary>
        [Column("package_type")]
        public string PackageType { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 建议提示
        /// </summary>
        [Column("suggest_tip")]
        public string SuggestTip { get; set; }
        /// <summary>
        /// 简要描述
        /// </summary>
        [Column("brief_description")]
        public string BriefDescription { get; set; }
        /// <summary>
        /// 描述链接url
        /// </summary>
        [Column("description_link")]
        public string DescriptionLink { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        [Column("detail_description")]
        public string DetailDescription { get; set; }
        /// <summary>
        /// 适配展示商品类型：0套餐  1单品  2套餐&单品
        /// </summary>
        [Column("product_type")]
        public int ProductType { get; set; }
        /// <summary>
        /// 图片logo
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序显示
        /// </summary>
        [Column("display_index")]
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        [Column("service_id")]
        public string ServiceId { get; set; }

        /// <summary>
        /// 删除标识
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
