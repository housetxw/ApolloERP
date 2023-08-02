using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    public class AttributeSearchRequest:BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public int isDeleted { get; set; }
    }
}
