using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Clients.Vehicle;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Vehicle;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Vehicle;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Vehicle;
using Ae.C.MiniApp.Api.Core.Response;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class VehicleService:IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<VehicleService> _logger;
        private readonly IVehicleClient _vehicleClient;
        private readonly string _source = "MiniApp";
        private readonly IIdentityService _identityService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="vehicleClient"></param>
        /// <param name="identityService"></param>
        public VehicleService(IMapper mapper, ApolloErpLogger<VehicleService> logger, IVehicleClient vehicleClient,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _logger = logger;
            _vehicleClient = vehicleClient;
            _identityService = identityService;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleBrandResponse>> GetVehicleBrandListAsync()
        {
            List<VehicleBrandResponse> response = new List<VehicleBrandResponse>();
            var result = await _vehicleClient.GetVehicleBrandListAsync();
            if (result != null && result.Any())
            {
                var hotBrand = result.Where(t => t.HotLevel > 0).ToList();
                if (hotBrand.Any())
                {
                    response.Add(new VehicleBrandResponse
                    {
                        BrandPrefix = "#",
                        DisplayName = "热门品牌",
                        BrandList = hotBrand.OrderByDescending(x => x.HotLevel).Select(_ => new VehicleBrandModel
                        {
                            Brand = _.Brand,
                            BrandSuffix = _.BrandSuffix,
                            BrandUrl = _.BrandUrl
                        }).ToList()
                    });
                }

                var brandGroup = result.GroupBy(_ => _.BrandPrefix)
                    .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                foreach (var brandItem in brandGroup.OrderBy(_ => _.Key))
                {
                    VehicleBrandResponse vehicleItem = new VehicleBrandResponse
                    {
                        BrandPrefix = brandItem.Key,
                        DisplayName = brandItem.Key,
                        BrandList = brandItem.Value.Select(_ => new VehicleBrandModel
                        {
                            Brand = _.Brand,
                            BrandSuffix = _.BrandSuffix,
                            BrandUrl = _.BrandUrl
                        }).ToList()
                    };
                    response.Add(vehicleItem);
                }
            }

            return response;
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleListResponse>> GetVehicleListByBrandAsync(VehicleListByBrandRequest request)
        {
            List<VehicleListResponse> response = new List<VehicleListResponse>();
            var result = await _vehicleClient.GetVehicleListByBrandAsync(new VehicleListByBrandClientRequest
            {
                Brand = request.Brand
            });
            if (result.Any())
            {
                var vehicleGroup = result.GroupBy(_ => _.Factory).ToDictionary(o => o.Key, o => o.Select(t => t))
                    .ToList();
                foreach (var vehicleItem in vehicleGroup)
                {
                    VehicleListResponse rsItem = new VehicleListResponse
                    {
                        Factory = vehicleItem.Key,
                        VehicleList = vehicleItem.Value.Select(_ => new VehicleListModel
                        {
                            VehicleId = _.VehicleId,
                            Vehicle = _.Vehicle,
                            VehicleSeries = _.VehicleSeries,
                            TireSize = _.TireSize
                        }).ToList()
                    };
                    response.Add(rsItem);
                }
            }

            return response;
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPaiLiangByVehicleIdAsync(GetPaiLiangByVehicleIdRequest request)
        {
            var result = await _vehicleClient.GetPaiLiangByVehicleIdAsync(new PaiLiangByVehicleIdClientRequest
            {
                VehicleId = request.VehicleId
            });
            return result ?? new List<string>();
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetVehicleNianByPaiLiangAsync(VehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleClient.GetVehicleNianByPaiLiangAsync(new VehicleNianByPaiLiangClientRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang
            });

            return result ?? new List<string>();
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSalesNameByNianResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianRequest request)
        {
            var result = await _vehicleClient.GetVehicleSalesNameByNianAsync(new VehicleSalesNameByNianClientRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian
            });

            return result?.Select(_ => new VehicleSalesNameByNianResponse
            {
                Tid = _.Tid,
                SalesName = _.SalesName
            })?.ToList() ?? new List<VehicleSalesNameByNianResponse>();
        }

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSearchByNameResponse>> VehicleSearchByNameAsync(
            VehicleSearchByNameRequest request)
        {
            var result = await _vehicleClient.VehicleSearchByNameAsync(new VehicleSearchByNameClientRequest
            {
                Name = request.Name
            });

            return result?.Select(_ => new VehicleSearchByNameResponse
            {
                Brand = _.Brand,
                BrandUrl = _.BrandUrl,
                VehicleId = _.VehicleId,
                Vehicle = _.Vehicle,
                Factory = _.Factory,
                VehicleSeries = _.VehicleSeries,
                TireSize = _.TireSize
            })?.ToList() ?? new List<VehicleSearchByNameResponse>();
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddUserCarAsync(AddUserCarRequest request)
        {
            AddUserCarClientRequest clientRequest = _mapper.Map<AddUserCarClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserId();
            clientRequest.DataSource = _source;
            return await _vehicleClient.AddUserCarAsync(clientRequest);
        }

        /// <summary>
        /// 设置用户的默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SetUserDefaultVehicleAsync(SetUserDefaultVehicleRequest request)
        {
            SetUserDefaultVehicleClientRequest clientRequest = new SetUserDefaultVehicleClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId,
                Submitter = _identityService.GetUserId()
            };
            return await _vehicleClient.SetUserDefaultVehicleAsync(clientRequest);
        }

        /// <summary>
        /// 获取用户配置的所有车型信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleVO>> GetAllUserVehiclesAsync(string userId)
        {
            var result = (await _vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest
            {
                UserId = userId
            })) ?? new List<UserVehicleDTO>();
            return _mapper.Map<List<UserVehicleVO>>(result).OrderByDescending(_ => _.DefaultCar).ToList();
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleVO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request)
        {
            var result = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId
            });
            if (result != null)
            {
                return _mapper.Map<UserVehicleVO>(result);
            }

            return null;
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserVehicleVO> GetUserDefaultVehicleAsync(string userId)
        {
            var result = await _vehicleClient.GetUserDefaultVehicleAsync(new UserDefaultVehicleClientRequest
            {
                UserId = userId
            });
            if (result != null)
            {
                return _mapper.Map<UserVehicleVO>(result);
            }

            return null;
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserCarAsync(DeleteUserCarRequest request)
        {
            var result = await _vehicleClient.DeleteUserCarAsync(new DeleteUserCarClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId,
                Submitter = _identityService.GetUserId()
            });

            return result;
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserCarAsync(EditUserCarRequest request)
        {
            EditUserCarClientRequest clientRequest = new EditUserCarClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId,
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
                Submitter = _identityService.GetUserId(),
                CarProperties = request.CarProperties?.Select(_ => new VehiclePropertyDTO
                {
                    PropertyKey = _.PropertyKey,
                    PropertyValue = _.PropertyValue
                })?.ToList()
            };
            if (!string.IsNullOrEmpty(request.InsureExpireDate))
            {
                clientRequest.InsureExpireDate = Convert.ToDateTime(request.InsureExpireDate);
            }
            return await _vehicleClient.EditUserCarAsync(clientRequest);
        }

        /// <summary>
        /// 获取保险公司列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<InsuranceCompanyVo>> GetInsuranceCompanyListAsync(InsuranceCompanyListRequest request)
        {
            var result = await _vehicleClient.GetInsuranceCompanyListAsync(new InsuranceCompanyListClientRequest()
            {
                SearchContent = request.SearchContent
            });

            return result?.Select(_ => new InsuranceCompanyVo
            {
                DisplayName = _.DisplayName,
                ImageUrl = _.ImageUrl
            })?.ToList() ?? new List<InsuranceCompanyVo>();
        }
    }
}
