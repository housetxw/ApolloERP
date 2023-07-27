using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.BasicData.Service.Core.Response
{
    public class RegionChinaDTO
    {
        /// <summary>
        /// RG内部使用唯一标识； 一旦初始化，便不会更改；
        /// RegionId只有一位时(省、自治区、直辖市)，是 '0'；其他级别的行政单位RegionId是6位；
        /// </summary>
        public string RegionId { get; set; } = string.Empty;

        /// <summary>
        /// 省市区名称全称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// RG内部使用唯一标识，省、自治区、直辖市 此值为‘0’；
        /// </summary>
        public string ParentId { get; set; } = string.Empty;

        /// <summary>
        /// 国家行政级编码；仅供参考，请不要用于任何系统的任何业务逻辑； (可能会有变化)
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 首字母 (小写)
        /// </summary>
        public string Initial { get; set; } = string.Empty;

        /// <summary>
        /// 省市区全称拼音 (全部小写，拼音间用空格隔离)
        /// </summary>
        public string Spell { get; set; } = string.Empty;

        /// <summary>
        /// 地区级别:
        /// 1-省、自治区、直辖市；
        /// 2-地级市、地区、自治州、盟；
        /// 3-市辖区、县级市、县；
        /// </summary>
        public RegionChinaType Level { get; set; }
    }

    public enum RegionChinaType
    {
        [Description("省、自治区、直辖市")]
        Province = 1,

        [Description("地级市、地区、自治州、盟")]
        City = 2,

        [Description("市辖区、县级市、县")]
        District = 3,
    }


    #region DB Essential Filed

    ///// <summary>
    ///// 是否删除
    ///// </summary>
    //public bool IsDeleted { get; set; }

    ///// <summary>
    ///// 创建人
    ///// </summary>
    //public string CreateBy { get; set; } = string.Empty;

    ///// <summary>
    ///// 创建时间
    ///// </summary>
    //public DateTime CreateTime { get; set; } = DateTime.Now;

    ///// <summary>
    ///// 修改人
    ///// </summary>
    //public string UpdateBy { get; set; } = string.Empty;

    ///// <summary>
    ///// 修改时间
    ///// </summary>
    //public DateTime UpdateTime { get; set; } = DateTime.Now; 

    #endregion DB Essential Filed
}
