using Ae.C.MiniApp.Api.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Product
{
    /// <summary>
    ///  类目商品信息
    /// </summary>
    public class CategoryProductResponse
    {
        /// <summary>
        /// 产品信息
        /// </summary>
        public List<ProductBaseInfoVo> products { get; set; }
    }
}
