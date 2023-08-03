using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Adaptation
{
    /// <summary>
    /// 切换安装方式
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
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        [IgnoreDataMember]
        public string Channel { get; set; }
    }
}
