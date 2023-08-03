using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Arrival
{
    /// <summary>
    /// 车辆信息
    /// </summary>
   public class VehicleVo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 车标Icon
        /// </summary>
        public string CarBrandIcon { get; set; } = string.Empty;

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;


        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; } = string.Empty;

        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; } = string.Empty;
    }
}
