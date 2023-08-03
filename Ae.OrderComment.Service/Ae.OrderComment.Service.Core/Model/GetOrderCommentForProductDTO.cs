﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
   public class GetOrderCommentForProductDTO: GetOrderCommentBaseDTO
    {
        /// <summary>
        /// 五级分值
        /// </summary>
        public int Score { get; set; }

        public List<OrderCommentProductDTO> Products { get; set; }
    }


    public class OrderCommentProductDTO {

        /// <summary>
        /// 评论编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品显示名称
        /// </summary>
        public string ProductDisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImageUrl { get; set; } = string.Empty;

        public int Score { get; set; }
    }
}
