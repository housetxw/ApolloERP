﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class OrderCommentDetailShopSubmitDTO : BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
