using ApolloErp.Web.WebApi;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Core.Interfaces
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<UserBaseInfoVo>, int>> GetUserList(UserListRequest request);

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserInfoResponse> GetUserInfo(GetUserInfoRequest request);

        /// <summary>
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoVo>> GetUsersByUserIds(UsersByUserIdsRequest request);

        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserBaseInfoVo> GetUserInfoByUserTel(UserInfoByUserTelRequest request);

        /// <summary>
        /// 查询用户已关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserAttentionProductResponse> GetUserAttentionProduct(UserAttentionProductRequest request);

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPersonalProduct(AddPersonalProductRequest request);

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CancelPersonalProduct(CancelPersonalProductRequest request);

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserPointResponse> GetUserPoint(UserPointRequest request);
        /// <summary>
        /// 内部员工积分查询（无限层级）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserPointResponse> GetTechUserAllPoint(UserPointRequest request);

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MemberLevelResponse> GetMemberLevel(MemberLevelRequest request);

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="request"></param>
        Task<bool> EditUserInfo(EditUserInfoRequest request);

        /// <summary>
        /// 操作用户积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> OperateUserPoint(OperateUserPointRequest request);

        /// <summary>
        /// 操作用户成长值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> OperateUserGrowthValue(OperateUserGrowthValueRequest request);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        Task<string> CreateUser(CreateUserRequest request);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoVo>> GetCommonUserInfo(CommonUserInfoRequest request);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserInfoVo>> SearchUserInfo(SearchUserInfoRequest request);

        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserInfoVo>> GetReferrerCustomerList(ReferrerCustomerListRequest req);

        /// <summary>
        /// 获取用户分享获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<int> GetReferrerCustomerNum(ReferrerCustomerRequest req);

        /// <summary>
        /// 获取门店当月获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ShopReferrerCustomerResDTO> GetShopNewCustomerInfo(ShopReferrerCustomerReqDTO req);

        Task<ApiPagedResultData<EmployeeReferrerCustomerResDTO>> GetEmployeeReferrerNewCustomerPage(EmployeeReferrerCustomerPageReqDTO req);

        /// <summary>
        /// 刷新用户成长值（job）
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshUserGrowthValue();

        /// <summary>
        /// 获取用户默认门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<DefaultRecommendShopResponse> GetDefaultRecommendShop(DefaultRecommendShopRequest request);

        /// <summary>
        /// 获取UserExpandShop
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserExpandShopVo> GetUserExpandShop(GetUserExpandShopRequest request);

        /// <summary>
        /// 成为店长 - 关联用户和门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> RelationShopAndUser(RelationShopAndUserRequest request);

        /// <summary>
        /// 实名认证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserAuthResponse> UserAuth(UserAuthRequest request);

        /// <summary>
        /// 添加银行卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddUserBankCard(AddUserBankCardRequest request);
    }
}
