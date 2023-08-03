using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 逆向
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReverseController))]
    public class ReverseController : ControllerBase
    {
        private readonly IReverseService reverseService;

        private readonly IShopOrderQueryClient _shopOrderQueryClient;


        public ReverseController(IReverseService reverseService)
        {
            this.reverseService = reverseService;
        }

        /// <summary>
        /// 获取申请原因集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons([FromQuery]GetReverseReasonsRequest request)
        {
            return await reverseService.GetReverseReasons(request);
        }

        /// <summary>
        /// 提交取消订单申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder([FromBody]ApiRequest<CancelOrderRequest> req)
        {
            //4对应reverse_reason_config的理由是其他
            //if (req.Data.ReasonId <= CommonConstant.Zero) req.Data.ReasonId = CommonConstant.Four;

            var result = await reverseService.CancelNewOrder(new ApiRequest<Core.Request.Reverse.CancelNewOrderRequest>()
            {
                Data = new Core.Request.Reverse.CancelNewOrderRequest()
                {
                    OrderNo = req.Data.OrderNo
                }
            });
          
            if (result.Code == ResultCode.Success)
                return Result.Success(new CreateReverseOrderBaseResponse()
                {
                    ReverseNo = string.Empty
                });
            return new ApiResult<CreateReverseOrderBaseResponse>()
            {
                Code = ResultCode.Failed,
                Message = "取消失败",
                Data = null
            };







        }
    }
}
