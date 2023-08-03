using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
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

    /// <summary>
    /// 评论选择的VO
    /// </summary>
    public class CommentSelectVO
    {
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 默认选中项
        /// </summary>
        public bool Checked { get; set; }
    }

    public class GetCommentListVO
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId => Id;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>

        public string UserId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 显示创建的时间
        /// </summary>
        public string ShowCreateTime => CreateTime.ToString("yyyy-MM-dd HH:mm:dd");
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 门店评分值
        /// </summary>
        public int ShopScore { get; set; }

        /// <summary>
        /// 评分内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片地址集合
        /// </summary>
        public List<ImgVO> ImgList { get; set; }
        /// <summary>
        /// 评论回复集合
        /// </summary>
        public List<CommentListReplyInfoVO> ReplyInfos { get; set; }

        /// <summary>
        /// 回复按钮集合
        /// </summary>
        public List<UserOperationVO> ReplyButtons { get; set; }
    }
}
