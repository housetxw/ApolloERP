using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 
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
    }
}
