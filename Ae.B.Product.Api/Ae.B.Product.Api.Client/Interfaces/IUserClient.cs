using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model.User;
using Ae.B.Product.Api.Client.Request.User;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IUserClient
    {
        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserBaseInfoDto> GetUserInfoByUserTel(UserInfoByUserTelClientRequest request);

        /// <summary>
        /// 用户姓名/手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserInfoDto>> SearchUserInfo(SearchUserInfoClientRequest request);

        /// <summary>
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoDto>> GetUsersByUserIds(List<string> userIds);
    }
}
