using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class UserCarRepository : AbstractRepository<UserCarDO>, IUserCarRepository
    {
        public UserCarRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 新增用户车型
        /// </summary>
        /// <param name="userCarDo"></param>
        /// <returns></returns>
        public async Task<bool> AddUserCarAsync(UserCarDO userCarDo)
        {
            string sql = @"INSERT INTO user_car (
id,
user_id,
nick_name,
car_number,
brand,
vehicle,
vehicle_id,
pai_liang,
nian,
tid,
sales_name,
buy_year,
buy_month,
insure_expire_date,
total_mileage,
last_bao_yang_km,
last_bao_yang_time,
vin_code,
default_car,
engine_no,
tire_size_for_single,
insurance_company,
registration_time,
car_no_province,
car_no_city,
data_source,
create_by,
update_by,
update_time 
)
VALUES
	(
	@car_id,
	@user_id,
	@nick_name,
	@car_number,
	@brand,
	@vehicle,
	@vehicle_id,
	@pai_liang,
	@nian,
	@tid,
	@sales_name,
	@buy_year,
	@buy_month,
	@insure_expire_date,
	@total_mileage,
	@last_bao_yang_km,
	@last_bao_yang_time,
	@vin_code,
	@default_car,
	@engine_no,
	@tire_size_for_single,
	@insurance_company,
	@registration_time,
	@car_no_province,
	@car_no_city,
	@data_source,
	@create_by,
	@create_by,
	NOW( ) 
	);";
            var para = new DynamicParameters();
            para.Add("@car_id", userCarDo.CarId);
            para.Add("@user_id", userCarDo.UserId);
            para.Add("@nick_name", userCarDo.NickName ?? string.Empty);
            para.Add("@car_number", userCarDo.CarNumber ?? string.Empty);
            para.Add("@brand", userCarDo.Brand ?? string.Empty);
            para.Add("@vehicle", userCarDo.Vehicle ?? string.Empty);
            para.Add("@vehicle_id", userCarDo.VehicleId ?? string.Empty);
            para.Add("@pai_liang", userCarDo.PaiLiang ?? string.Empty);
            para.Add("@nian", userCarDo.Nian ?? string.Empty);
            para.Add("@tid", userCarDo.Tid ?? string.Empty);
            para.Add("@sales_name", userCarDo.SalesName ?? string.Empty);
            para.Add("@buy_year", userCarDo.BuyYear ?? string.Empty);
            para.Add("@buy_month", userCarDo.BuyMonth ?? string.Empty);
            para.Add("@insure_expire_date", userCarDo.InsureExpireDate);
            para.Add("@total_mileage", userCarDo.TotalMileage);
            para.Add("@last_bao_yang_km", userCarDo.LastBaoYangKm);
            para.Add("@last_bao_yang_time", userCarDo.LastBaoYangTime);
            para.Add("@vin_code", userCarDo.VinCode ?? string.Empty);
            para.Add("@default_car", userCarDo.DefaultCar);
            para.Add("@engine_no", userCarDo.EngineNo ?? string.Empty);
            para.Add("@tire_size_for_single", userCarDo.TireSizeForSingle ?? string.Empty);
            para.Add("@insurance_company", userCarDo.InsuranceCompany ?? string.Empty);
            para.Add("@registration_time", userCarDo.RegistrationTime);
            para.Add("@car_no_province", userCarDo.CarNoProvince ?? string.Empty);
            para.Add("@car_no_city", userCarDo.CarNoCity ?? string.Empty);
            para.Add("@data_source", userCarDo.DataSource ?? string.Empty);
            para.Add("@create_by", userCarDo.CreateBy);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 设置用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> SetUserDefaultVehicleAsync(string userId, string carId, string updateBy)
        {
            string sql = @"UPDATE user_car 
SET default_car = 0,
update_time = NOW( ),
update_by = @update_by 
WHERE
	user_id = @user_id 
	AND id != @car_id 
	AND default_car = 1 
	AND is_deleted = 0;
UPDATE user_car 
SET default_car = 1,
update_time = NOW( ),
update_by = @update_by 
WHERE
	user_id = @user_id 
	AND id = @car_id;";

            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            para.Add("@car_id", carId);
            para.Add("@update_by", updateBy);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 查一个用户所有车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserCarDO>> GetAllUserVehiclesAsync(string userId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            var result = await GetListAsync<UserCarDO>("WHERE user_id = @user_id", para, !readOnly);
            return result.OrderByDescending(_ => _.UpdateTime);
        }

        /// <summary>
        /// 根据userId和carId获取用户车型数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserCarDO> GetUserVehicleByCarIdAsync(string userId, string carId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            para.Add("@id", carId);
            var result = await GetListAsync<UserCarDO>("WHERE user_id = @user_id AND id = @id", para, !readOnly);
            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserCarDO> GetUserDefaultVehicleAsync(string userId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            var result = (await GetListAsync<UserCarDO>("WHERE user_id = @user_id", para, !readOnly)).OrderByDescending(_ => _.DefaultCar).ThenByDescending(_ => _.UpdateTime);
            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 批量获取用户默认车辆
        /// </summary>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        public async Task<List<UserCarDO>> GetUserDefaultVehicleBatch(List<string> userIdList)
        {
            var para = new DynamicParameters();
            para.Add("@userIds", userIdList);
            var result = await GetListAsync<UserCarDO>("WHERE user_id IN @userIds AND default_car = 1", para);
            return result?.ToList() ?? new List<UserCarDO>();
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="carId"></param>
        /// <param name="submitter"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserCarAsync(string userId, string carId, string submitter)
        {
            string sql = @"UPDATE user_car 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	user_id = @user_id 
	AND id = @car_id;";

            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            para.Add("@car_id", carId);
            para.Add("@update_by", submitter);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 更新用户车辆信息
        /// </summary>
        /// <param name="userCarDo"></param>
        /// <returns></returns>
        public async Task<bool> EditUserCarAsync(EditUserCarRequest userCarDo)
        {
            var para = new DynamicParameters();
            para.Add("@user_id", userCarDo.UserId);
            para.Add("@car_id", userCarDo.CarId);
            para.Add("@update_by", userCarDo.Submitter);
            StringBuilder condition = new StringBuilder();

            #region 更新字段

            if (userCarDo.NickName != null)
            {
                condition.Append("nick_name = @nick_name,");
                para.Add("@nick_name", userCarDo.NickName);
            }

            if (userCarDo.CarNumber != null)
            {
                condition.Append("car_number = @car_number,");
                para.Add("@car_number", userCarDo.CarNumber);
            }

            if (userCarDo.Brand != null)
            {
                condition.Append("brand = @brand,");
                para.Add("@brand", userCarDo.Brand);
            }

            if (userCarDo.Vehicle != null)
            {
                condition.Append("vehicle = @vehicle,");
                para.Add("@vehicle", userCarDo.Vehicle);
            }

            if (userCarDo.VehicleId != null)
            {
                condition.Append("vehicle_id = @vehicle_id,");
                para.Add("@vehicle_id", userCarDo.VehicleId);
            }

            if (userCarDo.PaiLiang != null)
            {
                condition.Append("pai_liang = @pai_liang,");
                para.Add("@pai_liang", userCarDo.PaiLiang);
            }

            if (userCarDo.Nian != null)
            {
                condition.Append("nian = @nian,");
                para.Add("@nian", userCarDo.Nian);
            }

            if (userCarDo.Tid != null)
            {
                condition.Append("tid = @tid,");
                para.Add("@tid", userCarDo.Tid);
            }

            if (userCarDo.SalesName != null)
            {
                condition.Append("sales_name = @sales_name,");
                para.Add("@sales_name", userCarDo.SalesName);
            }

            if (userCarDo.BuyYear != null)
            {
                condition.Append("buy_year = @buy_year,");
                para.Add("@buy_year", userCarDo.BuyYear);
            }

            if (userCarDo.BuyMonth != null)
            {
                condition.Append("buy_month = @buy_month,");
                para.Add("@buy_month", userCarDo.BuyMonth);
            }

            if (userCarDo.InsureExpireDate != null)
            {
                condition.Append("insure_expire_date = @insure_expire_date,");
                para.Add("@insure_expire_date", userCarDo.InsureExpireDate);
            }

            if (userCarDo.TotalMileage != null)
            {
                condition.Append("total_mileage = @total_mileage,");
                para.Add("@total_mileage", userCarDo.TotalMileage);
            }

            if (userCarDo.LastBaoYangKm != null)
            {
                condition.Append("last_bao_yang_km = @last_bao_yang_km,");
                para.Add("@last_bao_yang_km", userCarDo.LastBaoYangKm);
            }

            if (userCarDo.LastBaoYangTime != null)
            {
                condition.Append("last_bao_yang_time = @last_bao_yang_time,");
                para.Add("@last_bao_yang_time", userCarDo.LastBaoYangTime);
            }

            if (userCarDo.VinCode != null)
            {
                condition.Append("vin_code = @vin_code,");
                para.Add("@vin_code", userCarDo.VinCode);
            }

            if (userCarDo.DefaultCar != null)
            {
                condition.Append("default_car = @default_car,");
                para.Add("@default_car", userCarDo.DefaultCar);
            }

            if (userCarDo.EngineNo != null)
            {
                condition.Append("engine_no = @engine_no,");
                para.Add("@engine_no", userCarDo.EngineNo);
            }

            if (userCarDo.TireSizeForSingle != null)
            {
                condition.Append("tire_size_for_single = @tire_size_for_single,");
                para.Add("@tire_size_for_single", userCarDo.TireSizeForSingle);
            }

            if (userCarDo.InsuranceCompany != null)
            {
                condition.Append("insurance_company = @insurance_company,");
                para.Add("@insurance_company", userCarDo.InsuranceCompany);
            }

            if (userCarDo.RegistrationTime != null)
            {
                condition.Append("registration_time = @registration_time,");
                para.Add("@registration_time", userCarDo.RegistrationTime);
            }

            if (userCarDo.CarNoProvince != null)
            {
                condition.Append("car_no_province = @car_no_province,");
                para.Add("@car_no_province", userCarDo.CarNoProvince);
            }

            if (userCarDo.CarNoCity != null)
            {
                condition.Append("car_no_city = @car_no_city,");
                para.Add("@car_no_city", userCarDo.CarNoCity);
            }

            if (userCarDo.CarNoCity != null)
            {
                condition.Append("car_no_city = @car_no_city,");
                para.Add("@car_no_city", userCarDo.CarNoCity);
            }

            #endregion

            string sql = condition.ToString();
            if (!string.IsNullOrEmpty(sql))
            {
                sql =
                    $"UPDATE user_car SET {sql}update_by = @update_by,update_time = NOW( ) WHERE user_id = @user_id AND id = @car_id AND is_deleted = 0";

                int id = 0;
                await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
                return id > 0;
            }

            return false;
        }

        /// <summary>
        /// 根据车牌号查询用户车辆信息
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public async Task<List<UserCarDO>> GetUserCarsByCarPlate(string carPlate)
        {
            var para = new DynamicParameters();
            para.Add("@carPlate", carPlate);
            var result = await GetListAsync<UserCarDO>("WHERE car_number = @carPlate", para);
            return result?.ToList() ?? new List<UserCarDO>();
        }
    }
}
