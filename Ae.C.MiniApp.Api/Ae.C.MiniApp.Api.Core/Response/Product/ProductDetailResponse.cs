using Ae.C.MiniApp.Api.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Product
{  
    /// <summary>
    /// 商品详细
    /// </summary>
    public class ProductDetailResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductDetalInfoVo Product { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<InstallserviceVo> InstallationServices { get; set; }

        /// <summary>
        /// 商品图片信息
        /// </summary>
        public List<string> Images { get; set; }


        /// <summary>
        /// 详情图片信息
        /// </summary>
        public List<string> DescriptionImages { get; set; }

        /// <summary>
        /// 商品属性 
        /// </summary>
        public List<string> Attributevalues { get; set; }

    }
}
