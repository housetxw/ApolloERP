using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class AttributeNameSearchClientRequest
    {
        /// <summary>
        /// 属性名称Ids
        /// </summary>
        public List<int> AttributeNameIds { get; set; }
    }
}
