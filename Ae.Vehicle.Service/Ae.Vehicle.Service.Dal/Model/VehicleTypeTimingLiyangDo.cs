using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_type_timing_liyang")]
    public class VehicleTypeTimingLiyangDo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 力洋Id
        /// </summary>
        [Column("level_id")]
        public string LevelId { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        [Column("factory")]
        public string Factory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("factory_code")]
        public string FactoryCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("brand_code")]
        public string BrandCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_series")]
        public string VehicleSeries { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_series_code")]
        public string VehicleSeriesCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_models")]
        public string VehicleModels { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("nian")]
        public string Nian { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("emission_standard")]
        public string EmissionStandard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_type")]
        public string VehicleType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_level")]
        public string VehicleLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("guide_price")]
        public decimal GuidePrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("listed_year")]
        public string ListedYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("manufacture_year")]
        public string ManufactureYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("stop_production_year")]
        public string StopProductionYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("joint_venture")]
        public string JointVenture { get; set; }
        /// <summary>
        /// 工信部车型代码
        /// </summary>
        [Column("miit_models_code")]
        public string MiitModelsCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("engine_no")]
        public string EngineNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("pai_liang")]
        public string PaiLiang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("air_intake_mode")]
        public string AirIntakeMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("fuel_type")]
        public string FuelType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("max_power_kw")]
        public string MaxPowerKw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("max_torque")]
        public string MaxTorque { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("max_torque_rpm")]
        public string MaxTorqueRpm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("cylinder_number")]
        public int CylinderNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("valve_num_per_cylinder")]
        public int ValveNumPerCylinder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("oil_supply_mode")]
        public string OilSupplyMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("twc")]
        public string Twc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("transmission_type")]
        public string TransmissionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("transmission_description")]
        public string TransmissionDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("front_brake_type")]
        public string FrontBrakeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("back_brake_type")]
        public string BackBrakeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("front_suspension_type")]
        public string FrontSuspensionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("rear_suspension_type")]
        public string RearSuspensionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("steering_type")]
        public string SteeringType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("drive_type")]
        public string DriveType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("car_body")]
        public string CarBody { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("length")]
        public string Length { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("max_weight")]
        public string MaxWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("door_number")]
        public string DoorNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("front_tire_size")]
        public string FrontTireSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("rear_tire_size")]
        public string RearTireSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("alloy_wheel")]
        public string AlloyWheel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("electric_trunk")]
        public string ElectricTrunk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("pai_liang_2")]
        public string PaiLiang2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("factory_en")]
        public string FactoryEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("brand_en")]
        public string BrandEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("series_en")]
        public string SeriesEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("models_en")]
        public string ModelsEn { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Column("version")]
        public int Version { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
