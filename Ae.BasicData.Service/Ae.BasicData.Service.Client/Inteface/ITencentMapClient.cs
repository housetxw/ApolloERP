using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Client.Request;
using Ae.BasicData.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BasicData.Service.Client.Inteface
{
    public interface ITencentMapClient
    {
        Task<ApiResult<AddressComponent>> GetPosition(GetPositionClientRequest request);

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetCoordinateClientResponse> GetCoordinate(GetCoordinateClientRequest request);
    }
}
