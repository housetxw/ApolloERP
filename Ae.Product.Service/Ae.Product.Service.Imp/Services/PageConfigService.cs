using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.Product.Service.Dal.Repository.Config;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Common.Exceptions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Repository;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Dal.Model.Config;
using Ae.Product.Service.Core.Enums;
using ApolloErp.Log;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Ae.Product.Service.Imp.Services
{
    public class PageConfigService : IPageConfigService
    {
        private readonly IMapper _mapper;
        private readonly IConfigAdvertisementRepository _configAdvertisementRepository;
        private readonly IConfigFrontCategoryRepository _configFrontCategoryRepository;
        private readonly IConfigHotProductRepository _configHotProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IConfigFrontCategoryProductRepository _configFrontCategoryProductRepository;
        private readonly IProductManageService _productManageService;
        private readonly IConfigPackageCardProductRepository _configPackageCardProductRepository;
        private readonly ApolloErpLogger<PageConfigService> _logger;
        private readonly IGrouponProductConfigRepository _grouponProductConfigRepository;
        private readonly IConfigShopPackageCardRepository _configShopPackageCardRepository;
        private readonly IConfiguration _configuration;

        public PageConfigService(IMapper mapper, IConfigAdvertisementRepository configAdvertisementRepository,
            IConfigFrontCategoryRepository configFrontCategoryRepository,
            IConfigHotProductRepository configHotProductRepository, IProductRepository productRepository,
            IConfigFrontCategoryProductRepository configFrontCategoryProductRepository,
            IProductManageService productManageService,
            IConfigPackageCardProductRepository configPackageCardProductRepository,
            ApolloErpLogger<PageConfigService> logger, IGrouponProductConfigRepository grouponProductConfigRepository,
            IConfigShopPackageCardRepository configShopPackageCardRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _configAdvertisementRepository = configAdvertisementRepository;
            _configFrontCategoryRepository = configFrontCategoryRepository;
            _configHotProductRepository = configHotProductRepository;
            _productRepository = productRepository;
            _configFrontCategoryProductRepository = configFrontCategoryProductRepository;
            _productManageService = productManageService;
            _configPackageCardProductRepository = configPackageCardProductRepository;
            _logger = logger;
            _grouponProductConfigRepository = grouponProductConfigRepository;
            _configShopPackageCardRepository = configShopPackageCardRepository;
            _configuration = configuration;
        }

        #region 首页活动配置

        /// <summary>
        /// 首页活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ConfigAdvertisementVo>> QueryConfigAdvertisement(QueryConfigAdvertisementRequest request)
        {
            var result = await _configAdvertisementRepository.GetValidConfigAdvertisement(request.TerminalType);

            return _mapper.Map<List<ConfigAdvertisementVo>>(result);
        }

        public async Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var res = new ApiResult<string>();
            try
            {
                var config = new ConfigAdvertisementDo
                {
                    UpdateBy = request.UpdateBy,
                    Id = request.Id,
                    UpdateTime = DateTime.Now,
                    IsOnline = request.IsOnline
                };
                await _configAdvertisementRepository.UpdateAsync(config, new List<string> { "IsOnline", "UpdateBy", "UpdateTime" });
                res.Code = ResultCode.Success;
                res.Message = (request.IsOnline == 1 ? "上架" : "下架") + "成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteConfigAdvertisement_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var res = new ApiResult<string>();
            try
            {
                var vo = _mapper.Map<ConfigAdvertisementDo>(request);
                vo.CreateTime = DateTime.Now;
                vo.IsOnline = 1;
                await _configAdvertisementRepository.InsertAsync(vo);
                res.Message = "添加成功";
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"AddConfigAdvertisement_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }


        public async Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request)
        {
            var result = new ApiPagedResult<ConfigAdvertisementVo>();
            try
            {
                var conditions = new StringBuilder(" where is_deleted=0 ");
                var param = new DynamicParameters();

                if (request.StartTime != null)
                {
                    conditions.Append(" AND (@startTime <= start_time OR @startTime <= end_time)");
                    param.Add("@startTime", request.StartTime.Value);
                }
                if (request.EndTime != null)
                {
                    conditions.Append(" AND (@endTime >= start_time OR @endTime >= end_time)");
                    param.Add("@endTime", request.EndTime.Value);
                }
                if (!string.IsNullOrWhiteSpace(request.TerminalType))
                {
                    param.Add("@terminal_type", request.TerminalType);
                    conditions.Append(" and terminal_type=@terminal_type");
                }
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    param.Add("@Title", request.Title);
                    conditions.Append(" and title=@Title");
                }

                var res = await _configAdvertisementRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", param);
                var vo = _mapper.Map<ApiPagedResultData<ConfigAdvertisementVo>>(res);

                result.Data = vo;

                result.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                result.Code = ResultCode.Exception;
            }
            return result;
        }

        public async Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var result = new ApiResult<ConfigAdvertisementVo>();
            try
            {
                var configAdvertisementDo = await _configAdvertisementRepository.GetAsync(request.Id);
                var vo = _mapper.Map<ConfigAdvertisementVo>(configAdvertisementDo);
                //移除下标
                vo.ImageUrl = configAdvertisementDo.ImageUrl;
                vo.RouteUrl = configAdvertisementDo.RouteUrl;
                result.Data = vo;
                result.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetConfigAdvertisement_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                result.Code = ResultCode.Exception;
            }
            return result;
        }

        public async Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var res = new ApiResult<string>();
            try
            {
                var vo = _mapper.Map<ConfigAdvertisementDo>(request);
                await _configAdvertisementRepository.UpdateAsync(vo, new List<string> { "Code", "Title", "ImageUrl", "BeginImageUrl" ,"BtnImageUrl" ,"Btn2ImageUrl" ,"Btn3ImageUrl" ,"EndImageUrl" ,
                    "RouteUrl","GotoUrl","Goto2Url","Goto3Url", "Type", "StartTime", "EndTime", "TerminalType", "Rank", "ExtendId", "UpdateBy", "UpdateTime" });
                res.Code = ResultCode.Success;
                res.Message = "修改成功";
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateConfigAdvertisement_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        #endregion

        /// <summary>
        ///获取前台一级类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ConfigFrontCategoryVo>> GetMainFrontCategory(MainFrontCategoryRequest request)
        {
            var result = await _configFrontCategoryRepository.GetConfigFrontCategoryByParentId(request.TerminalType, 0);

            return _mapper.Map<List<ConfigFrontCategoryVo>>(result);
        }

        /// <summary>
        /// 获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoVo>> GetHotProduct(HotProductRequest request)
        {
            List<ProductBaseInfoVo> result = new List<ProductBaseInfoVo>();
            var hotProduct = await _configHotProductRepository.GetConfigHotProduct(request.TerminalType);
            if (hotProduct.Any())
            {
                var hotPid = hotProduct.Select(_ => _.Pid).ToList();
                var productInfos = await _productRepository.GetProductByPidList(hotPid);

                result = _mapper.Map<List<ProductBaseInfoVo>>(productInfos.Where(_ => _.OnSale == 1))
                    .OrderBy(t => FindIndex(hotPid, t.ProductCode)).ToList();
            }

            return result;
        }

        private int FindIndex(List<string> pidList, string pid)
        {
            return pidList.FindIndex(t => t == pid);
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductVo>> GetHotProductPageList(
            HotProductPageListRequest request)
        {
            ApiPagedResultData<ConfigHotProductVo> response = new ApiPagedResultData<ConfigHotProductVo>()
            {
                Items = new List<ConfigHotProductVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<CategoryInfoVo> category = await _productManageService.GetAllCategoryFromCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var result = await _configHotProductRepository.GetHotProductPageList(new HotProductPageListCondition()
            {
                TerminalType = request.TerminalType,
                Key = request.Key,
                Brand = request.Brand,
                CategoryId = categoryId,
                OnSale = request.OnSale,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                response.Items = result.Items ?? new List<ConfigHotProductVo>();
            }

            return response;
        }

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductVo>> SearchProductForHot(
            SearchProductForHotRequest request)
        {
            ApiPagedResultData<ConfigHotProductVo> result = new ApiPagedResultData<ConfigHotProductVo>()
            {
                Items = new List<ConfigHotProductVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<CategoryInfoVo> category = await _productManageService.GetAllCategoryFromCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var productTask = _productRepository.SearchProductCommon(new SearchProductCommonCondition()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                OnSale = -1,
                CategoryId = categoryId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            var hotProductTask = _configHotProductRepository.GetConfigHotProduct(request.TerminalType.ToString());
            await Task.WhenAll(productTask, hotProductTask);
            var product = productTask.Result;
            var hotProduct = hotProductTask.Result ?? new List<ConfigHotProductDo>();
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    foreach (var itemP in product.Items)
                    {
                        var defaultD = hotProduct.FirstOrDefault(_ => _.Pid == itemP.ProductCode);
                        var itemD = _mapper.Map<ConfigHotProductVo>(itemP);
                        itemD.TerminalType = request.TerminalType;
                        if (defaultD != null)
                        {
                            itemD.HotProductId = defaultD.Id;
                            itemD.Rank = defaultD.Rank;
                        }

                        result.Items.Add(itemD);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddHotProduct(AddHotProductRequest request)
        {
            if (request.Products == null || !request.Products.Any())
            {
                throw new CustomException("产品不能为空");
            }

            var existProduct =
                await _configHotProductRepository.GetConfigHotProduct(request.TerminalType.ToString(), false);
            var existPid = existProduct.Select(_ => _.Pid).ToList();

            List<ConfigHotProductDo> add = request.Products.Where(_ => !existPid.Contains(_.Pid)).Select(_ =>
                new ConfigHotProductDo
                {
                    Pid = _.Pid,
                    Rank = _.Rank,
                    TerminalType = request.TerminalType.ToString(),
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                }).ToList();
            if (add.Any())
            {
                await _configHotProductRepository.InsertBatchAsync(add);
            }

            return true;
        }

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditHotProduct(EditHotProductRequest request)
        {
            ConfigHotProductDo update = new ConfigHotProductDo()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            List<string> filed = new List<string>();
            if (request.Rank.HasValue)
            {
                update.Rank = request.Rank.Value;
                filed.Add("Rank");
            }

            if (request.IsDeleted.HasValue)
            {
                update.IsDeleted = request.IsDeleted.Value;
                filed.Add("IsDeleted");
            }

            if (filed.Any())
            {
                filed.Add("UpdateBy");
                filed.Add("UpdateTime");

                await _configHotProductRepository.UpdateAsync(update, filed);
            }

            return true;
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductListByFrontCategoryIdResponse> GetProductListByFrontCategoryId(
            ProductListByFrontCategoryIdRequest request)
        {
            ApiPagedResultData<ProductBaseInfoVo> result = new ApiPagedResultData<ProductBaseInfoVo>()
            {
                Items = new List<ProductBaseInfoVo>()
            };
            var config =
                await _configFrontCategoryProductRepository.GetConfigFrontCategoryProductByCategoryId(
                    request.CategoryId);
            if (config != null && config.Any())
            {
                List<string> categoryId =
                    config.Where(_ => _.ProductType == "category").Select(_ => _.ProductId).ToList();
                List<string> pid = config.Where(_ => _.ProductType == "product").Select(_ => _.ProductId).ToList();
                List<string> brand = config.Where(_ => _.ProductType == "brand").Select(_ => _.ProductId).ToList();

                result = await _productManageService.GetProductByFrontCategoryConfig(categoryId, pid, brand,
                    request.PageIndex, request.PageSize);
            }

            return new ProductListByFrontCategoryIdResponse()
            {
                Products = result
            };
        }

        #region 套餐卡

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListRequest request)
        {
            ApiPagedResultData<PackageCardProductVo> result = new ApiPagedResultData<PackageCardProductVo>()
            {
                Items = new List<PackageCardProductVo>()
            };
            var productData = await _configPackageCardProductRepository.GetPackageCardProductPageList(
                new PackageCardProductPageListCondition()
                {
                    Type = request.Type,
                    OnSale = request.OnSale,
                    IsRetail = request.IsRetail,
                    Key = request.Key,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (productData != null)
            {
                result.TotalItems = productData.TotalItems;
                result.Items = productData.Items ?? new List<PackageCardProductVo>();
            }

            return result;
        }

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductVo>> SearchProductForPackageCard(
            SearchProductForPackageCardRequest request)
        {
            ApiPagedResultData<PackageCardProductVo> result = new ApiPagedResultData<PackageCardProductVo>()
            {
                Items = new List<PackageCardProductVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<CategoryInfoVo> category = await _productManageService.GetAllCategoryFromCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var productTask = _productRepository.SearchProductCommon(new SearchProductCommonCondition()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                OnSale = -1,
                CategoryId = categoryId,
                ProductAttribute = new List<int>() { 1 },
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            var packageCardTask = _configPackageCardProductRepository.GetListAsync("");

            await Task.WhenAll(productTask, packageCardTask);
            var product = productTask.Result;
            var packageCard = packageCardTask.Result?.AsList() ?? new List<ConfigPackageCardProductDO>();
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    foreach (var itemP in product.Items)
                    {
                        var defaultD = packageCard.FirstOrDefault(_ => _.Pid == itemP.ProductCode);
                        var itemD = _mapper.Map<PackageCardProductVo>(itemP);
                        if (defaultD != null)
                        {
                            itemD.PackageCardId = defaultD.Id;
                            itemD.Rank = defaultD.Rank;
                            itemD.Type = (PackageCardEnum)defaultD.Type;
                        }

                        result.Items.Add(itemD);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPackageCardProduct(AddPackageCardProductRequest request)
        {
            if (request.Products == null || !request.Products.Any())
            {
                throw new CustomException("产品不能为空");
            }

            var pidList = request.Products.Select(_ => _.Pid).ToList();
            var existProduct = await _configPackageCardProductRepository.GetPackageCardProductByPidList(pidList, false);
            var existPid = existProduct.Select(_ => _.Pid).ToList();
            List<ConfigPackageCardProductDO> add = request.Products.Where(_ => !existPid.Contains(_.Pid)).Select(_ =>
                new ConfigPackageCardProductDO
                {
                    Pid = _.Pid,
                    Rank = _.Rank,
                    Type = _.Type,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                }).ToList();
            if (add.Any())
            {
                await _configPackageCardProductRepository.InsertBatchAsync(add);
            }

            return true;
        }

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageCardProduct(EditPackageCardProductRequest request)
        {
            ConfigPackageCardProductDO update = new ConfigPackageCardProductDO()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            List<string> field = new List<string>();
            if (request.Rank.HasValue)
            {
                update.Rank = request.Rank.Value;
                field.Add("Rank");
            }

            if (request.Type.HasValue)
            {
                update.Type = request.Type.Value;
                field.Add("Type");
            }

            if (request.IsDeleted.HasValue)
            {
                update.IsDeleted = request.IsDeleted.Value;
                field.Add("IsDeleted");
            }

            if (field.Any())
            {
                field.Add("UpdateBy");
                field.Add("UpdateTime");
                await _configPackageCardProductRepository.UpdateAsync(update, field);
            }

            return true;
        }

        public async Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(GetShopPackageCardProductPageListRequest request)
        {
            ApiPagedResultData<GetShopPackageCardProductPageListVo> result = new ApiPagedResultData<GetShopPackageCardProductPageListVo>()
            {
                Items = new List<GetShopPackageCardProductPageListVo>()
            };
            var productData = await _configShopPackageCardRepository.GetShopPackageCardProductPageList(request);

            productData?.Items?.ToList()?.ForEach(_ =>
            {
                _.Image1 = $"{_configuration["ImageDomain"]}{_.Image1}";
            });

            if (productData != null)
            {
                result.TotalItems = productData.TotalItems;
                result.Items = productData.Items ?? new List<GetShopPackageCardProductPageListVo>();
            }

            return Result.Success(result.Items, result.TotalItems);
        }

        public async Task<ApiResult> SaveShopPackageCard(ApiRequest<ConfigShopPackageCardDTO> request)
        {
            if (request.Data.Id > 0)
            {
                var configShopPackageCardDO = _configShopPackageCardRepository.Get(request.Data.Id);

                configShopPackageCardDO.ShopIds = request.Data.ShopIds;

                configShopPackageCardDO.CardId = request.Data.CardId;

                configShopPackageCardDO.UpdateBy = request.Data.UpdateBy;
                configShopPackageCardDO.UpdateTime = DateTime.Now;

                configShopPackageCardDO.IsDeleted = request.Data.IsDeleted;

                await _configShopPackageCardRepository.UpdateAsync(configShopPackageCardDO, new List<string>() { "CardId", "ShopIds", "IsDeleted", "UpdateBy", "UpdateTime" });
            }
            else
            {
                var configShopPackageCardDO = _mapper.Map<ConfigShopPackageCardDO>(request.Data);
                configShopPackageCardDO.CreateTime = DateTime.Now;
                configShopPackageCardDO.IsDeleted = 0;

                var cards = await _configShopPackageCardRepository.GetListAsync(" where card_id=@CardId", new { CardId = request.Data.CardId });
                if (cards != null && cards.Any())
                    return Result.Failed("套餐卡已配置过门店");
                await _configShopPackageCardRepository.InsertAsync(configShopPackageCardDO);
            }

            return Result.Success();
        }

        public async Task<GetShopPackageCardProductPageListVo> GetShopCardDetail(ApiRequest<GetShopCardDetailRequest> request)
        {
            var data = await _configShopPackageCardRepository.GetShopCardDetail(request.Data);

            if (data != null)
            {
                data.Image1 = $"{_configuration["ImageDomain"]}{data.Image1}";
            }

            return data;

        }

        #endregion

        #region 美容团购商品管理

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductVo>> GetGrouponProductPageList(
            GrouponProductPageListRequest request)
        {
            ApiPagedResultData<GrouponProductVo> response = new ApiPagedResultData<GrouponProductVo>()
            {
                Items = new List<GrouponProductVo>()
            };
            var result = await _grouponProductConfigRepository.GetGrouponProductPageList(
                new GrouponProductPageListCondition()
                {
                    OnSale = request.OnSale,
                    Key = request.Key,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                response.Items = result.Items ?? new List<GrouponProductVo>();
            }

            return response;
        }

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductVo>> SearchProductForGroupon(
            SearchProductForGrouponRequest request)
        {
            ApiPagedResultData<GrouponProductVo> result = new ApiPagedResultData<GrouponProductVo>()
            {
                Items = new List<GrouponProductVo>()
            };
            List<int> categoryId = new List<int>();
            if (request.ChildCategoryId > 0)
            {
                categoryId.Add(request.ChildCategoryId);
            }
            else if (request.MainCategoryId > 0 || request.SecondCategoryId > 0)
            {
                List<CategoryInfoVo> category = await _productManageService.GetAllCategoryFromCache(0);
                if (request.SecondCategoryId > 0)
                {
                    categoryId = category.Where(_ => _.ParentId == request.SecondCategoryId).Select(_ => _.Id)
                        .ToList();
                }
                else if (request.MainCategoryId > 0)
                {
                    List<int> secondList = category.Where(_ => _.ParentId == request.MainCategoryId).Select(_ => _.Id)
                        .ToList();
                    categoryId =
                        category.Where(_ => secondList.Contains(_.ParentId)).Select(_ => _.Id).ToList();
                }
            }

            var productTask = _productRepository.SearchProductCommon(new SearchProductCommonCondition()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                OnSale = -1,
                CategoryId = categoryId,
                ProductAttribute = new List<int>() { 1,2,3 }, //实物产品以外都可以团购
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            var grouponProductTask = _grouponProductConfigRepository.GetListAsync("");
            await Task.WhenAll(productTask, grouponProductTask);
            var product = productTask.Result;
            var grouponProduct = grouponProductTask.Result?.AsList() ?? new List<GrouponProductConfigDO>();
            if (product != null)
            {
                result.TotalItems = product.TotalItems;
                if (product.Items != null && product.Items.Any())
                {
                    foreach (var itemP in product.Items)
                    {
                        var defaultD = grouponProduct.FirstOrDefault(_ => _.Pid == itemP.ProductCode);
                        var itemD = _mapper.Map<GrouponProductVo>(itemP);
                        if (defaultD != null)
                        {
                            itemD.GrouponId = defaultD.Id;
                            itemD.MinPrice = defaultD.MinPrice;
                            itemD.MaxPrice = defaultD.MaxPrice;
                        }

                        result.Items.Add(itemD);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddGrouponProduct(AddGrouponProductRequest request)
        {
            if (request.GrouponProduct == null || !request.GrouponProduct.Any())
            {
                throw new CustomException("请选择商品");
            }

            var existProduct = await _grouponProductConfigRepository.GetGrouponProductByPidList(
                request.GrouponProduct.Select(_ => _.Pid).ToList(), false);
            var existPid = existProduct.Select(_ => _.Pid).ToList();
            List<GrouponProductConfigDO> add = request.GrouponProduct.Where(_ => !existPid.Contains(_.Pid)).Select(_ =>
                new GrouponProductConfigDO()
                {
                    Pid = _.Pid,
                    MinPrice = _.MinPrice,
                    MaxPrice = _.MaxPrice,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now
                }).ToList();
            if (add.Any())
            {
                await _grouponProductConfigRepository.InsertBatchAsync(add);
            }

            return true;
        }

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditGrouponProduct(EditGrouponProductRequest request)
        {
            GrouponProductConfigDO update = new GrouponProductConfigDO()
            {
                Id = request.Id,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            List<string> field = new List<string>();
            if (request.MinPrice.HasValue)
            {
                update.MinPrice = request.MinPrice.Value;
                field.Add("MinPrice");
            }

            if (request.MaxPrice.HasValue)
            {
                update.MaxPrice = request.MaxPrice.Value;
                field.Add("MaxPrice");
            }

            if (request.IsDeleted.HasValue)
            {
                update.IsDeleted = request.IsDeleted.Value;
                field.Add("IsDeleted");
            }

            if (field.Any())
            {
                field.Add("UpdateBy");
                field.Add("UpdateTime");
                await _grouponProductConfigRepository.UpdateAsync(update, field);
            }

            return true;
        }






        #endregion
    }
}
