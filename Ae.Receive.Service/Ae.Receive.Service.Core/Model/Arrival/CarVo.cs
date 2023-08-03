using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class CarVo
    {
        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车标
        /// </summary>
        public string CarLogo { get; set; }

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNo { get; set; }
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

    }
}
