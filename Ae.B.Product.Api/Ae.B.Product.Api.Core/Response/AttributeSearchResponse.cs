using Ae.B.Product.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response
{
    public class AttributeSearchResponse
    {
        public List<AttributeNameVo> Items { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
