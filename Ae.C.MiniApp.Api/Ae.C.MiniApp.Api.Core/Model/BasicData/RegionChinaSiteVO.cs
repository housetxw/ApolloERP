using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class RegionChinaSiteVO
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
        public List<RegionChinaSiteCityVO> Children { get; set; }
    }
    public class RegionChinaSiteCityVO
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
        public List<RegionChinaSiteDistrictVO> Children { get; set; }
    }
    public class RegionChinaSiteDistrictVO
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
