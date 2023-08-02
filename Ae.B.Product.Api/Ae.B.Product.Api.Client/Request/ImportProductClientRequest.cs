using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class ImportProductClientRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductImportDto> Products { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
