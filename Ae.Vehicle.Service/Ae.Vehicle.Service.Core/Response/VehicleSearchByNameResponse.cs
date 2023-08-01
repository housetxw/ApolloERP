using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Response
{
    /// <summary>
    /// 车系搜索
    /// </summary>
    public class VehicleSearchByNameResponse
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌图片url
        /// </summary>
        public string BrandUrl { get; set; }

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
