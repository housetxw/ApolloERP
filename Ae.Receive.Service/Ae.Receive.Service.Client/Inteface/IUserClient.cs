using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.User;
using Ae.Receive.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IUserClient
    {
        Task<UserInfoClientResponse> GetUserInfo(GetUserInfoClientRequest request);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> CreateUser(CreateUserClientRequest request);
    }
}
