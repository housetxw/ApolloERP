using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Adapter
{
    /// <summary>
    /// 车型请求
    /// </summary>
    public class VehicleClientRequest
    {
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

        /// <summary>
        /// 五级属性
        /// </summary>
        public List<VehicleClientProperty> Properties { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 上路时间（Eg:2018-09）
        /// </summary>
        public string OnRoadTime { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 机油类别：汽油，柴油，
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// 均价
        /// </summary>
        public decimal AvgPrice { get; set; }
    }

    /// <summary>
    /// 车型五级属性
    /// </summary>
    public class VehicleClientProperty
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyKey { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }
    }
}
