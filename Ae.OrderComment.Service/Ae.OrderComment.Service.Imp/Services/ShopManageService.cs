using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Clients;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Imp.Services
{
    public class ShopManageService : IShopManageService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ShopManageService> logger;
        private readonly IShopManageClient shopManageClient;
        private static string defaultUser = "平台系统";
        public ShopManageService(IMapper mapper, ApolloErpLogger<ShopManageService> logger, IShopManageClient shopManageClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.shopManageClient = shopManageClient;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request)
        {
            var res = new ApiPagedResult<ShopSimpleInfoDTO>();
            var result = await this.shopManageClient.GetShopListAsync(new GetShopListClientRequest
            {
                PageIndex = 1,
                PageSize = 20,
                SimpleName = request.ShopName
            });
            if (result.Code == ResultCode.Success)
            {
                res = mapper.Map<ApiPagedResult<ShopSimpleInfoDTO>>(result);
            }
            return res;
        }
    }
}
