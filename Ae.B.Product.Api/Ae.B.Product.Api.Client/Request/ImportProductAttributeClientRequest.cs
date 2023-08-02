using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class ImportProductAttributeClientRequest
    { /// <summary>
      /// 商品属性信息
      /// </summary>
        public List<ProductAttributeImportDto> ProductAttributes { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
