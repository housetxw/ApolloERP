using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.HomeCare;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IHomeCareService
    {
        #region  技师领料
        Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecordPages(HomeCareRecordRequest request);

        Task<ApiResult<string>> CreateHomeCareRecord(HomeCareRecordDTO request);

        Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareProducts(HomeCareProductDTO request);

        Task<ApiResult<string>> OrderInstallNotify(HomeCareOrderDTO request);

        Task<ApiResult<List<EmployeeDTO>>> GetEmployes();

        #endregion

        #region  技师退料

        Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecordPages(HomeCareRecordRequest request);

        Task<ApiResult<string>> CreateHomeReturnRecord(HomeReturnRecordDTO request);

        Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProducts(HomeReturnProductDTO request);

        Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProductsByTech(HomeReturnRecordDTO request);
        #endregion

        #region App接口
        Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareStocks(HomeCareStockRequest request);

        Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecords(HomeCareRequest request);

        Task<ApiResult<HomeCareRecordDTO>> GetHomeCareRecord(HomeCareDetailRequest request);

        Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecords(HomeCareRequest request);

        Task<ApiResult<HomeReturnRecordDTO>> GetHomeReturnRecord(HomeCareDetailRequest request);


        #endregion

        #region  耗材领料
        Task<ApiPagedResult<ProductClaimRecordDTO>> GetProductClaimRecordPages(HomeCareRecordRequest request);

        Task<ApiResult<string>> CreateProductClaimRecord(ProductClaimRecordDTO request);
        Task<ApiResult<string>> CancelProductClaimRecord(HomeCareDetailRequest request);
        Task<ApiResult<string>> ClaimRepeatReduceStock(HomeCareDetailRequest request);

        Task<ApiResult<List<TechClaimProductDTO>>> GetProductClaimRecords(TechClaimProductDTO request);



        #endregion
    }
}
