using Ae.C.MiniApp.Api.Client.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Order
{
    public class SubmitOrderClientRequest : BaseSubmitOrderInfoDTO
    {
        /// <summary>
        /// 核销码
        /// </summary>
        public string Code { get; set; }
    }
}
