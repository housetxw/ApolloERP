using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
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
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 公里数
        /// </summary>
        public int TotalMileage { get; set; }

        /// <summary>
        /// 五级属性集合
        /// </summary>
        public List<VehiclePropertyVo> CarProperties { get; set; }
    }

    /// <summary>
    /// 五级属性
    /// </summary>
    public class VehiclePropertyVo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyKey { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }
    }

}
