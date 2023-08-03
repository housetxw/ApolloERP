using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    /// <summary>
    /// 门店评分
    /// </summary>
    public class ShopScoreVo
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
