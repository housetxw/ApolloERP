using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class ShangMenServiceRequest
    {
        /// <summary>
        /// 产品Pid
        /// </summary>
        [Required(ErrorMessage = "产品Id不能为空")]
        [MinLength(1, ErrorMessage = "产品Id不能为空")]
        public List<string> PidList { get; set; }
    }
}
