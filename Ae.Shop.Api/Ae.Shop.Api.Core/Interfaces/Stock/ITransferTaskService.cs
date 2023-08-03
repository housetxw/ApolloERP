using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface ITransferTaskService
    {
        /// <summary>
        /// 清点入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CheckInStock(CheckInStockRequest request);

        Task<TodaySignPackageApiPagedResult<GetTodayReceivePackageDTO>> GetTodayReceivePackage(GetTodayReceivePackageRequest request);

        //Task<ApiResult<ReceivePackageDTO>> GetReceivePackagesByPackageNos(GetReceivePackageRequest request);

        Task<ApiResult<ReceivePackageDTO>> SignInStock(SignInStockRequest request);

        Task<ApiResult<string>> CheckInStockForShop(CompanyInStockDTO request);

        Task<ApiResult<string>> VenderCheckInStock(AcceptCompanyStockRequest request);

    }
}
