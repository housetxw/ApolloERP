using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Promotion;
using Ae.C.MiniApp.Api.Client.Request.Promotion;
using Ae.C.MiniApp.Api.Core.Request.Promotion;
using Ae.C.MiniApp.Api.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IPromotionClient
    {
        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductPromotionDetailDto> GetProductPromotionDetail(ProductPromotionDetailClientRequest request);

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetPromotionActivitByProductCodeListResponse>>> GetPromotionActivitByProductCodeList(GetPromotionActivitByProductCodeListRequest request);
    }
}
