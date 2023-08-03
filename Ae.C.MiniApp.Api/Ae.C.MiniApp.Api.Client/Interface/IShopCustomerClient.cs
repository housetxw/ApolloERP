using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IShopCustomerClient
    {
        /// <summary>
        /// 用户关联门店(即创建门店会员信息)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddShopUserRelation(AddShopUserRelationRequest request);
    }
}
