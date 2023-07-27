using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Response
{
    public class RegionChinaResDTO : RegionChinaDTO { }

    public class RegionChinaResByLevelDTO
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
        /// 国家行政级编码；仅供参考，请不要用于任何系统的任何业务逻辑； (可能会有变化)
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// 首字母 (小写)
        /// </summary>
        public string Initial { get; set; } = string.Empty;
        /// <summary>
        /// RG内部使用唯一标识，省、自治区、直辖市 此值为‘0’；
        /// </summary>
        public string ParentId { get; set; } = string.Empty;
    }


}
