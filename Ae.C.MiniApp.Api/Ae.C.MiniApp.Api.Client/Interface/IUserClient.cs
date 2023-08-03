using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.User;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.User;
using Ae.C.MiniApp.Api.Client.Response.User;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserClient
    {
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserInfoClientResponse> GetUserInfo(GetUserInfoClientRequest request);

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserPointClientResponse> GetUserPoint(UserPointClientRequest request);

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MemberLevelClientResponse> GetMemberLevel(MemberLevelClientRequest request);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserInfo(EditUserInfoClientRequest request);

        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelClientRequest request);

        /// <summary>
        /// 关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPersonalProduct(AddPersonalProductClientRequest request);

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CancelPersonalProduct(CancelPersonalProductClientRequest request);

        /// <summary>
        /// 我的关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> GetUserAttentionProduct(UserAttentionProductClientRequest request);


        Task<ApiPagedResult<UserInfoClientResponse>> GetReferrerCustomerList(GetSharingOrdersRequest request);

        Task<int> GetReferrerCustomerNum(BaseUserClientRequest request);
    }
}
