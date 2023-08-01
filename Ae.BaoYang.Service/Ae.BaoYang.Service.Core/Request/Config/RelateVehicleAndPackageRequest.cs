using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 车型关联套餐
    /// </summary>
    public class RelateVehicleAndPackageRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        [Required(ErrorMessage = "ByType不能为空")]
        public string ByType { get; set; }

        /// <summary>
        /// 套餐pid
        /// </summary>
        [Required(ErrorMessage = "PackageId不能为空")]
        [MinLength(1, ErrorMessage = "PackageId不能为空")]
        public List<string> PackageId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
