using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetSalesByPidsRequest
    {
        /// <summary>
        /// PID集合
        /// </summary>
        [Required(ErrorMessage = "PID集合不可为空")]
        public List<string> Pids { get; set; }
    }
}
