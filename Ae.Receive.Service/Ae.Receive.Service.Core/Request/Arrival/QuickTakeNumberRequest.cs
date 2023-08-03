using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 快速排队拿号请求
    /// </summary>
    public class QuickTakeNumberRequest
    {
        /// <summary>
        /// 车型的信息
        /// </summary>
        public VehicleVo Vechicle { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ShopId
        /// </summary>
        public long ShopId { get; set; }


        /// <summary>
        /// 门店Name
        /// </summary>
        public string ShopName { get; set; }
    }
}
