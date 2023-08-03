using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 品牌查车系
    /// </summary>
    public class VehicleListResponse
    {
        /// <summary>
        /// 厂商（Eg:一汽大众奥迪）
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public List<VehicleListModel> VehicleList { get; set; }
    }

    /// <summary>
    /// 车系列表
    /// </summary>
    public class VehicleListModel
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
        /// 车系 显示（Eg:A4L）
        /// </summary>
        public string VehicleSeries { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public List<string> TireSize { get; set; }
    }
}
