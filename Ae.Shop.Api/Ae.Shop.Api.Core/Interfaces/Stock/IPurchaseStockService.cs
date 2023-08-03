using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IPurchaseStockService
    {
        /// <summary>
        /// 查询公司的供应商产品
        /// </summary>
        /// <returns></returns>
        Task<ICollection<VenderProductForAppVo>> SearchVenderProductListForApp();

        Task<ICollection<VenderProductForAppVo>> SearchVenderProductList();

        /// <summary>
        /// 初始化供应商的库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> SubmitVenderStock(VenderStockInitRequest request);

        /// <summary>
        /// 提交要货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> SubmitCompanyInStock(AcceptCompanyStockRequest request);

        /// <summary>
        /// 获取要货列表(待发货:1  已发货:2  待收货:3  已收货:4)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<VenderStockDTO>>> GetCompanyInStocks(GetCompanyStocksRequest request);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CompanySendStock(AcceptCompanyStockRequest request);

        /// <summary>
        /// 获取公司/供应商库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<VenderStockResponse>> GetCompanyStocks(GetCompanyStocksRequest request);

        /// <summary>
        /// 取消要货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CancelCompanySendStock(CancelTaskRequest request);

    }
}
