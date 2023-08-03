using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface ITransferService
    {
        Task<ApiPagedResult<AllotTaskDTO>> GetAllotPageData(AllotPageRequest request);

        //Task<ApiResult<List<GroupSelectDTO>>> GetSourceWarehouses(GetShopInfoRequest request);

        //Task<ApiResult<List<StockLocationDTO>>> GetAllotProductsStock(AllotStockRequest request);

        Task<ApiResult<AllotTaskDTO>> GetAllotTaskDetail(AllotPageRequest request);

        Task<ApiResult<string>> CreateAllotTask(AllotTaskDTO request);
        Task<ApiResult<string>> CreateAllotTaskAndUpdateStock(AllotTaskDTO request);

        //Task<ApiResult<string>> AuditAllotTask(AllotTaskDTO request);

        Task<ApiResult<List<BasicInfoDTO>>> GetAllotTaskStatus();

        //Task<ApiResult<string>> UpdateAllotTaskStatus(AllotTaskDTO request);

        //Task<ApiResult<string>> CancelAllotTask(AllotTaskDTO request);
        Task<ApiResult<List<GroupSelectDTO>>> GetSourceWarehouses(GetShopInfoRequest request);
    }
}
