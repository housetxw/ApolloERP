using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Product
{
    /// <summary>
    /// 商品搜索返回模型
    /// </summary>
    public class ProductSearchResponse
    {
        /// <summary>
        /// 搜索产品信息
        /// </summary>
        public List<SearchProductVo> products { get; set; }

        /// <summary>
        /// 搜索产品品牌汇总
        /// </summary>
        public List<string> Brands { get; set; }
    }
}
