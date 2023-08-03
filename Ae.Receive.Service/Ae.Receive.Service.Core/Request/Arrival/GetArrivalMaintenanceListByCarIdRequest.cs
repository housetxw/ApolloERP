using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetArrivalMaintenanceListByCarIdRequest
    {
        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// ShopId
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ServiceType { get; set; }
    }
}
