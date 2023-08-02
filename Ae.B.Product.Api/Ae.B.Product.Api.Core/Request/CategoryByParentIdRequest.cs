using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryByParentIdRequest
    {
        /// <summary>
        /// 父级类目Id
        /// </summary>
        public int ParentId { get; set; }
    }
}
