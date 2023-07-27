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
    public class ShopCarRepository : AbstractRepository<ShopCarDO>, IShopCarRepository
    {
        public ShopCarRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 修改门店车辆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> ModifyShopCar(ShopCarDO model) 
        {
            var num = await UpdateAsync(model,new[] { "CarNumber", "Brand", "Vehicle", "VehicleId", "PaiLiang", "Nian", "Tid", "SalesName", "VinCode", "TotalMileage", 
                "Price", "InsureStart", "InsureEnd", "InsuranceCompany", "UpdateBy", "UpdateTime" });
            return num;
        }

        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopCarDO>> GetShopCarListByShopId(GetShopCarPageListRequest request) 
        {
            string sqlCount = @"SELECT
COUNT(*) as cs
FROM
    shop_car sc";
            string sql = @"SELECT
id,
status,
car_number CarNumber,
brand,
vehicle,
pai_liang PaiLiang,
nian,
sales_name SalesName,
create_by CreateBy,
create_time CreateTime,
update_by UpdateBy,
update_time UpdateTime
FROM shop_car sc";


            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE sc.is_deleted = 0 AND sc.shop_id = @ShopId");
            if (!string.IsNullOrEmpty(request.CarNumber))
            {
                condition.Append(" AND sc.car_number = @CarNumber");
                para.Add("@CarNumber", request.CarNumber);
            }

            if (request.Status > -1)
            {
                condition.Append(" AND sc.status =@Status ");
                para.Add("@Status", request.Status);
            }
            PagedEntity<ShopCarDO> pagedEntity = new PagedEntity<ShopCarDO>();
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });

            if (count > 0)
            {
                condition.Append(" order by sc.id DESC");

                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();

                IEnumerable<ShopCarDO> ShopList = new List<ShopCarDO>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<ShopCarDO>(sql, para);
                });
                pagedEntity.Items = ShopList.ToList();
            }
            pagedEntity.TotalItems = count;
            return pagedEntity;
        }
        /// <summary>
        /// 修改门店车辆状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShopCarStatus(long id,int status) 
        {
            string sql = "UPDATE shop_car set status = @Status where id = @Id ";
            var para = new DynamicParameters();
            para.Add("@Id", id);
            para.Add("@Status", status);
            int num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num > 0;
        }
        
    }
}
