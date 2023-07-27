using AutoMapper;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ae.Shop.Service.Core.Model;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using System.Transactions;
using Ae.Shop.Service.Client.Inteface;
using ApolloErp.Log;
using Newtonsoft.Json;
using Ae.Shop.Service.Client.Model;
using ApolloErp.Login.Auth;
using Ae.Shop.Service.Core.Request.ShopServer;

namespace Ae.Shop.Service.Imp.Services
{
    /// <summary>
    /// 门店服务
    /// </summary>
    public class ShopServerService : IShopServerService
    {
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IMapper _mapper;
        private readonly IBaseServiceRepository _baseServiceRepository;
        private readonly IShopServiceCategoryRepository _shopServiceCategoryRepository;
        private readonly IShopServiceTypeRepository _shopServiceTypeRepository;
        private readonly IBaoYangClient _baoYangClient;
        private readonly ApolloErpLogger<ShopServerService> _logger;
        private readonly IShopRepository _shopRepository;
        private readonly IIdentityService identityService;
        public ShopServerService(
            IMapper mapper,
            IShopServiceRepository shopServiceRepository,
            IBaseServiceRepository baseServiceRepository,
            IShopServiceCategoryRepository shopServiceCategoryRepository,
            IShopServiceTypeRepository shopServiceTypeRepository,
            IBaoYangClient baoYangClient,
            ApolloErpLogger<ShopServerService> logger, IShopRepository shopRepository,
            IIdentityService identityService
            )
        {
            _mapper = mapper;
            _shopServiceRepository = shopServiceRepository;
            _baseServiceRepository = baseServiceRepository;
            _shopServiceCategoryRepository = shopServiceCategoryRepository;
            _shopServiceTypeRepository = shopServiceTypeRepository;
            _baoYangClient = baoYangClient;
            this._logger = logger;
            _shopRepository = shopRepository;
            this.identityService = identityService;
        }

        /// <summary>
        /// 根据PID查询门店服务基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetShopServiceListWithPIDResponse>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request)
        {
            var result = await _shopServiceRepository.GetShopServiceListWithPID(request.ShopId, request.ProductIds);
            List<GetShopServiceListWithPIDResponse> ShopServiceList = new List<GetShopServiceListWithPIDResponse>();
            request.ProductIds.ForEach(o => ShopServiceList.Add(new GetShopServiceListWithPIDResponse()
            {
                PID = o
            }));

            foreach (var item in ShopServiceList)
            {
                if (result.Where(o => o.ProductId == item.PID).Any())
                {
                    item.IsOnline = result.Where(o => o.ProductId == item.PID).FirstOrDefault().Status == 1;
                    item.CostPrice = result.Where(o => o.ProductId == item.PID).FirstOrDefault().CostPrice;
                }
            }

            return ShopServiceList;
        }

        /// <summary>
        /// 获取门店所有上架的服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetShopServiceListWithPIDResponse>> GetAllOnLineServicesByShopId(
            GetAllOnLineServicesByShopIdRequest request)
        {
            var result = await _shopServiceRepository.GetAllOnLineServicesByShopId(request.ShopId);

            return result.Select(_ => new GetShopServiceListWithPIDResponse
            {
                PID = _.ProductId,
                IsOnline = true,
                CostPrice = _.CostPrice
            }).ToList();
        }

