using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.Product
{
    public class ProductDetailClientVo
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoClientVo Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceClientVo> InstallationServices { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueClientVo> Attributevalues { get; set; }
    }
}
