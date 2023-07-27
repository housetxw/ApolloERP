using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Model
{
    public class RegionChinaSiteDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<RegionChinaSiteCityDTO> Children { get; set; }
    }
    public class RegionChinaSiteCityDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<RegionChinaSiteDistrictDTO> Children { get; set; }
    }
    public class RegionChinaSiteDistrictDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }
    }
}
