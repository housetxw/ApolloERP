using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Product.Api.Client.Model.Product;

namespace Ae.B.Product.Api.Client.Request.Product
{
    /// <summary>
    /// EditInstallServiceClientRequest
    /// </summary>
    public class EditInstallServiceClientRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public List<ProductForInstallDto> Products { get; set; }

        /// <summary>
        /// 安装服务
        /// </summary>
        public List<InstallServiceDto> InstallService { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// ProductForInstall
    /// </summary>
    public class ProductForInstallDto
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
