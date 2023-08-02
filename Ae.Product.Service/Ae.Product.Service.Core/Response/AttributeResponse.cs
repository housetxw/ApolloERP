using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
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
