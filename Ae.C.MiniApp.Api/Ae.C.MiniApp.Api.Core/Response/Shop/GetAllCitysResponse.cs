using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetAllCitysResponse
    {
        /// <summary>
        /// 所有城市集合
        /// </summary>
        public List<GetAllCitysVO> Citys { get; set; }
        /// <summary>
        /// 热门城市
        /// </summary>
        public List<CityInfoVO> HotCitys { get; set; }
    }
}
