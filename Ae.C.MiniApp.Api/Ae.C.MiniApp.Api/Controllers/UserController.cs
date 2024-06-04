using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Response.User;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.User;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Request.User;
using Ae.C.MiniApp.Api.Core.Response.User;
using Ae.C.MiniApp.Api.Core.Response.UserAddress;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(UserController))]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly ApolloErpLogger<UserController> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="identityService"></param>
        /// <param name="logger"></param>
        public UserController(IUserService userService, IIdentityService identityService,
            ApolloErpLogger<UserController> logger)
        {
            _userService = userService;
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<UserInfoResponse>> GetUserInfo()
        {
            string userId = _identityService.GetUserId();
            var result = await _userService.GetUserInfo(userId);

            return Result.Success(result);
        }

        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<UserInfoResponse>> GetUserInfoById([FromQuery] GetUserRequest request)
        {
            string userId = request.UserId;
            var result = await _userService.GetUserInfo(userId);

            return Result.Success(result);
        }

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserPointResponse>> GetUserPoint()
        {
            var result = await _userService.GetUserPoint(_identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 会员等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<MemberLevelResponse>> GetMemberLevel()
        {
            var result = await _userService.GetMemberLevel(_identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<bool>> EditUserInfo(ApiRequest<EditUserInfoRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.EditUserInfo(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改手机号获取验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ApiResult<bool>> SendVerifyCodeSmsForChangeMobile(
            ApiRequest<SendVerifyCodeSmsRequest> request)
        {
            var result = await _userService.SendChangeMobileVerificationCodeSms(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 验证当前手机号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> VerifyCurrentMobile(ApiRequest<VerifyCurrentMobileRequest> request)
        {
            var result = await _userService.VerifyCurrentMobile(request.Data, _identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 修改用户手机号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateUserMobile(ApiRequest<UpdateUserMobileRequest> request)
        {
            var result = await _userService.UpdateUserMobile(request.Data, _identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPersonalProduct(ApiRequest<AddPersonalProductRequest> request)
        {
            var result = await _userService.AddPersonalProduct(request.Data, _identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelPersonalProduct(ApiRequest<CancelPersonalProductRequest> request)
        {
            var result = await _userService.CancelPersonalProduct(request.Data, _identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 我的关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserProductVo>> GetUserAttentionProducts(
            [FromQuery] UserAttentionProductsRequest request)
        {
            request.UserId = _identityService.GetUserId();
            var result = await _userService.GetUserAttentionProducts(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserAddressVO>>> GetUserAddress()
        {
            List<UserAddressVO> result = await _userService.GetUserAddress(_identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressVO>> GetUserDefaultAddress()
        {
            var result = await _userService.GetUserDefaultAddress(_identityService.GetUserId());

            return Result.Success(result);
        }

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> AddUserAddress(ApiRequest<AddUserAddressRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.AddUserAddress(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserAddress(ApiRequest<EditUserAddressRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.EditUserAddress(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserAddress(ApiRequest<DeleteUserAddressRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.DeleteUserAddress(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SetDefaultAddress(ApiRequest<SetDefaultAddressRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.SetDefaultAddress(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUserAddressTag(
            [FromBody] ApiRequest<CreateUserAddressTagRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _userService.CreateUserAddressTag(request.Data);

            return result;
        }

        /// <summary>
        /// 获取用户标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAddressTagResponse>> GetUserAddressTagList()
        {
            var result = await _userService.GetUserAddressTagList(_identityService.GetUserId());

            return Result.Success(result);
        }


        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserInfoResponse>> GetReferrerCustomerList(
            [FromQuery] GetSharingOrdersRequest request)
        {
            request.UserId = _identityService.GetUserId();
            var result = await _userService.GetReferrerCustomerList(request);

            return result;
        }

    }
}