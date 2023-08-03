using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopReceiveCheckRepository : AbstractRepository<ShopReceiveCheckDo>, IShopReceiveCheckRepository
    {
        public ShopReceiveCheckRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 根据到店记录id查询检查报告
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<ShopReceiveCheckDo> GetReceiveCheckByRecId(long recId, bool readOnly = true)
        {
            var para = new DynamicParameters();

            para.Add("@recId", recId);

            var result = await GetListAsync<ShopReceiveCheckDo>("WHERE `receive_id` = @recId", para, !readOnly);

            return result?.OrderByDescending(_ => _.Id)?.FirstOrDefault();
        }

        /// <summary>
        /// 根据车牌号查Vin码
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public async Task<string> GetVinCodeByCarPlate(string carPlate)
        {
            if (string.IsNullOrEmpty(carPlate))
            {
                return string.Empty;
            }

            var para = new DynamicParameters();

            para.Add("@carPlate", carPlate);

            var result = await GetListAsync<ShopReceiveCheckDo>("WHERE car_plate = @carPlate AND vin_code != ''", para);

            return result?.OrderByDescending(_ => _.Id)?.FirstOrDefault()?.VinCode ?? string.Empty;
        }

        /// <summary>
        /// 根据到店记录批量检测报告
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckDo>> GetReceiveCheckByRecIds(List<long> recIds)
        {
            var para = new DynamicParameters();

            para.Add("@recIds", recIds);

            var result = await GetListAsync<ShopReceiveCheckDo>("WHERE `receive_id` IN @recIds", para);

            return result?.ToList() ?? new List<ShopReceiveCheckDo>();
        }

        /// <summary>
        /// 根据CarId查询检查报告记录
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckDo>> GetReceiveCheckByCarId(string carId)
        {
            var para = new DynamicParameters();

            para.Add("@carId", carId);

            var result = await GetListAsync<ShopReceiveCheckDo>("WHERE `car_id` = @carId AND check_status = 1", para);

            return result?.OrderByDescending(_ => _.UpdateTime)?.ToList() ?? new List<ShopReceiveCheckDo>();
        }

        /// <summary>
        /// 批量查询检查报告
        /// </summary>
        /// <param name="checkIds"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckDo>> GetReceiveCheckList(List<long> checkIds)
        {
            var para = new DynamicParameters();

            para.Add("@checkIds", checkIds);

            var result = await GetListAsync<ShopReceiveCheckDo>("WHERE `id` IN @checkIds", para);

            return result?.ToList();
        }
    }
}