        /// <summary>
        /// 新增服务大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddBaseServiceCategoryAsync(AddServiceCategoryRequest request)
        {
            
            //判断大类是否存在
            
            var serviceInfo = await _baseServiceRepository.GetBaseServiceByNameAsync(request.Name, request.Code);
            if (serviceInfo == null)
            {
                serviceInfo = new BaseServiceDO() 
                {
                    Name = request.Name,
                    Code = request.Code,
                    CreateBy = request.CreateBy,
                    CreateTime = DateTime.Now
                };
                var num = await _baseServiceRepository.InsertAsync(serviceInfo);
                if (num > 0) 
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Success,
                        Message = CommonConstant.OperateSuccess
                    };
                }
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.FriendlyMistake
                };
            }
            else
            {
                return new ApiResult() 
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.BaseServiceExistData
                };
            }
        }

        /// <summary>
        /// 新增基本服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddBaseServiceAsync(BaseServiceDTO model)
        {
            model.CreateTime = DateTime.Now;
            model.CreateBy = identityService.GetUserName();
            var baseServiceDO = _mapper.Map<BaseServiceDO>(model);
            var num = await _baseServiceRepository.InsertAsync(baseServiceDO);
            return num > 0;
        }

        /// <summary>
        /// 获取基本服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseServiceDTO> GetBaseServiceInfoAsync(GetBaseServiceInfoRequest request)
        {
            var info = await _baseServiceRepository.GetAsync(request.ServiceId);
            var result = _mapper.Map<BaseServiceDTO>(info);
            return result;
        }

        /// <summary>
        /// 获取基本服务
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaseServiceDTO>> GetBaseServiceListAsync(GetBaseServiceListRequest request)
        {
            if (request.CategoryId == 0)
            {
                var serviceTypeList = new List<BaseServiceDTO>();
                var serviceType = await _baoYangClient.GetServiceTypeEnum();
                if (serviceType != null && serviceType.Any())
                {
                    foreach (var item in serviceType)
                    {
                        var type = new BaseServiceDTO() 
                        {
                            Id = item.Id,
                            Name = item.DisplayName,
                            Code = item.ServiceType,
                        };
                        serviceTypeList.Add(type);
                    }
                }
                return serviceTypeList;
            }
            else 
            {
                var shopTypeList = await _baseServiceRepository.GetBaseServiceListAsync(request);
                return _mapper.Map<List<BaseServiceDTO>>(shopTypeList);
            }
        }

        /// <summary>
        /// 修改基本服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyBaseServiceInfoAsync(BaseServiceDTO request)
        {
            var info = await _baseServiceRepository.GetAsync(request.Id);
            var model = _mapper.Map<BaseServiceDO>(request);
            model.UpdateTime = DateTime.Now;
            model.CreateTime = info.CreateTime;
            var num = await _baseServiceRepository.UpdateAsync(model);
            return num > 0;
        }

        /// <summary>
        /// 获取门店服务列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <param name="type"> 1 门店开通过的服务；0门店未开通过的服务</param>
        /// <returns></returns>
        public async Task<List<ShopServiceDTO>> GetShopServiceListAsync(GetShopServiceListRequest request)
        {
            var shopServiceList = await _shopServiceRepository.GetShopServiceListAsync(request.ShopId, request.CategoryId);
            if (request.Type > 0)
            {
                shopServiceList = shopServiceList.Where(n => n.Id > 0).ToList().OrderByDescending(n => n.BaseServiceId).ToList();
            }
            else
            {
                shopServiceList = shopServiceList.Where(n => n.Id == 0).ToList().OrderByDescending(n => n.BaseServiceId).ToList();
            }
            var result = _mapper.Map<List<ShopServiceDTO>>(shopServiceList);
            return result;
        }

        /// <summary>
        /// 判断门店是否存在添加的服务
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="baseServiceId"></param>
        /// <returns></returns>
        public async Task<bool> SearchShopServiceByServiceId(long shopId, long baseServiceId)
        {
            var conditon = "where shop_id=@ShopId AND base_service_id = @BaseServiceId";
            var paras = new
            {
                ShopId = shopId,
                BaseServiceId = baseServiceId
            };
            var flag = false;
            var shopTypeList = await _shopServiceRepository.GetListAsync(conditon.ToString(), paras);
            if (shopTypeList != null && shopTypeList.Any())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 门店添加服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopServiceAsync(AddShopServiceRequest request)
        {
            bool isSuccess = false;
            var isExist = SearchShopServiceByServiceId(request.ShopId, request.BaseServiceId);
            var baseService = _baseServiceRepository.GetAsync(request.BaseServiceId);
            await Task.WhenAll(isExist, baseService);
            if (!isExist.Result && baseService.Result != null)
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
                {
                    isSuccess = await _shopServiceRepository.AddShopServiceAsync(request);
                    var categoryList = await _shopServiceCategoryRepository.ServiceCategoryByShopId(request.ShopId, baseService.Result.CategoryId);
                    if (isSuccess && categoryList != null && categoryList.Count == 0) 
                    {
                        var shopServiceCategory = new ShopServiceCategoryDO()
                        {
                            ShopId = request.ShopId,
                            CategoryId = baseService.Result.CategoryId,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now
                        };
                        await _shopServiceCategoryRepository.InsertAsync(shopServiceCategory);
                    }
                    ts.Complete();
                }
            }
            return isSuccess;
        }
        /// <summary>
        /// 查询门店服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopServiceDTO> GetShopServiceInfoAsync(GetShopServiceInfoRequest request)
        {
            var result = await _baseServiceRepository.GetShopServiceInfoAsync(request);
            return _mapper.Map<ShopServiceDTO>(result);
        }
        /// <summary>
        /// 修改门店服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopServiceAsync(ModifyShopServiceRequest request)
        {
            var num = 0;
            var info = await _shopServiceRepository.GetAsync(request.Id);
            if (info != null) 
            {
                info.CostPrice = request.CostPrice;
                info.SalePrice = request.SalePrice;
                info.Status = request.Status;
                info.UpdateBy = request.UpdateBy;
                info.UpdateTime = DateTime.Now;
                num = await _shopServiceRepository.UpdateAsync(info, new[] { "CostPrice", "SalePrice", "Status", "UpdateBy", "UpdateTime" });
            }
            return num > 0;
        }
        /// <summary>
        /// 获取门店开通的服务大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaseServiceDTO>> GetShopServiceCategory(GetShopRequest request) 
        {
            var list = await _shopServiceRepository.GetShopServiceCategory(request.ShopId);
            return _mapper.Map<List<BaseServiceDTO>>(list);
        }


        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeDTO>> GetShopServiceTypeListAsync(BaseGetShopRequest request)
        {
            List<ShopServiceTypeDTO> shopServiceTypes = new List<ShopServiceTypeDTO>();
            var list = await _shopServiceTypeRepository.GetShopServiceTypeListAsync(request.ShopId);
            if (list != null && list.Any()) 
            {
                var serviceType = await _baoYangClient.GetServiceTypeEnum();
                if (serviceType != null && serviceType.Any()) 
                {
                    foreach (var item in serviceType)
                    {
                        foreach (var itemType in list)
                        {
                            if (itemType.ServiceType == item.ServiceType) 
                            {
                                var type = new ShopServiceTypeDTO();
                                type.ShopServiceTypeId = itemType.Id;
                                type.ServiceTypeId = item.Id;
                                type.DisplayName = item.DisplayName;
                                type.Description = item.Description;
                                type.ServiceType = item.ServiceType;
                                type.ImageUrl = item.ImageUrl;
                                type.RouteUrl = item.RouteUrl;
                                type.OrderType= item.Id;
                                shopServiceTypes.Add(type);
                            }
                        }
                    }
                    var baoYangType = shopServiceTypes?.Where(_ => _.ServiceType == "BaoYang")?.FirstOrDefault();
                    var tireType= shopServiceTypes?.Where(_ => _.ServiceType == "Tire")?.FirstOrDefault();
                    //保养和轮胎的顺序是反的
                    baoYangType.OrderType = 2;
                    tireType.OrderType = 1;
                    //var tireTypeId = tireType?.ServiceTypeId;
                    //var baoYangTypeId = baoYangType?.ServiceTypeId;
                    //if (baoYangType != null && tireType != null)
                    //{
                    //    baoYangType.OrderType = tireTypeId ?? 0;
                    //    tireType.OrderType = baoYangTypeId ?? 0;
                    //}
                    return shopServiceTypes;
                }
            }
            return shopServiceTypes;
        }

        public async Task<ApiPagedResult<ShopServiceTypeNewDTO>> GetShopServiceTypePagesAsync(GetShopServiceTypePageRequest request)
        {
            var result = await _shopServiceTypeRepository.GetShopServiceTypePagesAsync(request);
            var vo = _mapper.Map<ApiPagedResultData<ShopServiceTypeNewDTO>>(result);

            if (vo.Items != null && vo.Items.Any())
            {
                var distinctShopIds = vo.Items.Select(r => r.ShopId).Distinct().ToList();
                var shopLists = await _shopRepository.GetShopListByIdsAsync(distinctShopIds);

                //var serviceTypes = new List<ServiceTypeDTO>();
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "保养服务", ServiceType= "BaoYang" });
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "轮胎安装", ServiceType = "Tire" });
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "美容洗车", ServiceType = "Wash" });
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "钣金维修", ServiceType = "SheetMetal" });
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "汽车改装", ServiceType = "CarModification" });
                //serviceTypes.Add(new ServiceTypeDTO { DisplayName = "其他", ServiceType = "Other" });

                 var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                var serviceTypeDic = serviceTypes.ToDictionary(r => r.ServiceType, r => r);

                if (shopLists.Any())
                {
                    var shopDic = shopLists.ToDictionary(r => r.Id, r => r);

                    foreach (var item in vo.Items)
                    {
                        if (serviceTypeDic.ContainsKey(item.ServiceType))
                        {
                            item.ServiceName = serviceTypeDic[item.ServiceType].DisplayName;
                        }

                        if (shopDic.ContainsKey(item.ShopId))
                        {
                            item.ShopName = shopDic[item.ShopId].SimpleName;
                        }
                    }
                }
            }
            var res = new ApiPagedResult<ShopServiceTypeNewDTO>();
            res.Code = ResultCode.Success;
            res.Data = vo;
            return res;
        }

        public async  Task<ApiResult<string>> DeleteShopServiceType(ShopServiceTypeNewDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var result = await _shopServiceTypeRepository.DeleteShopServiceType(new ShopServiceTypeDO
                {
                    Id = request.Id,
                    UpdateBy = "System",
                    IsDeleted = request.IsDeleted
                });
                res.Code = ResultCode.Success;                     
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"DeleteShopServiceType_Error Data:{JsonConvert.SerializeObject(request)}",ex);                     
            }            
            return res;
        }

        /// <summary>
        /// 自动生成门店服务类型
        /// </summary>
        /// <param name="shopId"></param>
        public async Task<bool> GenerateShopServiceType(long shopId)
        {
            var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
            var serviceTypeList = serviceTypes?.Select(t => new ShopServiceTypeDO
            {
                ShopId = shopId,
                IsDeleted = 0,
                CreateBy = "System",
                CreateTime = DateTime.Now,
                ServiceType = t.ServiceType
            });
            _shopServiceTypeRepository.InsertBatch(serviceTypeList);
            return true;
        }
    }
}
