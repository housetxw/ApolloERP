using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("bao_yang_part_accessory")]
    public class BaoYangPartAccessoryDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 五级车型tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 辅料名称
        /// </summary>
        [Column("accessory_name")]
        public string AccessoryName { get; set; }

        /// <summary>
        /// 升数
        /// </summary>
        [Column("volume")]
        public string Volume { get; set; } = string.Empty;

        /// <summary>
        /// 等级/防冻液沸点
        /// </summary>
        [Column("level")]
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// 数量
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        [Column("size")]
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// 接口
        /// </summary>
        [Column("interface")]
        public string Interface { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 粘度
        /// </summary>
        [Column("viscosity")]
        public string Viscosity { get; set; } = string.Empty;

        /// <summary>
        /// 来源
        /// </summary>
        [Column("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// 机油等级//防冻液冰点
        /// </summary>
        [Column("grade")]
        public string Grade { get; set; } = string.Empty;

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
        /// 修改人
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
