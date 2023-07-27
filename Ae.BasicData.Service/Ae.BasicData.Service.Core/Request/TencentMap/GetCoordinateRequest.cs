using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BasicData.Service.Core.Request
{
    public class GetCoordinateRequest
    {
        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }
    }
}
