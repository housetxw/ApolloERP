using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Response
{
    public class AttributeClientResponse
    {
        /// <summary>
        ///  属性名
        /// </summary>
        public AttributeNameDto AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public List<AttributevalueDto> AttributeValues { get; set; }
    }
}
