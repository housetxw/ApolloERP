using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaoYangService
    {
        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(GetBaoYangPackagesRequest request);

        /// <summary>
        /// 保养更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsWithCondition(
            SearchProductRequest request);
    }
}
