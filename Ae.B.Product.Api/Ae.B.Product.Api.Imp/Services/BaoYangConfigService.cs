using Ae.B.Product.Api.Client.Clients;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Login.Auth;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.BaoYang;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;
using Ae.B.Product.Api.Client.Model.Vehicle;
using Ae.B.Product.Api.Client.Request.Vehicle;
using Ae.B.Product.Api.Core.Response.BaoYang;
using Ae.B.Product.Api.Core.Response.Vehicle;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Ae.B.Product.Api.Client.Model;

namespace Ae.B.Product.Api.Imp.Services
{
    public class BaoYangConfigService : IBaoYangConfigService
    {
        private readonly IMapper _mapper;
        private readonly IVehicleClient _vehicleClient;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        private readonly IProductClient _productClient;

        public BaoYangConfigService(IMapper mapper, IVehicleClient vehicleClient, IBaoYangClient baoYangClient,
            IIdentityService identityService, IConfiguration configuration, IProductClient productClient)
        {
            _mapper = mapper;
            _vehicleClient = vehicleClient;
            _baoYangClient = baoYangClient;
            _identityService = identityService;
            _configuration = configuration;
            _productClient = productClient;
        }

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetBaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            GetBaoYangPartAdaptationsRequest request)
        {
            List<GetBaoYangPartAdaptationsResponse> result = new List<GetBaoYangPartAdaptationsResponse>();
            var vehicleList = await _vehicleClient.GetVehicleInfoList(new VehicleInfoListRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });
            if (vehicleList != null && vehicleList.Any())
            {
                var tidList = vehicleList.Select(_ => _.Tid).ToList();
                var baoYangConfig = await _baoYangClient.GetBaoYangPartAdaptationsAsync(
                    new BaoYangPartAdaptationsRequest
                    {
                        TidList = tidList
                    });
                vehicleList.ForEach(_ =>
                {
                    GetBaoYangPartAdaptationsResponse partItem = new GetBaoYangPartAdaptationsResponse();
                    partItem.Tid = _.Tid;
                    partItem.VehicleInfo = _mapper.Map<VehicleDetail>(_);
                    var baoYangItem = baoYangConfig.FirstOrDefault(t => t.Tid == _.Tid);
                    if (baoYangItem != null)
                    {
                        partItem.PartsAdaptation = baoYangItem.PartsAdaptation.Select(t =>
                            new BaoYangPartsAdaptationDetail
                            {
                                PartDisplayName = t.PartDisplayName,
                                OeCodeAdaptationDetails = t.OeCodeAdaptationDetails.Select(x =>
                                    new BaoYangPartsOeCodeAdaptationDetail
                                    {
                                        OePartCode = x.OePartCode,
                                        PartCodeAdaptation = x.PartCodeAdaptation.Select(x1 =>
                                            new BaoYangPartsPartCodeAdaptationDetail
                                            {
                                                Id = x1.Id,
                                                PartCode = x1.PartCode,
                                                Brand = x1.Brand,
                                                IsValidation = x1.IsValidation,
                                                AuditType = x1.AuditType,
                                                Status = x1.Status,
                                                PartsProducts = x1.PartsProducts.Select(x2 => new BaoYangPartsProduct
                                                {
                                                    ProductPid = x2.ProductPid,
                                                    IsOnSale = x2.IsOnSale,
                                                    IsStockOut = x2.IsStockOut
                                                }).ToList()
                                            }).ToList()
                                    }).ToList()
                            }).ToList();
                        partItem.PartsSpecialAdaptations = baoYangItem.PartsSpecialAdaptations.Select(x1 =>
                            new BaoYangPartsSpecialAdaptation
                            {
                                PartDisplayName = x1.PartDisplayName, SpecialAdaptationParts = x1.SpecialAdaptationParts
                                    .Select(x2 => new SpecialAdaptationPart
                                    {
                                        PartType = x2.PartType,
                                        PartCodeAdaptation = x2.PartCodeAdaptation.Select(x3 =>
                                            new BaoYangPartsPartCodeAdaptationDetail
                                            {
                                                Id = x3.Id,
                                                PartCode = x3.PartCode,
                                                Brand = x3.Brand,
                                                IsValidation = x3.IsValidation,
                                                AuditType = x3.AuditType,
                                                Status = x3.Status,
                                                PartsProducts = x3.PartsProducts.Select(x4 => new BaoYangPartsProduct
                                                {
                                                    ProductPid = x4.ProductPid,
                                                    IsOnSale = x4.IsOnSale,
                                                    IsStockOut = x4.IsStockOut
                                                }).ToList()
                                            }).ToList()
                                    }).ToList()
                            }).ToList();
                    }

                    result.Add(partItem);
                });
            }

