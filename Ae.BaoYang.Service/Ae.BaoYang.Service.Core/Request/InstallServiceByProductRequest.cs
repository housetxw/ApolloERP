using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// InstallServiceByProductRequest
    /// </summary>
    public class InstallServiceByProductRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        [Required(ErrorMessage = "产品不能为空")]
        [MinLength(1, ErrorMessage = "产品不能为空")]
        public List<InstallProductRequest> Products { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public VehicleRequest Vehicle { get; set; }
    }

    /// <summary>
    /// InstallProductRequest
    /// </summary>
    public class InstallProductRequest
    {
        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 安装类型
        /// </summary>
        public string InstallType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "数量必须大于0")]
        public int Num { get; set; }
    }
}
