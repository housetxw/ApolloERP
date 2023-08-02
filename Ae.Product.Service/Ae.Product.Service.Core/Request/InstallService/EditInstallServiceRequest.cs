using Ae.Product.Service.Core.Model.InstallService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.InstallService
{
    /// <summary>
    /// 删除安装服务
    /// </summary>
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

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
