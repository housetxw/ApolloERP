using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class ImportProductRequest
    {
        /// <summary>
        /// 商品信息s
        /// </summary>
        public List<ProductImportVo> Products { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
