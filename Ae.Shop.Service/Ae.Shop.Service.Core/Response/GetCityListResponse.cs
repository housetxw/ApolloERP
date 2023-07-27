using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetCityListResponse
    {
        /// <summary>
        /// 所有城市
        /// </summary>
        public List<GetAllCitysVO> Citys { get; set; }
        /// <summary>
        /// 热门城市
        /// </summary>
        public List<CityInfo> HotCitys { get; set; }
    }
}
