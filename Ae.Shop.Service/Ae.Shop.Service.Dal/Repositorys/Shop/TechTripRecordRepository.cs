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
    public class TechTripRecordRepository : AbstractRepository<TechTripRecordDO>, ITechTripRecordRepository
    {
        public TechTripRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 查询门店技师出行记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<TechTripRecordModel>> GetTripRecordPageList(GetTripRecordPageListRequest request)
        {
            string sqlCount = @"SELECT
    COUNT(*) as cs
    FROM
	tech_trip_record t
	LEFT JOIN employee e ON t.employee_id = e.id and e.is_deleted = 0 ";
            string sql = @"SELECT
	t.id,
    t.status,
    t.order_no OrderNo,
	t.car_number CarNumber,
    t.start_time StartTime,
    t.return_time ReturnTime,
	e.name,
	e.mobile,
    t.remark Remark,
    t.create_time CreateTime
    FROM 
	tech_trip_record t
	LEFT JOIN employee e ON t.employee_id = e.id and e.is_deleted = 0 ";

            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE t.is_deleted = 0 AND t.shop_id = @ShopId");

            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                condition.Append(" AND t.employee_id = @EmployeeId");
                para.Add("@EmployeeId", request.EmployeeId);
            }

            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                condition.Append(" AND t.order_no = @OrderNo");
                para.Add("@OrderNo", request.OrderNo);
            }
            if (!string.IsNullOrEmpty(request.CarNumber))
            {
                condition.Append(" AND t.car_number = @CarNumber");
                para.Add("@CarNumber", request.CarNumber);
            }
            if (!string.IsNullOrEmpty(request.Mobile))
            {
                condition.Append(" AND (e.name = @Mobile or e.mobile = @Mobile)");
                para.Add("@Mobile", request.Mobile);
            }

            if (request.Status > -1)
            {
                condition.Append(" AND t.status = @Status ");
                para.Add("@Status", request.Status);
            }
            if (request.StartTime != new DateTime(1900, 1, 1))
            {
                condition.Append(" AND start_time > @StartTime ");
                para.Add("@StartTime", request.StartTime);                
            }
            if (request.ReturnTime != new DateTime(1900, 1, 1))
            {
                condition.Append(" AND t.return_time < @ReturnTime ");
                para.Add("@ReturnTime", request.ReturnTime);
            }

            PagedEntity<TechTripRecordModel> pagedEntity = new PagedEntity<TechTripRecordModel>();
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });

            if (count > 0)
            {
                condition.Append(" order by t.id DESC");

                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();

                IEnumerable<TechTripRecordModel> ShopList = new List<TechTripRecordModel>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<TechTripRecordModel>(sql, para);
                });
                pagedEntity.Items = ShopList.ToList();
            }
            pagedEntity.TotalItems = count;
            return pagedEntity;
        }

        /// <summary>
        /// 查询技师出行记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TechTripRecordModel> GetTechTripRecordInfo(BaseGetInfoRequest request) 
        {
            var sql = @"SELECT
	t.id,
    t.status,
    t.order_no OrderNo,
	t.car_number CarNumber,
    t.start_time StartTime,
    t.return_time ReturnTime,
	t.start_mileage StartMileage,
	t.start_mileage_img StartMileageImg,
	t.end_mileage EndMileage,
	t.end_mileage_img EndMileageImg,
	t.start_oil StartOil,
	t.start_oil_img StartOilImg,
	t.end_oil EndOil,
	t.end_oil_img EndOilImg,
	t.refuelled Refuelled,
	s.brand,
	s.sales_name SalesName,
	e.name,
	e.mobile,
    t.remark Remark,
    t.create_by CreateBy,
	t.create_time CreateTime, 
    t.update_by UpdateBy,
    t.update_time UpdateTime
FROM
	tech_trip_record t
	LEFT JOIN employee e ON t.employee_id = e.id and e.is_deleted = 0
	LEFT JOIN shop_car s ON t.car_id = s.id  and s.is_deleted = 0
WHERE
	t.id = @Id and t.is_deleted = 0 ";
            var para = new DynamicParameters();
            para.Add("@Id", request.Id);

            var techTripRecordInfo = new TechTripRecordModel();
            await OpenSlaveConnectionAsync(async conn=> {
                techTripRecordInfo = await conn.QuerySingleAsync<TechTripRecordModel>(sql, para);
            });
            return techTripRecordInfo;
        }


    }
}
