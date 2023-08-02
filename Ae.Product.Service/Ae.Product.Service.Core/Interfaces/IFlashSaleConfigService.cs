using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Core.Interfaces
{
    public interface IFlashSaleConfigService
    {
        Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs(GetFlashSaleConfigRequest request);

        Task<ApiResult<string>> UpdateFlashSaleConfig(FlashSaleConfigDTO request);

        Task<ApiResult<string>> CreatFlashSaleConfig(FlashSaleConfigDTO request);

        Task<ApiResult<List<FlashSaleConfigDTO>>> GetActiveFlashSaleConfigs();

        Task<ApiResult<FlashSaleConfigDTO>> GetFlashSaleProduct(FlashSaleConfigDTO request);

        Task<ApiResult<string>> UpdateFlashSaleConfigNum(FlashSaleConfigDTO request);

        Task<ApiResult<string>> AutoOffFlashSaleConfig();
    }
}
