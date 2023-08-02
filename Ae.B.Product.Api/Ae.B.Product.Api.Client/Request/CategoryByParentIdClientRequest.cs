using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class CategoryByParentIdClientRequest
    {
        /// <summary>
        /// 父级类目Id
        /// </summary>
        public int ParentId { get; set; }
    }
}
