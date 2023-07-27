using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model.OrderComment
{
    public class ShopScoreDto
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 总评论数量
        /// </summary>
        public int TotalNum { get; set; }
    }
}
