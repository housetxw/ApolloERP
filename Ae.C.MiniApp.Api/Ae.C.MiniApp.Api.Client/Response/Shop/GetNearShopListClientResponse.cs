using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetNearShopListClientResponse
    {
        /// <summary>
        /// 门店信息列表
        /// </summary>
        public List<NearShopInfoDTO> ShopList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
