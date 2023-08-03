using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Activity;
using Ae.C.MiniApp.Api.Core.Response.Activity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IActivityService
    {
        Task<ApiResult<H5PromoteArticleQueryResponse>> H5PromoteArticleQuery(H5PromoteArticleQueryRequest request);
        Task<ApiResult<object>> GetWxacodeScene(GetWxacodeSceneRequest request);
        Task<ApiResult> SharePromotionContent(ApiRequest<SharePromotionContentRequest> request);
    }
}
