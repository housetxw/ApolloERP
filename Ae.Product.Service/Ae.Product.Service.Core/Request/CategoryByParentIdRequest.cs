using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 根据ParentId获取子类目
    /// </summary>
    public class CategoryByParentIdRequest
    {
        /// <summary>
        /// 父级类目Id
        /// </summary>
        public int ParentId { get; set; }
    }
}
