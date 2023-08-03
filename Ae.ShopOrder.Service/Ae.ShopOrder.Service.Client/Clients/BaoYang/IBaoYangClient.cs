using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model.BaoYang;
using Ae.ShopOrder.Service.Core.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.BaoYang
{
    public interface IBaoYangClient
    {
        Task<ApiResult<List<PartProductRefDTO>>> GetAdaptiveProductByPartTypeAndCarId(AdaptiveProductByPartTypeAndCarIdRequest request);

        Task<ApiResult<InstallServiceByProductDTO>> GetInstallServiceByProduct(
            InstallServiceByProductRequest request);


        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ServiceTypeEnumDTO>>> GetServiceTypeEnum();


        Task<ApiResult<List<InstallServiceDetailVo>>> GetAdditionalPriceByServiceId(
            AdditionalPriceByServiceIdRequest request);
    }
}
