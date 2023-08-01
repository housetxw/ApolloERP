using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Model
{
    public class VehicleTypeDto
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }
        /// <summary>
        /// 轮胎尺寸;分割
        /// </summary>
        public string Tires { get; set; }
        /// <summary>
        /// 轮胎匹配
        /// </summary>
        public string TiresMatch { get; set; }
        /// <summary>
        /// 德系;意系;日系……
        /// </summary>
        public string BrandCategory { get; set; }

        /// <summary>
        /// 大型车;中型车;
        /// </summary>
        public string VehicleBodyType { get; set; }

        /// <summary>
        /// 均价
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 车系级别
        /// </summary>
        public string VehicleLevel { get; set; }

        /// <summary>
        /// 四轮定位服务等级
        /// </summary>
        public string ServicePriceLevel { get; set; }

        /// <summary>
        /// 品牌下的厂商排序
        /// </summary>
        public int FactoryRank { get; set; }

        /// <summary>
        /// 厂商下的车型排序
        /// </summary>
        public int VehicleRank { get; set; }

        /// <summary>
        /// 品牌拼音
        /// </summary>
        public string BrandPy { get; set; }

        /// <summary>
        /// 品牌拼音-简音
        /// </summary>
        public string BrandJpy { get; set; }

        /// <summary>
        /// 车系拼音
        /// </summary>
        public string VehiclePy { get; set; }

        /// <summary>
        /// 车系拼音-简音
        /// </summary>
        public string VehicleJpy { get; set; }
    }
}
