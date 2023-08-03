using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model.User;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
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
        /// 操作用户积分
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
