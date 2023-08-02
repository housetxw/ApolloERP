using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class ProductPartClientRequest
    {
        /// <summary>
        /// 配件号
        /// </summary>
        public List<string> PartNos { get; set; }

        /// <summary>
        /// 是否包括属性信息
        /// </summary>
        public bool HasAttribute { get; set; } = false;

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }
    }
}
