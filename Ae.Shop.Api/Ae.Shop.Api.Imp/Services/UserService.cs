using AutoMapper;
using Ae.Shop.Api.Client.Clients.UserServer;
using Ae.Shop.Api.Client.Clients.VehicleServer;
using Ae.Shop.Api.Client.Request.User;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using Ae.Shop.Api.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class UserService : IUserService
    {
        private readonly IVehicleClient vehicleClient;
        private readonly IUserClient userClient;
        private readonly IMapper mapper;

        public UserService(IVehicleClient vehicleClient, IUserClient userClient, IMapper mapper)
        {
            this.vehicleClient = vehicleClient;
            this.userClient = userClient;
            this.mapper = mapper;
        }

        /// <summary>
        /// 根据手机号查询客户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserDetailVo> GetUserDetailByUserTel(UserDetailByUserTelRequest request)
        {
            UserDetailVo result = null;
            var userObject = await userClient.GetUserInfoByUserTel(new UserInfoByUserTelRequest()
            {
                UserTel = request.UserTel
            });
            if (userObject != null)
            {
                result = new UserDetailVo()
                {
                    UserId = userObject.UserId,
                    UserName = userObject.UserName,
                    UserTel = userObject.UserTel
                };
                var userVehicle = await vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest
                {
                    UserId = userObject.UserId
                });
                if (userVehicle != null && userVehicle.Any())
                {
                    var cars = mapper.Map<List<UserVehicleVo>>(userVehicle);
                    cars.ForEach(_ =>
                    {
                        _.CarType = _.Vehicle + "|" + _.PaiLiang + "|" + _.Nian + "|" + _.SalesName;
                    });

                    result.Cars = cars;
                }
            }

            return result;
        }
    }
}
