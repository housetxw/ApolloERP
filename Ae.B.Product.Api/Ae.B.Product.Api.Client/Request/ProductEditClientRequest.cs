using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class ProductEditClientRequest
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

        /// <summary>
        /// 套餐明细 List
        /// </summary>

        public List<ProductPackageDetailDto> PackageDetails { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
