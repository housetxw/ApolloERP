using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 删除车型
    /// </summary>
    public class DeleteVehicleByLiYangRequest
    {
        /// <summary>
        /// 力洋Id
        /// </summary>
        public List<string> LiYangId { get; set; }
    }
}
