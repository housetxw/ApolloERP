using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyTireProductRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系Id不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSize { get; set; }

        /// <summary>
        /// 产品Pid
        /// </summary>
        [Required(ErrorMessage = "商品Id不能为空")]
        [MinLength(1, ErrorMessage = "商品Id不能为空")]
        public List<string> PidList { get; set; }
    }
}
