using Ae.Shop.Api.Client.Model.User;
using Ae.Shop.Api.Client.Request.User;
using Ae.Shop.Api.Client.Response.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.UserServer
{
    public interface IUserClient
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserInfoClientResponse> GetUserInfo(UserInfoClientRequest request);

        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelRequest request);
    }
}
