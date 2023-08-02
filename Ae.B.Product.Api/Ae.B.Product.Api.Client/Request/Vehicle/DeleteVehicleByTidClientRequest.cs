using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Vehicle
{
    public class DeleteVehicleByTidClientRequest
    {
        /// <summary>
        /// tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
