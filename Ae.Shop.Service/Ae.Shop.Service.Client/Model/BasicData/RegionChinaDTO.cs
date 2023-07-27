using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model
{
    public class RegionChinaDTO
    {
        /// <summary>
        /// 行政编号
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// 省市区名称全称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 国家行政级编码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; } = string.Empty;
    }
}
