using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 五级评分值
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 选中的点评标签Id集合
        /// </summary>
        public List<long> SelectedLabelIds { get; set; }
    }
}
