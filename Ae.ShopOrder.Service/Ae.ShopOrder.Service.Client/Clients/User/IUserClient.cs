using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.User;
using Ae.ShopOrder.Service.Client.Request.User;
using Ae.ShopOrder.Service.Client.Response.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.User
{
    public interface IUserClient
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<UserInfoResponse>> GetUserInfo(GetUserInfoRequest request);

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<UserAddressDTO>>> GetUserAddress(UserAddressRequest request);


        /// <summary>
        /// 获取用户信息根据手机号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetUserInfoByUserTelResponse>> GetUserInfoByUserTel(GetUserInfoByUserTelRequest request);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CreateUser(CreateUserRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> OperateUserPoint(OperateUserPointRequest request);
        /// <summary>
        /// 操作用户成长值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> OperateUserGrowthValue(OperateUserGrowthValueRequest request);

    }
}
