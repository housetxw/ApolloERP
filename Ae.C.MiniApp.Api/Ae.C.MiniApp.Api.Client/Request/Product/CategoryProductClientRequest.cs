using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Product
{
    public class CategoryProductClientRequest
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int? CategoryId { get; set; }


        /// <summary>
        /// 是否包括属性信息
        /// </summary>
        public bool HasAttribute { get; set; } = false;


        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }

        /// <summary>
        ///   页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     每页数量
        /// </summary>
        public int PageSize { get; set; }
    }
}
