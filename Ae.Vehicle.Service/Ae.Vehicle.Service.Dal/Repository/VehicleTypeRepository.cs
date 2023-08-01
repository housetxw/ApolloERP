using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeRepository: AbstractRepository<VehicleTypeDO>, IVehicleTypeRepository
    {
        public VehicleTypeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeDO>> GetVehicleListByBrand(string brand)
        {
            var para = new DynamicParameters();
            para.Add("@brand", brand);

            var result = await GetListAsync(
                "WHERE brand = @brand AND is_deleted = 0 AND actived = 1 AND vehicle_id != '' AND vehicle != ''",
                para);

            return result;
        }

        /// <summary>
        /// 获取所有车系
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleTypeDO>> GetAllVehicleList()
        {
            var result = await GetListAsync<VehicleTypeDO>("");

            return result?.ToList() ?? new List<VehicleTypeDO>();
        }

        /// <summary>
        /// 根据车系Id查车系信息
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public async Task<VehicleTypeDO> GetVehicleByVehicleIdAsync(string vehicleId)
        {
            var para = new DynamicParameters();
            para.Add("@vehicle_id", vehicleId);

            var result = await GetListAsync(
                "WHERE vehicle_id = @vehicle_id AND is_deleted = 0 AND actived = 1",
                para);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 根据车系Id查车系信息
        /// </summary>
        /// <param name="vehicleIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeDO>> GetVehicleByVehicleIdAsync(List<string> vehicleIds)
        {
            var para = new DynamicParameters();
            if (vehicleIds == null || !vehicleIds.Any())
            {
                return new List<VehicleTypeDO>();
            }

            para.Add("@vehicleIds", vehicleIds);

            var result = await GetListAsync(
                "WHERE vehicle_id IN @vehicleIds AND is_deleted = 0 AND actived = 1",
                para);

            return result;
        }

        /// <summary>
        /// 车系模糊搜索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleTypeDO>> VehicleSearchByNameAsync(string name)
        {
            var para = new DynamicParameters();
            para.Add("@name", "%" + name + "%");

            var result = await GetListAsync(
                @"WHERE
	( brand LIKE @name 
    OR vehicle LIKE @name 
    OR brand_py LIKE @name 
    OR brand_jpy LIKE @name 
    OR vehicle_py LIKE @name 
    OR vehicle_jpy LIKE @name ) 
	AND is_deleted = 0 
	AND actived = 1",
                para);

            return result;
        }
    }
}
