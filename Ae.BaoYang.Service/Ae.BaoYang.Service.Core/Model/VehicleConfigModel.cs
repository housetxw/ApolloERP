using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 车型配置
    /// </summary>
    public class VehicleConfigModel
    {
        /// <summary>
        /// 款型Id
        /// </summary>
        public string Tid { get; set; }
        /// <summary>
        /// 车身结构：4门5座三厢车
        /// </summary>
        public string CarBody { get; set; }
        /// <summary>
        /// 发动机编号
        /// </summary>
        public string EngineNo { get; set; }
        /// <summary>
        /// 底盘编号
        /// </summary>
        public string ChassisNo { get; set; }
        /// <summary>
        /// 变速箱类型(英文)
        /// </summary>
        public string TransmissionTypeE { get; set; }
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
    }
}
