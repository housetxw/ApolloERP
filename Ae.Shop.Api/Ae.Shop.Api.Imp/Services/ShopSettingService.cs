using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopSettingService : IShopSettingService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ShopSettingService> logger;
        private readonly IIdentityService identityService;
        private readonly IShopAssetRepository shopAssetRepository;
        private readonly IShopAssetTypeRepository shopAssetTypeRepository;
        private readonly IShopAssetMaintainRepository shopAssetMaintainRepository;
        private readonly IShopAssetDiscardRepository shopAssetDiscardRepository;

        public ShopSettingService(IMapper mapper, ApolloErpLogger<ShopSettingService> logger, IIdentityService identityService, IShopAssetRepository shopAssetRepository, IShopAssetTypeRepository shopAssetTypeRepository, IShopAssetMaintainRepository shopAssetMaintainRepository, IShopAssetDiscardRepository shopAssetDiscardRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.shopAssetRepository = shopAssetRepository;
            this.shopAssetTypeRepository = shopAssetTypeRepository;
            this.shopAssetMaintainRepository = shopAssetMaintainRepository;
            this.shopAssetDiscardRepository = shopAssetDiscardRepository;
        }

        public async Task<ApiResult> DeleteShopAsset(BaseDeleteByIdRequest data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var userName = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                var model = new ShopAssetDO() { Id = data.Id, IsDeleted = 1, UpdateBy = userName, UpdateTime = DateTime.Now };
                var count = await shopAssetRepository.UpdateAsync(model, new List<string> { "IsDeleted", "UpdateBy", "UpdateTime" });
                if (count > 0)
                {
                    result = Result.Success(CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("DeleteShopAssetEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> DiscardShopAsset(ShopAssetDiscardVO data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                var userName = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                var shopAssetDiscardDO = mapper.Map<ShopAssetDiscardDO>(data);
                shopAssetDiscardDO.CreateBy = userName;
                shopAssetDiscardDO.CreateTime = DateTime.Now;
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var count = await shopAssetDiscardRepository.InsertAsync(shopAssetDiscardDO);
                    if (count > 0)
                    {
                        var updateCount = await shopAssetRepository.UpdateAsync(
                            new ShopAssetDO()
                            {
                                Id = data.AssetId,
                                UseStatus = 2,
                                UpdateBy = userName,
                                UpdateTime = DateTime.Now
                            },
                            new List<string> { "UpdateBy", "UpdateTime", "UseStatus" });
                        if (updateCount > 0)
                        {
                            result = Result.Success(CommonConstant.OperateSuccess);
                        }
                        else
                        {
                            throw new CustomException(CommonConstant.OperateFailed);
                        }
                    }
                    else
                    {
                        throw new CustomException(CommonConstant.OperateFailed);
                    }
                    ts.Complete();
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("DiscardShopAssetEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<ShopAssetVO>> GetShopAsset(BaseGetByIdRequest request)
        {
            var result = Result.Failed<ShopAssetVO>(CommonConstant.OperateFailed);
            try
            {
                var shopAssetDO = await shopAssetRepository.GetAsync(request.Id);
                if (shopAssetDO != null)
                {
                    var shopAssetVO = mapper.Map<ShopAssetVO>(shopAssetDO);
                    result = Result.Success(shopAssetVO, CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetShopAssetEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<ShopAssetVO>> GetShopAssetList(GetShopAssetListRequest request)
        {
            var result = Result.Failed(ResultCode.Failed, CommonConstant.QueryFailed, new List<ShopAssetVO>(), 0);
            try
            {
                var condtion = new StringBuilder("where 1=1 ");
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    condtion.Append("and shop_id=@ShopId ");
                    request.ShopId = shopId;
                }
                if (!string.IsNullOrWhiteSpace(request.FirstTypeCode))
                {
                    condtion.Append("and first_type_code=@FirstTypeCode ");
                }
                if (!string.IsNullOrWhiteSpace(request.SecondTypeCode))
                {
                    condtion.Append("and second_type_code=@SecondTypeCode ");
                }
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    condtion.Append("and name like concat('%',@Name,'%') ");
                }
                if (!string.IsNullOrWhiteSpace(request.Code))
                {
                    condtion.Append("and code like concat('%',@Code,'%') ");
                }
                if (request.AddMethod > 0)
                {
                    condtion.Append("and add_method=@AddMethod ");
                }
                if (request.UseStatus > 0)
                {
                    condtion.Append("and use_status=@UseStatus ");
                }
                if (request.StartUseDateOrigin > new DateTime(1900, 1, 1))
                {
                    condtion.Append("and start_use_date>=@StartUseDateOrigin ");
                }
                if (request.StartUseDateOff > new DateTime(1900, 1, 1))
                {
                    condtion.Append("and start_use_date<@StartUseDateOff ");
                    request.StartUseDateOff.AddDays(1);
                }
                var list = await shopAssetRepository.GetListPagedAsync(request.PageIndex, request.PageSize,
                    condtion.ToString(), "id desc", request);
                if (list != null)
                {
                    var shopAssetVOs = mapper.Map<List<ShopAssetVO>>(list.Items);
                    if (shopAssetVOs.Any())
                    {
                        var values = new List<string>();
                        foreach (var item in shopAssetVOs)
                        {
                            if (!string.IsNullOrWhiteSpace(item.FirstTypeCode) && !values.Contains(item.FirstTypeCode))
                            {
                                values.Add(item.FirstTypeCode);
                            }
                            if (!string.IsNullOrWhiteSpace(item.SecondTypeCode) && !values.Contains(item.SecondTypeCode))
                            {
                                values.Add(item.SecondTypeCode);
                            }
                        }
                        var types = await shopAssetTypeRepository.GetShopAssetTypesByValues(values);
                        if (types != null && types.Any())
                        {
                            foreach (var item in shopAssetVOs)
                            {
                                item.FirstTypeName = types.Find(_ => _.Value == item.FirstTypeCode)?.Label ?? string.Empty;
                                item.SecondTypeName = types.Find(_ => _.Value == item.SecondTypeCode)?.Label ?? string.Empty;
                            }
                        }
                    }
                    result = Result.Success(shopAssetVOs, list.TotalItems);
                }
                else
                {
                    result = Result.Success(new List<ShopAssetVO>(), 0);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetShopAssetListEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> MaintainShopAsset(ShopAssetMaintainVO data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                var userName = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                var shopAssetMaintainDO = mapper.Map<ShopAssetMaintainDO>(data);
                shopAssetMaintainDO.CreateBy = userName;
                shopAssetMaintainDO.CreateTime = DateTime.Now;
                var count = await shopAssetMaintainRepository.InsertAsync(shopAssetMaintainDO);
                if (count > 0)
                {
                    result = Result.Success(CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("MaintainShopAssetEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<List<ShopAssetTypeOptionsResponse>>> ShopAssetTypeOptions(ShopAssetTypeOptionsRequest request)
        {
            var result = Result.Failed<List<ShopAssetTypeOptionsResponse>>(CommonConstant.OperateFailed);
            try
            {
                var shopAssetTypes = (await shopAssetTypeRepository.GetListAsync("where 1=1"))?.ToList();
                if (shopAssetTypes != null && shopAssetTypes.Any())
                {
                    shopAssetTypes = shopAssetTypes.OrderBy(_ => _.Value).ToList();
                    var list = new List<ShopAssetTypeOptionsResponse>();
                    foreach (var item in shopAssetTypes)
                    {
                        if (item.ParentId != null)
                        {
                            var shopAssetTypeOptionsResponse = mapper.Map<ShopAssetTypeOptionsResponse>(item);
                            var findChildren = shopAssetTypes.FindAll(_ => _.ParentId == item.Id.ToString());
                            if (findChildren != null && findChildren.Any())
                            {
                                shopAssetTypeOptionsResponse.children = new List<ShopAssetTypeVO>();
                                foreach (var child in findChildren)
                                {
                                    var childVO = mapper.Map<ShopAssetTypeVO>(child);
                                    shopAssetTypeOptionsResponse.children.Add(childVO);
                                }
                            }
                            list.Add(shopAssetTypeOptionsResponse);
                        }
                    }
                    result = Result.Success(list, CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetShopAssetEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> UpsertShopAsset(ShopAssetVO data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var shopAssetDO = mapper.Map<ShopAssetDO>(data);
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                var userName = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                if (shopAssetDO.Id > 0)
                {
                    shopAssetDO.UpdateBy = userName;
                    shopAssetDO.UpdateTime = DateTime.Now;
                    var count = await shopAssetRepository.UpdateAsync(shopAssetDO, new List<string> { "UpdateBy", "UpdateTime", "StorageLocation", "Remark" });
                    if (count > 0)
                    {
                        result = Result.Success(CommonConstant.OperateSuccess);
                    }
                }
                else
                {
                    shopAssetDO.ShopId = shopId;
                    shopAssetDO.CreateBy = userName;
                    shopAssetDO.CreateTime = DateTime.Now;
                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var count = await shopAssetRepository.InsertAsync(shopAssetDO);
                        if (count > 0)
                        {
                            shopAssetDO.Code = $"{shopAssetDO.FirstTypeCode}{shopId}{shopAssetDO.Id}".ToUpper();
                            var updateCount = await shopAssetRepository.UpdateAsync(shopAssetDO, new List<string>() { "Code" });
                            if (updateCount > 0)
                            {
                                result = Result.Success(CommonConstant.OperateSuccess);
                            }
                        }
                        ts.Complete();
                    }
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("UpsertShopAssetEx", ex);
            }
            return result;
        }
    }
}
