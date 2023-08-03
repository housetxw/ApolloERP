using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using Ae.Shop.Api.Core.Response.Reserve;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IReserveService
    {
        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopReserveListVo>> GetReserveListPage(ReserveListPageRequest request);

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveDetailForWebVo> GetReserveDetailForWeb(ReserveDetailForWebRequest request);

        /// <summary>
        /// 获取门店开启的服务类别
        /// </summary>
        /// <returns></returns>
        Task<List<StatusEnum>> GetShopServiceType();

        /// <summary>
        /// 预约技师列表
        /// </summary>
        /// <returns></returns>
        Task<List<ReserveTechnicianVo>> GetReserveTechnician();

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        Task<ReserveDateForWebResponse> GetReserveDateForWeb();

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CancelReserve(CancelReserveRequest request);

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditReserve(EditReserveRequest request);

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ReserveListVo>> GetValidReserve(ValidReserveRequest request);

        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddReserve(AddReserveRequest request);
    }
}
