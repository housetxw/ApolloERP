using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// Tid查询可适配套餐（剔除已绑定的）
    /// </summary>
    public class BaoYangPackageByTidRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        [Required(ErrorMessage = "BaoYangType不能为空")]
        public string BaoYangType { get; set; }
    }
}
