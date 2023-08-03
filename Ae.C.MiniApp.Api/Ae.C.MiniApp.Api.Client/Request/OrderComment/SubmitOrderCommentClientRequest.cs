using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class SubmitOrderCommentClientRequest : BaseOrderCommentSubmitDTO
    {
        /// <summary>
        /// 技师点评提交信息
        /// </summary>
        public List<OrderCommentDetailTechSubmitDTO> Techs { get; set; }
        /// <summary>
        /// 门店点评提交信息
        /// </summary>
        public OrderCommentDetailShopSubmitDTO Shop { get; set; }
        /// <summary>
        /// 商品点评提交信息集合
        /// </summary>
        public List<OrderCommentDetailProductSubmitDTO> Products { get; set; }
    }
}
