using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    public class VehicleInfo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系表里面的品牌
        /// </summary>
        public string BrandFromVehicleType { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系表的车系
        /// </summary>
        public string VehicleFromVehicleType { get; set; }

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
        /// 发动机型号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 变速箱类型(英文)
        /// </summary>
        public string TransmissionTypeE { get; set; }

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

        /// <summary>
        /// 车身结构：4门5座三厢车
        /// </summary>
        public string CarBody { get; set; }

        /// <summary>
        /// 底盘编号
        /// </summary>
        public string ChassisNo { get; set; }

        /// <summary>
        /// 变速箱类型(中文)
        /// </summary>
        public string TransmissionTypeC { get; set; }

        /// <summary>
        /// 功率 单位KW
        /// </summary>
        public decimal PowerKw { get; set; }

        /// <summary>
        /// 气缸数
        /// </summary>
        public int ValveNumPerCylinder { get; set; }

        /// <summary>
        /// 进气形式
        /// </summary>
        public string AirIntakeMode { get; set; }

        /// <summary>
        /// 驱动方式
        /// </summary>
        public string DriveType { get; set; }

        /// <summary>
        /// 助力转向类型
        /// </summary>
        public string SteeringType { get; set; }

        /// <summary>
        /// 前制动器类型
        /// </summary>
        public string FrontBrakeType { get; set; }

        /// <summary>
        /// 后制动器类型
        /// </summary>
        public string BackBrakeType { get; set; }

        /// <summary>
        /// 卤素灯
        /// </summary>
        public string HalogenLamp { get; set; }

        /// <summary>
        /// 氙灯
        /// </summary>
        public string XenonLamp { get; set; }

        /// <summary>
        /// LED灯
        /// </summary>
        public string LedLamp { get; set; }

        /// <summary>
        /// 燃油滤类型
        /// </summary>
        public string FuelFilterType { get; set; }

        /// <summary>
        /// 前胎规格
        /// </summary>
        public string FrontTireSize { get; set; }

        /// <summary>
        /// 后胎规格
        /// </summary>
        public string RearTireSize { get; set; }

        /// <summary>
        /// 供油方式
        /// </summary>
        public string OilSupplyMode { get; set; }

        /// <summary>
        /// 铝合金轮毂
        /// </summary>
        public string AlloyWheel { get; set; }

        /// <summary>
        /// 胎压监测装置
        /// </summary>
        public string TireMonitorSystem { get; set; }

        /// <summary>
        /// 发动机启停技术
        /// </summary>
        public string AutoStartStopSys { get; set; }

        /// <summary>
        /// 前悬架类型
        /// </summary>
        public string FrontSuspensionType { get; set; }

        /// <summary>
        /// 后悬架类型
        /// </summary>
        public string RearSuspensionType { get; set; }

        /// <summary>
        /// 远光灯类型
        /// </summary>
        public string HighBeamType { get; set; }

        /// <summary>
        /// 近光灯类型
        /// </summary>
        public string DippedHeadlightType { get; set; }

        /// <summary>
        /// 变速箱型号
        /// </summary>
        public string Transmission { get; set; }

        /// <summary>
        /// 原配轮胎pid
        /// </summary>
        public List<string> Pid { get; set; }

        /// <summary>
        /// 原配轮胎产品(无产品)
        /// </summary>
        public List<string> NoProductName { get; set; }
    }
}
