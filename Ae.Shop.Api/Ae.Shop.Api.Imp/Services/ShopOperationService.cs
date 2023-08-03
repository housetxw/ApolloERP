using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopOperationService : IShopOperationService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ShopOperationService> logger;
        private readonly IIdentityService identityService;
        private readonly IOrderCommentClient orderCommentClient;

        public ShopOperationService(IMapper mapper, ApolloErpLogger<ShopOperationService> logger, IIdentityService identityService, IOrderCommentClient orderCommentClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.orderCommentClient = orderCommentClient;
        }

        public async Task<ApiResult<GetCommentListResponse>> GetCommentList(GetCommentListRequest request)
        {
            var result = Result.Failed<GetCommentListResponse>(CommonConstant.QueryFailed);
            try
            {
                var clientRequest = mapper.Map<GetCommentListClientRequest>(request);
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    clientRequest.ShopId = (int)shopId;
                }
                var clientResult = await orderCommentClient.GetCommentList(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<GetCommentListResponse>(clientResult.GetSuccessData());
                    result = Result.Success(response, CommonConstant.QuerySuccess);
                }
                else
                {
                    result = Result.Failed<GetCommentListResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetCommentListEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetOrderCommentForShopVO>> GetOrderCommentForShopList(ApiRequest<GetOrderCommentBaseRequest> request)
        {
            var result = Result.Failed(ResultCode.Failed, CommonConstant.QueryFailed, new List<GetOrderCommentForShopVO>(), 0);
            try
            {
                var clientRequest = mapper.Map<ApiRequest<GetOrderCommentBaseClientRequest>>(request);
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    clientRequest.Data.ShopId = (int)shopId;
                }
                var clientResult = await orderCommentClient.GetOrderCommentForShopList(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var list = mapper.Map<List<GetOrderCommentForShopVO>>(clientResult.GetSuccessData().Items);
                    result = Result.Success(list, clientResult.GetSuccessData().TotalItems);
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message, new List<GetOrderCommentForShopVO>(), 0);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderCommentForShopListEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> ReplyComment(ReplyCommentRequest data)
        {
            var result = Result.Failed(CommonConstant.OperateFailed);
            try
            {
                var clientRequest = mapper.Map<ReplyCommentClientRequest>(data);
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    clientRequest.ShopId = (int)shopId;
                }
                var userName = $"{identityService.GetUserName()}@{identityService.GetUserId()}";
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    clientRequest.UserName = userName;
                }
                var clientResult = await orderCommentClient.ReplyComment(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success(CommonConstant.OperateSuccess);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("ReplyCommentEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<ReplyDetailResponse>> ReplyDetail(ReplyDetailRequest request)
        {
            var result = Result.Failed<ReplyDetailResponse>(CommonConstant.QueryFailed);
            try
            {
                var clientRequest = mapper.Map<ReplyDetailClientRequest>(request);
                var organizationId = identityService.GetOrganizationId();
                long.TryParse(organizationId, out long shopId);
                if (shopId <= 0)
                {
                    throw new CustomException("登录信息异常");
                }
                else
                {
                    clientRequest.ShopId = (int)shopId;
                }
                var clientResult = await orderCommentClient.ReplyDetail(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<ReplyDetailResponse>(clientResult.GetSuccessData());
                    result = Result.Success(response, CommonConstant.QuerySuccess);
                }
                else
                {
                    result = Result.Failed<ReplyDetailResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("ReplyDetailEx", ex);
            }
            return result;
        }
    }
}
