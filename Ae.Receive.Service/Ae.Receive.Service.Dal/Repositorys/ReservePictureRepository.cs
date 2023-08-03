using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ReservePictureRepository : AbstractRepository<ReservePictureDO>, IReservePictureRepository
    {
        public ReservePictureRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        public async Task<List<ReservePictureDO>> GetReservePictureList(long reserveId)
        {
            var para = new DynamicParameters();
            para.Add("@reserveId", reserveId);
            var result = await GetListAsync<ReservePictureDO>("WHERE reserve_id = @reserveId", para);
            return result?.ToList() ?? new List<ReservePictureDO>();
        }
    }
}
