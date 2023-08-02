using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class ImportProductAttributeRequest
    {
        /// <summary>
        /// 商品属性信息
        /// </summary>
        public List<ProductAttributeImportVo> ProductAttributes { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
