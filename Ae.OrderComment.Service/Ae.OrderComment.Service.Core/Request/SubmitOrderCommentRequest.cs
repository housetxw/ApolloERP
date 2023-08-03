using Ae.OrderComment.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class SubmitOrderCommentRequest : BaseOrderCommentSubmitDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 技师点评提交信息
        /// </summary>
        public OrderCommentDetailTechSubmitDTO Tech { get; set; }
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
