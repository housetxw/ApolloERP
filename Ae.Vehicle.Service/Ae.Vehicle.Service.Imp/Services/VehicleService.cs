using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Vehicle.Service.Common.Exceptions;
using Ae.Vehicle.Service.Common.Helper;
using Ae.Vehicle.Service.Dal.Model;
using Ae.Vehicle.Service.Dal.Repository;
using Ae.Vehicle.Service.Core.Interfaces;
using Ae.Vehicle.Service.Core.Model;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Core.Response;
using Microsoft.Extensions.Configuration;
using LiYangService;
using Newtonsoft.Json;
using static LiYangService.RequestSoapClient;
using ApolloErp.Redis;
using ApolloErp.Log;

namespace Ae.Vehicle.Service.Imp.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IVehicleBrandRepository _vehicleBrandRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IVehicleTypeTimingRepository _vehicleTypeTimingRepository;
        private readonly IVehicleTypeTimingConfigRepository _vehicleTypeTimingConfigRepository;
        private readonly IVehicleTypeTimingTiresMatchRepository _vehicleTypeTimingTiresMatchRepository;
        private readonly IUserCarRepository _userCarRepository;
        private readonly IUserCarPropertyRepository _userCarPropertyRepository;
        private readonly IUserCarComponentsRepository _userCarComponentsRepository;
        private readonly IUserCarComponentsResultRepository _userCarComponentsResultRepository;
        private readonly string _vinAppKey = "dafbe5aae9045d4e";
        private readonly string _vinAppSecret = "9e30dea9fe5749279bf3653f89b84d6c";
        private readonly string _vinMethod = "level.vehicle.vin.mix";
        private readonly string redisKey = "Rg:Vehicle:Service:Vehicle";
        private readonly RedisClient _redisClient;
        private readonly ApolloErpLogger<VehicleService> _logger;
        private readonly IVehicleTypeTimingIdMapRepository _vehicleTypeTimingIdMapRepository;

        public VehicleService(IMapper mapper, IConfiguration configuration,
            IVehicleBrandRepository vehicleBrandRepository,
            IVehicleTypeRepository vehicleTypeRepository, IVehicleTypeTimingRepository vehicleTypeTimingRepository,
            IVehicleTypeTimingConfigRepository vehicleTypeTimingConfigRepository,
            IVehicleTypeTimingTiresMatchRepository vehicleTypeTimingTiresMatchRepository,
            IUserCarRepository userCarRepository, IUserCarPropertyRepository userCarPropertyRepository,
            IUserCarComponentsRepository userCarComponentsRepository,
            IUserCarComponentsResultRepository userCarComponentsResultRepository, RedisClient redisClient,
            ApolloErpLogger<VehicleService> logger, IVehicleTypeTimingIdMapRepository vehicleTypeTimingIdMapRepository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _vehicleBrandRepository = vehicleBrandRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _vehicleTypeTimingRepository = vehicleTypeTimingRepository;
            _vehicleTypeTimingConfigRepository = vehicleTypeTimingConfigRepository;
            _vehicleTypeTimingTiresMatchRepository = vehicleTypeTimingTiresMatchRepository;
            _userCarRepository = userCarRepository;
            _userCarPropertyRepository = userCarPropertyRepository;
            _userCarComponentsRepository = userCarComponentsRepository;
            _userCarComponentsResultRepository = userCarComponentsResultRepository;
            _redisClient = redisClient;
            _logger = logger;
            _vehicleTypeTimingIdMapRepository = vehicleTypeTimingIdMapRepository;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetVehicleBrandResponse>> GetVehicleBrandsAsync()
        {
            List<GetVehicleBrandResponse> brandData = new List<GetVehicleBrandResponse>();
            var result = await _vehicleBrandRepository.GetAllVehicleBrandsAsync();
            if (result != null && result.Any())
            {
                brandData = _mapper.Map<List<GetVehicleBrandResponse>>(result).OrderBy(_ => _.BrandPrefix).ThenBy(_ => _.Rank).ToList();
                brandData.ForEach(_ =>
                {
                    _.BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(_.Brand)}";
                });
            }

            return brandData;
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public async Task<List<GetVehicleListResponse>> GetVehicleListByBrandAsync(string brand)
        {
            List<GetVehicleListResponse> vehicleData = new List<GetVehicleListResponse>();
            var result = (await _vehicleTypeRepository.GetVehicleListByBrand(brand))?.ToList() ??
                         new List<VehicleTypeDO>();
            if (result.Any())
            {
                result = result.OrderByDescending(_ => _.FactoryRank).ThenBy(_ => _.VehicleRank).ToList();
                foreach (var vehicleItem in result)
                {
                    string[] vehicle = vehicleItem.Vehicle.Split(new[] { '-' });
                    string factory = String.Empty;
                    string vehicleSeries = String.Empty;
                    if (vehicle.Length > 1)
                    {
                        factory = vehicle[1];
                        vehicleSeries = vehicle[0];
                    }

                    GetVehicleListResponse vehicleModel = new GetVehicleListResponse
                    {
                        Vehicle = vehicleItem.Vehicle,
                        VehicleId = vehicleItem.VehicleId,
                        Factory = factory,
                        VehicleSeries = vehicleSeries,
                        TireSize = vehicleItem.Tires?.Split(new[] { ';' })?.ToList()
                    };
                    vehicleData.Add(vehicleModel);
                }
            }

            return vehicleData;
        }

        /// <summary>
        /// 获取排量
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPaiLiangByVehicleIdAsync(string vehicleId)
        {
            List<string> paiList = new List<string>();
            var result = (await _vehicleTypeTimingRepository.GetPaiLiangByVehicleIdAsync(vehicleId))?.ToList();
            if (result != null && result.Any())
            {
                paiList = result.Select(_ => _.PaiLiang).OrderBy(_ => _).ToList();
            }

            return paiList;
        }

        /// <summary>
        /// 根据排量查询生产年份
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <returns></returns>
        public async Task<List<string>> GetVehicleNianByPaiLiangAsync(string vehicleId, string paiLiang)
        {
            List<string> nianList = new List<string>();
            var result =
                (await _vehicleTypeTimingRepository.GetVehicleNianByPaiLiangAsync(vehicleId, paiLiang))?.ToList();
            if (result != null && result.Any())
            {
                foreach (var vehicleItem in result)
                {
                    string startYear = vehicleItem.ListedYear;
                    if (string.IsNullOrEmpty(vehicleItem.ListedYear))
                    {
                        startYear = vehicleItem.Nian;
                    }

                    string endYear = vehicleItem.StopProductionYear;
                    if (string.IsNullOrEmpty(endYear))
                    {
                        endYear = DateTime.Now.Year.ToString();
                    }

                    var startYearInt = ConvertObjectToInt32(startYear);
                    var endYearInt = ConvertObjectToInt32(endYear);
                    if (startYearInt != 0 && endYearInt != 0)
                    {
                        for (var i = startYearInt; i <= endYearInt; i++)
                        {
                            if (!nianList.Contains(i.ToString()))
                            {
                                nianList.Add(i.ToString());
                            }
                        }
                    }
                }
            }

            return nianList.OrderBy(_ => _).ToList();
        }

        /// <summary>
        /// 转换数据为int型
        /// TODO: move it to common method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        private static int ConvertObjectToInt32<T>(T source)
        {
            int result;

            if (source == null)
            {
                return 0;
            }
            else if (int.TryParse(String.Format("{0:f0}", source), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="paiLiang"></param>
        /// <param name="nian"></param>
        /// <returns></returns>
        public async Task<List<GetVehicleSalesNameByNianResponse>> GetVehicleSalesNameByNianAsync(string vehicleId,
            string paiLiang, string nian)
        {
            var result =
                await _vehicleTypeTimingRepository.GetVehicleSalesNameByNianAsync(vehicleId, paiLiang,
                    ConvertObjectToInt32(nian));
            var salesList = result?.OrderBy(_ => _.Nian)?.Select(_ => new GetVehicleSalesNameByNianResponse
            {
                Tid = _.Tid,
                SalesName = _.Nian + "款 " + _.SalesName
            })?.ToList() ?? new List<GetVehicleSalesNameByNianResponse>();

            return salesList;
        }

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSearchByNameResponse>> VehicleSearchByNameAsync(
            VehicleSearchByNameRequest request)
        {
            List<VehicleSearchByNameResponse> vehicleData = new List<VehicleSearchByNameResponse>();
            var result = (await _vehicleTypeRepository.VehicleSearchByNameAsync(request.Name))?.ToList() ??
                         new List<VehicleTypeDO>();
            if (result.Any())
            {
                result = result.OrderBy(_ => _.Brand).ThenByDescending(_ => _.FactoryRank)
                    .ThenByDescending(_ => _.VehicleRank).ToList();
                foreach (var vehicleItem in result)
                {
                    string[] vehicle = vehicleItem.Vehicle.Split(new[] { '-' });
                    string factory = String.Empty;
                    string vehicleSeries = String.Empty;
                    if (vehicle.Length > 1)
                    {
                        factory = vehicle[1];
                        vehicleSeries = vehicle[0];
                    }

                    VehicleSearchByNameResponse vehicleModel = new VehicleSearchByNameResponse
                    {
                        Brand = vehicleItem.Brand,
                        BrandUrl =
                            $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(vehicleItem.Brand)}",
                        Vehicle = vehicleItem.Vehicle,
                        VehicleId = vehicleItem.VehicleId,
                        Factory = factory,
                        VehicleSeries = vehicleSeries,
                        TireSize = vehicleItem.Tires?.Split(new[] { ';' })?.ToList()
                    };
                    vehicleData.Add(vehicleModel);
                }
            }

            return vehicleData;
        }

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleInfo>> GetVehicleInfoListAsync(VehicleInfoListRequest request)
        {
            List<VehicleInfo> result = new List<VehicleInfo>();
            var vehicleDetail =
                (await _vehicleTypeTimingRepository.GetVehicleListAsync(request.VehicleId, request.PaiLiang,
                    request.Nian, request.Tid))?.ToList() ?? new List<VehicleTypeTimingDO>();
            if (vehicleDetail.Any())
            {
                string vehicleId = request.VehicleId;
                if (string.IsNullOrEmpty(vehicleId))
                {
                    vehicleId = vehicleDetail[0].VehicleId;
                }
                var vehicleDataTask = _vehicleTypeRepository.GetVehicleByVehicleIdAsync(vehicleId);
                List<string> tids = vehicleDetail.Select(_ => _.Tid).ToList();
                var vehicleTimingConfigTask =
                    _vehicleTypeTimingConfigRepository.GetVehicleTypeTimingConfigByTidsAsync(tids);//查车型配置信息
                var tireMatchTask = _vehicleTypeTimingTiresMatchRepository.GetVehicleTiresMatchListAsync(tids);//查原配轮胎
                result = _mapper.Map<List<VehicleInfo>>(vehicleDetail);
                await Task.WhenAll(vehicleDataTask, vehicleTimingConfigTask, tireMatchTask);
                var vehicleData = vehicleDataTask.Result;
                var vehicleTimingConfig =
                    vehicleTimingConfigTask.Result?.ToList() ?? new List<VehicleTypeTimingConfigDO>();
                var tireMatch = tireMatchTask.Result?.ToList() ?? new List<VehicleTypeTimingTiresMatchDO>();
                if (vehicleData != null)
                {
                    result.ForEach(_ =>
                    {
                        var itemVehicleConfig = vehicleTimingConfig.FirstOrDefault(t => t.Tid == _.Tid);
                        var tireMatchItem = tireMatch.Where(t => t.Tid == _.Tid).ToList();
                        _.BrandFromVehicleType = vehicleData.Brand;
                        _.VehicleFromVehicleType = vehicleData.Vehicle;
                        _.CarBody = itemVehicleConfig?.CarBody;
                        _.EngineNo = itemVehicleConfig?.EngineNo;
                        _.ChassisNo = itemVehicleConfig?.ChassisNo;
                        _.TransmissionTypeE = itemVehicleConfig?.TransmissionTypeE;
                        _.TransmissionTypeC = itemVehicleConfig?.TransmissionTypeC;
                        _.PowerKw = itemVehicleConfig?.PowerKw ?? 0;
                        _.ValveNumPerCylinder = itemVehicleConfig?.ValveNumPerCylinder ?? 0;
                        _.AirIntakeMode = itemVehicleConfig?.AirIntakeMode;
                        _.DriveType = itemVehicleConfig?.DriveType;
                        _.SteeringType = itemVehicleConfig?.SteeringType;
                        _.FrontBrakeType = itemVehicleConfig?.FrontBrakeType;
                        _.BackBrakeType = itemVehicleConfig?.BackBrakeType;
                        _.HalogenLamp = itemVehicleConfig?.HalogenLamp;
                        _.XenonLamp = itemVehicleConfig?.XenonLamp;
                        _.LedLamp = itemVehicleConfig?.LedLamp;
                        _.FuelFilterType = itemVehicleConfig?.FuelFilterType;
                        _.FrontTireSize = itemVehicleConfig?.FrontTireSize;
                        _.RearTireSize = itemVehicleConfig?.RearTireSize;
                        _.OilSupplyMode = itemVehicleConfig?.OilSupplyMode;
                        _.AlloyWheel = itemVehicleConfig?.AlloyWheel;
                        _.TireMonitorSystem = itemVehicleConfig?.TireMonitorSystem;
                        _.AutoStartStopSys = itemVehicleConfig?.AutoStartStopSys;
                        _.FrontSuspensionType = itemVehicleConfig?.FrontSuspensionType;
                        _.RearSuspensionType = itemVehicleConfig?.RearSuspensionType;
                        _.HighBeamType = itemVehicleConfig?.HighBeamType;
                        _.DippedHeadlightType = itemVehicleConfig?.DippedHeadlightType;
                        _.Transmission = itemVehicleConfig?.Transmission;
                        _.Pid = tireMatchItem.Where(r => !string.IsNullOrEmpty(r.Pid)).Select(r => r.Pid).ToList();
                        _.NoProductName = tireMatchItem.Where(r => !string.IsNullOrEmpty(r.NoProductName))
                            .Select(t => t.NoProductName).ToList();
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTidsByVehicle(TidsByVehicleRequest request)
        {
            List<VehicleTypeTimingDO> vehicleDetail = new List<VehicleTypeTimingDO>();
            if (!string.IsNullOrEmpty(request.Tid))
            {
                vehicleDetail = (await _vehicleTypeTimingRepository.GetVehicleListAsync("", "", "", request.Tid))?.ToList() ?? new List<VehicleTypeTimingDO>();
            }
            else if (!string.IsNullOrEmpty(request.VehicleId))
            {
                vehicleDetail = (await _vehicleTypeTimingRepository.GetVehicleListAsync(request.VehicleId, request.PaiLiang, request.Nian, ""))?.ToList() ?? new List<VehicleTypeTimingDO>();
            }
            else if (!string.IsNullOrEmpty(request.Brand))
            {
                var vehicleData = (await _vehicleTypeRepository.GetVehicleListByBrand(request.Brand))?.ToList() ?? new List<VehicleTypeDO>();
                if (vehicleData.Any())
                {
                    var vehicleIdList = vehicleData.Select(_ => _.VehicleId).ToList();
                    var vehicleTask = await Task.WhenAll(vehicleIdList.Select(_ => _vehicleTypeTimingRepository.GetVehicleListAsync(_, "", "", "")));
                    foreach (var itemTask in vehicleTask)
                    {
                        vehicleDetail.AddRange(itemTask?.ToList() ?? new List<VehicleTypeTimingDO>());
                    }
                }
            }
            return vehicleDetail.Select(_ => _.Tid).ToList();
        }

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListAsync(VehicleInfoListRequest request)
        {
            List<VehicleBaseInfo> result = new List<VehicleBaseInfo>();
            var vehicleDetail =
                (await _vehicleTypeTimingRepository.GetVehicleListAsync(request.VehicleId, request.PaiLiang,
                    request.Nian, request.Tid))?.ToList() ?? new List<VehicleTypeTimingDO>();
            if (vehicleDetail.Any())
            {
                result = _mapper.Map<List<VehicleBaseInfo>>(vehicleDetail);
                string vehicleId = request.VehicleId;
                if (string.IsNullOrEmpty(vehicleId))
                {
                    vehicleId = vehicleDetail[0].VehicleId;
                }

                var vehicleData = await _vehicleTypeRepository.GetVehicleByVehicleIdAsync(vehicleId);
                if (vehicleData != null)
                {
                    result.ForEach(_ =>
                    {
                        _.Brand = vehicleData.Brand;
                        _.Vehicle = vehicleData.Vehicle;
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 根据Tids查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListByTids(
            VehicleInfoListByTidsRequest request)
        {
            List<VehicleBaseInfo> result = new List<VehicleBaseInfo>();
            var vehicleDetail = (await _vehicleTypeTimingRepository.GetVehicleListAsync(request.Tids))?.ToList() ??
                                new List<VehicleTypeTimingDO>();
            if (vehicleDetail.Any())
            {
                result = _mapper.Map<List<VehicleBaseInfo>>(vehicleDetail);
                List<string> vehicleId = vehicleDetail.Select(_ => _.VehicleId).Distinct().ToList();
                var vehicleData = (await _vehicleTypeRepository.GetVehicleByVehicleIdAsync(vehicleId))?.ToList() ??
                                  new List<VehicleTypeDO>();
                result.ForEach(_ =>
                {
                    var itemVehicle = vehicleData.FirstOrDefault(x => x.VehicleId == _.VehicleId);
                    if (itemVehicle != null)
                    {
                        _.Brand = itemVehicle.Brand;
                        _.Vehicle = itemVehicle.Vehicle;
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// 五级车型查询车型配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleConfigVO>> GetVehicleConfigByTidList(VehicleConfigRequest request)
        {
            List<VehicleConfigVO> result = new List<VehicleConfigVO>();
            var tidList = request.TidList;
            if (tidList != null && tidList.Any())
            {
                var vehicleTimingConfig =
                    (await _vehicleTypeTimingConfigRepository.GetVehicleTypeTimingConfigByTidsAsync(tidList))
                    ?.ToList() ?? new List<VehicleTypeTimingConfigDO>(); //查车型配置信息
                result = _mapper.Map<List<VehicleConfigVO>>(vehicleTimingConfig);
            }

            return result;
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddUserCarAsync(AddUserCarRequest request)
        {
            //todo 校验用户是否存在
            Guid carId = Guid.NewGuid();
            if (string.IsNullOrEmpty(request.SalesName) && !string.IsNullOrEmpty(request.Tid))
            {
                var tidItem = await _vehicleTypeTimingRepository.GetVehicleDetailByTidAsync(request.Tid);
                if (tidItem != null)
                {
                    request.SalesName = tidItem.Nian + "款 " + tidItem.SalesName;
                }
            }

            var vehicleResult = await _userCarRepository.AddUserCarAsync(new UserCarDO
            {
                CarId = carId,
                UserId = Guid.Parse(request.UserId),
                NickName = request.NickName,
                CarNumber = request.CarNumber,
                Brand = request.Brand,
                Vehicle = request.Vehicle,
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid,
                SalesName = request.SalesName,
                BuyYear = request.BuyYear,
                BuyMonth = request.BuyMonth,
                InsureExpireDate = request.InsureExpireDate,
                TotalMileage = request.TotalMileage,
                LastBaoYangKm = request.LastBaoYangKm,
                LastBaoYangTime = request.LastBaoYangTime,
                VinCode = request.VinCode,
                DefaultCar = request.DefaultCar,
                EngineNo = request.EngineNo,
                TireSizeForSingle = request.TireSizeForSingle,
                InsuranceCompany = request.InsuranceCompany,
                RegistrationTime = request.RegistrationTime,
                CarNoProvince = request.CarNoProvince,
                CarNoCity = request.CarNoCity,
                DataSource = request.DataSource,
                CreateBy = request.CreateBy
            });
            if (vehicleResult)
            {
                if (request.CarProperties != null && request.CarProperties.Any())
                {
                    await _userCarPropertyRepository.AddUserCarPropertyAsync(request.CarProperties.Select(_ =>
                        new UserCarPropertyDO
                        {
                            CarId = carId,
                            PropertyKey = _.PropertyKey,
                            PropertyValue = _.PropertyValue,
                            CreateBy = request.CreateBy
                        }).ToList()); //五级属性
                }

                if (request.DefaultCar)
                {
                    await _userCarRepository.SetUserDefaultVehicleAsync(request.UserId, carId.ToString(),
                        request.CreateBy);
                }

                return carId.ToString();
            }
            else
            {
                throw new CustomException("新增车型失败");
            }
        }

        /// <summary>
        /// 设置用户的默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SetUserDefaultVehicleAsync(SetUserDefaultVehicleRequest request)
        {
            var carResult = await _userCarRepository.GetUserVehicleByCarIdAsync(request.UserId, request.CarId, false);
            if (carResult != null)
            {
                var result =
                    await _userCarRepository.SetUserDefaultVehicleAsync(request.UserId, request.CarId, request.Submitter);

                return result;
            }
            else
            {
                throw new CustomException("此车型不存在！");
            }
        }

        /// <summary>
        /// 获取用户配置的所有车型信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleVO>> GetAllUserVehiclesAsync(AllUserVehiclesRequest request)
        {
            List<UserVehicleVO> result = new List<UserVehicleVO>();
            var carResult = (await _userCarRepository.GetAllUserVehiclesAsync(request.UserId, false))?.ToList() ??
                            new List<UserCarDO>();
            if (carResult.Any())
            {
                List<string> carIds = carResult.Select(_ => _.CarId.ToString()).ToList();
                var properties =
                    (await _userCarPropertyRepository.GetUserCarPropertiesAsync(carIds, false))?.ToList() ??
                    new List<UserCarPropertyDO>();
                result = _mapper.Map<List<UserVehicleVO>>(carResult);
                result.ForEach(_ =>
                {
                    _.CarProperties = properties.Where(x => x.CarId.ToString() == _.CarId).Select(x => new VehiclePropertyRequest
                    {
                        PropertyKey = x.PropertyKey,
                        PropertyValue = x.PropertyValue
                    }).ToList();
                    _.BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(_.Brand)}";
                });
            }

            return result;
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleVO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request)
        {
            var carResult = await _userCarRepository.GetUserVehicleByCarIdAsync(request.UserId, request.CarId);
            if (carResult != null)
            {
                var properties =
                    (await _userCarPropertyRepository.GetUserCarPropertiesAsync(new List<string> { request.CarId }))
                    ?.ToList() ??
                    new List<UserCarPropertyDO>();
                UserVehicleVO result = _mapper.Map<UserVehicleVO>(carResult);
                if (properties.Any())
                {
                    result.CarProperties = properties.Select(_ => new VehiclePropertyRequest
                    {
                        PropertyKey = _.PropertyKey,
                        PropertyValue = _.PropertyValue
                    }).ToList();
                }

                result.BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(result.Brand)}";

                return result;
            }

            return null;
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleVO> GetUserDefaultVehicleAsync(UserDefaultVehicleRequest request)
        {
            var carResult = await _userCarRepository.GetUserDefaultVehicleAsync(request.UserId);
            if (carResult != null)
            {
                var properties =
                    (await _userCarPropertyRepository.GetUserCarPropertiesAsync(new List<string>
                        {carResult.CarId.ToString()}))
                    ?.ToList() ??
                    new List<UserCarPropertyDO>();
                UserVehicleVO result = _mapper.Map<UserVehicleVO>(carResult);
                if (properties.Any())
                {
                    result.CarProperties = properties.Select(_ => new VehiclePropertyRequest
                    {
                        PropertyKey = _.PropertyKey,
                        PropertyValue = _.PropertyValue
                    }).ToList();
                }

                result.BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(result.Brand)}";

                return result;
            }

            return null;
        }

        /// <summary>
        /// 批量获取用户默认车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleVO>> GetUserDefaultVehicleBatch(UserDefaultVehicleBatchRequest request)
        {
            List<UserVehicleVO> data = new List<UserVehicleVO>();
            var result = await _userCarRepository.GetUserDefaultVehicleBatch(request.UserIdList);
            if (result != null && result.Any())
            {
                data = _mapper.Map<List<UserVehicleVO>>(result);
            }

            return data.Distinct(_ => _.UserId).ToList();
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserCarAsync(DeleteUserCarRequest request)
        {
            var result = await _userCarRepository.DeleteUserCarAsync(request.UserId, request.CarId, request.Submitter);
            if (result)
            {
                var carResult = (await _userCarRepository.GetAllUserVehiclesAsync(request.UserId, false))?.ToList() ??
                                new List<UserCarDO>();
                if (carResult.Any() && !carResult.Any(_ => _.DefaultCar))
                {
                    await _userCarRepository.SetUserDefaultVehicleAsync(request.UserId, carResult[0].CarId.ToString(),
                        request.Submitter);
                }
            }

            return result;
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserCarAsync(EditUserCarRequest request)
        {
            var carResult = await _userCarRepository.GetUserVehicleByCarIdAsync(request.UserId, request.CarId, false);
            if (carResult != null)
            {
                var propertyResult = false;
                var userCarResult = await _userCarRepository.EditUserCarAsync(request);
                if (userCarResult)
                {
                    if (request.DefaultCar == true)
                    {
                        await _userCarRepository.SetUserDefaultVehicleAsync(request.UserId, request.CarId,
                            request.Submitter);
                    }
                }

                if (request.CarProperties != null)
                {
                    propertyResult = await _userCarPropertyRepository.EditCarPropertiesAsync(request.CarProperties
                        .Select(_ => new UserCarPropertyDO
                        {
                            PropertyKey = _.PropertyKey,
                            PropertyValue = _.PropertyValue
                        }).ToList(), request.CarId, request.Submitter);
                }

                return propertyResult || userCarResult;
            }
            else
            {
                throw new CustomException("此车型不存在！");
            }
        }

        /// <summary>
        /// 根据车系Id查详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VehicleTypeVo> GetVehicleTypeById(VehicleTypeByIdRequest request)
        {
            var result = await _vehicleTypeRepository.GetVehicleByVehicleIdAsync(request.VehicleId);
            if (result != null)
            {
                return _mapper.Map<VehicleTypeVo>(result);
            }

            return null;
        }

        /// <summary>
        /// 五级车型查询原配轮胎尺寸
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OeTireSizeResponse> GetOeTireSizeByTid(OeTireSizeByTidRequest request)
        {
            var result =
                (await _vehicleTypeTimingConfigRepository.GetVehicleTypeTimingConfigByTidsAsync(new List<string>()
                    {request.Tid}))?.ToList();
            if (result != null && result.Any())
            {
                var currentData = result[0];
                return new OeTireSizeResponse()
                {
                    FrontTireSize = currentData.FrontTireSize,
                    RearTireSize = currentData.RearTireSize
                };
            }

            return null;
        }

        /// <summary>
        /// 原配轮胎查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OeTirePidByTidResponse> GetOeTirePidByTid(OeTirePidByTidRequest request)
        {
            OeTirePidByTidResponse result = new OeTirePidByTidResponse();
            var tireMatch = (await _vehicleTypeTimingTiresMatchRepository.GetVehicleTiresMatchListAsync(
                new List<string>()
                {
                    request.Tid
                }))?.ToList(); //查原配轮胎
            if (tireMatch != null && tireMatch.Any())
            {
                result.PidList = tireMatch.Where(_ => !string.IsNullOrEmpty(_.Pid)).Select(_ => _.Pid).ToList();
            }

            return result;
        }

        /// <summary>
        /// 用户车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserCarFileVo> GetUserCarFile(UserCarFileRequest request)
        {
            var carResultTask = _userCarRepository.GetUserVehicleByCarIdAsync(request.UserId, request.CarId);
            var partConfigTask = _userCarComponentsRepository.GetCarComponents();
            var userCarPartTask = _userCarComponentsResultRepository.GetUserCarComponentsResult(request.CarId, 1);
            await Task.WhenAll(carResultTask, partConfigTask, userCarPartTask);
            var carResult = carResultTask.Result;
            var partConfig = partConfigTask.Result ?? new List<UserCarComponentsDo>();
            var userCarPart = userCarPartTask.Result ?? new List<UserCarComponentsResultDo>();
            if (carResult != null)
            {
                UserCarFileVo result = _mapper.Map<UserCarFileVo>(carResult);

                result.BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(result.Brand)}";

                var carPartCategory = partConfig.GroupBy(_ => _.TypeDes).Select(_ => new CarPartsSituation
                {
                    Title = _.Key,
                    Items = _.Select(t => new CarPartsItem
                    {
                        PartsId = t.Id,
                        KeyName = t.KeyName,
                        DisplayName = t.DisplayName,
                        Status = userCarPart.Any(f => f.PartId == t.Id) ? 1 : 0
                    }).ToList()
                }).ToList();

                result.CarPartsSituation = carPartCategory;

                return result;
            }

            return null;
        }

        /// <summary>
        /// 更新车辆部位检查信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCarPartCheckResult(UpdateCarPartCheckResultRequest request)
        {
            Guid.TryParse(request.CarId, out var carId);
            var errorComponents = request.CarErrorComponents;
            if (errorComponents != null && errorComponents.Any())
            {
                var partConfig = await _userCarComponentsRepository.GetCarComponents();
                List<UserCarComponentsResultDo> componentsResultList = new List<UserCarComponentsResultDo>();
                errorComponents.ForEach(_ =>
                {
                    var itemConfig = partConfig.FirstOrDefault(t => t.KeyName == _.KeyName);
                    if (itemConfig != null)
                    {
                        componentsResultList.Add(new UserCarComponentsResultDo
                        {
                            CarId = carId,
                            PartId = itemConfig.Id,
                            ResultValue = 1,
                            CheckId = request.CheckId,
                            CategoryId = request.CategoryId,
                            PropertyId = string.Join(",", _.PropertyId),
                            CreateBy = request.SubmitBy,
                            CreateTime = DateTime.Now
                        });
                    }
                });

                if (componentsResultList.Any())
                {
                    await _userCarComponentsResultRepository.InsertBatchAsync(componentsResultList);
                }
            }

            return true;
        }

        /// <summary>
        /// 车辆部位异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserCarComponentsResultVo>> GetUserCarComponentsErrorCheck(UserCarComponentsErrorCheckRequest request)
        {
            List<UserCarComponentsResultVo> result = new List<UserCarComponentsResultVo>();
            var partConfigTask = _userCarComponentsRepository.GetCarComponents();
            var userCarPartTask = _userCarComponentsResultRepository.GetUserCarComponentsResult(request.CarId, 1);
            await Task.WhenAll(partConfigTask, userCarPartTask);
            var partConfig = partConfigTask.Result ?? new List<UserCarComponentsDo>();
            var userCarPart = userCarPartTask.Result ?? new List<UserCarComponentsResultDo>();
            var defaultConfig = partConfig.FirstOrDefault(_ => _.KeyName == request.KeyName);
            if (defaultConfig != null)
            {
                result = userCarPart.Where(_ => _.PartId == defaultConfig.Id).Select(_ => new UserCarComponentsResultVo
                {
                    CheckId = _.CheckId,
                    CategoryId = _.CategoryId,
                    PropertyIds = _.PropertyId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt64(t)).ToList()
                }).ToList();
            }
            return result;
        }

        /// <summary>
        /// 根据车牌查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<string>, int>> GetUserIdByCarPlate(UserIdByCarPlateRequest request)
        {
            var carPlate = request.CarPlate;

            var userCars = (await _userCarRepository.GetUserCarsByCarPlate(carPlate)) ?? new List<UserCarDO>();

            var userList = userCars.OrderByDescending(_ => _.CreateTime).Select(x => x.UserId.ToString()).Distinct().ToList();

            return new Tuple<List<string>, int>(userList.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList(), userList.Count);
        }

        /// <summary>
        /// Vin码识别车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSimpleVo>> GetVehiclesByVin(VehiclesByVinRequest request)
        {
            if (request.VinCode.Length != 17)
            {
                throw new CustomException("Vin码格式有误！");
            }

            List<VehicleSimpleVo> result = new List<VehicleSimpleVo>();
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":LevelData:{request.VinCode}:{timestamp}";
            var levelData = await RedisHelper.GetOrSetAsync(_redisClient, key, () => LevelData(request.VinCode),
                new TimeSpan(1, 0, 0, 0));
            var levelIds = levelData?.Result?.Select(_ => _.LevelId)?.ToList() ?? new List<string>();
            var vinYear = levelData?.Additional?.VinYear;
            if (levelIds.Any())
            {
                var tidMap = await _vehicleTypeTimingIdMapRepository.GetVehicleTimingIdMapByLevelId(levelIds);
                var tidList = tidMap.Select(_ => _.Tid).Distinct().ToList();
                if (tidList.Any())
                {

                    var tidData = await _vehicleTypeTimingRepository.GetVehiclesByTidList(tidList);
                    result = tidData.Select(_ => new VehicleSimpleVo
                    {
                        Brand = _.Brand,
                        BrandUrl = $"{_configuration["ImageDomain"]}{ImageHelper.GetLogoUrlByName(_.Brand)}",
                        Vehicle = _.Vehicle,
                        VehicleId = _.VehicleId,
                        PaiLiang = _.PaiLiang,
                        Nian = _.Nian,
                        ManufactureYear = GetManufactureYearByVin(request.VinCode, vinYear, _.ListedYear),
                        SalesName = _.SalesName,
                        Tid = _.Tid
                    }).ToList();
                }
            }

            return result;
        }

        /// <summary>
        /// 调用力洋数据
        /// </summary>
        /// <param name="vinCode"></param>
        /// <returns></returns>
        private async Task<LevelDataVo> LevelData(string vinCode)
        {
            string input =
                $"<root><appkey>{_vinAppKey}</appkey><appsecret>{_vinAppSecret}</appsecret><method>{_vinMethod}</method><requestformat>json</requestformat><vin>{vinCode}</vin></root>";

            _logger.Info($"LevelData_Para = {input}");

            var client = new RequestSoapClient(EndpointConfiguration.RequestSoap);

            var result = await client.LevelDataAsync(input); //返回结果

            _logger.Info($"LevelData_Result={result}，Para={input}");

            return JsonConvert.DeserializeObject<LevelDataVo>(result);
        }

        private string GetManufactureYearByVin(string vinCode, string vinYear, string listedYear)
        {
            if (!string.IsNullOrEmpty(vinYear))
            {
                return vinYear;
            }

            var d = 2000 + SwitchVin(vinCode.Substring(9, 1));

            var startYear = Convert.ToInt32(listedYear);
            for (int i = startYear; i < startYear + 30; i++)
            {
                var t = i - d;
                if (t % 30 == 0)
                {
                    return i.ToString();
                }
            }

            return listedYear;
        }

        private int SwitchVin(string code)
        {
            switch (code)
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                case "A":
                    return 10;
                case "B":
                    return 11;
                case "C":
                    return 12;
                case "D":
                    return 13;
                case "E":
                    return 14;
                case "F":
                    return 15;
                case "G":
                    return 16;
                case "H":
                    return 17;
                case "J":
                    return 18;
                case "K":
                    return 19;
                case "L":
                    return 20;
                case "M":
                    return 21;
                case "N":
                    return 22;
                case "P":
                    return 23;
                case "R":
                    return 24;
                case "S":
                    return 25;
                case "T":
                    return 26;
                case "V":
                    return 27;
                case "W":
                    return 28;
                case "X":
                    return 29;
                case "Y":
                    return 30;
                default: throw new CustomException("Vin码格式有误");
            }
        }
    }
}
