using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedGoose.Web.WebApi;
using Rg.C.MiniApp.Api.Core.Interfaces;
using Rg.C.MiniApp.Api.Core.Model;
using Rg.C.MiniApp.Api.Core.Request;
using Rg.C.MiniApp.Api.Core.Response;
using Rg.C.MiniApp.Api.Core.Response.UserAddress;
using Rg.C.MiniApp.Api.Filters;

namespace Rg.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 用户地址
    /// </summary>
    [Route("[controller]/[action]")]
    [Filter(nameof(UserAddressController))]
    [ApiController]
    [Obsolete]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressService userAddressService;

        public UserAddressController(IUserAddressService userAddressService) {
            this.userAddressService = userAddressService;
        }

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressResponse>> GetUserAddressList([FromQuery]GetUserAddressRequest request) {
            var result = await userAddressService.GetUserAddressList(request);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取用户地址详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressVO>> GetUserAddressDetail([FromQuery]GetUserAddressRequest request)
        {
            var result = await userAddressService.GetUserAddressDetail(request);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> DeleteUserAddress([FromBody]ApiRequest<DeleteUserAddressRequest> request)
        {
            var result = await userAddressService.DeleteUserAddress(request.Data);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 创建用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUserAddress([FromBody]ApiRequest<CreateUserAddressRequest> request)
        {
            var result = await userAddressService.CreateUserAddress(request.Data);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 更新用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateUserAddress([FromBody]ApiRequest<UpdateUserAddressRequest> request)
        {
            var result = await userAddressService.UpdateUserAddress(request.Data);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUserAddressTag([FromBody]ApiRequest<CreateUserAddressTagRequest> request)
        {
            var result = await userAddressService.CreateUserAddressTag(request.Data);
            return await Task.FromResult(result);

        }

        /// <summary>
        /// 获取用户地址列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressTagResponse>> GetUserAddressTagList([FromQuery]GetUserAddressTagRequest request)
        {
            var result = await userAddressService.GetUserAddressTagList(request);
            return await Task.FromResult(result);
        }

    }
}
