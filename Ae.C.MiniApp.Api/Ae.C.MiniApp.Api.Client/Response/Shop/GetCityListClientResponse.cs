using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetCityListClientResponse
    {
        /// <summary>
        /// 所有城市
        /// </summary>
        public List<GetAllCitysDTO> Citys { get; set; }
        /// <summary>
        /// 热门城市
        /// </summary>
        public List<CityInfo> HotCitys { get; set; }
    }
}
