using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IFlashSaleConfigClient
    {
        Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs(GetFlashSaleConfigRequest request);

        Task<ApiResult<string>> UpdateFlashSaleConfig(FlashSaleConfigDTO request);

        Task<ApiResult<string>> CreatFlashSaleConfig(FlashSaleConfigDTO request);

    }
}
