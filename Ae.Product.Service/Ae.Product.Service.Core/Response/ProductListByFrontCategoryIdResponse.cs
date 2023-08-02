using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Response
{
    /// <summary>
    /// ProductListByFrontCategoryIdResponse
    /// </summary>
    public class ProductListByFrontCategoryIdResponse
    {
        /// <summary>
        /// Products
        /// </summary>
        public ApiPagedResultData<ProductBaseInfoVo> Products { get; set; }
    }
}
