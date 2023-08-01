using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class UserCarComponentsResultRepository : AbstractRepository<UserCarComponentsResultDo>, IUserCarComponentsResultRepository
    {
        public UserCarComponentsResultRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 获取车辆部件检查状况
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        public async Task<List<UserCarComponentsResultDo>> GetUserCarComponentsResult(string carId, int resultValue)
        {
            var para = new DynamicParameters();

            para.Add("@carId", carId);
            para.Add("@resultValue", resultValue);

            var result = await GetListAsync<UserCarComponentsResultDo>("WHERE `car_id` = @carId AND result_value = @resultValue", para);

            return result?.ToList() ?? new List<UserCarComponentsResultDo>();
        }
    }
}
