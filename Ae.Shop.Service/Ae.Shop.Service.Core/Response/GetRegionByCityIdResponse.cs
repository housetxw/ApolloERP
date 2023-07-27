using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetRegionByCityIdResponse
    {
        /// <summary>
        /// 定位门店list
        /// </summary>
        public List<ShopLocationVO> ShopLocation { get; set; }
    }
}
