using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class GetOrderCarsRequest
    {
        /// <summary>
        /// 订单IDS
        /// </summary>
        [Required]
        public List<long> OrderIds { get; set; }
    }
}
