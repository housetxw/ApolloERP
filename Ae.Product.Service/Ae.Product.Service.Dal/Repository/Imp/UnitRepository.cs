using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.Product.Service.Core.Enums;
using Ae.Product.Service.Core.Request;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class UnitRepository : AbstractRepository<DimUnitDO>, IUnitRepository
    {
        public UnitRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 分页查询商品单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<DimUnitDO>> GetProductUnitList(GetProductUnitListRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" where 1=1");
            if (!string.IsNullOrWhiteSpace(request.UnitName))
            {

                builder.AppendLine(" and unit_name = @UnitName");
                parameters.Add("@UnitName", request.UnitName.Trim());
            }

            if (request.Id > 0)
            {
                builder.AppendLine(" and id =@Id");
                parameters.Add("@Id", request.Id);
            }
            switch (request.IsForbid)
            {
                case (int)IsForbidEnumType.No:
                    builder.AppendLine(" and is_deleted =@IsDeleted");
                    parameters.Add("@IsDeleted", (int)IsForbidEnumType.No);
                    break;
                case (int)IsForbidEnumType.Yes:
                    builder.AppendLine(" and is_deleted =@IsDeleted");
                    parameters.Add("@IsDeleted", (int)IsForbidEnumType.Yes);
                    break;
                default:
                    builder.AppendLine(" and is_deleted IN @IsDeleted");
                    parameters.Add("@IsDeleted", new int[] { 0, 1 });
                    break;
            }

            var sqlCount = @"select Count(1) FROM dim_unit " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });


            var sql = "Select id AS Id ,unit_name AS UnitName,is_deleted AS IsDeleted, create_by AS CreateBy,create_time AS CreateTime, update_by AS UpdateBy," +
                     " update_time AS UpdateTime from dim_unit" + builder.ToString() + " Order by update_time DESC limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";

            IEnumerable<DimUnitDO> dimUnits = null;
            await OpenConnectionAsync(async conn => dimUnits = await conn.QueryAsync<DimUnitDO>(sql, parameters));


            PagedEntity<DimUnitDO> response = new PagedEntity<DimUnitDO>();
            response.TotalItems = total;
            response.Items = dimUnits.ToList();

            return response;
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="dimUnitDo"></param>
        /// <returns></returns>
        public async Task<int> EditUnitAsync(DimUnitDO dimUnitDo)
        {
            var sql =
                "update dim_unit set unit_name=@UnitName,is_deleted=@IsDeleted,update_by=@UpdateBy,update_time=@UpdateTime where id=@Id";
            int id = 0;
            await OpenConnectionAsync(async conn =>
            {
                id = await conn.ExecuteAsync(sql, new
                {
                    UnitName = dimUnitDo.UnitName,
                    IsDeleted = dimUnitDo.IsDeleted,
                    UpdateBy = dimUnitDo.UpdateBy,
                    UpdateTime = dimUnitDo.UpdateTime,
                    Id = dimUnitDo.Id
                });
            }
            );
            return id;
        }
    }
}
