using Ae.Receive.Service.Dal.Repositorys.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Model;
using AutoMapper;
using System.Threading.Tasks;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Response;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Client.Clients.ShopServer;

namespace Ae.Receive.Service.Imp.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRespository iTestRespository;
        private readonly TestClient testClient;
        private readonly IMapper mapper;

        public TestService(ITestRespository iTestRespository, TestClient testClient, IMapper mapper)
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
