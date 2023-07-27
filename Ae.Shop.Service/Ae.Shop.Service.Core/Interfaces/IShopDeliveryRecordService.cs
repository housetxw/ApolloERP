using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IShopDeliveryRecordService
    {
        Task<ApiResult<ShopDeliveryRecordDTO>> GetShopDeliveryRecord(ShopDeliveryRecordDTO request);

        Task<ApiResult<string>> AddShopDeliveryRecord(ShopDeliveryRecordDTO request);

        Task<ApiResult<string>> AddShopDeliveryConfig(ShopDeliveryConfigDTO request);

        Task<ApiResult<List<ShopDeliveryConfigDTO>>> GetShopDeliveryConfigs(ShopDeliveryConfigDTO request);
    }
}
