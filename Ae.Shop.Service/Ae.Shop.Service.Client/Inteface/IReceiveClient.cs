using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Client.Model.Receive;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IReceiveClient
    {
        /// <summary>
        /// 根据到店记录Id查询到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopReceiveDto>> GetReceiveByIds(ReceiveByIdsClientRequest request);
        Task<long> GetLastShopForLastArrival(GetLastShopForLastArrivalClientRequest request);
        Task<GetShopTotalReserveClientResponse> GetShopTotalReserve(BaseGetShopClientRequest request);

        Task<GetShopArrivalOrderForStaticResponse> GetShopArrivalOrderForStatic(GetShopArrivalForStaticClientRequest request);
    }
}
