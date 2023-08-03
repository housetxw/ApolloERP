using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class SubmitOrderCommentRequest : BaseOrderCommentSubmitVO
    {
        /// <summary>
        /// 技师点评提交信息
        /// </summary>
        public List<OrderCommentDetailTechSubmitVO> Techs { get; set; }
        /// <summary>
        /// 门店点评提交信息
        /// </summary>
        public OrderCommentDetailShopSubmitVO Shop { get; set; }
        /// <summary>
        /// 商品点评提交信息集合
        /// </summary>
        public List<OrderCommentDetailProductSubmitVO> Products { get; set; }
    }
}
