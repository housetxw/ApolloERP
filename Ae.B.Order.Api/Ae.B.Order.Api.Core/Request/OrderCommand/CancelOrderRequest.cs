using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request
{
    public class CancelOrderRequest : CreateReverseOrderBaseRequest
    {

        public string CreateBy { get; set; }
    }
}
