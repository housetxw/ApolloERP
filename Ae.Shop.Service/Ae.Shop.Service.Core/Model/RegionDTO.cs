using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class RegionDTO
    {
        /// <summary>
        /// 行政编号
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// 省市区名称全称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; } = string.Empty;
    }
}
