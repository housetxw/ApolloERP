using Ae.OrderComment.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Request
{
    public class UpdateShopScoreByShopIdsRequest
    {
        /// <summary>
        /// 门店好评列表
        /// </summary>
        public List<ShopScoreDTO> ShopScoreList { get; set; }
    }
}
