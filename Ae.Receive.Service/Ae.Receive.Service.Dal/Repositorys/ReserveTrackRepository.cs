using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Dal.Model.Extend;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ReserveTrackRepository : AbstractRepository<ReserveTrackDO>, IReserveTrackRepository
    {
        public ReserveTrackRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 预约处理记录
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        public async Task<List<ReserveTrackLogDo>> GetReserveTrackList(long reserveId)
        {
            var para = new DynamicParameters();
            para.Add("@reserveId", reserveId);
            string sql = @"SELECT
    A.id AS LogId,
	A.content AS Content,
	A.opt_type AS OptType,
	A.create_time AS CreateTime,
	B.field_name AS FieldName,
	B.before_value AS BeforeValue,
	B.after_value AS AfterValue
FROM
	reserve_track A
	LEFT JOIN reserve_track_detail B ON ( A.id = B.log_id ) 
WHERE
	A.reserve_id = @reserveId 
	AND A.is_deleted = 0;";


            var result = new List<ReserveTrackLogDo>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ReserveTrackLogDo>(sql, para))?.ToList() ??
                         new List<ReserveTrackLogDo>();
            });
            return result;
        }
    }
}
