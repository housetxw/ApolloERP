using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopCarRecordRepository : AbstractRepository<ShopCarRecordDO>, IShopCarRecordRepository
    {
        public ShopCarRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }



        /// <summary>
        /// 查询门店车辆记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopCarRecordDO>> GetShopCarRecordListByShopId(GetShopCarRecordPageListRequest request)
        {
            string sqlCount = @"SELECT
COUNT(*) as cs
FROM
    shop_car_record";
            string sql = @"SELECT
id,
order_no OrderNo,
technician,
mobile,
car_number CarNumber,
start_mileage StartMileage,
end_mileage EndMileage,
oil_wear OilWear,
start_time StartTime,
end_time EndTime,
remark Remark,
is_deleted IsDeleted,
create_by CreateBy,
create_time CreateTime,
update_by UpdateBy,
update_time UpdateTime
FROM shop_car_record ";


            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE shop_id = @ShopId ");

            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                condition.Append(" AND order_no = @OrderNo");
                para.Add("@OrderNo", request.OrderNo);
            }
            if (!string.IsNullOrEmpty(request.CarNumber))
            {
                condition.Append(" AND car_number = @CarNumber");
                para.Add("@CarNumber", request.CarNumber);
            }
            if (!string.IsNullOrEmpty(request.Mobile))
            {
                condition.Append(" AND (technician = @Mobile or mobile = @Mobile)");
                para.Add("@Mobile", request.Mobile);
            }

            if (request.IsDeleted > -1)
            {
                condition.Append(" AND is_deleted =@IsDeleted ");
                para.Add("@IsDeleted", request.IsDeleted);
            }
            if (request.StartTime != new DateTime(1900, 1, 1))
            {
                condition.Append(" AND start_time > @StartTime ");
                para.Add("@StartTime", request.StartTime);
                if (request.EndTime != new DateTime(1900, 1, 1))
                {
                    condition.Append(" AND end_time < @EndTime ");
                    para.Add("@EndTime", request.EndTime);
                }
            }
            
            PagedEntity<ShopCarRecordDO> pagedEntity = new PagedEntity<ShopCarRecordDO>();
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });

            if (count > 0)
            {
                condition.Append(" order by id DESC");

                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();

                IEnumerable<ShopCarRecordDO> ShopList = new List<ShopCarRecordDO>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<ShopCarRecordDO>(sql, para);
                });
                pagedEntity.Items = ShopList.ToList();
            }
            pagedEntity.TotalItems = count;
            return pagedEntity;
        }

        /// <summary>
        /// 修改门店车辆使用记录
        /// </summary>
        /// <param name="shopCarRecordDO"></param>
        /// <returns></returns>
        public async Task<int> ModifyShopCarRecordInfo(ShopCarRecordDO shopCarRecordDO) 
        {
            return await UpdateAsync(shopCarRecordDO, new[] { "CarId", "CarNumber", "Technician", "Mobile", "StartTime", "EndTime", "StartMileage", "EndMileage", "OilWear", "UpdateBy", "UpdateTime" });
        }

    }
}
