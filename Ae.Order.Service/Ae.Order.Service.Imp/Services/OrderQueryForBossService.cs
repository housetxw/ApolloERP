using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Dal.Repository.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Newtonsoft.Json;
using Ae.Order.Service.Dal.Repository.Interfaces;

namespace Ae.Order.Service.Imp.Services
{
    public class OrderQueryForBossService : IOrderQueryForBossService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryForMiniAppService> logger;
        private readonly IOrderForBossRepository orderForBossRepository;
        private readonly IOrderCarRepository _orderCarRepository;

        public OrderQueryForBossService(IMapper mapper, ApolloErpLogger<OrderQueryForMiniAppService> logger, IOrderForBossRepository orderForBossRepository, IOrderCarRepository orderCarRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderForBossRepository = orderForBossRepository;
            _orderCarRepository = orderCarRepository;
        }

        public async Task<ApiPagedResult<GetOrderListForBossResponse>> GetOrderListForBoss(GetOrderListForBossRequest request)
        {
            var result = new ApiPagedResult<GetOrderListForBossResponse>()
            {
                Code = ResultCode.Failed,
                Message = "加载异常，请重试",
                Data = new ApiPagedResultData<GetOrderListForBossResponse>() { Items = new List<GetOrderListForBossResponse>() }
            };
            try
            {
                var pageList = await orderForBossRepository.GetOrderListForBoss(request);
                if (pageList != null)
                {
                    result = new ApiPagedResult<GetOrderListForBossResponse>()
                    {
                        Code = ResultCode.Success,
                        Message = "查询成功",
                        Data = new ApiPagedResultData<GetOrderListForBossResponse>()
                        {
                            TotalItems = pageList.TotalItems,
                            Items = mapper.Map<List<GetOrderListForBossResponse>>(pageList.Items)
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderListForBossEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request)
        {
            var result = new ApiPagedResult<GetPackageCardRecordsResponse>
            {
                Data = new ApiPagedResultData<GetPackageCardRecordsResponse> { Items = new List<GetPackageCardRecordsResponse>() }
            };
            try
            {
                var conditions = new StringBuilder(" where is_deleted=0 and produce_type=14 ");
                var param = new DynamicParameters();
                if (!string.IsNullOrWhiteSpace(request.UserName))
                {
                    conditions.Append(" and (user_phone=@username or user_name like  CONCAT('%',@username,'%')) ");
                    param.Add("@username", request.UserName);
                }
                var res = await orderForBossRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", param);

                if (res.Items != null && res.Items.Any())
                {
                    foreach (var item in res.Items)
                    {
                        result.Data.Items.Add(new GetPackageCardRecordsResponse
                        {
                            ActualAmount = item.ActualAmount,
                            Id = item.Id,
                            OrderNo = item.OrderNo,
                            OrderTime = item.OrderTime,
                            UserName = item.UserName,
                            UserPhone = item.UserPhone
                        });
                    }
                    result.Data.TotalItems = res.TotalItems;
                }
                result.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                logger.Error($"GetPackageCardRecords_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                result.Code = ResultCode.Exception;
            }
            return result;
        }
    }
}
