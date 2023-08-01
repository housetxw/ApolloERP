using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.User.Service.Common.Constant;
using Ae.User.Service.Core.Interfaces;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using Ae.User.Service.WebApi.Filters;

namespace Ae.User.Service.Controllers
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
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<UserBaseInfoVo>>> GetUsersByUserIds([FromBody] UsersByUserIdsRequest request)
        {
            var result = await _userService.GetUsersByUserIds(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserBaseInfoVo>> GetUserInfoByUserTel([FromQuery] UserInfoByUserTelRequest request)
        {
            var result = await _userService.GetUserInfoByUserTel(request);
            return Result.Success(result);
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
        /// 获取用户地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserAddressVO>>> GetUserAddress([FromQuery] UserAddressRequest request)
        {
            return await Task.FromResult(new ApiResult<List<UserAddressVO>>());
        }

        /// <summary>
        /// 查询用户已关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserAttentionProductResponse>> GetUserAttentionProduct(
            [FromQuery] UserAttentionProductRequest request)
        {
            var result = await _userService.GetUserAttentionProduct(new UserAttentionProductRequest()
            {
                UserId = request.UserId
            });

            return Result.Success(result);
        }

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPersonalProduct([FromBody]AddPersonalProductRequest request)
        {
            var result = await _userService.AddPersonalProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelPersonalProduct([FromBody]CancelPersonalProductRequest request)
        {
            var result = await _userService.CancelPersonalProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserPointResponse>> GetUserPoint([FromQuery] UserPointRequest request)
        {
            var result = await _userService.GetUserPoint(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 内部员工积分查询（无限层级）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserPointResponse>> GetTechUserAllPoint([FromQuery] UserPointRequest request)
        {
            var result = await _userService.GetTechUserAllPoint(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<MemberLevelResponse>> GetMemberLevel([FromQuery] MemberLevelRequest request)
        {
            var result = await _userService.GetMemberLevel(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserInfo([FromBody]EditUserInfoRequest request)
        {
            var result = await _userService.EditUserInfo(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 操作用户积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> OperateUserPoint([FromBody]OperateUserPointRequest request)
        {
            var result = await _userService.OperateUserPoint(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 操作用户成长值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> OperateUserGrowthValue([FromBody]OperateUserGrowthValueRequest request)
        {
            var result = await _userService.OperateUserGrowthValue(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUser(request);

            return new ApiResult<string>()
            {
                Data = result,
                Code = ResultCode.Success,
                Message = "操作成功"
            };
        }

        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserInfoVo>> GetReferrerCustomerList([FromQuery] ReferrerCustomerListRequest request)
        {
            var result = await _userService.GetReferrerCustomerList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取用户分享获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<int>> GetReferrerCustomerNum(
            [FromQuery] ReferrerCustomerRequest req)
        {
            var res = await _userService.GetReferrerCustomerNum(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取门店当月获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopReferrerCustomerResDTO>> GetShopNewCustomerInfo(
            [FromQuery] ShopReferrerCustomerReqDTO req)
        {
            var res = await _userService.GetShopNewCustomerInfo(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取B端员工或门店分享码，获得的新客信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeeReferrerCustomerResDTO>> GetEmployeeReferrerNewCustomerPage(
            [FromQuery] EmployeeReferrerCustomerPageReqDTO req)
        {
            var res = await _userService.GetEmployeeReferrerNewCustomerPage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<UserBaseInfoVo>>> GetCommonUserInfo([FromBody] CommonUserInfoRequest request)
        {
            var res = await _userService.GetCommonUserInfo(request);

            return Result.Success(res);
        }


        /// <summary>
        /// 搜索用户 BOSS用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserInfoVo>> SearchUserInfo([FromQuery] SearchUserInfoRequest request)
        {
            var result = await _userService.SearchUserInfo(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 刷新用户成长值（job）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> RefreshUserGrowthValue()
        {
            var result = await _userService.RefreshUserGrowthValue();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户默认门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DefaultRecommendShopResponse>> GetDefaultRecommendShop(
            [FromQuery] DefaultRecommendShopRequest request)
        {
            var result = await _userService.GetDefaultRecommendShop(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取UserExpandShop
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserExpandShopVo>> GetUserExpandShop([FromQuery] GetUserExpandShopRequest request)
        {
            var result = await _userService.GetUserExpandShop(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 成为店长 - 关联用户和门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> RelationShopAndUser([FromBody] RelationShopAndUserRequest request)
        {
            var result = await _userService.RelationShopAndUser(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 实名认证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<UserAuthResponse>> UserAuth([FromBody] UserAuthRequest request)
        {
            var result = await _userService.UserAuth(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加银行卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddUserBankCard([FromBody] AddUserBankCardRequest request)
        {
            var result = await _userService.AddUserBankCard(request);

            return Result.Success(result);
        }
    }
}