using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Api.Core.Model.ShopCost;
using Ae.Shop.Api.Core.Request.ShopCost;
using Ae.Shop.Api.Dal.Model.ShopCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopCost
{
    public class ShopCostRepository:AbstractRepository<ShopCostDO>, IShopCostRepository
    {
        private readonly ApolloErpLogger<ShopCostRepository> logger;
        private readonly string className;
        public ShopCostRepository(ApolloErpLogger<ShopCostRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }


        /// <summary>
        /// 得到门店费用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopCostDO>> GetShopCostList(ShopCostRequest request)
        {
            PagedEntity<ShopCostDO> response = new PagedEntity<ShopCostDO>();

            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1 and c.is_deleted=@IsDelete");

            parameters.Add("@IsDelete", request.IsDeleted);
            builder.AppendLine(" and c.shop_Id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);


            if (!string.IsNullOrEmpty(request.CostType))
            {
                bool iscostType = int.TryParse(request.CostType, out var costType);
                builder.AppendLine(" and c.cost_type=@CostType");
                parameters.Add("@CostType", costType);
            }

            if (!string.IsNullOrEmpty(request.PayChannel))
            {
                builder.AppendLine(" and c.pay_channel=@PayChannel");
                parameters.Add("@PayChannel", request.PayChannel);
            }

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                bool isSuccessStart = DateTime.TryParse(request.StartDate, out var startDate);
                bool isSuccessEnd = DateTime.TryParse(request.EndDate, out var endDate);
                if (isSuccessStart && isSuccessEnd)
                {
                    builder.AppendLine($" and  c.date between @StartDate and @EndDate");

                    parameters.Add("@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", endDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }
            
            var sqlCount = @"select Count(1) FROM shop_cost c " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            var sql = @"select c.id as Id,c.shop_id as ShopId,c.cost_type as CostType,c.date as Date,c.money as Money,c.pay_channel as PayChannel,
                        c.status as Status,c.remark as Remark,c.is_deleted as IsDelete,c.create_by as CreateBy,c.create_time as CreateTime,
                        c.update_by as UpdateBy,c.update_time as UpdateTime,t.`name` as CostTypeLabel from shop_cost c left join shop_cost_type t on c.cost_type=t.`code` "
                         + builder.ToString() + " order by c.create_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;

            IEnumerable<ShopCostDO> costDos = null;
            await OpenConnectionAsync(async conn => costDos = await conn.QueryAsync<ShopCostDO>(sql, parameters));

            response.TotalItems = total;
            response.Items = costDos.ToList();

            return response;
        }

        /// <summary>
        /// 得到门店费用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopCostTypeDO>> GetShopCostTypeListCondition()
        {
            var sql = @"select code, name from shop_cost_type where is_deleted=0";

            IEnumerable<ShopCostTypeDO> costTypeDos = null;
            await OpenConnectionAsync(async conn => costTypeDos = await conn.QueryAsync<ShopCostTypeDO>(sql));

            return costTypeDos?.ToList() ;
        }

        /// <summary>
        /// 得到费用详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ShopCostDetailDO> GetShopCostDetail(long Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);
            var getList = await GetListAsync<ShopCostDetailDO>("where id=@Id", parameters, false);
            return getList?.FirstOrDefault();
        }

        /// <summary>
        ///新增费用
        /// </summary>
        /// <param name="shopCostDO"></param>
        /// <returns></returns>
        public async Task<int> AddShopCost(AddShopCostDo shopCostDO)
        {
            int id = await InsertAsync(shopCostDO);
            return id;
        }

        /// <summary>
        ///编辑费用
        /// </summary>
        /// <param name="shopCostDO"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShopCost(AddShopCostDo shopCostDO)
        {
            var param = new List<string>
            {
                "Date",
                "CostType",
                "Money",
                "PayChannel",
                "InvoiceType",
                "Remark",
                "UpdateBy",
                "UpdateTime"
            };
            return await UpdateAsync(shopCostDO, param) >= 0;
        }

        /// <summary>
        ///删除费用
        /// </summary>
        /// <param name="shopCostDO"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShopCost(AddShopCostDo shopCostDO)
        {
            var param = new List<string>
            {
                "IsDeleted",
                "UpdateBy",
                "UpdateTime"
            };
            return await UpdateAsync(shopCostDO, param) >= 0;
        }

    }
}
