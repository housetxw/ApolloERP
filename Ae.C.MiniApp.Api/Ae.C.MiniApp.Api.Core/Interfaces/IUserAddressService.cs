using RedGoose.Web.WebApi;
using Rg.C.MiniApp.Api.Core.Model;
using Rg.C.MiniApp.Api.Core.Request;
using Rg.C.MiniApp.Api.Core.Response.UserAddress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rg.C.MiniApp.Api.Core.Interfaces
{
  public  interface IUserAddressService
    {
        Task<ApiResult<UserAddressResponse>> GetUserAddressList(GetUserAddressRequest request);

        Task<ApiResult<UserAddressVO>> GetUserAddressDetail(GetUserAddressRequest request);

        Task<ApiResult<string>> DeleteUserAddress(DeleteUserAddressRequest request);

        Task<ApiResult<string>> CreateUserAddress(CreateUserAddressRequest request);

        Task<ApiResult<string>> UpdateUserAddress(UpdateUserAddressRequest request);

        Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request);

        Task<ApiResult<UserAddressTagResponse>> GetUserAddressTagList(GetUserAddressTagRequest request);

    }
}
