using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class UpdateOrderCarRequest
    {
        [Required]
        public string OrderNo { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
