using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 原配轮胎尺寸Request
    /// </summary>
    public class OeTireSizeByTidRequest
    {
        /// <summary>
        /// 五级属性Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }
    }
}
