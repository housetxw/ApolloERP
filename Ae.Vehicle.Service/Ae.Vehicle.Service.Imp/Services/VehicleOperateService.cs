using Ae.Vehicle.Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Dal.Repository;
using System.Linq;
using System.Transactions;
using Ae.Vehicle.Service.Dal.Model;
using Ae.Vehicle.Service.Common.Helper;

namespace Ae.Vehicle.Service.Imp.Services
{
    public class VehicleOperateService : IVehicleOperateService
    {
        private readonly IVehicleTypeTimingRepository _vehicleTypeTimingRepository;
        private readonly IVehicleTypeTimingConfigRepository _vehicleTypeTimingConfigRepository;
        private readonly IVehicleTypeTimingIdMapRepository _vehicleTypeTimingIdMapRepository;
        private readonly IVehicleTypeTimingLiyangRepository _vehicleTypeTimingLiyangRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IVehicleBrandRepository _vehicleBrandRepository;
        private readonly IVehicleTypeTimingCopyRepository _vehicleTypeTimingCopyRepository;
        private readonly IBaoYangPartsRepository _baoYangPartsRepository;
        private readonly IBaoYangPartAccessoryRepository _baoYangPartAccessoryRepository;
        private readonly IBaoYangPartsLiyangRepository _baoYangPartsLiyangRepository;
        private readonly IBaoYangPartAccessoryLiyangRepository _baoYangPartAccessoryLiyangRepository;

