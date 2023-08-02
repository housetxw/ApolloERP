using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 删除车型request
    /// </summary>
    public class DeleteVehicleByTidRequest
    {
        /// <summary>
        /// tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }
    }
}
