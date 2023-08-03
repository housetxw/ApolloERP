using ApolloErp.Data.DapperExtensions;
using Ae.C.Login.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.Login.Api.Dal.Repositorys.User
{
    public interface IUserRespository
    {
        Task<UserInfoDO> GetUserInfoByOpenId(string openId, int loginType);
        Task<UserInfoDO> GetUserInfoByMobile(string mobile, int loginType);
        Task<bool> CreateUser(UserDO user);
        Task<bool> UpdateUserMobileByUserId(UserDO req);
        Task<UserInfoDO> GetUserInfoByMobile(string mobile);


    }
}
