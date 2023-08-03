using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Interface
{
    public interface IUserClient
    {
        Task<ApiResult<UserInfoClientResponse>> GetUserInfo(GetUserInfoClientRequest request);

        /// <summary>
        ///  userId批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoDto>> GetUsersByUserIds(List<string> userIds);
    }
}
