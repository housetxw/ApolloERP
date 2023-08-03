﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Client.Request
{
    public class BatchGetOrderCountByShopIdRequest
    {
        /// <summary>
        /// 门店ID集合
        /// </summary>
        [Required(ErrorMessage = "门店ID集合不可为空")]
        public List<long> ShopIds { get; set; }
    }
}