            return result;
        }

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<PartNameListResponse>> GetPartNameListAsync()
        {
            List<PartNameListResponse> result = new List<PartNameListResponse>();
            var partList = await _baoYangClient.GetPartNameListAsync();
            if (partList != null && partList.Any())
            {
                result = _mapper.Map<List<PartNameListResponse>>(partList);
            }

            return result;
        }

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPartCodeAsync(AddPartCodeRequest request)
        {
            var result = await _baoYangClient.AddPartCodeAsync(new AddPartCodeRemoteRequest
            {
                TidList = request.TidList,
                PartName = request.PartName,
                OePartCode = request.OePartCode,
                PartCodes = request.PartCodes.Select(_ => new PartCodeModelRemote
                {
                    PartCode = _.PartCode,
                    Brand = _.Brand
                }).ToList(),
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PartCodeVo>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeRequest request)
        {
            var result = await _baoYangClient.GetPartCodeAndBrandByOe(new PartCodeAndBrandByOeClientRequest
            {
                OeCode = request.OeCode,
                PartName = request.PartName
            });

            return result?.Select(_ => new PartCodeVo
            {
                PartCode = _.PartCode,
                Brand = _.Brand
            })?.ToList() ?? new List<PartCodeVo>();
        }

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRequest request)
        {
            var result = await _baoYangClient.AddSpecialPartCodeAsync(new AddSpecialPartRemoteRequest
            {
                TidList = request.TidList,
                PartName = request.PartName,
                PartCodes = request.PartCodes.Select(t => new SpecialPartCodeRemoteModel
                {
                    Brand = t.Brand,
                    PartCode = t.PartCode,
                    PartType = t.PartType
                }).ToList(),
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 批量添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAdaptationAsync(BatchAddAdaptationRequest request)
        {
            var result = await _baoYangClient.BatchAddAdaptationAsync(new BatchAddAdaptationRemoteRequest
            {
                TidList = request.TidList,
                NormalPart = request.NormalPart.Select(t => new NormalPartRemote
                {
                    PartName = t.PartName,
                    OePartCode = t.OePartCode,
                    PartCodes = t.PartCodes.Select(_ => new PartCodeModelRemote
                    {
                        PartCode = _.PartCode,
                        Brand = _.Brand
                    }).ToList()
                }).ToList(),
                SpecialPart = request.SpecialPart.Select(t => new SpecialPartRemote
                {
                    PartName = t.PartName,
                    PartCodes = t.PartCodes.Select(t1 => new SpecialPartCodeRemoteModel
                    {
                        Brand = t1.Brand,
                        PartCode = t1.PartCode,
                        PartType = t1.PartType
                    }).ToList()
                }).ToList(),
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeAsync(DeletePartCodeRequest request)
        {
            var result = await _baoYangClient.DeletePartCodeAsync(new DeletePartCodeRemoteRequest
            {
                Tid = request.Tid,
                PartName = request.PartName,
                Id = request.Id,
                OePartCode = request.OePartCode,
                PartType = request.PartType,
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOePartCodeAsync(UpdateOePartCodeRequest request)
        {
            var result = await _baoYangClient.UpdateOePartCodeAsync(new UpdateOePartCodeRemoteRequest
            {
                Tid = request.Tid,
                PartName = request.PartName,
                OriginalOePartCode = request.OriginalOePartCode,
                OePartCode = request.OePartCode,
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePartCodeAsync(UpdatePartCodeRequest request)
        {
            var result = await _baoYangClient.UpdatePartCodeAsync(new UpdatePartCodeRemoteRequest
            {
                Id = request.Id,
                Tid = request.Tid,
                PartCode = request.PartCode,
                Brand = request.Brand,
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPartsVo>> GetBaoYangParts()
        {
            var baoYangParts = await _baoYangClient.GetBaoYangParts();
            var result = _mapper.Map<List<BaoYangPartsVo>>(baoYangParts);
            return result;
        }

        /// <summary>
        /// 查询车型已绑定套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaoYangPackageRefResponse> GetBaoYangPackageRef(BaoYangPackageRefRequest request)
        {
            BaoYangPackageRefResponse response = new BaoYangPackageRefResponse();
            List<BaoYangPackageRefVo> packages = new List<BaoYangPackageRefVo>();
            List<VehicleBaseInfo> vehicle = new List<VehicleBaseInfo>();
            List<string> tidList = new List<string>();
            string packageId = string.Empty;
            if (!(request.SearchType == 4 && string.IsNullOrEmpty(request.Tid)))
            {
                vehicle = await _vehicleClient.GetVehicleBaseInfoList(new VehicleInfoListRequest()
                {
                    VehicleId = request.VehicleId,
                    PaiLiang = request.PaiLiang,
                    Nian = request.Nian,
                    Tid = request.Tid
                });
                if (vehicle == null || !vehicle.Any())
                {
                    return response;
                }

                tidList = vehicle.Select(_ => _.Tid).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(request.PackageId))
                {
                    return response;
                }

                packageId = request.PackageId;
            }

            var result = await _baoYangClient.GetBaoYangPackageRef(new BaoYangPackageRefClientRequest()
            {
                TidList = tidList,
                PackageId = packageId
            });
            if (result != null)
            {
                packages = _mapper.Map<List<BaoYangPackageRefVo>>(result);
            }

            if (!tidList.Any() && packages.Any())
            {
                vehicle = await _vehicleClient.GetVehicleBaseInfoListByTids(new VehicleBaseInfoListByTidsClientRequest()
                {
                    Tids = packages.Select(_ => _.Tid).Distinct().ToList()
                });
            }

            packages.ForEach(_ =>
            {
                var itemCar = vehicle.FirstOrDefault(t => t.Tid == _.Tid);
                if (itemCar != null)
                {
                    _.VehicleName =
                        $"{itemCar.Brand}|{itemCar.Vehicle}|{itemCar.PaiLiang}|{itemCar.ListedYear}-{itemCar.StopProductionYear}年产|{itemCar.Nian}款 {itemCar.SalesName}";
                }
            });
            response.Packages = packages;
            response.Vehicles = vehicle.Select(_ => new VehicleTidResponse
            {
                Tid = _.Tid,
                VehicleName =
                    $"{_.Brand}|{_.Vehicle}|{_.PaiLiang}|{_.ListedYear}-{_.StopProductionYear}年产|{_.Nian}款 {_.SalesName}"
            }).ToList();
            return response;
        }

        /// <summary>
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigVo>> GetPackageByType()
        {
            var result = await _baoYangClient.GetPackageByType();

            return result?.Select(_ => new BaoYangTypeConfigVo
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName
            })?.ToList() ?? new List<BaoYangTypeConfigVo>();
        }

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageVo>> GetBaoYangPackageByTid(BaoYangPackageByTidRequest request)
        {
            var result = await _baoYangClient.GetBaoYangPackageByTid(new BaoYangPackageByTidClientRequest()
            {
                Tid = request.Tid,
                BaoYangType = request.BaoYangType
            });

            return result?.Select(_ => new BaoYangPackageVo
            {
                PackageName = _.PackageName,
                PackagePid = _.PackagePid
            })?.ToList() ?? new List<BaoYangPackageVo>();
        }

        /// <summary>
        /// 添加保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> RelateVehicleAndPackage(RelateVehicleAndPackageRequest request)
        {
            var result = await _baoYangClient.RelateVehicleAndPackage(new RelateVehicleAndPackageClientRequest()
            {
                Tid = request.Tid,
                ByType = request.ByType,
                PackageId = request.PackageId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangPackageRef(DeleteBaoYangPackageRefRequest request)
        {
            var result = await _baoYangClient.DeleteBaoYangPackageRef(new DeleteBaoYangPackageRefClientRequest()
            {
                Id = request.Id,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangAccessoryVo>> GetPartAccessoryConfig()
        {
            List<BaoYangAccessoryVo> data = new List<BaoYangAccessoryVo>();
            var result = await _baoYangClient.GetPartAccessoryConfig();
            if (result != null && result.Any())
            {
                data = _mapper.Map<List<BaoYangAccessoryVo>>(result);
            }

            return data;
        }

        /// <summary>
        /// 辅料配置列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPartAccessoryVo>> GetBaoYangPartAccessory(BaoYangPartAccessoryRequest request)
        {
            List<BaoYangPartAccessoryVo> result = new List<BaoYangPartAccessoryVo>();
            var vehicleList = await _vehicleClient.GetVehicleInfoList(new VehicleInfoListRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });
            if (vehicleList != null && vehicleList.Any())
            {
                var tidList = vehicleList.Select(_ => _.Tid).Distinct().ToList();
                var partAccessory =
                    (await _baoYangClient.GetBaoYangPartAccessory(new BaoYangPartAccessoryClientRequest()
                        {TidList = tidList}))?.ToList() ?? new List<BaoYangPartAccessoryDto>();
                vehicleList.ForEach(_ =>
                {
                    BaoYangPartAccessoryVo itemPart = new BaoYangPartAccessoryVo()
                    {
                        Tid = _.Tid,
                        VehicleInfo = _mapper.Map<VehicleDetail>(_)
                    };
                    var defaultAccessory = partAccessory.FirstOrDefault(x => x.Tid == _.Tid)?.Details;
                    if (defaultAccessory != null)
                    {
                        itemPart.Details = _mapper.Map<List<PartAccessoryVo>>(defaultAccessory);
                    }

                    result.Add(itemPart);
                });
            }

            return result;
        }

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAccessory(UpdateAccessory request)
        {
            var clientRequest = _mapper.Map<UpdateAccessoryClientRequest>(request);
            clientRequest.SubmitBy = _identityService.GetUserName();
            var result = await _baoYangClient.UpdateAccessory(clientRequest);
            return result;
        }

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchEditAccessory(BatchEditAccessoryRequest request)
        {
            var clientRequest = _mapper.Map<BatchEditAccessoryClientRequest>(request);
            clientRequest.SubmitBy = _identityService.GetUserName();
            var result = await _baoYangClient.BatchEditAccessory(clientRequest);
            return result;
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccessory(DeleteAccessoryRequest request)
        {
            var result = await _baoYangClient.DeleteAccessory(new DeleteAccessoryClientRequest()
            {
                Tid = request.Tid,
                AccessoryName = request.AccessoryName,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 保养五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyAdaptationVo>> GetBaoYangPropertyAdaptation(
            BaoYangPropertyAdaptationRequest request)
        {
            List<BaoYangPropertyAdaptationVo> result = new List<BaoYangPropertyAdaptationVo>();
            var vehicle = await _vehicleClient.GetVehicleInfoList(new VehicleInfoListRequest()
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });
            if (vehicle == null || !vehicle.Any())
            {
                return result;
            }

            var tidList = vehicle.Select(_ => _.Tid).Distinct().ToList();
            var propertyResult = await _baoYangClient.GetBaoYangPropertyAdaptation(
                new BaoYangPropertyAdaptationClientRequest()
                {
                    BaoYangType = request.BaoYangType,
                    TidList = tidList
                });
            if (propertyResult != null && propertyResult.Any())
            {
                propertyResult.ForEach(_ =>
                {
                    var defaultVehicle = vehicle.FirstOrDefault(x => x.Tid == _.Tid);
                    result.Add(new BaoYangPropertyAdaptationVo()
                    {
                        Brand = defaultVehicle?.BrandFromVehicleType,
                        VehicleId = defaultVehicle?.VehicleId,
                        Vehicle = defaultVehicle?.VehicleFromVehicleType,
                        PaiLiang = defaultVehicle?.PaiLiang,
                        Nian = defaultVehicle?.Nian,
                        SalesName = defaultVehicle?.SalesName,
                        Tid = defaultVehicle?.Tid,
                        ListedYear = defaultVehicle?.ListedYear,
                        StopProductionYear = defaultVehicle?.StopProductionYear,
                        FuelType = defaultVehicle?.FuelType,
                        EngineNo = defaultVehicle?.EngineNo,
                        OeCode = _.OeCode,
                        PartCode = _.PartCode,
                        Property = _.Property,
                        PropertyValue = _.PropertyValue,
                        ImageUrl = _.ImageUrl,
                        Id = _.Id,
                        BaoYangType = request.BaoYangType
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OeCodeMapVo>> GetOeCodeMapList(OeCodeMapRequest request)
        {
            ApiPagedResultData<OeCodeMapVo> data = new ApiPagedResultData<OeCodeMapVo>()
            {
                Items = new List<OeCodeMapVo>()
            };
            var result = await _baoYangClient.GetOeCodeMapList(new OeCodeMapClientRequest()
            {
                OeCode = request.OeCode,
                PartCode = request.PartCode,
                PartType = request.PartType,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = result.Items.Select(_ => new OeCodeMapVo
                    {
                        PartName = _.PartName,
                        OePartCode = _.OePartCode,
                        PartCode = _.PartCode,
                        VehicleBrand = _.VehicleBrand
                    }).ToList();
                }
            }

            return data;
        }

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OePartCodeDetailVo> GetOeCodeMapDetailByOeCode(OeCodeMapDetailByOeCodeRequest request)
        {
            var result = await _baoYangClient.GetOeCodeMapDetailByOeCode(new OeCodeMapDetailByOeCodeClientRequest()
            {
                OePartCode = request.OePartCode
            });
            if (result != null)
            {
                return new OePartCodeDetailVo()
                {
                    OePartCode = result.OePartCode,
                    PartType = result.PartType,
                    VehicleBrand = result.VehicleBrand,
                    PartCodes = result.PartCodes?.Select(t => new ParCodeDetailVo
                    {
                        Brand = t.Brand,
                        PartCode = t.PartCode
                    })?.ToList() ?? new List<ParCodeDetailVo>()
                };
            }

            throw new CustomException($"OE号：{request.OePartCode} 不存在！");
        }

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOePartCode(DeleteOePartCodeRequest request)
        {
            var result = await _baoYangClient.DeleteOePartCode(new DeleteOePartCodeClientRequest()
            {
                OePartCode = request.OePartCode,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditOePartCode(EditOePartCodeRequest request)
        {
            var result = await _baoYangClient.EditOePartCode(new EditOePartCodeClientRequest()
            {
                OePartCode = request.OePartCode,
                PartName = request.PartName,
                VehicleBrand = request.VehicleBrand,
                PartCode = request.PartCode?.Select(x => new ParCodeDetailClientRequest
                {
                    Brand = x.Brand,
                    PartCode = x.PartCode
                })?.ToList() ?? new List<ParCodeDetailClientRequest>(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddOePartCode(AddOePartCodeRequest request)
        {
            var result = await _baoYangClient.AddOePartCode(new AddOePartCodeClientRequest()
            {
                OePartCode = request.OePartCode,
                PartName = request.PartName,
                VehicleBrand = request.VehicleBrand,
                PartCode = request.PartCode?.Select(x => new ParCodeDetailClientRequest
                {
                    Brand = x.Brand,
                    PartCode = x.PartCode
                })?.ToList() ?? new List<ParCodeDetailClientRequest>(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyDescriptionVo>> GetBaoYangPropertyDescription()
        {
            var result = await _baoYangClient.GetBaoYangPropertyDescription();

            return result?.Select(_ => new BaoYangPropertyDescriptionVo
            {
                DisplayName = _.DisplayName
            })?.ToList() ?? new List<BaoYangPropertyDescriptionVo>();
        }

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePropertyAdaptation(DeletePropertyAdaptationRequest request)
        {
            var result = await _baoYangClient.DeletePropertyAdaptation(new DeletePropertyAdaptationClientRequest
            {
                Id = request.Id,
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SavePropertyAdaptation(SavePropertyAdaptationRequest request)
        {
            var result = await _baoYangClient.SavePropertyAdaptation(new SavePropertyAdaptationClientRequest()
            {
                Id = request.Id,
                Tid = request.Tid,
                BaoYangType = request.BaoYangType,
                Property = request.Property,
                PropertyValue = request.PropertyValue,
                OePartCode = request.OePartCode,
                PartCode = request.PartCode,
                ImageUrl = request.ImageUrl,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        #region 配件号关联商品

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPartProductVo>> GetBaoYangProductRef(
            BaoYangProductRefRequest request)
        {
            ApiPagedResultData<BaoYangPartProductVo> data = new ApiPagedResultData<BaoYangPartProductVo>()
            {
                Items = new List<BaoYangPartProductVo>()
            };
            var result = await _baoYangClient.GetBaoYangProductRef(new BaoYangProductRefClientRequest()
            {
                PartCode = request.PartCode,
                Pid = request.Pid,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null)
                {
                    data.Items = _mapper.Map<List<BaoYangPartProductVo>>(result.Items);
                }
            }

            return data;
        }

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefRequest request)
        {
            var result = await _baoYangClient.DeleteBaoYangProductRef(new DeleteBaoYangProductRefClientRequest()
            {
                Id = request.Id,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InsertPartsAssociation(InsertPartsAssociationRequest request)
        {
            var result = await _baoYangClient.InsertPartsAssociation(new InsertPartsAssociationClientRequest()
            {
                PartProductRef = request.PartProductRef?.Select(_ => new PartProductRefClientRequest
                {
                    Pid = _.Pid,
                    PartCode = _.PartCode
                })?.ToList() ?? new List<PartProductRefClientRequest>(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> CheckPartCode(CheckPartCodeRequest request)
        {
            var result = await _baoYangClient.CheckPartCode(new CheckPartCodeClientRequest()
            {
                PartCode = request.PartCode
            });

            return result ?? new List<string>();
        }

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckProductResponse> CheckProduct(CheckProductRequest request)
        {
            var result = await _baoYangClient.CheckProduct(new CheckProductClientRequest()
            {
                Pid = request.Pid
            });

            if (result != null)
            {
                return new CheckProductResponse()
                {
                    Name = result.Name,
                    Category = result.Category
                };
            }

            return null;
        }

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckPartCodeResultResponse> CheckPartCodeAndProduct(CheckPartCodeAndProductRequest request)
        {
            var result = await _baoYangClient.CheckPartCodeAndProduct(new CheckPartCodeAndProductClientRequest()
            {
                PartCode = request.PartCode,
                Pid = request.Pid
            });

            if (result != null)
            {
                return new CheckPartCodeResultResponse()
                {
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };
            }

            return new CheckPartCodeResultResponse()
            {
                IsSuccess = false,
                Message = "系统异常，请联系管理员"
            };
        }

        #endregion

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageConfigVo>> GetPackageTypeConfig(
            PackageTypeConfigRequest request)
        {
            ApiPagedResultData<BaoYangPackageConfigVo> response = new ApiPagedResultData<BaoYangPackageConfigVo>()
            {
                Items = new List<BaoYangPackageConfigVo>()
            };
            var result = await _baoYangClient.GetPackageTypeConfig(new PackageTypeConfigClientRequest()
            {
                DisplayName = request.DisplayName,
                IsDeleted = request.IsDeleted,
                CategoryAlias = request.CategoryAlias,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null)
                {
                    response.Items = _mapper.Map<List<BaoYangPackageConfigVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageTypeConfig(EditPackageTypeConfigRequest request)
        {
            EditPackageTypeConfigClientRequest clientRequest = _mapper.Map<EditPackageTypeConfigClientRequest>(request);
            clientRequest.UpdateBy = _identityService.GetUserName();
            var result = await _baoYangClient.EditPackageTypeConfig(clientRequest);
            return result;
        }

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddPackageTypeConfig(AddPackageTypeConfigRequest request)
        {
            AddPackageTypeConfigClientRequest clientRequest = _mapper.Map<AddPackageTypeConfigClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserName();
            var result = await _baoYangClient.AddPackageTypeConfig(clientRequest);

            return result;
        }

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangTypeConfigDetailVo>> GetBaoYangTypeConfig(
            BaoYangTypeConfigRequest request)
        {
            ApiPagedResultData<BaoYangTypeConfigDetailVo> result = new ApiPagedResultData<BaoYangTypeConfigDetailVo>()
            {
                Items = new List<BaoYangTypeConfigDetailVo>()
            };

            var baoYangType = await _baoYangClient.GetBaoYangTypeConfig(new BaoYangTypeConfigClientRequest()
            {
                DisplayName = request.DisplayName,
                TypeGroup = request.TypeGroup,
                IsDeleted = request.IsDeleted,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (baoYangType != null)
            {
                result.TotalItems = baoYangType.TotalItems;
                if (baoYangType.Items != null && baoYangType.Items.Any())
                {
                    result.Items = _mapper.Map<List<BaoYangTypeConfigDetailVo>>(baoYangType.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangCategoryConfigVo>> GetBaoYangCategoryConfig()
        {
            var result = await _baoYangClient.GetBaoYangCategoryConfig();

            return result.Select(_ => new BaoYangCategoryConfigVo
            {
                CategoryAlias = _.CategoryAlias,
                CategoryName = _.CategoryName,
                CategorySimpleName = _.CategorySimpleName
            }).ToList();
        }

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigRequest request)
        {
            EditBaoYangTypeConfigClientRequest clientRequest = _mapper.Map<EditBaoYangTypeConfigClientRequest>(request);
            clientRequest.SubmitBy = _identityService.GetUserName();
            var result = await _baoYangClient.EditBaoYangTypeConfig(clientRequest);
            return result;
        }

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        public async Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigRequest request)
        {
            AddBaoYangTypeConfigClientRequest clientRequest = _mapper.Map<AddBaoYangTypeConfigClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserName();
            var result = await _baoYangClient.AddBaoYangTypeConfig(clientRequest);
            return result;
        }

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigVo>> GetValidBaoYangTypeConfig()
        {
            var result = await _baoYangClient.GetValidBaoYangTypeConfig();

            return result?.Select(_ => new BaoYangTypeConfigVo
            {
                BaoYangType = _.BaoYangType,
                DisplayName = _.DisplayName
            })?.ToList() ?? new List<BaoYangTypeConfigVo>();
        }

        /// <summary>
        /// 车型安装服务加价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<VehicleInstallAddFeeVo>> GetVehicleInstallAddFee(
            VehicleInstallAddFeeRequest request)
        {
            ApiPagedResultData<VehicleInstallAddFeeVo> result = new ApiPagedResultData<VehicleInstallAddFeeVo>()
            {
                Items = new List<VehicleInstallAddFeeVo>()
            };
            List<VehicleInstallAddFeeVo> data = new List<VehicleInstallAddFeeVo>();
            var vehicleTask = _vehicleClient.GetVehicleInfoList(new VehicleInfoListRequest()
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });
            var installServiceTask = GetBaoYangInstallServiceList();
            var serviceAddFeeTask = _baoYangClient.GetAllInstallAddFeeConfig();
            await Task.WhenAll(vehicleTask, installServiceTask, serviceAddFeeTask);
            var vehicle = vehicleTask.Result ?? new List<VehicleInfo>();
            var installService = installServiceTask.Result ?? new List<ProductBaseInfoDto>();
            var serviceAddFee = serviceAddFeeTask.Result ?? new List<InstallAddFeeConfigDto>();
            vehicle.ForEach(_ =>
            {
                if (!string.IsNullOrEmpty(request.ServiceId))
                {
                    installService = installService.Where(x => x.ProductCode == request.ServiceId).ToList();
                }

                installService.ForEach(t =>
                {
                    VehicleInstallAddFeeVo itemV = new VehicleInstallAddFeeVo()
                    {
                        Brand = _.Brand,
                        Vehicle = _.Vehicle,
                        VehicleId = _.VehicleId,
                        PaiLiang = _.PaiLiang,
                        Nian = _.Nian,
                        SalesName = _.SalesName,
                        Tid = _.Tid,
                        ListedYear = _.ListedYear,
                        EngineNo = _.EngineNo,
                        StopProductionYear = _.StopProductionYear,
                        GuidePrice = _.GuidePrice.ToString("#0.00"),
                        ServiceId = t.ProductCode,
                        ServiceName = t.Name,
                        OriginalPrice = t.SalesPrice.ToString("#0.00")
                    };
                    var defaultAdd = serviceAddFee.FirstOrDefault(f =>
                        f.ServiceId == t.ProductCode && f.CarMinPrice <= _.GuidePrice && f.CarMaxPrice > _.GuidePrice);
                    if (defaultAdd != null)
                    {
                        itemV.InstallAddFeeId = defaultAdd.Id;
                        itemV.AdditionalPrice = defaultAdd.AdditionalPrice.ToString("#0.00");
                        itemV.Remark = defaultAdd.Remark;
                    }

                    data.Add(itemV);
                });
            });
            result.TotalItems = data.Count;
            result.Items = data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            return result;
        }

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<InstallAddFeeConfigVo>> GetInstallAddFeeConfig(
            InstallAddFeeConfigRequest request)
        {
            ApiPagedResultData<InstallAddFeeConfigVo> result = new ApiPagedResultData<InstallAddFeeConfigVo>()
            {
                Items = new List<InstallAddFeeConfigVo>()
            };
            var installService = await _baoYangClient.GetInstallAddFeeConfig(new InstallAddFeeConfigClientRequest()
            {
                ServiceId = request.ServiceId,
                GuidePrice = request.GuidePrice,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (installService != null)
            {
                result.TotalItems = installService.TotalItems;
                if (installService.Items != null && installService.Items.Any())
                {
                    result.Items = _mapper.Map<List<InstallAddFeeConfigVo>>(installService.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddInstallAddFeeConfig(AddInstallAddFeeConfigRequest request)
        {
            var result = await _baoYangClient.AddInstallAddFeeConfig(new AddInstallAddFeeConfigClientRequest()
            {
                ServiceId = request.ServiceId,
                CarMinPrice = request.CarMinPrice,
                CarMaxPrice = request.CarMaxPrice,
                AdditionalPrice = request.AdditionalPrice,
                Remark = request.Remark,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallAddFeeConfig(EditInstallAddFeeConfigRequest request)
        {
            var result = await _baoYangClient.EditInstallAddFeeConfig(new EditInstallAddFeeConfigClientRequest()
            {
                Id = request.Id,
                ServiceId = request.ServiceId,
                CarMinPrice = request.CarMinPrice,
                CarMaxPrice = request.CarMaxPrice,
                AdditionalPrice = request.AdditionalPrice,
                Remark = request.Remark,
                IsDeleted = request.IsDeleted,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 获取保养安装服务
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangInstallServiceVo>> GetBaoYangInstallService()
        {
            var result = await GetBaoYangInstallServiceList();
            return result.Select(_ => new BaoYangInstallServiceVo
            {
                ProductCode = _.ProductCode,
                Name = _.Name
            }).ToList();
        }

        private async Task<List<ProductBaseInfoDto>> GetBaoYangInstallServiceList()
        {
            var categoryId = _configuration["BaoYangServer:BaoYangInstallCategoryId"];
            Int32.TryParse(categoryId, out int baoYangInstallCategoryId);
            var result = await _productClient.SelectProductsByCategory(baoYangInstallCategoryId);
            return result ?? new List<ProductBaseInfoDto>();
        }
    }
}
