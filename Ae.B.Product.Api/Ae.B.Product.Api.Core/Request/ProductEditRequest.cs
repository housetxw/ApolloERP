﻿using Ae.B.Product.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    public class ProductEditRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoVo Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceVo> InstallationServices { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueVo> Attributevalues { get; set; }
        /// <summary>
        /// 套餐明细 List
        /// </summary>
        public List<ProductPackageDetailVo> PackageDetails { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
