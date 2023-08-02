using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Login.Auth;
using Ae.B.FMS.Api.Core.Model.Settlement;
using Ae.B.FMS.Api.Core.Request.Settlement;

namespace Ae.B.FMS.Api.Imp.Services
{
    public class SettlementService : ISettlementService
    {

        private readonly IMapper _mapper;
        private readonly ISettlementClient settlementClient;
        private readonly IIdentityService identityService;

        public SettlementService(IMapper mapper,
          ISettlementClient settlementClient, IIdentityService identityService)
        {
            _mapper = mapper;
            this.settlementClient = settlementClient;
            this.identityService = identityService;
        }



        public async Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request)
        {
            var clientRequest = _mapper.Map<GetSettlementDetailClientRequest>(request);

            var result = await settlementClient.GetSettlementDetail(clientRequest);

            var vo = _mapper.Map<List<GetSettlementDetailResponse>>(result);

            vo.ForEach(r =>
            {
                r.InstallTimeStr = r.InstallTime.ToString("yyyy-MM-dd");
            });

            return vo;
        }

        public async Task<ApiPagedResult<GetSettlementListResponse>> GetSettlementList(GetSettlementListRequest request)
        {
            if (request.SettlementTimes != null && request.SettlementTimes.Any())
            {
                request.StartSettlementTime = request.SettlementTimes[0];
                request.EndSettlementTime = request.SettlementTimes[1];
            }


            var clientRequest = _mapper.Map<GetSettlementListClientRequest>(request);

            clientRequest.PayStatus = string.Join(",", request.PayStatuss);

            var result = await settlementClient.GetSettlementList(clientRequest);

            var vo = _mapper.Map<ApiPagedResult<GetSettlementListResponse>>(result);
            return vo;
        }



        public async Task<ApiResult> SaveSettlementReview(SaveSettlementReviewRequest request)
        {
            request.CreateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            var clientRequest = _mapper.Map<SaveSettlementReviewClientRequest>(request);
            var result = await settlementClient.SaveSettlementReview(clientRequest);
            return result;
        }

        public async Task<ApiResult<SettlementPayReviewResponse>> SettlementPayReviewDO(GetSettlementDetailRequest request)
        {
            var result = await settlementClient.SettlementPayReviewDO(new GetSettlementDetailClientRequest
            {
                SettlementBatchNo = request.SettlementBatchNo
            });
            var vo = _mapper.Map<ApiResult<SettlementPayReviewResponse>>(result);
            if (vo.Data != null)
                vo.Data.CreateTimeStr = vo.Data.CreateTime.ToString("yyyy/MM/dd");
            return vo;
        }

        public async Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail(GetPurchaseSettlementDetailRequest request)
        {
            return await settlementClient.GetPurchaseSettlementDetail(request);
        }

        public async Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request)
        {
            int.TryParse(identityService.GetOrganizationId(), out var shopId);
            request.ShopId = shopId;
            return await settlementClient.GetPurchaseSettlementList(request);
        }

        public async Task<ApiResult<string>> SavePurchaseSettlementOrder(ApiRequest<SavePurchaseSettlementOrderRequest> request)
        {
            int.TryParse(identityService.GetOrganizationId(), out var shopId);
            request.Data.ShopId = shopId;
            request.Data.CreateUser = identityService.GetUserName();
            return await settlementClient.SavePurchaseSettlementOrder(request);
        }

        public async Task<ApiResult<string>> SavePurchaseSettlementReview(ApiRequest<SavePurchaseSettlementReviewRequest> request)
        {
            int.TryParse(identityService.GetOrganizationId(), out var shopId);
            request.Data.ShopId = shopId;
            request.Data.CreateUser = identityService.GetUserName();
            return await settlementClient.SavePurchaseSettlementReview(request);
        }
    }
}
