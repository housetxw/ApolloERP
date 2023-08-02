using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{

    [Table("rel_product_installservice")]
    public class RelProductInstallserviceDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 安装方式（例如：添加防冻液/更换防冻液）
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 商品id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 产品pid
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; } = string.Empty;

        /// <summary>
        /// 服务id
        /// </summary>
        [Column("service_id")]
        public string ServiceId { get; set; } = string.Empty;

        /// <summary>
        /// 服务名称
        /// </summary>
        [Column("service_name")]
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// 服务价格
        /// </summary>
        [Column("service_price")]
        public decimal ServicePrice { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 包安装
        /// </summary>
        [Column("free_install")]
        public sbyte FreeInstall { get; set; }

        /// <summary>
        /// 服务数量
        /// </summary>
        [Column("change_num")]
        public sbyte ChangeNum { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Column("is_default")]
        public sbyte IsDefault { get; set; } = 1;

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
