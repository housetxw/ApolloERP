using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Dapper;
using System.Linq;
using Ae.User.Service.Dal.Model.Request;

namespace Ae.User.Service.Dal.Repository
{
    public class UserAddressRepository : AbstractRepository<UserAddressDO>, IUserAddressRepository
    {
        public UserAddressRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 核对用户地址是否重复
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> CheckUserAddressRepeat(UserAddressDO request)
        {
            var sql = @"SELECT
	                    count( 1 ) 
                    FROM
	                    user_address 
                    WHERE
	                    is_deleted = 0 
	                    AND province_id = @province_id 
	                    AND city_id = @city_id 
	                    AND district_id = @district_id 
	                    AND address_line = @address_line and user_id=@user_id
                       AND user_name = @user_name AND mobile_number = @mobile_number";
            var param = new DynamicParameters();
            param.Add("@province_id",request.ProvinceId);
            param.Add("@city_id", request.CityId);
            param.Add("@district_id", request.DistrictId);
            param.Add("@address_line", request.AddressLine);
            param.Add("@user_id", request.UserId);
            param.Add("@user_name", request.UserName);
            param.Add("@mobile_number", request.MobileNumber);

            object obj=null;

            await OpenConnectionAsync(async conn => obj = await conn.ExecuteScalarAsync(sql, param));
            return obj != null ? Convert.ToInt32(obj) : -1;
        }

        /// <summary>
        /// 核对用户地址标签是否重复
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> CheckUserAddressTagRepeat(UserAddressTagDO request)
        {
            var sql = @"select count(1) from user_address_tag where is_deleted=0 and (user_id=@user_id or user_id='00000000-0000-0000-0000-00000000000z')  and tag_name=@tag_name";

            var param = new DynamicParameters();
            param.Add("@tag_name", request.TagName);
            param.Add("@user_id", request.UserId);

            object obj = null;

            await OpenConnectionAsync(async conn => obj = await conn.ExecuteScalarAsync(sql, param));
            return obj != null ? Convert.ToInt32(obj) : -1;
        }


        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> CreateUserAddress(UserAddressDO request)
        {
            request.CreateTime = DateTime.Now;
            return await InsertAsync(request);
        }

        public async  Task<int> CreateUserAddressTag(UserAddressTagDO request)
        {
            return await InsertAsync<UserAddressTagDO>(request);
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async  Task<int> DeleteUserAddress(long addressId,string updateBy)
        {
            var sql = @"UPDATE user_address 
                        SET is_deleted = 1,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @Id";
            var param = new DynamicParameters();
            param.Add("@update_by",updateBy);
            param.Add("@Id", addressId);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAddressDO>> GetUserAddressAsync(string userId)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            var result = await GetListAsync<UserAddressDO>("WHERE user_id = @userId", para);

            return result;
        }

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserAddressDO> GetUserAddressAsync(string userId, int addressId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            para.Add("@id", addressId);
            var result = await GetListAsync<UserAddressDO>("WHERE user_id = @userId AND id = @id", para, !readOnly);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// 设置用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> SetUserDefaultAddressAsync(string userId, int addressId, string updateBy)
        {
            string sql = @"UPDATE user_address 
SET default_address = 0,
update_time = NOW( ),
update_by = @update_by 
WHERE
	user_id = @user_id 
	AND id != @id 
	AND default_address = 1 
	AND is_deleted = 0;
UPDATE user_address 
SET default_address = 1,
update_time = NOW( ),
update_by = @update_by 
WHERE
	user_id = @user_id 
	AND id = @id;";

            var para = new DynamicParameters();
            para.Add("@user_id", userId);
            para.Add("@id", addressId);
            para.Add("@update_by", updateBy);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }

        /// <summary>
        /// 查询地址详情
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task<UserAddressDO> GetUserAddressDetail(long addressId)
        {
            var para = new DynamicParameters();
            para.Add("@Id", addressId);
            var result = await GetListAsync("where id=@Id",para);
            return result.FirstOrDefault();
        }

        public async  Task<List<UserAddressTagDO>> GetUserAddressTags(string userId)
        {
            var sql = @"select id Id,tag_name TagName,tag_id TagId from user_address_tag where (user_id=@user_id or user_id='00000000-0000-0000-0000-00000000000z') and is_deleted=0";
            var param = new DynamicParameters();
            param.Add("@user_id",userId);
            IEnumerable<UserAddressTagDO> addressList = null;
            await OpenConnectionAsync(async conn => addressList = await conn.QueryAsync<UserAddressTagDO>(sql, param));

            return addressList.Any() ? addressList.ToList() : new List<UserAddressTagDO>();
        }

        /// <summary>
        /// 更新默认地址标识
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> UdpateUserAddressDefault(UpdateUserAddressDefaultRequest request)
        {
            var result = -1;
            var sql = @"UPDATE user_address 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        default_address = @default_address 
                        WHERE
	                        id = @Id";

            var param = new DynamicParameters();
            param.Add("@update_by",request.UpdateBy);
            param.Add("@Id",request.AddressId);
            param.Add("@default_address",request.DefaultAddress);
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更改用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> UpdateUserAddress(UserAddressDO request)
        {
            var result = -1;
            var sql = @"UPDATE user_address 
                        SET address_type = @address_type,
                        address_tag = @address_tag,
                        user_name = @user_name,
                        mobile_number = @mobile_number,
                        province = @province,
                        city = @city,
                        district = @district,
                        address_line = @address_line,
                        province_id = @province_id,
                        city_id = @city_id,
                        district_id = @district_id,
                        default_address = @default_address,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @addressId";

            var param = new DynamicParameters();
            param.Add("@address_type",request.AddressType);
            param.Add("@address_tag", request.AddressTag);
            param.Add("@user_name", request.UserName);
            param.Add("@mobile_number", request.MobileNumber);
            param.Add("@province", request.Province);
            param.Add("@city", request.City);
            param.Add("@district", request.District);
            param.Add("@address_line", request.AddressLine);
            param.Add("@province_id", request.ProvinceId);
            param.Add("@city_id", request.CityId);
            param.Add("@district_id", request.DistrictId);
            param.Add("@default_address", request.DefaultAddress);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@addressId", request.Id);

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}



