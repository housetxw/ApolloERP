using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserAddressRepository : IRepository<UserAddressDO>
    {
        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserAddressDO>> GetUserAddressAsync(string userId);

        Task<int> DeleteUserAddress(long addressId, string updateBy);

        Task<int> UpdateUserAddress(UserAddressDO request);

        Task<int> CreateUserAddress(UserAddressDO request);

        Task<UserAddressDO> GetUserAddressDetail(long addressId);

        Task<int> UdpateUserAddressDefault(UpdateUserAddressDefaultRequest request);

        Task<int> CreateUserAddressTag(UserAddressTagDO request);

        Task<List<UserAddressTagDO>> GetUserAddressTags(string userId);

        Task<int> CheckUserAddressRepeat(UserAddressDO request);


        Task<int> CheckUserAddressTagRepeat(UserAddressTagDO request);

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserAddressDO> GetUserAddressAsync(string userId, int addressId, bool readOnly = true);

        /// <summary>
        /// 设置用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> SetUserDefaultAddressAsync(string userId, int addressId, string updateBy);
    }
}
