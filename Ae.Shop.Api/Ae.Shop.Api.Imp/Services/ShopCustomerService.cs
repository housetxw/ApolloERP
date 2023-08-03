using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.UserServer;
using Ae.Shop.Api.Client.Clients.VehicleServer;
using Ae.Shop.Api.Client.Model.Vehicle;
using Ae.Shop.Api.Client.Request.ShopManage;
using Ae.Shop.Api.Client.Request.User;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using Ae.Shop.Api.Core.Request.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopCustomerService : IShopCustomerService
    {
        private readonly IShopMangeClient shopMangeClient;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IUserClient userClient;
        private readonly IVehicleClient vehicleClient;

        public ShopCustomerService(IShopMangeClient shopMangeClient, IIdentityService identityService, IMapper mapper,
            IUserClient userClient, IVehicleClient vehicleClient)
        {
            this.shopMangeClient = shopMangeClient;
            this.identityService = identityService;
            this.mapper = mapper;
            this.userClient = userClient;
            this.vehicleClient = vehicleClient;
        }

        /// <summary>
        /// 门店客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCustomerListVo>> GetShopCustomerList(ShopCustomerListRequest request)
        {
            ApiPagedResultData<ShopCustomerListVo> result = new ApiPagedResultData<ShopCustomerListVo>
            {
                Items = new List<ShopCustomerListVo>()
            };
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var data = await shopMangeClient.GetShopCustomerList(new ShopCustomerListClientRequest
            {
                ShopId = shopId,
                UserName = request.UserName,
                UserTel = request.UserTel,
                StartInTime = request.StartInTime,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (data != null)
            {
                result.TotalItems = data.TotalItems;
                if (data.Items != null)
                {
                    result.Items = mapper.Map<List<ShopCustomerListVo>>(data.Items);
                }
            }
            return result;
        }

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopCustomerDetailVo> GetShopCustomerDetail(ShopCustomerDetailRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var shopCustomerTask = shopMangeClient.GetShopCustomerDetail(new ShopCustomerDetailClientRequest
            {
                UserId = request.UserId,
                ShopId = shopId
            });
            var userTask = userClient.GetUserInfo(new UserInfoClientRequest()
            {
                UserId = request.UserId
            });
            var vehicleTask = vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest
            {
                UserId = request.UserId
            });
            await Task.WhenAll(shopCustomerTask, userTask, vehicleTask);
            var shopCustomer = shopCustomerTask.Result;
            var user = userTask.Result;
            var vehicle = vehicleTask.Result ?? new List<UserVehicleDto>();
            if (shopCustomer == null || user == null)
            {
                throw new CustomException("客户信息异常");
            }

            ShopCustomerDetailVo result = mapper.Map<ShopCustomerDetailVo>(user);
            result.LastArriveTime = shopCustomer.LastArriveTime ?? string.Empty;
            result.Vehicles = mapper.Map<List<UserVehicleVo>>(vehicle);
            return result;
        }
    }
}
