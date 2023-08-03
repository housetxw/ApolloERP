using Ae.OrderComment.Service.Core.Model.OrderCommentForApp;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.OrderComment.Service.Core.Model.OrderCommentForApp;

namespace Ae.OrderComment.Service.Core.Response.OrderCommentForApp
{
    /// <summary>
    ///返回门店的评论列表结果
    /// </summary>
    public class GetCommentListResponse
    {
        /// <summary>
        /// 订单类型下拉选项
        /// </summary>
        public List<CommentSelectVO> OrderTypeSelectItems { get; set; }

        /// <summary>
        /// 评分选项下拉选项
        /// </summary>
        public List<CommentSelectVO> ScoreLevelSelectItems { get; set; }

        /// <summary>
        /// 回复选项下拉选项
        /// </summary>
        public List<CommentSelectVO> ReplyStatusSelectItems { get; set; }


        /// <summary>
        /// 门店评价平均分
        /// </summary>
        public List<KeyValuePair<string, string>> ShopAvgScore { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 分页数据
        /// </summary>
        public List<GetCommentListVO> Items { get; set; }
    }
}
