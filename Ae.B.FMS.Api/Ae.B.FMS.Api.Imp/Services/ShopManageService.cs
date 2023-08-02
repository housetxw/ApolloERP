using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.FMS.Api.Imp.Services
{
   public class ShopManageService: IShopManageService
    {
        private readonly IMapper _mapper;
        private readonly IShopManageClient shopManageClient;


        public ShopManageService(IMapper mapper,
          IShopManageClient shopManageClient)
        {
            _mapper = mapper;
            this.shopManageClient = shopManageClient;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync(GetShopListRequest request)
        {
            var res = await shopManageClient.GetShopListAsync(new GetShopListClientRequest
            {
                PageIndex = 1,
                PageSize = 20,
                SimpleName = request.SimpleName
            });

            var vo = _mapper.Map<ApiPagedResult<ShopSimpleInfoResponse>>(res);
            return vo;
        }
    }
}
