using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    public class ProductDetailDto
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoDto Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceDto> InstallationServices { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueDto> Attributevalues { get; set; }
    }
}
