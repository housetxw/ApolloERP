using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.BasicData.Service.Core.Response;

namespace Ae.BasicData.Service.Dal.Model
{
    [Table("region_china")]
    public class RegionChinaDO
    {
        /// <summary>
        /// 省市区表主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// RG内部使用唯一标识； 一旦初始化，便不会更改；
        /// </summary>
        [Column("region_id")]
        public string RegionId { get; set; } = string.Empty;

        /// <summary>
        /// 省市区名称全称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// RG内部使用唯一标识，省、自治区、直辖市 此值为‘0’；
        /// </summary>
        [Column("parent_id")]
        public string ParentId { get; set; } = string.Empty;

        /// <summary>
        /// 国家行政级编码；仅供参考，请不要用于任何系统的任何业务逻辑； (可能会有变化)
        /// </summary>
        [Column("country_code")]
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 首字母 (小写)
        /// </summary>
        [Column("initial")]
        public string Initial { get; set; } = string.Empty;

        /// <summary>
        /// 省市区全称拼音 (全部小写，拼音间用空格隔离)
        /// </summary>
        [Column("spell")]
        public string Spell { get; set; } = string.Empty;

        /// <summary>
        /// 地区级别: 1-省、自治区、直辖市； 2-地级市、地区、自治州、盟；3-市辖区、县级市、县；
        /// </summary>
        [Column("level")]
        public RegionChinaType Level { get; set; }

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
