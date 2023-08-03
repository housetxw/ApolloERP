using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.UserAddress;
using Ae.C.MiniApp.Api.Client.Request.UserAddress;
using Ae.C.MiniApp.Api.Client.Response.UserAddress;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Client.Request.User;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IUserAddressClient
    {
        Task<ApiResult<UserAddressClientResponse>> GetUserAddressList(GetUserAddressClientRequest request);

        Task<ApiResult<UserAddressDTO>> GetUserAddressDetail(GetUserAddressClientRequest request);

        Task<ApiResult<bool>> DeleteUserAddress(DeleteUserAddressClientRequest request);

        Task<ApiResult<int>> CreateUserAddress(CreateUserAddressClientRequest request);

        Task<ApiResult<bool>> UpdateUserAddress(UpdateUserAddressClientRequest request);

        Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagClientRequest request);

        Task<ApiResult<UserAddressTagClientResponse>> GetUserAddressTagList(GetUserAddressTagClientRequest request);

        Task<bool> SetDefaultAddress(SetDefaultAddressClientRequest request);
    }
}
