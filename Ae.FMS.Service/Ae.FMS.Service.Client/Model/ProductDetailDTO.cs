using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Model
{
    public class ProductDetailDTO
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoDTO Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceDTO> InstallationServices { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueDTO> Attributevalues { get; set; }
    }
}
