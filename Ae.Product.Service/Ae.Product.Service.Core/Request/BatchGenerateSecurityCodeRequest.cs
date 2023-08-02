using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class BatchGenerateSecurityCodeRequest
    {
        /// <summary>
        /// 生成个数
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "数量必须大于0")]
        public int Count { get; set; }
    }
}
