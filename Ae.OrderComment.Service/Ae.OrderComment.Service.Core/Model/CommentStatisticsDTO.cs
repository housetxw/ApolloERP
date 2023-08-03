using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    /// <summary>
    /// 评价统计模型
    /// </summary>
    public class CommentStatisticsDTO
    {
        /// <summary>
        /// 总评数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 好评数
        /// </summary>
        public int GoodCount { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        public decimal GoodRate { get; set; }
    }
}
