﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class OrderCommentDetailShopFormDTO
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店图片
        /// </summary>
        public string ShopImageUrl { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
    }
}
