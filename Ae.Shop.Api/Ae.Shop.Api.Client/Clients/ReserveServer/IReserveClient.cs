using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.Reserve;
using Ae.Shop.Api.Client.Request.Reserve;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.ReserveServer
{
    public interface IReserveClient
    {
        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopReserveListDto>> GetReserveListPage(ReserveListPageClientRequest request);

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveDetailForWebDto> GetReserveDetailForWeb(ReserveDetailForWebClientRequest request);

        /// <summary>
        /// 预约主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopReserveDTO>> GetShopReserveDO(ReserveDetailRequest request);

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ShopReserveOrderDTO>>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request);

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        Task<List<ReserveDateDto>> GetReserveDateForWeb(ReserveDateClientRequest request);

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CancelReserveV2(CancelReserveClientRequest request);

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <returns></returns>
        Task<List<ReserveListDto>> GetValidReserve(ValidReserveClientReserve request);

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditReserve(EditReserveClientRequest request);

        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddReserve(AddReserveClientRequest request);
    }
}
