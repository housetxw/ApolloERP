using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 原配轮胎查询Request
    /// </summary>
    public class OeTirePidByTidRequest
    {
        /// <summary>
        /// 五级属性Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }
    }
}
