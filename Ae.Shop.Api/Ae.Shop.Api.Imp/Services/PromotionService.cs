using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Promotion;
using Ae.Shop.Api.Core.Request.Promotion;
using Ae.Shop.Api.Dal.Model.Product;
using Ae.Shop.Api.Dal.Model.Promotion;
using Ae.Shop.Api.Dal.Repositorys.Product;
using Ae.Shop.Api.Dal.Repositorys.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ae.Shop.Api.Dal.Model.Product.Extend;
using Ae.Shop.Api.Dal.Model.ShopManage;
using Ae.Shop.Api.Dal.Repositorys.ShopManage;

namespace Ae.Shop.Api.Imp.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionActivityProductRepository promotionActivityProductRepository;
        private readonly IIdentityService identityService;
        private readonly IPromotionActivityRepository promotionActivityRepository;
        private readonly IFctShopProductRepository fctShopProductRepository;
        private readonly IConfiguration configuration;
        private readonly IPromotionActivityShopRepository promotionActivityShopRepository;
        private readonly ApolloErpLogger<PromotionService> logger;
        private readonly IGrouponProductConfigRepository grouponProductConfigRepository;
        private readonly IShopGrouponProductRepository shopGrouponProductRepository;

        public PromotionService(IPromotionActivityProductRepository promotionActivityProductRepository,
            IIdentityService identityService, IPromotionActivityRepository promotionActivityRepository,
            IFctShopProductRepository fctShopProductRepository, IConfiguration configuration,
            IPromotionActivityShopRepository promotionActivityShopRepository, ApolloErpLogger<PromotionService> logger,
            IGrouponProductConfigRepository grouponProductConfigRepository,
            IShopGrouponProductRepository shopGrouponProductRepository)
        {
            this.promotionActivityProductRepository = promotionActivityProductRepository;
            this.identityService = identityService;
            this.promotionActivityRepository = promotionActivityRepository;
            this.fctShopProductRepository = fctShopProductRepository;
            this.configuration = configuration;
            this.promotionActivityShopRepository = promotionActivityShopRepository;
            this.logger = logger;
            this.grouponProductConfigRepository = grouponProductConfigRepository;
            this.shopGrouponProductRepository = shopGrouponProductRepository;
        }

        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PromotionActivityListVo>> SearchPromotionActivity(SearchPromotionActivityRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            DateTime now = DateTime.Now;
            ApiPagedResultData<PromotionActivityListVo> result = new ApiPagedResultData<PromotionActivityListVo>();
            DateTime? startTime = null;
            DateTime? endTime = null;
            List<string> activityIds = new List<string>();
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                DateTime.TryParse(request.StartDate, out var tempTime);
                startTime = tempTime;
            }
            if (!string.IsNullOrEmpty(request.EndDate))
            {
                DateTime.TryParse(request.EndDate, out var tempTime);
                endTime = tempTime;
            }
            if (!string.IsNullOrEmpty(request.ProductCode))
            {
                var activityProduct = await promotionActivityProductRepository.GetPromotionProductByPid(request.ProductCode);
                if (activityProduct != null && activityProduct.Any())
                {
                    activityIds = activityProduct.Select(_ => _.ActivityId.ToString()).ToList();
                }
            }
            var promotionResult = await promotionActivityRepository.SearchPromotionActivity(new SearchPromotionCondition
            {
                ActivityId = request.ActivityId,
                ActivityName = request.ActivityName,
                StartTime = startTime,
                EndTime = endTime,
                Status = request.Status,
                ActivityIds = activityIds,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                DateTimeNow = now,
                ShopId = shopId
            });
            result.TotalItems = promotionResult.TotalItems;
            List<PromotionActivityListVo> activityData = new List<PromotionActivityListVo>();
            if (promotionResult.Items != null && promotionResult.Items.Any())
            {
                foreach (var itemActivity in promotionResult.Items)
                {
                    var status = TransferStatus(itemActivity.AuditStatus, itemActivity.StartTime, itemActivity.EndTime, now);
                    activityData.Add(new PromotionActivityListVo
                    {
                        ActivityId = itemActivity.Id.ToString(),
                        Name = itemActivity.Name,
                        Subtitle = itemActivity.Subtitle,
                        StartTime = itemActivity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        EndTime = itemActivity.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = itemActivity.CreateBy,
                        Status = status.Item1,
                        StatusDisplay = status.Item2
                    });
                }
            }
            result.Items = activityData;

            return result;
        }

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public async Task<PromotionActivityDetailVo> GetPromotionActivityDetail(PromotionActivityDetailRequest request)
        {
            PromotionActivityDetailVo result = null;
            var resultPromotionTask = promotionActivityRepository.GetPromotionDetail(request.ActivityId);
            var resultProductTsk = promotionActivityProductRepository.GetPromotionProductByActivityId(request.ActivityId);
            await Task.WhenAll(resultPromotionTask, resultProductTsk);

            var resultPromotion = resultPromotionTask.Result;
            var resultProduct = resultProductTsk.Result ?? new List<PromotionActivityProductDo>();
            if (resultPromotion != null)
            {
                List<FctShopProductDO> products = new List<FctShopProductDO>();
                if (resultProduct.Any())
                {
                    var pidList = resultProduct.Select(_ => _.ProductCode).Distinct().ToList();
                    products = await fctShopProductRepository.GetShopProductByProductCodes(pidList);
                }
                var status = TransferStatus(resultPromotion.AuditStatus, resultPromotion.StartTime, resultPromotion.EndTime, DateTime.Now);
                result = new PromotionActivityDetailVo
                {
                    ActivityId = resultPromotion.Id.ToString(),
                    Name = resultPromotion.Name,
                    Subtitle = resultPromotion.Subtitle,
                    StartTime = resultPromotion.StartTime.ToString("yyyy-MM-dd"),
                    EndTime = resultPromotion.EndTime.AddDays(-1).ToString("yyyy-MM-dd"),
                    Status = status.Item1,
                    StatusDisplay = status.Item2,
                    Label = resultPromotion.Label,
                    PromotionType = resultPromotion.PromotionType.ToString(),
                    PromotionChannel = resultPromotion.ApplyChannel.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Description = resultPromotion.Description,
                    ThumbUrl = resultPromotion.ThumbUrl,
                    Products = resultProduct.Select(_ => new PromotionProductVo
                    {
                        ProductCode = _.ProductCode,
                        ProductName = _.ProductName,
                        PromotionPrice = _.PromotionPrice,
                        LimitQuantity = _.LimitQuantity,
                        SoldQuantity = _.SoldQuantity,
                        Price = products.FirstOrDefault(t => t.ProductCode == _.ProductCode)?.SalesPrice ?? 0
                    }).ToList()
                };
            }

            return result;
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="request">门店商品编码</param>
        /// <returns></returns>
        public async Task<ShopProductVo> GetShopProductByCode(ShopProductByCodeRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var result = await fctShopProductRepository.GetShopProductByProductCode(request.ShopProductCode, shopId);

            if (result != null)
            {
                var defaultUrl = configuration["DefaultIcon"];
                ShopProductVo data = new ShopProductVo
                {
                    ShopId = result.ShopId,
                    ProductCode = result.ProductCode,
                    ProductName = result.ProductName,
                    DisplayName = result.DisplayName,
                    Description = result.Description,
                    StandardPrice = result.StandardPrice,
                    SalesPrice = result.SalesPrice,
                    OnSale = result.OnSale,
                    Icon = string.IsNullOrEmpty(result.Icon) ? defaultUrl : result.Icon
                };

                return data;
            }

            return null;
        }

        /// <summary>
        /// 商品搜索（根据Pid或商品名称）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopProductVo>> SearchProductByNameOrCode(SearchProductByNameOrCodeRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var result = await fctShopProductRepository.GetShopProductsByNameOrCode(request.SearchContent, shopId);

            var defaultUrl = configuration["DefaultIcon"];
            return result.Select(_ => new ShopProductVo
            {
                ShopId = _.ShopId,
                ProductCode = _.ProductCode,
                ProductName = _.ProductName,
                DisplayName = _.DisplayName,
                Description = _.Description,
                StandardPrice = _.StandardPrice,
                SalesPrice = _.SalesPrice,
                OnSale = _.OnSale,
                Icon = string.IsNullOrEmpty(_.Icon) ? defaultUrl : _.Icon
            }).ToList();
        }

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddPromotionActivity(AddPromotionActivityRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            string createdBy = identityService.GetUserName();
            DateTime.TryParse(request.StartTime, out var startTime);
            DateTime.TryParse(request.EndTime, out var endTime);

            var isExistValidActivity = await IsExistValidActivity(request.ActivityProduct.Select(_ => _.ProductCode).ToList());
            if (isExistValidActivity)
            {
                throw new CustomException("当前商品已存在有效活动");
            }

            if (startTime.CompareTo(endTime) >= 0)
            {
                throw new CustomException("结束时间必须大于开始时间");
            }

            Guid activityId = Guid.NewGuid();
            PromotionActivityDo promotionActivityDo = new PromotionActivityDo
            {
                Id = activityId,
                Name = request.Name,
                Subtitle = request.Subtitle,
                Description = request.Description,
                ThumbUrl = request.ThumbUrl,
                ActivityType = request.ActivityType,
                PromotionType = request.PromotionType,
                Label = request.Label,
                StartTime = startTime,
                EndTime = endTime.AddDays(1),
                ApplyChannel = string.Join(",", request.ApplyChannel ?? new List<string>()),
                AuditStatus = 0,
                CreateBy = createdBy,
                CreateTime = DateTime.Now
            };

            using (TransactionScope ts = new TransactionScope())
            {
                activityId = await promotionActivityRepository.InsertAsync<Guid>(promotionActivityDo);
                if (activityId != Guid.Empty)
                {
                    if (request.ActivityProduct != null && request.ActivityProduct.Any())
                    {
                        List<PromotionActivityProductDo> promotionActivityProductDo = request.ActivityProduct.Select(_ => new PromotionActivityProductDo
                        {
                            ActivityId = activityId,
                            ProductCode = _.ProductCode,
                            ProductName = _.ProductName,
                            PromotionPrice = _.PromotionPrice,
                            LimitQuantity = _.LimitQuantity,
                            CreateBy = createdBy,
                            CreateTime = DateTime.Now
                        }).ToList();
                        await promotionActivityProductRepository.InsertBatchAsync(promotionActivityProductDo);
                    }

                    List<PromotionActivityShopDo> promotionActivityShopDo = new List<PromotionActivityShopDo> {
                        new PromotionActivityShopDo
                        {
                            ActivityId = activityId,
                            ShopId = shopId,
                            CreateBy = createdBy,
                            CreateTime = DateTime.Now
                        }
                    };

                    await promotionActivityShopRepository.InsertBatchAsync(promotionActivityShopDo);
                }
                ts.Complete();
            }

            return activityId.ToString();
        }

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> FinishPromotion(FinishPromotionRequest request)
        {
            Guid.TryParse(request.ActivityId, out var activityId);

            PromotionActivityDo promotionActivityDo = new PromotionActivityDo
            {
                Id = activityId,
                AuditStatus = 3,
                UpdateBy = identityService.GetUserName(),
                UpdateTime = DateTime.Now
            };

            var result = await promotionActivityRepository.UpdateAsync(promotionActivityDo, new List<string> { "AuditStatus", "UpdateBy", "UpdateTime" });
            logger.Info($"FinishPromotion Para={JsonConvert.SerializeObject(request)} Result={JsonConvert.SerializeObject(result)}");
            return result > 0;
        }

        /// <summary>
        /// 编辑促销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPromotion(EditPromotionRequest request)
        {
            string createdBy = identityService.GetUserName();
            DateTime.TryParse(request.StartTime, out var startTime);
            DateTime.TryParse(request.EndTime, out var endTime);
            Guid.TryParse(request.ActivityId, out var activityId);
            var detail = await promotionActivityRepository.GetPromotionDetail(request.ActivityId, false);
            if (detail == null)
            {
                throw new CustomException("活动不存在！");
            }
            if (detail.AuditStatus != 0)
            {
                throw new CustomException("活动已审核，不能修改！");
            }
            if (startTime.CompareTo(endTime) >= 0)
            {
                throw new CustomException("结束时间必须大于开始时间");
            }
            PromotionActivityDo promotionActivityDo = new PromotionActivityDo
            {
                Id = activityId,
                Name = request.Name,
                Subtitle = request.Subtitle,
                Description = request.Description,
                ThumbUrl = request.ThumbUrl,
                ActivityType = request.ActivityType,
                PromotionType = request.PromotionType,
                Label = request.Label,
                StartTime = startTime,
                EndTime = endTime.AddDays(1),
                ApplyChannel = string.Join(",", request.ApplyChannel ?? new List<string>()),
                UpdateBy = createdBy,
                UpdateTime = DateTime.Now
            };

            using (TransactionScope ts = new TransactionScope())
            {
                var result = await promotionActivityRepository.UpdateAsync(promotionActivityDo,
                    new List<string>
                    {
                        "Name","Subtitle","Description","ThumbUrl","ActivityType","PromotionType","Label",
                        "StartTime","EndTime","ApplyChannel","UpdateBy","UpdateTime"
                    });
                if (result > 0)
                {
                    await promotionActivityProductRepository.DeletePromotionProduct(request.ActivityId, createdBy);
                    if (request.ActivityProduct != null && request.ActivityProduct.Any())
                    {
                        List<PromotionActivityProductDo> promotionActivityProductDo = request.ActivityProduct.Select(_ => new PromotionActivityProductDo
                        {
                            ActivityId = activityId,
                            ProductCode = _.ProductCode,
                            ProductName = _.ProductName,
                            PromotionPrice = _.PromotionPrice,
                            LimitQuantity = _.LimitQuantity,
                            CreateBy = createdBy,
                            CreateTime = DateTime.Now
                        }).ToList();
                        await promotionActivityProductRepository.InsertBatchAsync(promotionActivityProductDo);
                    }
                }
                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 校验商品是否已存在有效活动
        /// </summary>
        /// <param name="pidList"></param>
        /// <returns></returns>
        private async Task<bool> IsExistValidActivity(List<string> pidList)
        {
            DateTime now = DateTime.Now;
            var productList = await promotionActivityRepository.GetPromotionByPidList(pidList, false);
            productList = productList.Where(_ => now.CompareTo(_.EndTime) < 0 && (_.AuditStatus == 0 || _.AuditStatus == 1)).ToList();
            return productList.Count > 0;
        }

        /// <summary>
        /// 促销状态转换
        /// </summary>
        /// <param name="auditStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        private Tuple<int, string> TransferStatus(int auditStatus, DateTime startTime, DateTime endTime, DateTime now)
        {
            int status = 0;
            string statusDisplay = string.Empty;
            if (auditStatus == 0)
            {
                status = 1;
                statusDisplay = "待审核";
            }
            else if (auditStatus == 2)
            {
                status = 2;
                statusDisplay = "已拒绝";
            }
            else if (auditStatus == 1)
            {
                if (now < startTime)
                {
                    status = 3;
                    statusDisplay = "未开始";
                }
                else if (now < endTime)
                {
                    status = 4;
                    statusDisplay = "进行中";
                }
                else
                {
                    status = 5;
                    statusDisplay = "已结束";
                }
            }
            else
            {
                status = 5;
                statusDisplay = "已结束";
            }
            return new Tuple<int, string>(status, statusDisplay);
        }

        #region 美容团购

        /// <summary>
        /// 查询门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        public async Task<ApiPagedResultData<GrouponProductPageVo>> GetGrouponProductPageList(
            GrouponProductPageListRequest request)
        {
            List<GrouponProductPageVo> data = new List<GrouponProductPageVo>();
            Int32.TryParse(identityService.GetOrganizationId(), out int shopId);
            var productTask = grouponProductConfigRepository.SearchGrouponProduct(request.ProductName);
            var shopGrouponTask = shopGrouponProductRepository.GetShopGrouponProduct(shopId, null);
            await Task.WhenAll(productTask, shopGrouponTask);
            var product = productTask.Result ?? new List<GrouponProductDto>();
            var shopGroupon = shopGrouponTask.Result ?? new List<ShopGrouponProductDO>();
            product.ForEach(_ =>
            {
                var defaultS = shopGroupon.FirstOrDefault(t => t.ServiceId == _.ProductCode);
                GrouponProductPageVo itemG = new GrouponProductPageVo()
                {
                    Pid = _.ProductCode,
                    Name = _.Name,
                    OriginalPrice = _.SalesPrice,
                    Price = 0,
                    MinPrice = _.MinPrice,
                    MaxPrice = _.MaxPrice,
                    Status = 0
                };
                if (defaultS != null)
                {
                    itemG.Price = defaultS.Price;
                    itemG.Status = defaultS.Status;
                }

                if (request.Status.HasValue)
                {
                    if (request.Status.Value == itemG.Status)
                    {
                        data.Add(itemG);
                    }
                }
                else
                {
                    data.Add(itemG);
                }
            });

            return new ApiPagedResultData<GrouponProductPageVo>()
            {
                TotalItems = data.Count,
                Items = data.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList()
            };
        }

        /// <summary>
        /// 美容团购产品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GrouponProductPageVo> GetGrouponProductDetail(GrouponProductDetailRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out int shopId);
            var productDetailTask = grouponProductConfigRepository.GetGrouponProductDetail(request.Pid);
            var shopGrouponDetailTask = shopGrouponProductRepository.GetShopGrouponProductDetail(shopId, request.Pid);
            await Task.WhenAll(productDetailTask, shopGrouponDetailTask);
            var productDetail = productDetailTask.Result;
            var shopGrouponDetail = shopGrouponDetailTask.Result;
            if (productDetail == null)
            {
                throw new CustomException("当前产品已下架/已禁用");
            }

            decimal price = 0;
            sbyte status = 0;
            if (shopGrouponDetail != null)
            {
                price = shopGrouponDetail.Price;
                status = shopGrouponDetail.Status;
            }

            return new GrouponProductPageVo()
            {
                Pid = productDetail.ProductCode,
                Name = productDetail.Name,
                OriginalPrice = productDetail.SalesPrice,
                Price = price,
                MinPrice = productDetail.MinPrice,
                MaxPrice = productDetail.MaxPrice,
                Status = status
            };
        }

        /// <summary>
        /// 保存门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveShopGrouponProduct(SaveShopGrouponProductRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out int shopId);
            var productDetailTask = grouponProductConfigRepository.GetGrouponProductDetail(request.Pid);
            var shopGrouponDetailTask =
                shopGrouponProductRepository.GetShopGrouponProductDetail(shopId, request.Pid, false);
            await Task.WhenAll(productDetailTask, shopGrouponDetailTask);
            var productDetail = productDetailTask.Result;
            var shopGrouponDetail = shopGrouponDetailTask.Result;
            if (productDetail == null)
            {
                throw new CustomException("当前产品已下架/已禁用");
            }

            if (request.Status == 1)
            {
                if (request.Price < productDetail.MinPrice || request.Price > productDetail.MaxPrice)
                {
                    throw new CustomException($"售价超出限制范围：{productDetail.MinPrice} ~ {productDetail.MaxPrice}");
                }
            }

            if (shopGrouponDetail != null)
            {
                await shopGrouponProductRepository.UpdateAsync(new ShopGrouponProductDO()
                {
                    Id = shopGrouponDetail.Id,
                    Price = request.Price,
                    Status = request.Status,
                    UpdateBy = identityService.GetUserName(),
                    UpdateTime = DateTime.Now
                }, new List<string>()
                {
                    "Price", "Status", "UpdateBy", "UpdateTime"
                });
            }
            else
            {
                await shopGrouponProductRepository.InsertAsync(new ShopGrouponProductDO()
                {
                    ShopId = (long) shopId,
                    ServiceId = productDetail.ProductCode,
                    ServiceName = productDetail.Name,
                    Price = request.Price,
                    Status = request.Status,
                    CreateBy = identityService.GetUserName(),
                    CreateTime = DateTime.Now
                });
            }

            return true;
        }

        #endregion
    }
}
