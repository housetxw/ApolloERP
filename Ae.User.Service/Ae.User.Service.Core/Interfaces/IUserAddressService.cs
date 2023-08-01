using ApolloErp.Web.WebApi;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Core.Interfaces
{
   public interface IUserAddressService
    {
        Task<UserAddressResponse> GetUserAddressList(GetUserAddressRequest request);

        Task<UserAddressDTO> GetUserAddressDetail(GetUserAddressRequest request);

        Task<ApiResult<bool>> DeleteUserAddress(DeleteUserAddressRequest request);

        Task<int> CreateUserAddress(CreateUserAddressRequest request);

        Task<bool> UpdateUserAddress(UpdateUserAddressRequest request);

        Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request);

        Task<UserAddressTagResponse> GetUserAddressTagList(GetUserAddressTagRequest request);

        /// <summary>
        /// 设为默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SetDefaultAddress(SetDefaultAddressRequest request);
    }
}
