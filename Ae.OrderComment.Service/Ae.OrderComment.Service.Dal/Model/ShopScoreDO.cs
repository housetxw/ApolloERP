using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
    public class ShopScoreDO
    {
        /// <summary>
        /// 
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 总评论数量
        /// </summary>
        public int TotalNum { get; set; }
    }
}
