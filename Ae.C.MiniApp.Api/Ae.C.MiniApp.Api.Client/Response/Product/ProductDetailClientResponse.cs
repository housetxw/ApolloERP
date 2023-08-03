using Ae.C.MiniApp.Api.Client.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Product
{
    public class ProductDetailClientResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoDto Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallservicDto> InstallationServices { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueDto> Attributevalues { get; set; }
    }
}
