using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Core.Interfaces;
using Ae.B.User.Api.Core.Model.User;
using Ae.B.User.Api.Core.Model.Vehicle;
using Ae.B.User.Api.Core.Request.Address;
using Ae.B.User.Api.Core.Request.User;
using Ae.B.User.Api.Core.Request.Vehicle;
using Ae.B.User.Api.Core.Response.User;
using Ae.B.User.Api.Filters;

namespace Ae.B.User.Api.Controllers
{
    /// <summary>
    /// 用户信息相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(UserController))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserBaseInfoVo>> GetUserList([FromQuery] UserListRequest request)
        {
            var result = await _userService.GetUserList(request);

            return Result.Success(result.Item1, result.Item2);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserInfoResponse>> GetUserInfo([FromQuery] GetUserInfoRequest request)
        {
            var result = await _userService.GetUserInfo(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserInfoResponse>> GetUserInfoByUserTel(
            [FromQuery] UserInfoByUserTelRequest request)
        {
            var result = await _userService.GetUserInfoByUserTel(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUser([FromBody] ApiRequest<CreateUserRequest> request)
        {
            var result = await _userService.CreateUser(request.Data);

            return Result.Success<string>(result);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveUser([FromBody] ApiRequest<EditUserInfoRequest> request)
        {
            var result = await _userService.EditUserInfo(request.Data);

            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 搜索用户 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserInfoVo>> SearchUserInfo([FromQuery] SearchUserInfoRequest request)
        {
            var result = await _userService.SearchUserInfo(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        #region 用户车型

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddUserCar([FromBody] ApiRequest<AddUserCarRequest> request)
        {
            var result = await _userService.AddUserCar(request.Data);

            return Result.Success<string>(result);
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleVO>> GetUserVehicleByCarId(
            [FromQuery] UserVehicleByCarIdRequest request)
        {
            var result = await _userService.GetUserVehicleByCarId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserCar([FromBody] ApiRequest<DeleteUserCarRequest> request)
        {
            var result = await _userService.DeleteUserCar(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserCar([FromBody] ApiRequest<EditUserCarRequest> request)
        {
            var result = await _userService.EditUserCar(request.Data);

            return Result.Success(result);
        }

        #endregion

        #region 用户地址

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserAddressVo>>> GetAllUserAddress([FromQuery] AllUserAddressRequest request)
        {
            var result = await _userService.GetAllUserAddress(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> AddUserAddress([FromBody] ApiRequest<AddUserAddressRequest> request)
        {
            var result = await _userService.AddUserAddress(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserAddress([FromBody] ApiRequest<EditUserAddressRequest> request)
        {
            var result = await _userService.EditUserAddress(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserAddress([FromBody] ApiRequest<DeleteUserAddressRequest> request)
        {
            var result = await _userService.DeleteUserAddress(request.Data);

            return Result.Success(result);
        }

        #endregion
    }
}