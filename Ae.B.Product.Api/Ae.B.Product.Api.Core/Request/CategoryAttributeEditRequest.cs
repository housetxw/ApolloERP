using Ae.B.Product.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
   public class CategoryAttributeEditRequest
    {
        /// <summary>
        /// 类目属性信息
        /// </summary>
        public List<CategoryAttributeVo> CategoryAttributes { get; set; }

        /// <summary>
        /// 类目Id
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
