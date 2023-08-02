using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    public class CategoryAttributeRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public int? IsDeleted { get; set; }
        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }
    }
}
