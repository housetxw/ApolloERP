using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.User.Service.Core.Interfaces;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using Ae.User.Service.WebApi.Filters;

namespace Ae.User.Service.Controllers
{

    /// <summary>
    /// 用户地址相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(UserAddressController))]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService userAddressService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userAddressService"></param>
        public UserAddressController(IUserAddressService userAddressService)
        {
            this.userAddressService = userAddressService;
        }

        /// <summary>
        /// 创建用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> CreateUserAddress([FromBody] CreateUserAddressRequest request)
        {
            var result = await userAddressService.CreateUserAddress(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 更新用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateUserAddress([FromBody] UpdateUserAddressRequest request)
        {
            var result = await userAddressService.UpdateUserAddress(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 设为默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SetDefaultAddress([FromBody]SetDefaultAddressRequest request)
        {
            var result = await userAddressService.SetDefaultAddress(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserAddress([FromBody]DeleteUserAddressRequest request)
        {
            var result = await userAddressService.DeleteUserAddress(request);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取用户地址详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressDTO>> GetUserAddressDetail([FromQuery]GetUserAddressRequest request)
        {
            var result = await userAddressService.GetUserAddressDetail(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户地址列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressResponse>> GetUserAddressList([FromQuery]GetUserAddressRequest request)
        {
            var result = await userAddressService.GetUserAddressList(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressTagResponse>> GetUserAddressTagList([FromQuery]GetUserAddressTagRequest request)
        {
            var result = await userAddressService.GetUserAddressTagList(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUserAddressTag([FromBody]CreateUserAddressTagRequest request)
        {
            var result = await userAddressService.CreateUserAddressTag(request);
            return await Task.FromResult(result);
        }
    }
}