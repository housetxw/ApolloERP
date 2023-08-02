﻿using Ae.B.Product.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response
{
    public class ProductSearchResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductAllInfoVo> Items { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 聚合后的商品品牌
        /// </summary>
        public List<string> Brands { get; set; }
    }
}
