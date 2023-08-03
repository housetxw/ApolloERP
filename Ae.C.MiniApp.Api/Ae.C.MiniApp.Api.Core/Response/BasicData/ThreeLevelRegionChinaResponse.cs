using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class ThreeLevelRegionChinaResponse
    {
        /// <summary>
        /// 省市区集合
        /// </summary>
        public List<RegionChinaSiteVO> Children { get; set; }
    }
}
