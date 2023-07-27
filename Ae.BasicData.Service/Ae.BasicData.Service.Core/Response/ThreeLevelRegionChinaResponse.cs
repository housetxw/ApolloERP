using Ae.BasicData.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Response
{
    public class ThreeLevelRegionChinaResponse
    {
        /// <summary>
        /// 省市区集合
        /// </summary>
        public List<RegionChinaSiteDTO> Children { get; set; }
    }
}
