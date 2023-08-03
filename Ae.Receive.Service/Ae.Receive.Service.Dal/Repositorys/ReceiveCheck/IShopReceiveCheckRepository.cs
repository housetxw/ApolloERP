using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveCheckRepository : IRepository<ShopReceiveCheckDo>
    {
        /// <summary>
        /// 根据到店记录id查询检查报告
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<ShopReceiveCheckDo> GetReceiveCheckByRecId(long recId, bool readOnly = true);

        /// <summary>
        /// 根据车牌号查Vin码
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        Task<string> GetVinCodeByCarPlate(string carPlate);

        /// <summary>
        /// 根据到店记录批量检测报告
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckDo>> GetReceiveCheckByRecIds(List<long> recIds);

        /// <summary>
        /// 根据CarId查询检查报告记录
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckDo>> GetReceiveCheckByCarId(string carId);

        /// <summary>
        /// 批量查询检查报告
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckDo>> GetReceiveCheckList(List<long> checkIds);
    }
}
