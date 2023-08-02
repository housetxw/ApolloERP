using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 车系查排量
    /// </summary>
    public class GetPaiLiangByVehicleIdRequest : BaseGetRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系Id不能为空")]
        public string VehicleId { get; set; }
    }
}
