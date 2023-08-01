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
    public class UserCarPropertyRepository : AbstractRepository<UserCarPropertyDO>, IUserCarPropertyRepository
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public UserCarPropertyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 新增五级车型属性
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public async Task<bool> AddUserCarPropertyAsync(List<UserCarPropertyDO> properties)
        {
            if (properties.Any())
            {
                string sql = @"INSERT user_car_property ( car_id, property_key, property_value, create_by )
VALUES {0}";
                StringBuilder condition = new StringBuilder();
                var para = new DynamicParameters();
                para.Add("@car_id", properties[0].CarId);
                para.Add("@create_by", properties[0].CreateBy);
                for (int i = 0; i < properties.Count; i++)
                {
                    if (i == properties.Count - 1)
                    {
                        condition.Append($"( @car_id, @property_key{i}, @property_value{i}, @create_by );");
                    }
                    else
                    {
                        condition.Append($"( @car_id, @property_key{i}, @property_value{i}, @create_by ),");
                    }

                    para.Add($"@property_key{i}", properties[i].PropertyKey);
                    para.Add($"@property_value{i}", properties[i].PropertyValue);
                }

                sql = string.Format(sql, condition);
                int id = 0;
                await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
                return id > 0;
            }

            return false;
        }

        /// <summary>
        /// 查用户车型五级属性
        /// </summary>
        /// <param name="carIds"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserCarPropertyDO>> GetUserCarPropertiesAsync(List<string> carIds,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@car_id", carIds);
            var result = await GetListAsync<UserCarPropertyDO>("WHERE car_id IN @car_id", para, !readOnly);
            return result;
        }

        /// <summary>
        /// 编辑用户车型五级属性
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="carId"></param>
        /// <param name="submitter"></param>
        /// <returns></returns>
        public async Task<bool> EditCarPropertiesAsync(List<UserCarPropertyDO> properties, string carId,
            string submitter)
        {
            var para = new DynamicParameters();
            para.Add("@car_id", carId);
            para.Add("@create_by", submitter);
            string sql = @"UPDATE user_car_property 
SET is_deleted = 1,
update_by = @create_by,
update_time = NOW( ) 
WHERE
	car_id = @car_id 
	AND is_deleted = 0;";
            if (properties.Any())
            {
                string sqlT = @"INSERT user_car_property ( car_id, property_key, property_value, create_by )
VALUES {0}";
                StringBuilder condition = new StringBuilder();
                for (int i = 0; i < properties.Count; i++)
                {
                    if (i == properties.Count - 1)
                    {
                        condition.Append($"( @car_id, @property_key{i}, @property_value{i}, @create_by );");
                    }
                    else
                    {
                        condition.Append($"( @car_id, @property_key{i}, @property_value{i}, @create_by ),");
                    }

                    para.Add($"@property_key{i}", properties[i].PropertyKey);
                    para.Add($"@property_value{i}", properties[i].PropertyValue);
                }

                sqlT = string.Format(sqlT, condition);
                sql = sql + sqlT;
            }

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }
    }
}
