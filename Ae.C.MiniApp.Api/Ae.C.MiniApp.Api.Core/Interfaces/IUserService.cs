using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.User;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Request.User;
using Ae.C.MiniApp.Api.Core.Response.User;
using Ae.C.MiniApp.Api.Core.Response.UserAddress;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserInfoResponse> GetUserInfo(string userId);

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserPointResponse> GetUserPoint(string userId);

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<MemberLevelResponse> GetMemberLevel(string userId);

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserInfo(EditUserInfoRequest request);

        /// <summary>
        /// 发送修改手机号验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SendChangeMobileVerificationCodeSms(SendVerifyCodeSmsRequest request);

        /// <summary>
        /// 验证当前手机号
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> VerifyCurrentMobile(VerifyCurrentMobileRequest request, string userId);

        /// <summary>
        /// 修改用户手机号
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> UpdateUserMobile(UpdateUserMobileRequest request, string userId);

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AddPersonalProduct(AddPersonalProductRequest request, string userId);

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> CancelPersonalProduct(CancelPersonalProductRequest request, string userId);

        /// <summary>
        /// 我的关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserProductVo>> GetUserAttentionProducts(UserAttentionProductsRequest request);

        /// <summary>
        /// 获取用户所有地址信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<UserAddressVO>> GetUserAddress(string userId);

        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserAddressVO> GetUserDefaultAddress(string userId);

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddUserAddress(AddUserAddressRequest request);

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserAddress(EditUserAddressRequest request);

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAddress(DeleteUserAddressRequest request);

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SetDefaultAddress(SetDefaultAddressRequest request);

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request);

        /// <summary>
        /// 获取用户标签列表
        /// </summary>
        /// <returns></returns>
        Task<UserAddressTagResponse> GetUserAddressTagList(string userId);

        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiPagedResult<UserInfoResponse>> GetReferrerCustomerList(GetSharingOrdersRequest req);
    }
}
