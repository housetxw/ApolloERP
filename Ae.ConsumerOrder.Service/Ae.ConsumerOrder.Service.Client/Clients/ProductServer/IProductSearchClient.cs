using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Product;
using Ae.ConsumerOrder.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IProductSearchClient
    {
        Task<ApiResult<List<ProductDetailDTO>>> GetProductsByProductCodes(ProductDetailSearchRequest request);
        Task<ApiResult<List<ProductPackageDTO>>> GetPackageProductsByCodes(PackageProductSearchRequest request);

        Task<ApiResult<List<GetShopProductByCodeDTO>>> GetShopProductByCodes(GetShopProductByCodeRequest request);

        Task<ApiResult<FlashSaleConfigDTO>> GetFlashSaleProduct(FlashSaleConfigDTO request);

        Task<ApiResult<string>> UpdateFlashSaleConfigNum( FlashSaleConfigDTO request);

        Task<ApiResult<List<FlashSaleConfigDTO>>> GetActiveFlashSaleConfigs();

        Task<ApiResult<List<CheckDoorProductVo>>> CheckDoorProduct(
             CheckDoorProductRequest request);

    }
}
