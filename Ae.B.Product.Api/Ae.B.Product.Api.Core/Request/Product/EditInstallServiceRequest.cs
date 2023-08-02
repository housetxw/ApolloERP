using Ae.B.Product.Api.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class EditInstallServiceRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public List<ProductForInstall> Products { get; set; }

        /// <summary>
        /// 安装服务
        /// </summary>
        public List<InstallServiceVo> InstallService { get; set; }
    }

    /// <summary>
    /// ProductForInstall
    /// </summary>
    public class ProductForInstall
    {
        /// <summary>
        /// ProductId
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pid { get; set; }
    }
}
