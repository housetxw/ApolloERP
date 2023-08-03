using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Vehicle
{
    public class VehicleBaseInfoDto
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 开始生产年份
        /// </summary>
        public string ListedYear { get; set; }

        /// <summary>
        /// 停止生产年份
        /// </summary>
        public string StopProductionYear { get; set; }

        /// <summary>
        /// 厂商
        /// </summary>
        public string Factory { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public string VehicleSeries { get; set; }

        /// <summary>
        /// 车系级别：中型车，紧凑型SUV，紧凑型车……
        /// </summary>
        public string VehicleLevel { get; set; }

        /// <summary>
        /// 车系类型：三厢车，SUV，两厢车，三厢车……
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 上市月份
        /// </summary>
        public string ListedMonth { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        public string ManufactureYear { get; set; }

        /// <summary>
        /// 生产状况
        /// </summary>
        public string ProductionStatus { get; set; }

        /// <summary>
        /// 排气标准：国Ⅴ，国Ⅳ（国Ⅴ），欧Ⅵ……
        /// </summary>
        public string EmissionStandard { get; set; }

        /// <summary>
        /// 品牌所属国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 销售状态：在销，停销
        /// </summary>
        public string SalesStatus { get; set; }

        /// <summary>
        /// 国产;合资;进口
        /// </summary>
        public string JointVenture { get; set; }

        /// <summary>
        /// 机油类别：汽油，柴油，
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public decimal GuidePrice { get; set; }

        /// <summary>
        /// 最小价
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 均价
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// 最大价
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// 气缸数量
        /// </summary>
        public int CylinderNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
