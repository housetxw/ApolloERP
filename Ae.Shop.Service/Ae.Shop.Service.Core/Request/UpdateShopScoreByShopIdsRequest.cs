using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class UpdateShopScoreByShopIdsRequest
    {
        /// <summary>
        /// 门店好评列表
        /// </summary>
        public List<ShopScoreDTO> ShopScoreList { get; set; }
    }
}
