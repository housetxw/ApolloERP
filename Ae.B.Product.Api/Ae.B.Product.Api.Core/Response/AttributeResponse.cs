using Ae.B.Product.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response
{
    public class AttributeResponse
    {
        /// <summary>
        ///  属性名
        /// </summary>
        public AttributeNameVo AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public List<AttributevalueVo> AttributeValues { get; set; }
    }
}
