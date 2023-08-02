using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    public class ProductSearchInfoVo : ProductBaseInfoVo
    {
        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueVo> Attributevalues { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceVo> InstallationServices { get; set; }
    }
}
