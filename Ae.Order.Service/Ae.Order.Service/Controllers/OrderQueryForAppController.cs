using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Common.Constant;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.ShopOrder;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Request.ShopOrder;
using Ae.Order.Service.Core.Response.Order;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 订单查询ForApp
    /// </summary>
    [Route("[controller]/[action]")]
    //  [Filter(nameof(OrderQueryForAppController))]
    public class OrderQueryForAppController : Controller
    {

        private readonly IOrderQueryForAppService orderQueryForAppService;

        private readonly string constMessage = "无数据";

        public OrderQueryForAppController(IOrderQueryForAppService orderQueryService)
        {
            this.orderQueryForAppService = orderQueryService;
        }

        /// <summary>
        /// 根据手机号/订单编号/商品名称
        /// </summary>
        /// <param name="getOrdersForSearchRequest"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ApiPagedResult<GetOrdersForSearchDTO>> GetOrdersForSearch(
            [FromBody] GetOrdersForSearchRequest getOrdersForSearchRequest)
        {
            //TODO 这一层应该有一个实体的转换
            //TODO Application Request  To Service Request
            //TODO Service Response To Application Response
            //TODO 业务沉淀下去是有必要 在Application层 做出响应的Model 映射的

            ApiPagedResultData<GetOrdersForSearchDTO> getOrdersForSearch = await orderQueryForAppService.GetOrdersForSearch(getOrdersForSearchRequest);

            ApiPagedResult<GetOrdersForSearchDTO> response = new ApiPagedResult<GetOrdersForSearchDTO>()
            {
                Code = ResultCode.Success,
                Data = getOrdersForSearch
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }


            return response;

        }

        /// <summary>
        ///  订单列表For 业务状态
        /// </summary>
        /// <param name="getOrdersForStatusRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetOrdersForStatusDTO>> GetOrdersForStatus([FromBody]GetOrdersForStatusRequest getOrdersForStatusRequest)
        {
            ApiPagedResultData<GetOrdersForStatusDTO> getOrdersForSearch = await orderQueryForAppService.GetOrdersForStatus(getOrdersForStatusRequest);

            ApiPagedResult<GetOrdersForStatusDTO> response = new ApiPagedResult<GetOrdersForStatusDTO>()
            {
                Code = ResultCode.Success,
                Data = getOrdersForSearch
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = CommonConstant.ErrorOccured;
                response.Code = ResultCode.Failed;
            }
            return response;
        }


        /// <summary>
        /// 订单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetOrderForStatusNumResponse>>> GetOrderForStatusNum([FromQuery]GetOrderForStatusNumRequest request)
        {
            var taskWaitingSign = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            {
                PageIndex = 1,
                PageSize = 1,
                Content = string.Empty,
                OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.WaitingSign,
                SearchType = GetOrdersTypeEnum.NotSearch,
                ShopId = request.ShopId
            });

            var taskWatingDispatch = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            {
                PageIndex = 1,
                PageSize = 1,
                Content = string.Empty,
                OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.WaitingDispatch,
                SearchType = GetOrdersTypeEnum.NotSearch,
                ShopId = request.ShopId
            });

            var taskABuilding = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            {
                PageIndex = 1,
                PageSize = 1,
                Content = string.Empty,
                OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.ABuilding,
                SearchType = GetOrdersTypeEnum.NotSearch,
                ShopId = request.ShopId
            });


            //var taskInstalled = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            //{
            //    PageIndex = 1,
            //    PageSize = 1,
            //    Content = string.Empty,
            //    OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.Installed,
            //    SearchType = GetOrdersTypeEnum.NotSearch,
            //    ShopId = request.ShopId
            //});

            //var taskCanceled = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            //{
            //    PageIndex = 1,
            //    PageSize = 1,
            //    Content = string.Empty,
            //    OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.Canceled,
            //    SearchType = GetOrdersTypeEnum.NotSearch,
            //    ShopId = request.ShopId
            //});

            //var taskAll = orderQueryForAppService.GetOrdersForStatus(new GetOrdersForStatusRequest()
            //{
            //    PageIndex = 1,
            //    PageSize = 1,
            //    Content = string.Empty,
            //    OrderBusinessStatus = Core.Enums.OrderBusinessStatusEnum.All,
            //    SearchType = GetOrdersTypeEnum.NotSearch,
            //    ShopId = request.ShopId
            //});


            Task.WaitAll(new[] { taskWaitingSign, taskWatingDispatch, taskABuilding });

            List<GetOrderForStatusNumResponse> data = new List<GetOrderForStatusNumResponse>();
            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.WaitingSign.ToString(),
                Name = "待签收",
                Count = taskWaitingSign.Result.TotalItems
            });

            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.WaitingDispatch.ToString(),
                Name = "待派工",
                Count = taskWatingDispatch.Result.TotalItems
            });

            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.ABuilding.ToString(),
                Name = "施工中",
                Count = taskABuilding.Result.TotalItems
            });

            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.Installed.ToString(),
                Name = "已安装",
                Count = 0
            });

            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.Canceled.ToString(),
                Name = "已取消",
                Count = 0
            });

            data.Add(new GetOrderForStatusNumResponse()
            {
                Code = OrderBusinessStatusEnum.All.ToString(),
                Name = "全部",
                Count = 0
            });

            return await Task.FromResult(Result.Success(data));

        }
    }
}
