﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class GetOrderCommentForShopDTO : GetOrderCommentBaseDTO
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long DetailShopId { get; set; }

        /// <summary>
        /// 门店图片
        /// </summary>
        public string ShopImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }

    }
}
