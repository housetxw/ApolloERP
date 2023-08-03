using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Core.Response.Arrival;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IArrivalRepository : IRepository<ShopArrivalDO>
    {
        Task<List<TodayArrivalShopStatisticsResDTO>> GetTodayArrivalShopStatistics(TodayArrivalShopStatisticsReqDTO req);

        Task<List<MonthArrivalShopStatisticsResDTO>> GetMonthArrivalShopStatistics(MonthArrivalShopStatisticsReqDTO req);

        Task<List<NewCustomerArrivalShopResDTO>> GetNewCustomerArrivalShopStatistics(NewCustomerArrivalShopReqDTO req);

        Task<List<NewCustomerArrivalShopSaleroomResDTO>> GetNewCustomerArrivalShopSaleroomStatistics(NewCustomerArrivalShopSaleroomReqDTO req);

        /// <summary>
        /// 得到生成的Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> GetQuqueNumberGenerator(GetQuqueNumberGeneratorRequest request);

        /// <summary>
        /// 保存排队号
        /// </summary>
        /// <param name="ququeNumberGeneratorDO"></param>
        /// <returns></returns>
        Task<long> SaveQuqueNumberGenerator(QuqueNumberGeneratorDO ququeNumberGeneratorDO);

        /// <summary>
        /// 保存到店记录
        /// </summary>
        /// <param name="shopArrivalDO"></param>
        /// <returns></returns>
        Task<long> SaveArrival(ShopArrivalDO shopArrivalDO);

        /// <summary>
        /// 用户车型历史消费记录
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<HistoryArrivalConsumerDTO> GetHistoryArrivalConsumer(string carId, string userId);

        /// <summary>
        /// 得到历史到店记录
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ShopArrivalDO>> GetShopArrivals(string carId, string userId);

        /// <summary>
        /// 到店记录下面订单
        /// </summary>
        /// <param name="arrivalIds"></param>
        /// <returns></returns>
        Task<List<ShopArrivalOrderDO>> GetShopArrivalOrders(List<long> arrivalIds);

        /// <summary>
        /// 得到到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request);
        Task<GetArrivalListResponse> GetLastArrival(GetLastArrivalRequest request);

        /// <summary>
        /// 到店记录
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        Task<ShopArrivalDO> GetShopArrival(long arrivalId);

        /// <summary>
        /// 更新技师
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateTechnicians(DispatchTechnicianSaveRequest request);

        /// <summary>
        /// 得到未消费离店记录
        /// </summary>
        /// <returns></returns>
        Task<List<ShopArrivalReasonDO>> GetShopArrivalOutPay();

        /// <summary>
        /// 离店原因保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> LeaveShopReasonSave(ShopArrivalCancelDO request);

        /// <summary>
        /// 更新到店记录状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="UpdateBy"></param>
        /// <returns></returns>
        Task<long> UpdateShopArrivalStaus(int status, long arrivalId, string userName);

        /// <summary>
        /// 删除到店记录订单
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        Task<int> DeleteShopArrivalOrder(long arrivalId, string OrderNo, string userName);

        Task<int> DeleteShopArrivalOrder(long arrivalId, string userName);
        Task<int> DeleteShopArrivalOrder(string OrderNo, string userName);

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> GetLastShopForLastArrival(GetLastShopForLastArrivalRequest request);

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopArrivalOrderDO>> GetOrdersForReserver(GetOrdersForReserverRequest request);

        /// <summary>
        /// 更改到店记录车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> UpdateArrivalVehicle(UpdateArrivalVehicleRequest request);

        /// <summary>
        /// 得到排队号是否产生
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<bool> GetShopArrivalForQueue(string userId, string carId, long shopId);

        /// <summary>
        /// 得打服务列表到店金额
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal?> GetHistoryArrivalConsumerPrice(string carId, string userId);

        /// <summary>
        /// 得到服务列表的历史到店数量
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetHistoryArrivalConsumerCount(string carId, string userId);

        /// <summary>
        /// 得到到店记录状态数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetArrivalListCountResponse> GetArrivalListCount(GetArrivalListRequest request);

        /// <summary>
        /// 得到绩效统计的到店记录
        /// </summary>,'' GROUP BY user_id order by id ASC
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopArrivalDO>> GetShopArrivalForStatic(GetShopArrivalForStaticRequest request);

        /// <summary>
        /// 得到车辆最后入店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> GetLastArriveTimeByCarId(GetArrialMaintenanceRequest request);

        /// <summary>
        /// 得到到店的次数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> GetArriveCountByCarId(GetArrialMaintenanceRequest request);

        /// <summary>
        /// 得到到店的消费金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<double> GetArriveConsumptionAmountByCarId(GetArrialMaintenanceRequest request);

        /// <summary>
        /// 得到维修保养记录头
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetArrivalMaintenanceListByCarIdVo>> GetArrivalMaintenanceHeader(GetArrivalMaintenanceListByCarIdRequest request);

        /// <summary>
        /// 得到维修保养项目信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetArrivalMaintenanceItemResponse>> GetArrivalMaintenanceItem(GetArrivalMaintenanceListByCarIdRequest request);

    }
}
