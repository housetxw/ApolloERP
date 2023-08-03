using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ae.FMS.Service.Client.Response;
using Ae.FMS.Service.Client.Request;
using Ae.FMS.Service.Client.Model;

namespace Ae.FMS.Service.Client.Clients
{
    public class OrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        /// <summary>
        /// 获取门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OrderClientDTO>> GetOrderBaseInfoForBusinessStatus(GetOrderBaseInfoForBusinessStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetOrderBaseInfoForBusinessStatusRequest,
                ApiPagedResult<OrderClientDTO>>(configuration["OrderServer:GetOrderBaseInfoForBusinessStatus"], request);
            return result.Data;
        }

        /// <summary>
        /// 计算核对账单金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetAccountCheckAmountClientDTO>>> GetAccountCheckAmount(GetAccountCheckAmountClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetAccountCheckAmountClientRequest,
                ApiResult<List<GetAccountCheckAmountClientDTO>>>(configuration["OrderServer:GetAccountCheckAmount"], request);
            return result;
        }

        // /OrderQuery/GetNoReconciliationAmount
        /// <summary>
        /// 根据门店返回未对账的数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetNoReconciliationAmountClientDTO>>> GetNoReconciliationAmount(GetNoReconciliationAmountClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetNoReconciliationAmountClientRequest,
                ApiResult<List<GetNoReconciliationAmountClientDTO>>>(configuration["OrderServer:GetNoReconciliationAmount"], request);
            return result;
        }

        /// <summary>
        /// 更新订单的对账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderStatus(UpdateOrderStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result = await client.PostAsJsonAsync<UpdateOrderStatusClientRequest,
                ApiResult>(configuration["ShopOrderServer:UpdateOrderStatus"], request);
            return result;
        }

        /// <summary>
        /// 查询订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderClientDTO>>> GetOrderBaseInfo(GetOrderBaseInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetOrderBaseInfoClientRequest,
                ApiResult<List<OrderClientDTO>>>(configuration["OrderServer:GetOrderBaseInfo"], request);
            return result;
        }

        public async Task<ApiResult<OrderClientDTO>> GetOrderByNo(GetOrderByNoClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.GetAsJsonAsync<GetOrderByNoClientRequest,
                ApiResult<OrderClientDTO>>(configuration["OrderServer:GetOrderByNo"], request);
            return result;
        }

        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct([FromBody] GetOrderProductRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetOrderProductRequest,
                ApiResult< List < OrderProductDTO >>> (configuration["OrderServer:GetOrderProduct"], request);
            return result;
        }

        /// <summary>
        /// 查询订单优惠卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderCouponDTO>>> GetOrderCoupons([FromBody] GetOrderCouponsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<GetOrderCouponsRequest,
                ApiResult<List<OrderCouponDTO>>>(configuration["OrderServer:GetOrderCoupons"], request);
            return result;
        }

        /// <summary>
        ///批量 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public async Task<ApiResult> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result = await client.PostAsJsonAsync<BatchUpdatePayStatusRequest,
               ApiResult>(configuration["OrderServer:BatchUpdatePayStatus"], request);
            return result;
        }

        /// <summary>
        /// 得到套餐卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderPackageCardDTO>>> GetOrderPackageCard( GetOrderPackageCardRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result = await client.PostAsJsonAsync<GetOrderPackageCardRequest,
                ApiResult<List<OrderPackageCardDTO>>>(configuration["ShopOrderServer:GetOrderPackageCard"], request);
            return result;
        }


    }
}
