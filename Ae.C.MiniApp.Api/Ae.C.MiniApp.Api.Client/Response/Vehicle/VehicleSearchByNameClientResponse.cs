﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class VehicleSearchByNameClientResponse
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
