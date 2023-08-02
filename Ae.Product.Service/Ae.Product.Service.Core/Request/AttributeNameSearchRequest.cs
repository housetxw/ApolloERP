using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class AttributeNameSearchRequest
    {
        /// <summary>
        /// 属性名称Ids
        /// </summary>
        public List<int> AttributeNameIds { get; set; }
    }
}
