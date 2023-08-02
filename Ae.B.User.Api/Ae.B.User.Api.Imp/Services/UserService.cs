using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Client.Clients;
using Ae.B.User.Api.Client.Model.Vehicle;
using Ae.B.User.Api.Client.Request;
using Ae.B.User.Api.Client.Request.Address;
using Ae.B.User.Api.Client.Request.Vehicle;
using Ae.B.User.Api.Client.Response;
using Ae.B.User.Api.Common.Exceptions;
using Ae.B.User.Api.Core.Interfaces;
using Ae.B.User.Api.Core.Model.User;
using Ae.B.User.Api.Core.Model.Vehicle;
using Ae.B.User.Api.Core.Request.Address;
using Ae.B.User.Api.Core.Request.User;
using Ae.B.User.Api.Core.Request.Vehicle;
using Ae.B.User.Api.Core.Response;
using Ae.B.User.Api.Core.Response.User;

namespace Ae.B.User.Api.Imp.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserClient _userClient;
        private readonly VehicleClient _vehicleClient;
        private readonly IIdentityService _identityService;
        private readonly string _source = "BOSS";

        public UserService(IMapper mapper, UserClient userClient, VehicleClient vehicleClient,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _userClient = userClient;
            _vehicleClient = vehicleClient;
            _identityService = identityService;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<UserBaseInfoVo>, int>> GetUserList(UserListRequest request)
        {
            GetUserListClientRequest clientRequest = _mapper.Map<GetUserListClientRequest>(request);

            var result = await _userClient.GetUserList(clientRequest);

            return new Tuple<List<UserBaseInfoVo>, int>(_mapper.Map<List<UserBaseInfoVo>>(result.Items),
                result.TotalItems);
        }

        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> GetUserInfo(GetUserInfoRequest request)
        {
            var resultTask = _userClient.GetUserInfo(new UserInfoClientRequest() {UserId = request.UserId});
            var addressResultTask =
                _userClient.GetUserAddressList(new UserAddressListClientRequest() {UserId = request.UserId});
            var vehicleResultTask = _vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest()
                {UserId = request.UserId});
            await Task.WhenAll(resultTask, addressResultTask, vehicleResultTask);
            var result = resultTask.Result;
            var addressResult = addressResultTask.Result ?? new List<UserAddressDTO>();
            var vehicleResult = vehicleResultTask.Result ?? new List<UserVehicleDto>();
            if (result != null)
            {
                var userResult = _mapper.Map<UserInfoResponse>(result);
                userResult.Vehicles = _mapper.Map<List<UserVehicleVo>>(vehicleResult);
                userResult.Addresses = _mapper.Map<List<UserAddressVo>>(addressResult);
                return userResult;
            }

            throw new CustomException("客户信息不存在");
        }

        /// <summary>
        /// 手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> GetUserInfoByUserTel(UserInfoByUserTelRequest request)
        {
            var result = await _userClient.GetUserInfoByUserTel(new UserInfoByUserTelClientRequest()
                {UserTel = request.UserTel});
            if (result != null)
            {
                var addressResultTask = _userClient.GetUserAddressList(new UserAddressListClientRequest()
                    {UserId = result.UserId});
                var vehicleResultTask = _vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest()
                    {UserId = result.UserId});
                await Task.WhenAll(addressResultTask, vehicleResultTask);
                var addressResult = addressResultTask.Result ?? new List<UserAddressDTO>();
                var vehicleResult = vehicleResultTask.Result ?? new List<UserVehicleDto>();
                var userResult = _mapper.Map<UserInfoResponse>(result);
                userResult.Vehicles = _mapper.Map<List<UserVehicleVo>>(vehicleResult);
                userResult.Addresses = _mapper.Map<List<UserAddressVo>>(addressResult);
                return userResult;
            }

            return null;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateUser(CreateUserRequest request)
        {
            var result = await _userClient.CreateUser(new CreateUserClientRequest()
            {
                SubmitBy = _identityService.GetUserName(),
                UserName = request.UserName,
                UserTel = request.UserTel,
                Gender = request.Gender,
                Channel = ChannelType.Boss,
                ReferrerType = ReferrerType.BossWeb
            });

            return result;
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserInfo(EditUserInfoRequest request)
        {
            var result = await _userClient.EditUserInfo(new EditUserInfoClientRequest
            {
                UserId = request.UserId,
                UserName = request.UserName,
                Gender = request.Gender,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 搜索用户 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserInfoVo>> SearchUserInfo(SearchUserInfoRequest request)
        {
            ApiPagedResultData<UserInfoVo> data = new ApiPagedResultData<UserInfoVo>()
            {
                Items = new List<UserInfoVo>()
            };
            var result = await _userClient.SearchUserInfo(new SearchUserInfoClientRequest()
            {
                UserName = request.UserName,
                UserTel = request.UserTel,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null)
                {
                    data.Items = _mapper.Map<List<UserInfoVo>>(result.Items);
                }
            }

            return data;
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddUserCar(AddUserCarRequest request)
        {
            var clientRequest = _mapper.Map<AddUserCarClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserName();
            clientRequest.DataSource = _source;
            var result = await _userClient.AddUserCarAsync(clientRequest);
            return result;
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleVO> GetUserVehicleByCarId(UserVehicleByCarIdRequest request)
        {
            var result = await _userClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest()
            {
                UserId = request.UserId,
                CarId = request.CarId
            });
            if (result != null)
            {
                return _mapper.Map<UserVehicleVO>(result);
            }

            return null;
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserCar(DeleteUserCarRequest request)
        {
            var result = await _userClient.DeleteUserCarAsync(new DeleteUserCarClientRequest()
            {
                UserId = request.UserId,
                CarId = request.CarId,
                Submitter = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserCar(EditUserCarRequest request)
        {

            EditUserCarClientRequest clientRequest = new EditUserCarClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId,
                NickName = request.NickName,
                CarNumber = request.CarNumber,
                Brand = request.Brand,
                Vehicle = request.Vehicle,
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid,
                SalesName = request.SalesName,
                BuyYear = request.BuyYear,
                BuyMonth = request.BuyMonth,
                TotalMileage = request.TotalMileage,
                LastBaoYangKm = request.LastBaoYangKm,
                LastBaoYangTime = request.LastBaoYangTime,
                VinCode = request.VinCode,
                DefaultCar = request.DefaultCar,
                EngineNo = request.EngineNo,
                TireSizeForSingle = request.TireSizeForSingle,
                InsuranceCompany = request.InsuranceCompany,
                RegistrationTime = request.RegistrationTime,
                CarNoProvince = request.CarNoProvince,
                CarNoCity = request.CarNoCity,
                Submitter = _identityService.GetUserId(),
                CarProperties = request.CarProperties?.Select(_ => new VehiclePropertyClientRequest
                {
                    PropertyKey = _.PropertyKey,
                    PropertyValue = _.PropertyValue
                })?.ToList()
            };
            if (!string.IsNullOrEmpty(request.InsureExpireDate))
            {
                clientRequest.InsureExpireDate = Convert.ToDateTime(request.InsureExpireDate);
            }

            var result = await _userClient.EditUserCarAsync(clientRequest);
            return result;
        }

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserAddressVo>> GetAllUserAddress(AllUserAddressRequest request)
        {
            List<UserAddressVo> result = new List<UserAddressVo>();
            var addressResult = await _userClient.GetUserAddressList(new UserAddressListClientRequest()
                {UserId = request.UserId});
            if (addressResult != null)
            {
                result = _mapper.Map<List<UserAddressVo>>(addressResult);
            }

            return result;
        }

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddUserAddress(AddUserAddressRequest request)
        {
            var clientRequest = _mapper.Map<CreateUserAddressClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserId();
            var result = await _userClient.CreateUserAddress(clientRequest);
            return result.Data;
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserAddress(EditUserAddressRequest request)
        {
            var clientRequest = _mapper.Map<UpdateUserAddressClientRequest>(request);
            clientRequest.UpdateBy = _identityService.GetUserId();
            clientRequest.Id = request.AddressId;
            var result = await _userClient.UpdateUserAddress(clientRequest);
            return result.Data;
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAddress(DeleteUserAddressRequest request)
        {
            var clientRequest = new DeleteUserAddressClientRequest()
            {
                AddressId = request.AddressId,
                UserId = request.UserId,
                UpdateBy = _identityService.GetUserId()
            };
            var result = await _userClient.DeleteUserAddress(clientRequest);
            return result.Data;
        }
    }
}
