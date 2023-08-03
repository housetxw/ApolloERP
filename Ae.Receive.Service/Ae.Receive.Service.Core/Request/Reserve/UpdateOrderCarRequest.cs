using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Reserve
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
