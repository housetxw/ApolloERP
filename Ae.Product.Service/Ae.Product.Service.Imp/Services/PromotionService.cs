using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Common.Exceptions;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model.Promotion;
using Ae.Product.Service.Core.Request.Promotion;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Promotion;
using Ae.Product.Service.Dal.Repository;
using Ae.Product.Service.Dal.Repository.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ae.Product.Service.Imp.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionActivityRepository _promotionActivityRepository;
        private readonly IPromotionActivityProductRepository _promotionActivityProductRepository;
        private readonly IFctShopProductRepository _fctShopProductRepository;
        private readonly IPromotionActivityShopRepository _promotionActivityShopRepository;
        private readonly IPromotionActivityOrderRepository _promotionActivityOrderRepository;
        private readonly IShopProductManageService _shopProductManageService;
        private readonly IConfiguration _configuration;
        private readonly IServiceTypeEnumRepository _serviceTypeEnumRepository;
        private readonly ApolloErpLogger<PromotionService> _logger;

        public PromotionService(IPromotionActivityRepository promotionActivityRepository,
            IPromotionActivityProductRepository promotionActivityProductRepository,
            IFctShopProductRepository fctShopProductRepository,
            IPromotionActivityShopRepository promotionActivityShopRepository,
            IPromotionActivityOrderRepository promotionActivityOrderRepository,
            IShopProductManageService shopProductManageService, IConfiguration configuration,
            IServiceTypeEnumRepository serviceTypeEnumRepository, ApolloErpLogger<PromotionService> logger)
        {
            _promotionActivityRepository = promotionActivityRepository;
            _promotionActivityProductRepository = promotionActivityProductRepository;
            _fctShopProductRepository = fctShopProductRepository;
            _promotionActivityShopRepository = promotionActivityShopRepository;
            _promotionActivityOrderRepository = promotionActivityOrderRepository;
            _shopProductManageService = shopProductManageService;
            _configuration = configuration;
            _serviceTypeEnumRepository = serviceTypeEnumRepository;
            _logger = logger;
        }

        /// <summary>
        /// 促销列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PromotionBriefVo>> GetPromotionList(PromotionListRequest request)
        {
            ApiPagedResultData<PromotionBriefVo> result = new ApiPagedResultData<PromotionBriefVo>
            {
                TotalItems = 0,
                Items = new List<PromotionBriefVo>()
            };
            Tuple<int, List<PromotionDetailDo>> promotionData = new Tuple<int, List<PromotionDetailDo>>(0, new List<PromotionDetailDo>());
            if (request.Status == 0)
            {
                promotionData = await _promotionActivityRepository.GetValidPromotionActivity(request.ShopId, request.PageIndex, request.PageSize);
            }
            else if (request.Status == 1)
            {
                promotionData = await _promotionActivityRepository.GetFinishPromotionActivity(request.ShopId, request.PageIndex, request.PageSize);
            }
            var totalCount = promotionData.Item1;
            var promotionList = promotionData.Item2;
            if (promotionList.Any())
            {
                var activityList = promotionList.Select(_ => _.ActivityId).ToList();
                List<FctShopProductDO> products = new List<FctShopProductDO>();
                var activityProduct = await _promotionActivityProductRepository.GetPromotionProductByActivityIds(activityList);
                if (activityProduct.Any())
                {
                    var pidList = activityProduct.Select(_ => _.ProductCode).Distinct().ToList();
                    products = await _fctShopProductRepository.GetShopProductByProductCodes(pidList);
                }

                promotionList.ForEach(_ =>
                {
                    var itemPromotion = activityProduct.Where(t => t.ActivityId == _.ActivityId);
                    var itemPid = itemPromotion.Select(t => t.ProductCode).ToList();
                    PromotionBriefVo itemActivity = new PromotionBriefVo
                    {
                        ActivityId = _.ActivityId.ToString(),
                        Name = _.Name,
                        Subtitle = _.Subtitle,
                        StartTime = _.StartTime.ToString("yyyy-MM-dd"),
                        EndTime = _.EndTime.AddDays(-1).ToString("yyyy-MM-dd"),
                        ThumbUrl = GetImageUrl(_.ThumbUrl),
                        PromotionPrice = itemPromotion.Sum(t => t.PromotionPrice),
                        OriginalPrice = products.Where(t => itemPid.Contains(t.ProductCode)).Sum(t => t.SalesPrice)
                    };

                    result.Items.Add(itemActivity);
                });

                result.TotalItems = totalCount;
            }

            return result;
        }

        private string GetImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (imageUrl.StartsWith("https://"))
                {
                    return imageUrl;
                }
                else
                {
                    return _configuration["ImageDomain"] + imageUrl;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 促销详情（B端）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request)
        {
            PromotionDetailVo result = null;
            var promotionMainTask = _promotionActivityRepository.GetPromotionDetail(request.ActivityId);
            var promotionProductTask = _promotionActivityProductRepository.GetPromotionProductByActivityId(request.ActivityId);
            await Task.WhenAll(promotionMainTask, promotionProductTask);
            var promotionMain = promotionMainTask.Result;
            var promotionProduct = promotionProductTask.Result ?? new List<PromotionActivityProductDo>();
            if (promotionMain != null)
            {
                int availQuantity = 0;
                int status = -1;
                DateTime now = DateTime.Now;
                if (promotionMain.AuditStatus == 1)
                {
                    if (now >= promotionMain.EndTime)
                    {
                        status = 1;
                    }
                    else if (now >= promotionMain.StartTime)
                    {
                        status = 0;
                    }
                }
                else if (promotionMain.AuditStatus == 3 || promotionMain.AuditStatus == 4)
                {
                    status = 1;
                }
                List<FctShopProductDO> products = new List<FctShopProductDO>();
                if (promotionProduct.Any())
                {
                    availQuantity = promotionProduct[0].LimitQuantity - promotionProduct[0].SoldQuantity;
                    if (availQuantity < 0)
                    {
                        availQuantity = 0;
                    }
                    var pidList = promotionProduct.Select(_ => _.ProductCode).Distinct().ToList();
                    products = await _fctShopProductRepository.GetShopProductByProductCodes(pidList);
                }
                result = new PromotionDetailVo
                {
                    ActivityId = promotionMain.Id.ToString(),
                    Name = promotionMain.Name,
                    Subtitle = promotionMain.Subtitle,
                    StartTime = promotionMain.StartTime.ToString("yyyy-MM-dd"),
                    EndTime = promotionMain.EndTime.AddDays(-1).ToString("yyyy-MM-dd"),
                    ThumbUrl = GetImageUrl(promotionMain.ThumbUrl),
                    Description = promotionMain.Description,
                    PromotionPrice = promotionProduct.Sum(t => t.PromotionPrice),
                    OriginalPrice = products.Sum(t => t.SalesPrice),
                    AvailQuantity = availQuantity,
                    Status = status,
                    DefaultPid = promotionProduct.FirstOrDefault()?.ProductCode ?? string.Empty
                };
            }

            return result;
        }

        /// <summary>
        /// 结束促销
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
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            var result = await _promotionActivityRepository.UpdateAsync(promotionActivityDo, new List<string> { "AuditStatus", "UpdateBy", "UpdateTime" });
            _logger.Info($"FinishPromotion Para={JsonConvert.SerializeObject(request)} Result={JsonConvert.SerializeObject(result)}");
            return result > 0;
        }

        /// <summary>
        /// 商品促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductPromotionDetailVo> GetProductPromotionDetail(ProductPromotionDetailRequest request)
        {
            DateTime now = DateTime.Now;
            var activity = await _promotionActivityRepository.GetPromotionDetail(request.ActivityId);
            if (activity == null)
            {
                throw new CustomException("该活动已取消");
            }
            var status = activity.AuditStatus;
            switch (status)
            {
                case 1:
                    if (now.CompareTo(activity.EndTime) >= 0)
                    {
                        throw new CustomException("该活动已结束");
                    }
                    if (now.CompareTo(activity.StartTime) < 0)
                    {
                        throw new CustomException("该活动未开始");
                    }
                    break;
                case 3:
                    throw new CustomException("该活动已结束");
                case 4:
                    throw new CustomException("该活动已结束");
                default:
                    throw new CustomException("该活动已取消");
            }

            var productPromotion = await _promotionActivityProductRepository.GetPromotionProductDetail(request.ActivityId, request.Pid);
            var product = _shopProductManageService.GetShopProductByCode(request.Pid);
            if (productPromotion == null)
            {
                throw new CustomException("该商品已结束活动");
            }
            if (product == null)
            {
                throw new CustomException("该商品已下架");
            }
            if (product.OnSale == 0)
            {
                throw new CustomException("该商品已下架");
            }
            var serviceType = await _serviceTypeEnumRepository.GetAsync<ServiceTypeEnumDo>(product.CategoryId);//服务大类查询
            ProductPromotionDetailVo result = new ProductPromotionDetailVo
            {
                ActivityId = activity.Id.ToString(),
                StartTime = activity.StartTime.ToString("yyyy.M.d"),
                EndTime = activity.EndTime.AddDays(-1).ToString("yyyy.M.d"),
                Name = activity.Name,
                Subtitle = activity.Subtitle,
                Description = activity.Description,
                IconUrl = GetImageUrl(product.Icon),
                //ImageUrl = product.Images?.Select(t => GetImageUrl(t))?.ToList() ?? new List<string>(),
                CategoryId = product.CategoryId,
                ServiceCode = serviceType?.ServiceType ?? string.Empty,
                ServiceName = serviceType?.DisplayName ?? string.Empty,
                PromotionPrice = productPromotion.PromotionPrice,
                OriginalPrice = product.SalesPrice,
                AvailQuantity = productPromotion.LimitQuantity - productPromotion.SoldQuantity,
                Tags = new List<Core.Model.TagInfo>
                {
                    new Core.Model.TagInfo
                    {
                        Tag = activity.Label,
                        TagColor = "#FF0000",
                        Type = "Promotion"
                    }
                }
            };
            return result;
        }

        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PromotionActivityListVo>> SearchPromotionActivity(SearchPromotionActivityRequest request)
        {
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
                var activityProduct = await _promotionActivityProductRepository.GetPromotionProductByPid(request.ProductCode);
                if (activityProduct != null && activityProduct.Any())
                {
                    activityIds = activityProduct.Select(_ => _.ActivityId.ToString()).ToList();
                }
            }
            var promotionResult = await _promotionActivityRepository.SearchPromotionActivity(new SearchPromotionCondition
            {
                ActivityId = request.ActivityId,
                ActivityName = request.ActivityName,
                StartTime = startTime,
                EndTime = endTime,
                Status = request.Status,
                ActivityIds = activityIds,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                DateTimeNow = now
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
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddPromotionActivity(AddPromotionActivityRequest request)
        {
            var isExistValidActivity = await IsExistValidActivity(request.ActivityProduct.Select(_ => _.ProductCode).ToList());
            if (isExistValidActivity)
            {
                throw new CustomException("当前商品已存在有效活动");
            }
            if (request.StartTime.CompareTo(request.EndTime) >= 0)
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
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                ApplyChannel = string.Join(",", request.ApplyChannel ?? new List<string>()),
                AuditStatus = 0,
                CreateBy = request.CreateBy,
                CreateTime = DateTime.Now
            };
            using (TransactionScope ts = new TransactionScope())
            {
                activityId = await _promotionActivityRepository.InsertAsync<Guid>(promotionActivityDo);
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
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now
                        }).ToList();
                        await _promotionActivityProductRepository.InsertBatchAsync(promotionActivityProductDo);
                    }
                    if (request.ShopIds != null && request.ShopIds.Any())
                    {
                        List<PromotionActivityShopDo> promotionActivityShopDo = request.ShopIds.Select(_ => new PromotionActivityShopDo
                        {
                            ActivityId = activityId,
                            ShopId = _,
                            CreateBy = request.CreateBy,
                            CreateTime = DateTime.Now
                        }).ToList();
                        await _promotionActivityShopRepository.InsertBatchAsync(promotionActivityShopDo);
                    }

                    await AuditPromotionActivity(activityId, "自动审核", request.CreateBy, 1);
                }
                ts.Complete();
            }

            return activityId.ToString();
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AuditPromotionActivity(AuditPromotionActivityRequest request)
        {
            Guid.TryParse(request.ActivityId, out var activityId);

            var result = await AuditPromotionActivity(activityId, "人工审核审核", request.SubmitBy, 1);

            _logger.Info($"AuditPromotionActivity Para={JsonConvert.SerializeObject(request)} Result={JsonConvert.SerializeObject(result)}");

            return result > 0;
        }

        /// <summary>
        /// 校验商品是否已存在有效活动
        /// </summary>
        /// <param name="pidList"></param>
        /// <returns></returns>
        private async Task<bool> IsExistValidActivity(List<string> pidList)
        {
            DateTime now = DateTime.Now;
            var productList = await _promotionActivityRepository.GetPromotionByPidList(pidList, false);
            productList = productList.Where(_ => now.CompareTo(_.EndTime) < 0 && (_.AuditStatus == 0 || _.AuditStatus == 1)).ToList();
            return productList.Count > 0;
        }

        /// <summary>
        /// 活动审核
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="auditRemark"></param>
        /// <param name="auditBy"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        private async Task<int> AuditPromotionActivity(Guid activityId, string auditRemark, string auditBy, int auditStatus)
        {
            DateTime now = DateTime.Now;
            var activityDetail = await _promotionActivityRepository.GetPromotionDetail(activityId.ToString(), false);
            if (activityDetail == null)
            {
                throw new CustomException("活动不存在");
            }
            var status = activityDetail.AuditStatus;
            if (status == 1)
            {
                throw new CustomException("活动已审核");
            }
            else if (status == 2)
            {
                throw new CustomException("活动已拒绝审核");
            }
            else if (status > 2)
            {
                throw new CustomException("活动已结束");
            }
            else if (now.CompareTo(activityDetail.EndTime) >= 0)
            {
                throw new CustomException("活动已结束");
            }
            PromotionActivityDo promotionActivityDo = new PromotionActivityDo
            {
                Id = activityId,
                AuditStatus = auditStatus,
                AuditBy = auditBy,
                AuditRemark = auditRemark,
                AuditTime = now,
                UpdateBy = auditBy,
                UpdateTime = now
            };
            return await _promotionActivityRepository.UpdateAsync(promotionActivityDo, new List<string> { "AuditStatus", "AuditBy", "AuditRemark", "AuditTime", "UpdateBy", "UpdateTime" });
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

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductPromotionInfoVo>> GetPromotionActivitByProductCodeList(PromotionActivitByProductCodeListRequest request)
        {
            DateTime now = DateTime.Now;
            List<ProductPromotionInfoVo> result = new List<ProductPromotionInfoVo>();
            if (request.ProductCodeList == null || !request.ProductCodeList.Any())
            {
                return result;
            }
            var productList = await _promotionActivityRepository.GetPromotionByPidList(request.ProductCodeList, false);
            productList = productList.Where(_ => _.AuditStatus == 1 && now.CompareTo(_.StartTime) >= 0 && now.CompareTo(_.EndTime) < 0 && _.ApplyChannel.Split(new char[] { ',' }).Contains(request.PromotionChannel)).ToList();

            result = productList.Select(_ => new ProductPromotionInfoVo
            {
                ProductCode = _.ProductCode,
                ActicityId = _.Id.ToString(),
                PromotionPrice = _.PromotionPrice,
                Label = _.Label,
                AvailQuantity = _.LimitQuantity - _.SoldQuantity
            }).ToList();

            return result;
        }

        /// <summary>
        /// 单个商品查询促销信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="promotionChannel"></param>
        /// <returns></returns>
        public async Task<ProductPromotionVo> GetPromotionActivityByPid(string productCode,
            string promotionChannel)
        {
            DateTime now = DateTime.Now;
            var productList = await _promotionActivityRepository.GetPromotionByPidList(new List<string> {productCode});
            var promotion = productList.Where(_ =>
                _.AuditStatus == 1 && now.CompareTo(_.StartTime) >= 0 && now.CompareTo(_.EndTime) < 0 &&
                _.ApplyChannel.Split(new char[] {','}).Contains(promotionChannel)).ToList().FirstOrDefault();
            if (promotion != null)
            {
                return new ProductPromotionVo
                {
                    ProductCode = promotion.ProductCode,
                    ActivityId = promotion.Id.ToString(),
                    PromotionPrice = promotion.PromotionPrice,
                    Label = promotion.Label,
                    AvailQuantity = promotion.LimitQuantity - promotion.SoldQuantity,
                    Name = promotion.Name,
                    Subtitle = promotion.Subtitle,
                    Description = promotion.Description,
                    StartDate = promotion.StartTime.ToString("yyyy.MM.dd"),
                    EndDate = promotion.EndTime.AddDays(-1).ToString("yyyy.MM.dd")
                };
            }

            return null;
        }

        /// <summary>
        /// 更新促销商品数量
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InsertPromotionActivityOrder(InsertPromotionActivityOrderRequest request)
        {
            _logger.Info($"InsertPromotionActivityOrder_Start Para={JsonConvert.SerializeObject(request)}");
            DateTime now = DateTime.Now;
            Guid.TryParse(request.UserId, out var userId);
            var orderProduct = request.Products?.Where(_ => !string.IsNullOrEmpty(_.ActivityId));
            if (orderProduct != null && orderProduct.Any())
            {
                foreach (var itemOrder in orderProduct)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        Guid.TryParse(itemOrder.ActivityId, out var acticityId);
                        var productDetail =
                            await _promotionActivityProductRepository.GetPromotionProductDetail(itemOrder.ActivityId,
                                itemOrder.ProductCode, false);
                        if (productDetail != null)
                        {
                            PromotionActivityProductDo promotionActivityProductDo = new PromotionActivityProductDo
                            {
                                Id = productDetail.Id,
                                SoldQuantity = productDetail.SoldQuantity + itemOrder.Num,
                                UpdateBy = request.SubmitBy,
                                UpdateTime = now
                            };
                            var result2 = await _promotionActivityProductRepository.UpdateAsync(
                                promotionActivityProductDo,
                                new List<string> {"SoldQuantity", "UpdateBy", "UpdateTime"});

                            PromotionActivityOrderDo promotionActivityOrderDo = new PromotionActivityOrderDo
                            {
                                ActivityId = acticityId,
                                OrderNo = request.OrderNo,
                                UserId = userId,
                                ProductCode = itemOrder.ProductCode,
                                Num = itemOrder.Num,
                                OrderStatus = 0,
                                CreateBy = request.SubmitBy,
                                CreateTime = now
                            };
                            var result3 = await _promotionActivityOrderRepository.InsertAsync(promotionActivityOrderDo);
                        }

                        ts.Complete();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PromotionActivityDetailVo> GetPromotionActivityDetail(PromotionActivityDetailRequest request)
        {
            PromotionActivityDetailVo result = null;
            var resultPromotionTask = _promotionActivityRepository.GetPromotionDetail(request.ActivityId);
            var resultProductTsk = _promotionActivityProductRepository.GetPromotionProductByActivityId(request.ActivityId);
            await Task.WhenAll(resultPromotionTask, resultProductTsk);

            var resultPromotion = resultPromotionTask.Result;
            var resultProduct = resultProductTsk.Result ?? new List<PromotionActivityProductDo>();
            if (resultPromotion != null)
            {
                List<FctShopProductDO> products = new List<FctShopProductDO>();
                if (resultProduct.Any())
                {
                    var pidList = resultProduct.Select(_ => _.ProductCode).Distinct().ToList();
                    products = await _fctShopProductRepository.GetShopProductByProductCodes(pidList);
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
                    PromotionChannel = resultPromotion.ApplyChannel,
                    Description = resultPromotion.Description,
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
        /// 根据订单号查询商品参加的活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductActivityVo>> GetProductActivityByOrderNos(
            ProductActivityByOrderNosRequest request)
        {
            var productData =
                await _promotionActivityOrderRepository.GetPromotionActivityOrderByOrderNos(request.OrderNos);

            return productData?.Select(_ => new ProductActivityVo
            {
                OrderNo = _.OrderNo,
                ProductCode = _.ProductCode,
                ActivityId = _.ActivityId.ToString(),
                Num = _.Num
            })?.ToList() ?? new List<ProductActivityVo>();
        }
    }
}
