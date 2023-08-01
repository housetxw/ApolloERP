using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_type_timing_config")]
    public class VehicleTypeTimingConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 款型Id
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }
        /// <summary>
        /// 车身结构：4门5座三厢车
        /// </summary>
        [Column("car_body")]
        public string CarBody { get; set; }
        /// <summary>
        /// 发动机编号
        /// </summary>
        [Column("engine_no")]
        public string EngineNo { get; set; }
        /// <summary>
        /// 底盘编号
        /// </summary>
        [Column("chassis_no")]
        public string ChassisNo { get; set; }

        /// <summary>
        /// 变速箱类型 自动  or  手动
        /// </summary>
        [Column("transmission_type")]
        public string TransmissionType { get; set; }

        /// <summary>
        /// 变速箱类型(英文)
        /// </summary>
        [Column("transmission_type_e")]
        public string TransmissionTypeE { get; set; }
        /// <summary>
        /// 变速箱类型(中文)
        /// </summary>
        [Column("transmission_type_c")]
        public string TransmissionTypeC { get; set; }
        /// <summary>
        /// 功率 单位KW
        /// </summary>
        [Column("power_kw")]
        public decimal PowerKw { get; set; }
        /// <summary>
        /// 气缸数
        /// </summary>
        [Column("valve_num_per_cylinder")]
        public int ValveNumPerCylinder { get; set; }
        /// <summary>
        /// 进气形式
        /// </summary>
        [Column("air_intake_mode")]
        public string AirIntakeMode { get; set; }
        /// <summary>
        /// 驱动方式
        /// </summary>
        [Column("drive_type")]
        public string DriveType { get; set; }
        /// <summary>
        /// 助力转向类型
        /// </summary>
        [Column("steering_type")]
        public string SteeringType { get; set; }
        /// <summary>
        /// 前制动器类型
        /// </summary>
        [Column("front_brake_type")]
        public string FrontBrakeType { get; set; }
        /// <summary>
        /// 后制动器类型
        /// </summary>
        [Column("back_brake_type")]
        public string BackBrakeType { get; set; }
        /// <summary>
        /// 卤素灯
        /// </summary>
        [Column("halogen_lamp")]
        public string HalogenLamp { get; set; }
        /// <summary>
        /// 氙灯
        /// </summary>
        [Column("xenon_lamp")]
        public string XenonLamp { get; set; }
        /// <summary>
        /// LED灯
        /// </summary>
        [Column("led_lamp")]
        public string LedLamp { get; set; }
        /// <summary>
        /// 燃油滤类型
        /// </summary>
        [Column("fuel_filter_type")]
        public string FuelFilterType { get; set; }
        /// <summary>
        /// 前胎规格
        /// </summary>
        [Column("front_tire_size")]
        public string FrontTireSize { get; set; }
        /// <summary>
        /// 后胎规格
        /// </summary>
        [Column("rear_tire_size")]
        public string RearTireSize { get; set; }
        /// <summary>
        /// 供油方式
        /// </summary>
        [Column("oil_supply_mode")]
        public string OilSupplyMode { get; set; }
        /// <summary>
        /// 铝合金轮毂
        /// </summary>
        [Column("alloy_wheel")]
        public string AlloyWheel { get; set; }
        /// <summary>
        /// 胎压监测装置
        /// </summary>
        [Column("tire_monitor_system")]
        public string TireMonitorSystem { get; set; }
        /// <summary>
        /// 发动机启停技术
        /// </summary>
        [Column("auto_start_stop_sys")]
        public string AutoStartStopSys { get; set; }
        /// <summary>
        /// 前悬架类型
        /// </summary>
        [Column("front_suspension_type")]
        public string FrontSuspensionType { get; set; }
        /// <summary>
        /// 后悬架类型
        /// </summary>
        [Column("rear_suspension_type")]
        public string RearSuspensionType { get; set; }
        /// <summary>
        /// 远光灯类型
        /// </summary>
        [Column("high_beam_type")]
        public string HighBeamType { get; set; }
        /// <summary>
        /// 近光灯类型
        /// </summary>
        [Column("dipped_headlight_type")]
        public string DippedHeadlightType { get; set; }
        /// <summary>
        /// 变速箱型号
        /// </summary>
        [Column("transmission")]
        public string Transmission { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