        public VehicleOperateService(IVehicleTypeTimingRepository vehicleTypeTimingRepository,
            IVehicleTypeTimingLiyangRepository vehicleTypeTimingLiyangRepository,
            IVehicleTypeTimingConfigRepository vehicleTypeTimingConfigRepository,
            IVehicleTypeTimingIdMapRepository vehicleTypeTimingIdMapRepository,
            IVehicleTypeRepository vehicleTypeRepository, IVehicleBrandRepository vehicleBrandRepository,
            IVehicleTypeTimingCopyRepository vehicleTypeTimingCopyRepository,
            IBaoYangPartsRepository baoYangPartsRepository,
            IBaoYangPartAccessoryRepository baoYangPartAccessoryRepository,
            IBaoYangPartsLiyangRepository baoYangPartsLiyangRepository,
            IBaoYangPartAccessoryLiyangRepository baoYangPartAccessoryLiyangRepository)
        {
            _vehicleTypeTimingRepository = vehicleTypeTimingRepository;
            _vehicleTypeTimingLiyangRepository = vehicleTypeTimingLiyangRepository;
            _vehicleTypeTimingConfigRepository = vehicleTypeTimingConfigRepository;
            _vehicleTypeTimingIdMapRepository = vehicleTypeTimingIdMapRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleBrandRepository = vehicleBrandRepository;
            _vehicleTypeTimingCopyRepository = vehicleTypeTimingCopyRepository;
            _baoYangPartsRepository = baoYangPartsRepository;
            _baoYangPartAccessoryRepository = baoYangPartAccessoryRepository;
            _baoYangPartsLiyangRepository = baoYangPartsLiyangRepository;
            _baoYangPartAccessoryLiyangRepository = baoYangPartAccessoryLiyangRepository;
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleByTid(DeleteVehicleByTidRequest request)
        {
            var result = await _vehicleTypeTimingRepository.DeleteVehicleByTid(request.Tid, request.SubmitBy);

            return result;
        }

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddVehicle(AddVehicleRequest request)
        {
            VehicleTypeTimingDO vehicleTypeTimingDo = new VehicleTypeTimingDO
            {

            };

            return await Task.FromResult("");
        }

        /// <summary>
        /// 力洋车型数据初始化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InitLiYangData(InitLiYangDataRequest request)
        {
            /** 第一次初始化
            List<string> noBrand = new List<string>();
            List<VehicleTypeDO> noVehicle = new List<VehicleTypeDO>();
            if (request.NonStr != "ApolloErpIni1235")
            {
                return false;
            }
            var liYangData = await _vehicleTypeTimingLiyangRepository.GetAllVehicleTypeTimingLiyang();
            var brandData = await _vehicleBrandRepository.GetAllVehicleBrandsAsync();
            var vehicleData = await _vehicleTypeRepository.GetAllVehicleList();
            var minTid = 200000;
            DateTime now = DateTime.Now;
            var groupData = liYangData.GroupBy(_ => new { _.BrandCode, _.FactoryCode, _.VehicleSeriesCode, _.VehicleModels, _.PaiLiang2, _.Nian, _.SalesName }).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            List<VehicleTypeTimingConfigDO> configList = new List<VehicleTypeTimingConfigDO>();
            List<VehicleTypeTimingDO> timingList = new List<VehicleTypeTimingDO>();
            List<VehicleTypeTimingIdMapDo> mapList = new List<VehicleTypeTimingIdMapDo>();
            foreach (var itemTid in groupData)
            {
                var defaultItem = itemTid.Value.FirstOrDefault();
                var key = itemTid.Key;
                var tid = minTid.ToString();

                VehicleTypeTimingDO itemTiming = new VehicleTypeTimingDO
                {
                    Tid = tid,
                    Factory = defaultItem.Factory,
                    Brand = defaultItem.Brand,
                    VehicleSeries = defaultItem.VehicleSeries,
                    Vehicle = defaultItem.VehicleModels,
                    PaiLiang = defaultItem.PaiLiang2,
                    SalesName = defaultItem.SalesName,
                    Nian = defaultItem.Nian,
                    VehicleLevel = defaultItem.VehicleLevel,
                    VehicleType = defaultItem.VehicleType,
                    ListedYear = defaultItem.ListedYear,
                    ProductionStatus = "",
                    EmissionStandard = defaultItem.EmissionStandard,
                    StopProductionYear = defaultItem.StopProductionYear.Length == 4 ? defaultItem.StopProductionYear : string.Empty,
                    Country = "",
                    SalesStatus = "",
                    JointVenture = defaultItem.JointVenture,
                    FuelType = defaultItem.FuelType,
                    GuidePrice = defaultItem.GuidePrice,
                    CylinderNumber = defaultItem.CylinderNumber,
                    Status = "Active",
                    CreateBy = "System",
                    CreateTime = now,
                    UpdateBy = "System",
                    UpdateTime = now
                };

                if (!brandData.Any(_ => _.BrandSuffix == defaultItem.Brand))
                {
                    if (!noBrand.Contains(defaultItem.Brand))
                    {
                        noBrand.Add(defaultItem.Brand);
                    }
                }
                var vehicleStr = defaultItem.VehicleModels + "-" + (defaultItem.Factory == "一汽大众(奥迪)" ? "一汽大众奥迪" : defaultItem.Factory);
                var defaultVehicle = vehicleData.FirstOrDefault(_ => _.Brand.Substring(4) == defaultItem.Brand && _.Vehicle == vehicleStr);
                if (defaultVehicle != null)
                {
                    itemTiming.VehicleId = defaultVehicle.VehicleId;
                }
                else
                {
                    itemTiming.VehicleId = "";
                    if (!noVehicle.Any(_ => _.Brand == defaultItem.Brand && _.Vehicle == vehicleStr))
                    {
                        noVehicle.Add(new VehicleTypeDO
                        {
                            Brand = defaultItem.Brand,
                            Vehicle = vehicleStr
                        });
                    }
                }


                timingList.Add(itemTiming);
                var power = defaultItem.MaxPowerKw;
                if (power.Contains("("))
                {
                    power = power.Split(new char[] { '(' })[0];
                }
                else if (power.Contains("/"))
                {
                    power = power.Split(new char[] { '/' })[0];
                }
                else
                {

                }
                Decimal.TryParse(power, out var powerKw);
                VehicleTypeTimingConfigDO itemConfig = new VehicleTypeTimingConfigDO
                {
                    Tid = tid,
                    CarBody = defaultItem.CarBody,
                    EngineNo = defaultItem.EngineNo,
                    ChassisNo = "",
                    TransmissionType = defaultItem.TransmissionType,
                    TransmissionTypeC = defaultItem.TransmissionDescription,
                    TransmissionTypeE = "",
                    PowerKw = powerKw,
                    ValveNumPerCylinder = defaultItem.ValveNumPerCylinder,
                    AirIntakeMode = defaultItem.AirIntakeMode,
                    DriveType = defaultItem.DriveType,
                    SteeringType = defaultItem.SteeringType,
                    FrontBrakeType = defaultItem.FrontBrakeType,
                    BackBrakeType = defaultItem.BackBrakeType,
                    HalogenLamp = "",
                    XenonLamp = "",
                    LedLamp = "",
                    FuelFilterType = "",
                    FrontTireSize = defaultItem.FrontTireSize,
                    RearTireSize = defaultItem.RearTireSize,
                    OilSupplyMode = defaultItem.OilSupplyMode,
                    AlloyWheel = defaultItem.AlloyWheel,
                    TireMonitorSystem = "",
                    AutoStartStopSys = "",
                    FrontSuspensionType = defaultItem.FrontSuspensionType,
                    RearSuspensionType = defaultItem.RearSuspensionType,
                    HighBeamType = "",
                    DippedHeadlightType = "",
                    Transmission = "",
                    CreateBy = "System",
                    CreateTime = now,
                    UpdateBy = "System",
                    UpdateTime = now
                };
                configList.Add(itemConfig);
                itemTid.Value.ForEach(_ =>
                {
                    VehicleTypeTimingIdMapDo mapItem = new VehicleTypeTimingIdMapDo
                    {
                        Tid = tid,
                        ExternalId = _.LevelId,
                        Source = "LiYangLevelId",
                        CreateTime = now,
                        UpdateTime = now
                    };
                    mapList.Add(mapItem);
                });
                minTid++;
            }

            await _vehicleTypeTimingConfigRepository.InsertBatchAsync(configList);
            await _vehicleTypeTimingRepository.InsertBatchAsync(timingList);
            await _vehicleTypeTimingIdMapRepository.InsertBatchAsync(mapList);**/

            /** 厂商英文/车系英文
            var liYangData = await _vehicleTypeTimingLiyangRepository.GetAllVehicleTypeTimingLiyang();
            var minTid = 1;
            DateTime now = DateTime.Now;
            var groupData = liYangData.GroupBy(_ => new { _.BrandCode, _.FactoryCode, _.VehicleSeriesCode, _.VehicleModels, _.PaiLiang2, _.Nian, _.SalesName }).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            foreach (var _ in groupData)
            {
                var defaultItem = _.Value.FirstOrDefault();
                var key = _.Key;


                VehicleTypeTimingDO ss = new VehicleTypeTimingDO
                {
                    Id = minTid,
                    FactoryCode = defaultItem.FactoryCode,
                    FactoryEn = defaultItem.FactoryEn,
                    VehicleEn = defaultItem.ModelsEn
                };

                await _vehicleTypeTimingRepository.UpdateAsync(ss, new List<string> { "FactoryCode", "FactoryEn", "VehicleEn" });
                minTid++;
            }**/

            //List<VehicleTypeDO> noVehicle = new List<VehicleTypeDO>();

            //var vehicleData = await _vehicleTypeRepository.GetAllVehicleList();
            //vehicleData = vehicleData.Where(_ => _.VehicleId.StartsWith("VE-")).ToList();
            //var emptyVehicleId = await _vehicleTypeTimingRepository.GetAllVehicleId();
            //emptyVehicleId = emptyVehicleId.Where(_ => _.VehicleId.StartsWith("VEN-")).ToList();

            //foreach (var _ in emptyVehicleId)
            ////foreach (var _ in emptyVehicleId.Where(t => t.Brand == "长安跨越"))
            //{
            //    string factory = _.Factory;
            //    if (_.Factory == "雪铁龙汽车")
            //    {
            //        factory = "雪铁龙进口";
            //    }
            //    //else if (_.Factory == "北京吉普(现北京奔驰)")
            //    //{
            //    //    factory = "北京吉普";
            //    //}

            //    var vehicle = _.Vehicle.Replace("-", "—");//

            //    //if (vehicle.Contains("("))
            //    //{
            //    //    vehicle = vehicle.Split(new char[] { '(' })[0];
            //    //}

            //    if (vehicle.Contains("["))
            //    {
            //        //vehicle = vehicle.Split(new char[] { '[' })[0].Trim();
            //        //vehicle = vehicle.Replace(" [", "(").Replace("]", ")");
            //        //var lastaa = vehicle.Split(new char[] { '[' })[1];
            //        //vehicle = lastaa.Substring(0, lastaa.Length - 1);
            //    }
            //    var vehicleStr = vehicle + "-" + factory;
            //    var defaultItem = vehicleData.Where(t => t.Vehicle == vehicleStr).ToList();
            //    if (defaultItem.Count == 1)
            //    {
            //        VehicleTypeTimingDO ss = new VehicleTypeTimingDO
            //        {
            //            Id = _.Id,
            //            VehicleId = defaultItem[0].VehicleId
            //        };
            //        await _vehicleTypeTimingRepository.UpdateAsync(ss, new List<string> { "VehicleId" });
            //    }
            //    else if (defaultItem.Count == 0)
            //    {
            //        //var vehicleIdPrefix = "VEN-";
            //        //var factory = _.Factory.Replace("-", "·");
            //        //var factoryCode = _.FactoryCode;
            //        //var vehicleEn = _.VehicleEn.Replace("'", "").Replace(" ", "");
            //        //var vehicle = _.Vehicle.Replace("-", " ");

            //        //var vehicleId = vehicleIdPrefix + factoryCode + "-" + vehicleEn;
            //        //var vehicleE = vehicle + "-" + factory;
            //        //var brand = "XX" + _.Brand;
            //        //if (!noVehicle.Any(t => t.VehicleId == vehicleId))
            //        //{
            //        //    noVehicle.Add(new VehicleTypeDO
            //        //    {
            //        //        Brand = brand,
            //        //        VehicleId = vehicleId,
            //        //        Vehicle = vehicleE,
            //        //        Tires = "",
            //        //        TiresMatch = "",
            //        //        BrandCategory = "",
            //        //        VehicleBodyType = "",
            //        //        AvgPrice = 0,
            //        //        MaxPrice = 0,
            //        //        MinPrice = 0,
            //        //        VehicleLevel = "",
            //        //        ServicePriceLevel = "",
            //        //        FactoryRank = 0,
            //        //        VehicleRank = 0,
            //        //        BrandJpy = "",
            //        //        BrandPy = "",
            //        //        VehicleJpy = "",
            //        //        VehiclePy = "",
            //        //        Actived = 0,
            //        //        IsDeleted = false,
            //        //        CreateTime = DateTime.Now,
            //        //        CreateBy = "",
            //        //        UpdateBy = "",
            //        //        UpdateTime = DateTime.Now
            //        //    });
            //        //}

            //        //VehicleTypeTimingDO ss = new VehicleTypeTimingDO
            //        //{
            //        //    Id = _.Id,
            //        //    VehicleId = vehicleId
            //        //};
            //        //await _vehicleTypeTimingRepository.UpdateAsync(ss, new List<string> { "VehicleId" });
            //    }
            //}

            //await _vehicleTypeRepository.InsertBatchAsync(noVehicle);

            //var brandData = await _vehicleBrandRepository.GetAllVehicleBrandsAsync();
            //var vehicleData = await _vehicleTypeRepository.GetAllVehicleList();

            //vehicleData = vehicleData.Where(_ => _.Brand.StartsWith("XX")).ToList();
            //foreach (var _ in vehicleData)
            //{
            //    var brand = _.Brand.Substring(2);
            //    var defaulta = brandData.Where(x => x.BrandSuffix == brand).ToList();
            //    if (defaulta.Count == 1)
            //    {
            //        VehicleTypeDO ss = new VehicleTypeDO {
            //            Id = _.Id,
            //            Brand = defaulta[0].Brand
            //        };
            //        await _vehicleTypeRepository.UpdateAsync(ss, new List<string> { "Brand" });
            //    }
            //}


            //var vehicleData = await _vehicleTypeRepository.GetAllVehicleList();
            //var brandList = vehicleData.GroupBy(_ => _.Brand).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            //foreach (var t1 in brandList)
            //{
            //    var vehicleList = t1.Value.GroupBy(_ => _.Vehicle.Split(new char[] { '-' })[1]).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            //    int x1 = 1;
            //    foreach (var t2 in vehicleList)
            //    {
            //        int x2 = 1;
            //        foreach (var t3 in t2.Value.OrderBy(_ => _.Vehicle.Split(new char[] { '-' })[0]))
            //        {
            //            VehicleTypeDO ss = new VehicleTypeDO
            //            {
            //                Id = t3.Id,
            //                FactoryRank = x1,
            //                VehicleRank = x2
            //            };
            //            await _vehicleTypeRepository.UpdateAsync(ss, new List<string> { "FactoryRank", "VehicleRank" });
            //            x2++;
            //        }
            //        x1++;
            //    }
            //}

            var vehicleData = await _vehicleTypeRepository.GetAllVehicleList();
            var vehicleTiming = await _vehicleTypeTimingRepository.GetAllVehicleTiming();
            var vehicleConfig = await _vehicleTypeTimingConfigRepository.GetListAsync<VehicleTypeTimingConfigDO>("");
            foreach (var _ in vehicleData)
            {
                List<string> tireSize = new List<string>();
                var tidList = vehicleTiming.Where(x => x.VehicleId == _.VehicleId).Select(f => f.Tid).ToList();
                var config = vehicleConfig.Where(x => tidList.Contains(x.Tid)).ToList();
                foreach (var configItem in config)
                {
                    if (!tireSize.Contains(configItem.FrontTireSize))
                    {
                        tireSize.Add(configItem.FrontTireSize);
                    }
                    if (!tireSize.Contains(configItem.RearTireSize))
                    {
                        tireSize.Add(configItem.RearTireSize);
                    }
                }

                VehicleTypeDO ss = new VehicleTypeDO
                {
                    Id = _.Id,
                    Tires = string.Join(";", tireSize)
                };
                await _vehicleTypeRepository.UpdateAsync(ss, new List<string> { "Tires" });
            }


            return await Task.FromResult(true);
        }

        /// <summary>
        /// 根据力洋Id删除车型数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleByLiYang(DeleteVehicleByLiYangRequest request)
        {
            var levelIdList = request.LiYangId;
            await _vehicleTypeTimingLiyangRepository.Delete(levelIdList);
            await _vehicleTypeTimingIdMapRepository.Delete(levelIdList);
            await ClearVehicleTypeTiming();
            return true;
        }

        private async Task<bool> ClearVehicleTypeTiming()
        {
            var result = await _vehicleTypeTimingRepository.GetEmptyTid();

            if (result != null && result.Any())
            {
                await _vehicleTypeTimingRepository.Delete(result);
            }

            return true;
        }

        /// <summary>
        /// 同步vehicleId
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncVehicleId()
        {
            var result = await _vehicleTypeTimingRepository.GetAllVehicleTiming();
            var emptyVehicleId = result.Where(_ => string.IsNullOrEmpty(_.VehicleId)).ToList();
            foreach (var vehicleItem in emptyVehicleId)
            {
                var defaultVehicle = result.FirstOrDefault(_ =>
                    _.Brand == vehicleItem.Brand && _.Factory == vehicleItem.Factory &&
                    _.VehicleSeries == vehicleItem.VehicleSeries && _.Vehicle == vehicleItem.Vehicle &&
                    !string.IsNullOrEmpty(_.VehicleId));
                if (defaultVehicle != null)
                {
                    await _vehicleTypeTimingRepository.UpdateAsync(new VehicleTypeTimingDO()
                    {
                        VehicleId = defaultVehicle.VehicleId,
                        Id = vehicleItem.Id
                    }, new List<string>() {"VehicleId"});
                }
            }

            return true;
        }

        /// <summary>
        /// 新增力洋新数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddVehicleByLiYang(AddVehicleByLiYangRequest request)
        {
            return true;

            //var minTid = 241002;//241002~241363 四月数据
            //var minTid = 241364;//241364~242235 五月数据
            //var minTid = 242236;//242236~242899 六月数据
            //var minTid = 242900;//242900~243386 七月数据
            //var minTid = 243387;//243387~243391 八月数据
            //var minTid = 243392;//243392~244068 九月数据
            //var minTid = 244069;//244069~244369 十月数据
            //var minTid = 244370;//244370~244915 十一月数据
            //var minTid = 244916;//244916~244985十二月数据
            //var minTid = 244986;//244986~245446 1月数据
            //var minTid = 245447;//245447~245540 2月数据
            var minTid = 245541;//245541~ 3月数据
            DateTime now = DateTime.Now;
            List<VehicleTypeTimingDO> timingList = new List<VehicleTypeTimingDO>();
            List<VehicleTypeTimingConfigDO> configList = new List<VehicleTypeTimingConfigDO>();
            List<VehicleTypeTimingIdMapDo> mapList = new List<VehicleTypeTimingIdMapDo>();
            var liYangData = await _vehicleTypeTimingLiyangRepository.GetAllVehicleTypeTimingLiyang(request.Version);
            var groupData = liYangData
                .GroupBy(_ => new
                {
                    _.BrandCode,
                    _.FactoryCode,
                    _.VehicleSeriesCode,
                    _.VehicleModels,
                    _.PaiLiang2,
                    _.Nian,
                    _.SalesName
                }).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            var allTidDetail = await _vehicleTypeTimingRepository.GetAllVehicleTiming();
            foreach (var itemTid in groupData)
            {
                var defaultItem = itemTid.Value.FirstOrDefault();
                var key = itemTid.Key;
                var tid = minTid.ToString();
                var existTid = allTidDetail.FirstOrDefault(_ =>
                    _.Factory == defaultItem.Factory && _.Brand == defaultItem.Brand &&
                    _.VehicleSeries == defaultItem.VehicleSeries && _.Vehicle == defaultItem.VehicleModels &&
                    _.PaiLiang == defaultItem.PaiLiang2 && _.Nian == defaultItem.Nian &&
                    _.SalesName == defaultItem.SalesName);
                if (existTid == null)
                {
                    VehicleTypeTimingDO itemTiming = new VehicleTypeTimingDO
                    {
                        Tid = tid,
                        Factory = defaultItem.Factory,
                        FactoryCode = defaultItem.FactoryCode,
                        FactoryEn = defaultItem.FactoryEn,
                        VehicleEn = defaultItem.ModelsEn,
                        Brand = defaultItem.Brand,
                        VehicleSeries = defaultItem.VehicleSeries,
                        Vehicle = defaultItem.VehicleModels,
                        PaiLiang = defaultItem.PaiLiang2,
                        SalesName = defaultItem.SalesName,
                        Nian = defaultItem.Nian,
                        VehicleLevel = defaultItem.VehicleLevel,
                        VehicleType = defaultItem.VehicleType,
                        ListedYear = defaultItem.ListedYear,
                        ProductionStatus = "",
                        EmissionStandard = defaultItem.EmissionStandard,
                        StopProductionYear = defaultItem.StopProductionYear.Length == 4
                            ? defaultItem.StopProductionYear
                            : string.Empty,
                        Country = "",
                        SalesStatus = "",
                        JointVenture = defaultItem.JointVenture,
                        FuelType = defaultItem.FuelType,
                        GuidePrice = defaultItem.GuidePrice,
                        CylinderNumber = defaultItem.CylinderNumber,
                        Status = "Active",
                        CreateBy = "System",
                        CreateTime = now,
                        UpdateBy = "System",
                        UpdateTime = now,
                        IsDeleted = false
                    };

                    itemTiming.VehicleId = "";

                    timingList.Add(itemTiming);

                    var power = defaultItem.MaxPowerKw;
                    if (power.Contains("("))
                    {
                        power = power.Split(new char[] {'('})[0];
                    }
                    else if (power.Contains("/"))
                    {
                        power = power.Split(new char[] {'/'})[0];
                    }

                    Decimal.TryParse(power, out var powerKw);
                    VehicleTypeTimingConfigDO itemConfig = new VehicleTypeTimingConfigDO
                    {
                        Tid = tid,
                        CarBody = defaultItem.CarBody,
                        EngineNo = defaultItem.EngineNo,
                        ChassisNo = "",
                        TransmissionType = defaultItem.TransmissionType,
                        TransmissionTypeC = defaultItem.TransmissionDescription,
                        TransmissionTypeE = "",
                        PowerKw = powerKw,
                        ValveNumPerCylinder = defaultItem.ValveNumPerCylinder,
                        AirIntakeMode = defaultItem.AirIntakeMode,
                        DriveType = defaultItem.DriveType,
                        SteeringType = defaultItem.SteeringType,
                        FrontBrakeType = defaultItem.FrontBrakeType,
                        BackBrakeType = defaultItem.BackBrakeType,
                        HalogenLamp = "",
                        XenonLamp = "",
                        LedLamp = "",
                        FuelFilterType = "",
                        FrontTireSize = defaultItem.FrontTireSize,
                        RearTireSize = defaultItem.RearTireSize,
                        OilSupplyMode = defaultItem.OilSupplyMode,
                        AlloyWheel = defaultItem.AlloyWheel,
                        TireMonitorSystem = "",
                        AutoStartStopSys = "",
                        FrontSuspensionType = defaultItem.FrontSuspensionType,
                        RearSuspensionType = defaultItem.RearSuspensionType,
                        HighBeamType = "",
                        DippedHeadlightType = "",
                        Transmission = "",
                        CreateBy = "System",
                        CreateTime = now,
                        UpdateBy = "System",
                        UpdateTime = now
                    };

                    configList.Add(itemConfig);
                    itemTid.Value.ForEach(_ =>
                    {
                        VehicleTypeTimingIdMapDo mapItem = new VehicleTypeTimingIdMapDo
                        {
                            Tid = tid,
                            ExternalId = _.LevelId,
                            Source = "LiYangLevelId",
                            CreateTime = now,
                            UpdateTime = now
                        };
                        mapList.Add(mapItem);
                    });
                    minTid++;
                }
                else
                {
                    itemTid.Value.ForEach(_ =>
                    {
                        VehicleTypeTimingIdMapDo mapItem = new VehicleTypeTimingIdMapDo
                        {
                            Tid = existTid.Tid,
                            ExternalId = _.LevelId,
                            Source = "LiYangLevelId",
                            CreateTime = now,
                            UpdateTime = now
                        };
                        mapList.Add(mapItem);
                    });
                }
            }

            using (TransactionScope ts = new TransactionScope())
            {
                await _vehicleTypeTimingConfigRepository.InsertBatchAsync(configList);
                await _vehicleTypeTimingRepository.InsertBatchAsync(timingList);
                await _vehicleTypeTimingIdMapRepository.InsertBatchAsync(mapList);
                ts.Complete();
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 保养适配数据同步
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncBaoYangParts()
        {
            #region 屏蔽

            //var copyDataTask = _vehicleTypeTimingCopyRepository.GetAAllVehicleTypeTiming();
            //var vehicleDataTask = _vehicleTypeTimingRepository.GetAllVehicleTiming();
            //var baoYangPartsTask = _baoYangPartsRepository.GetAllBaoYangParts();
            //var baoYangAccessoryTask = _baoYangPartAccessoryRepository.GetAllBaoYangAccessory();
            //await Task.WhenAll(copyDataTask, vehicleDataTask, baoYangPartsTask, baoYangAccessoryTask);
            //var copyData = copyDataTask.Result ?? new List<VehicleTypeTimingCopyDO>();
            //var vehicleData = vehicleDataTask.Result ?? new List<VehicleTypeTimingDO>();
            //var baoYangParts = baoYangPartsTask.Result ?? new List<BaoYangPartsDO>();
            //var baoYangAccessory = baoYangAccessoryTask.Result ?? new List<BaoYangPartAccessoryDO>();
            //var copyGroup = copyData.GroupBy(_ => new { _.VehicleId, _.PaiLiang, _.Nian })
            //    .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            //var vehicleGroup = vehicleData.GroupBy(_ => new { _.VehicleId, _.PaiLiang, _.Nian })
            //    .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            //foreach (var vehicleItem in vehicleGroup)
            //{
            //    var copyItem = copyGroup.FirstOrDefault(_ =>
            //        _.Key.VehicleId == vehicleItem.Key.VehicleId && _.Key.PaiLiang == vehicleItem.Key.PaiLiang &&
            //        _.Key.Nian == vehicleItem.Key.Nian);
            //    if (copyItem.Value != null && copyItem.Value.Any())
            //    {
            //        var newTid = vehicleItem.Value.Select(_ => _.Tid).ToList();
            //        var oldTid = copyItem.Value.Select(_ => _.Tid).ToList();
            //        var itemParts = baoYangParts.Where(_ => oldTid.Contains(_.Tid))
            //            .Distinct(_ => $"{_.PartName}{_.PartCode}").ToList();
            //        var itemAccessory = baoYangAccessory.Where(_ => _.Tid == oldTid[0]).ToList();
            //        if (itemParts.Any())
            //        {
            //            List<BaoYangPartsDO> insertParts = new List<BaoYangPartsDO>();
            //            foreach (var itemNew in newTid)
            //            {
            //                insertParts.AddRange(itemParts.Select(_ => new BaoYangPartsDO()
            //                {
            //                    Tid = itemNew,
            //                    PartName = _.PartName,
            //                    PartCode = _.PartCode,
            //                    OePartCode = _.OePartCode,
            //                    Source = _.Source,
            //                    Brand = _.Brand,
            //                    Validated = _.Validated,
            //                    ValidatedBy = _.ValidatedBy,
            //                    ValidatedTime = _.ValidatedTime,
            //                    CreateBy = _.CreateBy,
            //                    CreateTime = _.CreateTime,
            //                    UpdateBy = _.UpdateBy,
            //                    UpdateTime = _.UpdateTime
            //                }));
            //            }

            //            await _baoYangPartsRepository.InsertBatchAsync(insertParts);
            //        }

            //        if (itemAccessory.Any())
            //        {
            //            List<BaoYangPartAccessoryDO> insertAccessory = new List<BaoYangPartAccessoryDO>();
            //            foreach (var itemNew in newTid)
            //            {
            //                insertAccessory.AddRange(itemAccessory.Select(_ => new BaoYangPartAccessoryDO()
            //                {
            //                    Tid = itemNew,
            //                    AccessoryName = _.AccessoryName,
            //                    Volume = _.Volume,
            //                    Level = _.Level,
            //                    Source = _.Source,
            //                    Quantity = _.Quantity,
            //                    Size = _.Size,
            //                    Interface = _.Interface,
            //                    Description = _.Description,
            //                    Grade = _.Grade,
            //                    Viscosity = _.Viscosity,
            //                    CreateBy = _.CreateBy,
            //                    CreateTime = _.CreateTime,
            //                    UpdateBy = _.UpdateBy,
            //                    UpdateTime = _.UpdateTime
            //                }));
            //            }

            //            await _baoYangPartAccessoryRepository.InsertBatchAsync(insertAccessory);
            //        }
            //    }
            //}

            #endregion

            #region 力洋四滤适配数据

            // var liYangPartsTask = _baoYangPartsLiyangRepository.GetAllBaoYangParts();
            // var liYangMapTask = _vehicleTypeTimingIdMapRepository.GetListAsync<VehicleTypeTimingIdMapDo>("");
            // var partsTask = _baoYangPartsRepository.GetAllBaoYangParts();
            // await Task.WhenAll(liYangPartsTask, liYangMapTask, partsTask);
            // var liYangParts = liYangPartsTask.Result ?? new List<BaoYangPartsLiyangDO>();
            // var liYangMap = liYangMapTask.Result?.ToList() ?? new List<VehicleTypeTimingIdMapDo>();
            // var parts = partsTask.Result?.ToList() ?? new List<BaoYangPartsDO>();
            // var tidGroup = liYangMap.GroupBy(_ => _.Tid).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            // List<BaoYangPartsDO> insertParts = new List<BaoYangPartsDO>();
            // foreach (var itemTid in tidGroup)
            // {
            //     var levelId = itemTid.Value.Select(_ => _.ExternalId).ToList();
            //     var liYangData = liYangParts.Where(_ => levelId.Contains(_.LevelId))
            //         .Distinct(_ => $"{_.Brand}_{_.PartName}_{_.PartCode}").ToList();
            //     insertParts.AddRange(liYangData
            //         .Where(_ => !parts.Exists(t =>
            //             t.Tid == itemTid.Key && t.PartName == _.PartName && t.PartCode == _.PartCode)).Select(_ =>
            //             new BaoYangPartsDO()
            //             {
            //                 Tid = itemTid.Key,
            //                 PartName = _.PartName,
            //                 PartCode = _.PartCode,
            //                 OePartCode = "",
            //                 Source = "ApolloErp",
            //                 Brand = "AUK/优冠",
            //                 Validated = true,
            //                 ValidatedBy = "",
            //                 ValidatedTime = DateTime.Now,
            //                 CreateBy = "System",
            //                 CreateTime = DateTime.Now,
            //                 UpdateBy = "System",
            //                 UpdateTime = DateTime.Now
            //             }));
            // }
            //
            // if (insertParts.Any())
            // {
            //     await _baoYangPartsRepository.InsertBatchAsync(insertParts);
            // }

            #endregion

            #region 力洋机油适配数据

            var liYangPartAccessoryTask = _baoYangPartAccessoryLiyangRepository.GetAllPartAccessory();
            var liYangMapTask = _vehicleTypeTimingIdMapRepository.GetListAsync<VehicleTypeTimingIdMapDo>("");
            var accessoryTask = _baoYangPartAccessoryRepository.GetAllBaoYangAccessory();
            await Task.WhenAll(liYangPartAccessoryTask, liYangMapTask, accessoryTask);
            var liYangPartAccessory = liYangPartAccessoryTask.Result ?? new List<BaoYangPartAccessoryLiyangDO>();
            var liYangMap = liYangMapTask.Result?.ToList() ?? new List<VehicleTypeTimingIdMapDo>();
            var accessory = accessoryTask.Result ?? new List<BaoYangPartAccessoryDO>();
            var tidGroup = liYangMap.GroupBy(_ => _.Tid).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
            List<BaoYangPartAccessoryDO> insertAccessory = new List<BaoYangPartAccessoryDO>();
            foreach (var itemTid in tidGroup)
            {
                if (!accessory.Exists(_ => _.Tid == itemTid.Key))
                {
                    var levelId = itemTid.Value.Select(_ => _.ExternalId).ToList();
                    var liYangData = liYangPartAccessory.FirstOrDefault(_ => levelId.Contains(_.LevelId));
                    if (liYangData != null)
                    {
                        var volume = liYangData.Volume;
                        if (volume.Contains("-"))
                        {
                            volume = volume.Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries)[0];
                        }
                        else if (volume.Contains("("))
                        {
                            volume = volume.Split(new[] {'('}, StringSplitOptions.RemoveEmptyEntries)[0];
                        }

                        insertAccessory.Add(new BaoYangPartAccessoryDO()
                        {
                            Tid = itemTid.Key,
                            AccessoryName = "发动机油",
                            Volume = volume,
                            Level = "HX3",
                            Source = "ApolloErp",
                            Quantity = 1,
                            Size = "",
                            Interface = "",
                            Description =
                                liYangData.Level.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                                    .FirstOrDefault() ?? "",
                            Grade = liYangData.Grade,
                            Viscosity = liYangData.Viscosity.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                                .FirstOrDefault() ?? "",
                            CreateBy = "System",
                            CreateTime = DateTime.Now,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        });
                    }
                }
            }

            if (insertAccessory.Any())
            {
                await _baoYangPartAccessoryRepository.InsertBatchAsync(insertAccessory);
            }

            #endregion

            return await Task.FromResult(true);
        }
    }
}
