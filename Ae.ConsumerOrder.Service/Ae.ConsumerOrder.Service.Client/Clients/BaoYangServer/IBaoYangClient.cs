using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.BaoYang;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IBaoYangClient
    {
        Task<ApiResult<List<PartProductRefDTO>>> GetAdaptiveProductByPartTypeAndCarId(AdaptiveProductByPartTypeAndCarIdRequest request);

        Task<ApiResult<InstallServiceByProductDTO>> GetInstallServiceByProduct(InstallServiceByProductRequest request);
    }
}
