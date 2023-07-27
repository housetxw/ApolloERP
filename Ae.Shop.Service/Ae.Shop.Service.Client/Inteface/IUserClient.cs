using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Client.Model.User;
using Ae.Shop.Service.Client.Request;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IUserClient
    {
        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoDto>> GetCommonUserInfo(CommonUserInfoRequest request);

        /// <summary>
        /// GetUsersByUserIds
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserBaseInfoDto>> GetUsersByUserIds(UsersByUserIdsClientRequest request);
    }
}
