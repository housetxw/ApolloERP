using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.AccountAuthority.Service.Core.Model;
using AutoMapper;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Client.Interface.ShopServer;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;

namespace Ae.AccountAuthority.Service.Imp.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRespository iTestRespository;
        private readonly ITestClient testClient;
        private readonly IMapper mapper;

        public TestService(ITestRespository iTestRespository, ITestClient testClient, IMapper mapper)
        {
            this.iTestRespository = iTestRespository;
            this.testClient = testClient;
            this.mapper = mapper;
        }

        public async Task<GetListResponse> GetPurchase(GetListRequest request)
        {
            var data = await iTestRespository.PurchaseInfo(request.PageIndex, request.PageSize, request.ShopId);
            List<Purchase> purchase = mapper.Map<List<Purchase>>(data.Items);
            GetListResponse result = new GetListResponse();
            result.TotalItems = data.TotalItems;
            result.ShopList = purchase;
            return result;
        }

        public async Task<bool> GetShopInfo()
        {
            return await testClient.UpdateShopInfo();
        }

    }
}
