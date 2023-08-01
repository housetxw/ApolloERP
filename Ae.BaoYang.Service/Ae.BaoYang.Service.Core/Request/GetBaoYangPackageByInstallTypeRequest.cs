using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBaoYangPackageByInstallTypeRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        [Required(ErrorMessage = "PackageType不能为空")]
        public string PackageType { get; set; }

        /// <summary>
        /// 安装类型
        /// </summary>
        [Required(ErrorMessage = "InstallType不能为空")]
        public string InstallType { get; set; }

        /// <summary>
        /// 地区ID
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        [Required(ErrorMessage = "Channel不能为空")]
        public string Channel { get; set; }
    }
}
