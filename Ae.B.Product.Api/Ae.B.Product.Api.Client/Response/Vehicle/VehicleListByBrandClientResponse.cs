using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Response.Vehicle
{
    public class VehicleListByBrandClientResponse
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 厂商（Eg:一汽大众奥迪）
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// 车系 显示（Eg:A4L）
        /// </summary>
        public string VehicleSeries { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public List<string> TireSize { get; set; }
    }
}
