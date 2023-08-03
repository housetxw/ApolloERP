using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Pay;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Pay;
using Rg.Pay.Service.Core.Enums.Pay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class PayService : IPayService
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<PayService> logger;
        private readonly IPayClient payClient;
        private readonly IIdentityService identityService;
        private readonly string payFailedMessage = "支付异常，请重试";

        public PayService(IMapper mapper, IConfiguration configuration, ApolloErpLogger<PayService> logger, IPayClient payClient, IIdentityService identityService)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
            this.payClient = payClient;
            this.identityService = identityService;
        }

        public async Task<ApiResult> ClosePay(ApiRequest<ClosePayRequest> request)
        {
            var result = Result.Failed(payFailedMessage);
            try
            {
                var clientRequest = new ClosePayClientRequest()
                {
                    PayNo = request.Data.PayNo,
                    UpdateBy = $"{identityService.GetUserName()}@{identityService.GetUserId()}"
                };
                var clientResult = await payClient.ClosePay(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success("关闭成功");
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("ClosePayEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<ConfirmPayResponse>> ConfirmPay(ApiRequest<ConfirmPayRequest> request)
        {
            var result = Result.Failed<ConfirmPayResponse>(payFailedMessage);
            try
            {
                var clientRequest = mapper.Map<ConfirmPayClientRequest>(request.Data);
                clientRequest.Method = PayMethodEnum.OnlinePay;
                clientRequest.Channel = PayChannelEnum.WxPay;
                clientRequest.TerminalType = PayTerminalTypeEnum.ApolloErpWxMiniApp;
                clientRequest.SceneType = PaySceneTypeEnum.JsApi;
                clientRequest.CreateBy = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                clientRequest.AppId = CommonConstant.AppId;
                var clientResult = await payClient.ConfirmPay(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<ConfirmPayResponse>(clientResult.GetSuccessData());
                    result = Result.Success(response, "请求成功");
                }
                else
                {
                    result = Result.Failed<ConfirmPayResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("ConfirmPayEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 微信转账到支付宝账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WeChatTransferAliPayResponse> WeChatTransferAliPay(WeChatTransferAliPayRequest request)
        {
            var result = await payClient.WeChatTransferAliPay(new WeChatTransferAliPayClientRequest()
            {
                OpenId = request.OpenId,
                Identity = request.Identity,
                UserName = request.UserName,
                Amount = request.Amount,
                SubmitBy = identityService.GetUserId()
            });
            if (result != null)
            {
                return new WeChatTransferAliPayResponse()
                {
                    PayNo = result.PayNo,
                    Status = result.Status,
                    WxMiniAppPayCallData = result.WxMiniAppPayCallData
                };
            }
            else
            {
                throw new CustomException("系统异常，请重试");
            }
        }
    }
}
