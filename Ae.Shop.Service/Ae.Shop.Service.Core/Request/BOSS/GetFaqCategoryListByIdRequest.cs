using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetFaqCategoryListByIdRequest
    {
        /// <summary>
        /// FAQ分类ID
        /// </summary>
        public long CategoryId { get; set; }
    }
}
