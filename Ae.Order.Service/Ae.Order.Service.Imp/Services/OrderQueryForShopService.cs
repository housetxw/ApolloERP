using AutoMapper;
using AutoMapper.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Repository.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Imp.Services
{
    public class OrderQueryForShopService : IOrderQueryForShopService
    {
        private readonly IMapper _mapper;
      
        private readonly ApolloErpLogger<OrderQueryForShopService> _logger;
        private readonly IOrderQueryForShopRepository _orderQueryForShopRepository;
      

        public OrderQueryForShopService(IMapper mapper,
            ApolloErpLogger<OrderQueryForShopService> logger, IOrderQueryForShopRepository orderQueryForShopRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _orderQueryForShopRepository = orderQueryForShopRepository;
         
        }

        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request)
        {
            var dalResponse = await _orderQueryForShopRepository.GetOrderInfoListForShop(request);

            ApiPagedResultData<OrderDTO> apiPagedResultData = _mapper.Map<ApiPagedResultData<OrderDTO>>(dalResponse);

            return new ApiPagedResult<OrderDTO>()
            {
                Code = ResultCode.Success,
                Data = apiPagedResultData
            };
        }
    }
}
