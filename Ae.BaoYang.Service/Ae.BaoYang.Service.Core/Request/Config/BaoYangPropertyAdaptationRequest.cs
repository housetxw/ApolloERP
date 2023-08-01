using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 五级属性适配列表
    /// </summary>
    public class BaoYangPropertyAdaptationRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        [MinLength(1, ErrorMessage = "Tid不能为空")]
        public List<string> TidList { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }
    }
}
