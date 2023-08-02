using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Imp.Services
{
    public class FlashSaleConfigService : IFlashSaleConfigService
    {
        private readonly IMapper _mapper;
        private readonly IFlashSaleConfigClient _flashSaleConfigClient;
        private readonly IIdentityService _identityService;

        public FlashSaleConfigService(IMapper mapper, IFlashSaleConfigClient flashSaleConfigClient,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _flashSaleConfigClient = flashSaleConfigClient;
            _identityService = identityService;
        }

        public async Task<ApiResult<string>> CreatFlashSaleConfig(FlashSaleConfigDTO request)
        {
            request.CreateBy = _identityService.GetUserName();
            if (request.Times != null && request.Times.Any())
            {
                request.ActiveStartTime = Convert.ToDateTime(request.Times[0]);
                request.ActiveEndTime = Convert.ToDateTime(request.Times[1]);
            }
            return await _flashSaleConfigClient.CreatFlashSaleConfig(request);
        }

        public async Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs(GetFlashSaleConfigRequest request)
        {
            return await _flashSaleConfigClient.GetFlashSaleConfigs(request);
        }

        public async Task<ApiResult<string>> UpdateFlashSaleConfig(FlashSaleConfigDTO request)
        {

            request.UpdateBy = _identityService.GetUserName();
            request.Status = "已取消";
            return await _flashSaleConfigClient.UpdateFlashSaleConfig(request);
        }
    }
}
