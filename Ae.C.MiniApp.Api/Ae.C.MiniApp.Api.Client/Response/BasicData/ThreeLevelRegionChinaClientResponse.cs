using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class ThreeLevelRegionChinaClientResponse
    {
        /// <summary>
        /// 省市区集合
        /// </summary>
        public List<RegionChinaSiteDTO> Children { get; set; }
    }
}
