using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Request.BaoYang
{
    public class GetBaoYangPackagesRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 保养类型（Eg:xby,ys……）
        /// </summary>
        public List<string> BaoYangType { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public List<string> ProductId { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public int ShopId { get; set; }
    }

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
        public List<VehiclePropertyDto> Properties { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 上路时间（Eg:2018-09）
        /// </summary>
        public string OnRoadTime { get; set; }
    }

    public class VehiclePropertyDto
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
