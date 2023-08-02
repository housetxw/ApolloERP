using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class CategoryAttributeEditClientRequest
    {
        /// <summary>
        /// 类目属性信息
        /// </summary>
        public List<CategoryAttributeDto> CategoryAttributes { get; set; }

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
