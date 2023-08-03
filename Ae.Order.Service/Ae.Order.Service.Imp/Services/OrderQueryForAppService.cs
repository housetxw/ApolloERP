using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Model.ShopOrder;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Request.ShopOrder;
using Ae.Order.Service.Dal.Condition.ShopOrder;
using Ae.Order.Service.Dal.Repository.ShopOrder;

namespace Ae.Order.Service.Imp.Services
{
    public class OrderQueryForAppService : IOrderQueryForAppService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryForAppService> logger;
        private readonly IOrderForAppRepository orderQueryForAppRepository;
        private readonly IOrderQueryService orderQueryService;
        public OrderQueryForAppService(IMapper mapper, ApolloErpLogger<OrderQueryForAppService> logger,
            IOrderForAppRepository orderRepository, IOrderQueryService orderQueryService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderQueryForAppRepository = orderRepository;
            this.orderQueryService = orderQueryService;
           
        }
        /// <summary>
        /// 查询订单信息根据客户搜索条件（手机号、订单号、产品信息)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetOrdersForSearchDTO>> GetOrdersForSearch(GetOrdersForSearchRequest request)
        {
            ApiPagedResultData<OrderDTO> getMainOrders = await GetMainOrdersForSearch(request);

            if (getMainOrders?.Items != null && getMainOrders.Items.Any())
            {
                var orderNos = getMainOrders.Items.Select(x => x.OrderNo).ToList();
                var getOrderDetails = await orderQueryService.GetOrderDetailByOrderIds(orderNos);

                ApiPagedResultData<GetOrdersForSearchDTO> response = new ApiPagedResultData<GetOrdersForSearchDTO>();

                response = mapper.Map<ApiPagedResultData<GetOrdersForSearchDTO>>(getMainOrders);

                if (response.Items != null && response.Items.Any())
                {
                    response.Items.ToList().ForEach(x =>
                    {
                        var orderProducts = getOrderDetails.Where(_ => x.Id == _.OrderId && _.ParentOrderPackagePid == 0).ToList();

                        var mapperOrderProducts = mapper.Map<List<GetOrdersProductDTO>>(orderProducts);

                        x.Goods = mapperOrderProducts;
                    });
                }

                return response;
            }

            return null;

        }

        /// <summary>
        /// 查询主订单信息根据搜索条件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OrderDTO>> GetMainOrdersForSearch(GetOrdersForSearchRequest request)
        {
            GetMainOrdersForSearchCondition dalRequest = mapper.Map<GetMainOrdersForSearchCondition>(request);
            dalRequest.Channels = new List<int>()
            {
                (int)ChannelEnum.ApolloErpToC, (int)ChannelEnum.ApolloErpToShop
            };
            var dalResponse = await orderQueryForAppRepository.GetMainOrdersForSearch(dalRequest);

            ApiPagedResultData<OrderDTO> response = mapper.Map<ApiPagedResultData<OrderDTO>>(dalResponse);
            return response;
        }

        /// <summary>
        /// 查询订单信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetOrdersForStatusDTO>> GetOrdersForStatus(GetOrdersForStatusRequest request)
        {
            ApiPagedResultData<OrderDTO> getMainOrders = await GetMainOrdersForStatus(request);

            if (getMainOrders?.Items != null && getMainOrders.Items.Any())
            {
                var orderNos = getMainOrders.Items.Select(x => x.OrderNo).ToList();
                var getOrderDetails = await orderQueryService.GetOrderDetailByOrderIds(orderNos);

                ApiPagedResultData<GetOrdersForStatusDTO> response = new ApiPagedResultData<GetOrdersForStatusDTO>();

                response = mapper.Map<ApiPagedResultData<GetOrdersForStatusDTO>>(getMainOrders);

                if (response.Items != null && response.Items.Any())
                {
                    response.Items.ToList().ForEach(x =>
                    {
                        var orderProducts = getOrderDetails.Where(_ => x.Id == _.OrderId).ToList();

                        var mapperOrderProducts = mapper.Map<List<GetOrdersProductDTO>>(orderProducts);

                        x.Goods = mapperOrderProducts;
                    });
                }

                return response;
            }
            return mapper.Map<ApiPagedResultData<GetOrdersForStatusDTO>>(getMainOrders);



        }

        /// <summary>
        /// 查询主订单信息根据业务状态
        /// </summary>
        /// <param name="getOrdersForStatusRequest"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OrderDTO>> GetMainOrdersForStatus(GetOrdersForStatusRequest getOrdersForStatusRequest)
        {
            GetMainOrdersForStatusCondition dalRequest = mapper.Map<GetMainOrdersForStatusCondition>(getOrdersForStatusRequest);
            dalRequest.Channels = new List<int>()
            {
                (int)ChannelEnum.ApolloErpToC, (int)ChannelEnum.ApolloErpToShop
            };
            var dalResponse = await orderQueryForAppRepository.GetMainOrdersForStatus(dalRequest);
            ApiPagedResultData<OrderDTO> response = mapper.Map<ApiPagedResultData<OrderDTO>>(dalResponse);
            return response;
        }

      
    }
}
