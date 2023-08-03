using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Activity;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Activity;
using Ae.C.MiniApp.Api.Client.Response.Activity;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IActivityClient
    {
        /// <summary>
        /// 生成小程序码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GenMinAppCodeResponse>> GenMinAppCode(GenMinAppCodeClientRequest request);
        /// <summary>
        /// 获取推广内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PromoteContentDTO>> GetPromoteContent(GetPromoteContentRequest request);
        /// <summary>
        /// 解析微信小程序场景参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<object>> GetWxacodeScene(GetWxacodeSceneClientRequest request);
        /// <summary>
        /// 分享推广内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SharePromotionContent(ApiRequest<SharePromotionContentClientRequest> request);
    }
}
