﻿using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetShopListResponse
    {
        /// <summary>
        /// 门店信息列表
        /// </summary>
        public List<ShopSimpleInfoDTO> ShopList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
