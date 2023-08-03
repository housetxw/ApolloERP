using Ae.Shop.Api.Dal.Repositorys.Test;
using AutoMapper;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Client.Clients;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Common.Exceptions;
using System;

namespace Ae.Shop.Api.Imp.Services
{
    public class TestService : ITestService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<TestService> logger;
        private readonly IIdentityService identityService;
        private readonly ITestRespository testRespository;
        private readonly IShopMangeClient shopMangeClient;

        public TestService(IMapper mapper, ApolloErpLogger<TestService> logger, IIdentityService identityService, ITestRespository testRespository, IShopMangeClient shopMangeClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.testRespository = testRespository;
            this.shopMangeClient = shopMangeClient;
        }

        public async Task<ApiResult<ShopVO>> GetShopById(GetShopRequest request)
        {
            var result = Result.Failed<ShopVO>("查询异常");
            try
            {
                var organizationId = identityService.GetOrganizationId();
                var clientRequest = mapper.Map<GetShopClientRequest>(request);
                var clientResult = await shopMangeClient.GetShopById(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<ShopVO>(clientResult.GetSuccessData());
                    result = Result.Success(response, "查询成功");
                }
            }
            catch (CustomException cex)
            {
                logger.Warn("GetShopByIdCex", cex);
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetShopByIdEx", ex);
            }
            return result;
        }
    }
}
