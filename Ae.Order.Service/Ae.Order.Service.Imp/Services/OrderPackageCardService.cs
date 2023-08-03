using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using System.Linq;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Client.Model;
using Ae.Order.Service.Client.Request;
using Ae.Order.Service.Core.Response.Order;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Repository.Order;
using Ae.Order.Service.Dal.Model;

namespace Ae.Order.Service.Imp.Services
{
    public class OrderPackageCardService : IOrderPackageCardService
    {
        private readonly IOrderPackageCardRepository orderPackageCardRepository;
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderPackageCardService> logger;
        private readonly IShopManageClient shopManageClient;
        private readonly IOrderRepository _orderRepository;

        public OrderPackageCardService(IMapper mapper,
            ApolloErpLogger<OrderPackageCardService> logger, IOrderPackageCardRepository orderPackageCardRepository,
            IShopManageClient shopManageClient, IOrderRepository orderRepository)
        {

            this.mapper = mapper;
            this.logger = logger;
            this.orderPackageCardRepository = orderPackageCardRepository;
            this.shopManageClient = shopManageClient;
            _orderRepository = orderRepository;
        }

        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request)
        {
            var conditions = new StringBuilder(" where is_deleted =0 ");
            var param = new DynamicParameters();

            var result = new ApiPagedResult<GetOrderPackageCardsResponse>();
            result.Data = new ApiPagedResultData<GetOrderPackageCardsResponse> { Items = new List<GetOrderPackageCardsResponse>() };

            try
            {
                if (!string.IsNullOrWhiteSpace(request.UserId))
                {
                    param.Add("@user_id", request.UserId);
                    conditions.Append(" and user_id=@user_id");
                }
                if (!string.IsNullOrWhiteSpace(request.UserPhone))
                {
                    param.Add("@user_phone", request.UserPhone);
                    conditions.Append(" and user_phone=@user_phone");
                }
                if (!string.IsNullOrWhiteSpace(request.SourceOrderNo))
                {
                    param.Add("@SourceOrderNo", request.SourceOrderNo);
                    conditions.Append(" and source_order_no=@SourceOrderNo");
                }
                if (!string.IsNullOrWhiteSpace(request.ProductName))
                {
                    var productId = request.ProductName;
                    var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    if (productNames.Any())
                    {
                        string productName = string.Join("%", productNames);
                        conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                        param.Add("@productId", productId);
                    }
                }
                if (!string.IsNullOrEmpty(request.Code))
                {
                    param.Add("@Code", request.Code);
                    conditions.Append(" and code=@Code");
                }
                if (request.Status > 0)
                {
                    param.Add("@status", request.Status);
                    conditions.Append(" and status=@status");
                }
                //-1未使用含过期
                if (request.Status == -1)
                {
                    conditions.Append(" and status <> 1");
                }

                var cardResult = await orderPackageCardRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "status asc,end_valid_time asc", param);

                var shopDic = new Dictionary<long, ShopSimpleInfoClientDTO>();
                if (cardResult.Items != null && cardResult.Items.Any())
                {
                    foreach (var item in cardResult.Items)
                    {
                        string shopName = string.Empty;
                        if (shopDic.ContainsKey(item.VerifyShopId))
                        {
                            shopName = shopDic[item.VerifyShopId]?.SimpleName;
                        }
                        else
                        {
                            var shopRes = await shopManageClient.GetShopSimpleInfoAsync(new GetShopRequest
                            {
                                ShopId = item.VerifyShopId
                            });
                            shopDic.Add(item.VerifyShopId, shopRes?.Data);
                            shopName = shopRes?.Data.SimpleName;
                        }

                        result.Data.Items.Add(new GetOrderPackageCardsResponse
                        {
                            Channel = item.Channel,
                            SourceOrderNo = item.SourceOrderNo,
                            UserPhone = item.UserPhone,
                            Code = item.Code,
                            EndValidTime = item.EndValidTime,
                            StartValidTime = item.StartValidTime,
                            Status = GetPackageCardStatus(item.Status),
                            VerifyOrderNo = item.VerifyOrderNo,
                            VerifyShopName = shopName,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            PackageName = item.PackageName,
                            VerifyTime = item.VerifyTime,
                            Remark = item.Remark
                        });
                    }
                }
                result.Data.TotalItems = cardResult.TotalItems;
                result.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                result.Code = ResultCode.Exception;
            }
            return result;
        }

        public async Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request)
        {
            var data = await _orderRepository.GetPackageCardMainInfo(request);

            return Result.Success(data);
        }

        public string GetPackageCardStatus(int status)
        {
            string statusText = string.Empty;
            switch (status)
            {
                case 0:
                    statusText = "未使用";
                    break;
                case 1:
                    statusText = "已使用";
                    break;
                case 2:
                    statusText = "已过期";
                    break;
                case 3:
                    statusText = "作废";
                    break;
                default:
                    statusText = "未使用";
                    break;
            }

            return statusText;
        }

        public async Task<ApiResult> UpdateOrderPackage(ApiRequest<UpdateOrderPackageRequest> request)
        {
            var getOrderPackageCards = await orderPackageCardRepository.GetListAsync(" where is_deleted=0 and code=@Code and source_order_no=@SourceOrderNo",
                new { request.Data.Code, request.Data.SourceOrderNo });

            if (getOrderPackageCards?.Count() > 0)
            {
                OrderPackageCardDO packageCardDO = getOrderPackageCards?.FirstOrDefault();
                var oldStatus = packageCardDO.Status;
                packageCardDO.Status = request.Data.Status;
                packageCardDO.UpdateBy = request.Data.UpdateBy;
                packageCardDO.UpdateTime = DateTime.Now;
                if (request.Data.Status == 3)
                {
                    packageCardDO.Remark = packageCardDO.Remark + packageCardDO.UpdateBy + "操作了作废" + packageCardDO.UpdateTime.ToString() + " ";
                }
                else if (oldStatus == 1)
                {
                    packageCardDO.Remark = packageCardDO.Remark + packageCardDO.UpdateBy + "操作了恢复" + packageCardDO.UpdateTime.ToString() + " ";
                }
                else
                {
                    bool isSuccess = DateTime.TryParse(request.Data.ValidateEnd, out var endTime);
                    if (isSuccess)
                    {
                        packageCardDO.Remark = packageCardDO.Remark + packageCardDO.UpdateBy + "操作了延期（" + packageCardDO.EndValidTime.ToString() + "-->" + endTime.ToString() + "）" + packageCardDO.UpdateTime.ToString() + " ";

                        packageCardDO.EndValidTime = endTime;
                    }
                    else
                    {
                        return Result.Failed("时间格式不正确");
                    }

                }
                await orderPackageCardRepository.UpdateAsync(getOrderPackageCards?.FirstOrDefault(), new List<string>() { "Status", "EndValidTime", "Remark", "UpdateBy", "UpdateTime" });

            }
            else
            {
                return Result.Failed("查无此核销码。");
            }

            return Result.Success("操作成功");
        }
    }

}
