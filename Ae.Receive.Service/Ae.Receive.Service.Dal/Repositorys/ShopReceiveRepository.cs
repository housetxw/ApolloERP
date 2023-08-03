using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopReceiveRepository : AbstractRepository<ShopReceiveDO>, IShopReceiveRepository
    {
        public ShopReceiveRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        public async Task<List<ShopReceiveDO>> GetReceiveByUserTelAndTime(int shopId, List<string> userTel,
            DateTime? startTime, DateTime? endTime)
        {
            var para = new DynamicParameters();
            para.Add("@userTel", userTel);
            para.Add("@shopId", shopId);
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id = @shopId");
            condition.Append(" AND user_tel IN @userTel");
            if (startTime != null)
            {
                condition.Append(" AND arrival_time >= @startTime");
                para.Add("@startTime", startTime.Value);
            }

            if (endTime != null)
            {
                condition.Append(" AND arrival_time < @endTime");
                para.Add("@endTime", endTime.Value);
            }

            var result = await GetListAsync<ShopReceiveDO>(condition.ToString(), para);

            return result?.ToList() ?? new List<ShopReceiveDO>();
        }

        /// <summary>
        /// 根据UserId查询历史到店记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveDO>> GetHistoryReceiveByUserId(string userId)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);

            var result = await GetListAsync<ShopReceiveDO>("WHERE user_id = @userId", para);

            return result?.ToList() ?? new List<ShopReceiveDO>();
        }

        public async Task<List<ShopReceiveDO>> GetReceiveListByIds(List<long> recIds)
        {
            var para = new DynamicParameters();
            para.Add("@recIds", recIds);

            var result = await GetListAsync<ShopReceiveDO>("WHERE `id` IN @recIds", para);

            return result?.ToList() ?? new List<ShopReceiveDO>();
        }

        public async Task<List<ShopReceiveDO>> GetShopReceiveByReserveIds(List<long> reserveIds)
        {
            var para = new DynamicParameters();
            para.Add("@reserveIds", reserveIds);

            var result = await GetListAsync<ShopReceiveDO>("WHERE `reserve_no` IN @reserveIds", para);

            return result?.ToList() ?? new List<ShopReceiveDO>();
        }

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopReceiveDO>> GetUserReceivePageList(string userId, int pageIndex, int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);

            var result = await GetListPagedAsync<ShopReceiveDO>(pageIndex, pageSize, "WHERE user_id = @userId", "`id` DESC", para);

            return result;
        }
    }
}
