using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetCityListResponse
    {
        /// <summary>
        /// 所有城市
        /// </summary>
        public List<RegionChinaVO> ShopLocation { get; set; }
        /// <summary>
        /// 热门城市
        /// </summary>
        public List<RegionChinaVO> HotCity { get; set; }
    }
}
