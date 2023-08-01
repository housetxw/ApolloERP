using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_install_type_config")]
    public class BaoYangInstallTypeConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 保养项目
        /// </summary>
        [Column("package_type")]
        public string PackageType { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 安装方式
        /// </summary>
        [Column("install_type")]
        public string InstallType { get; set; }
        /// <summary>
        /// 安装方式名称
        /// </summary>
        [Column("install_type_name")]
        public string InstallTypeName { get; set; }
        /// <summary>
        /// 保养项目
        /// </summary>
        [Column("bao_yang_types")]
        public string BaoYangTypes { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [Column("is_default")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否需要全部
        /// </summary>
        [Column("need_all")]
        public bool NeedAll { get; set; }
        /// <summary>
        /// 文字格式
        /// </summary>
        [Column("text_format")]
        public string TextFormat { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        [Column("channel")]
        public string Channel { get; set; }
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
