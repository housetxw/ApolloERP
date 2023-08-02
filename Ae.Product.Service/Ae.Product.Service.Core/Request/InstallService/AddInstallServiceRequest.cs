using Ae.Product.Service.Core.Model.InstallService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.InstallService
{
    /// <summary>
    /// AddInstallServiceRequest
    /// </summary>
    public class AddInstallServiceRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public List<ProductForInstall> Products { get; set; }

        /// <summary>
        /// 安装服务
        /// </summary>
        public InstallServiceVo InstallService { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
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
