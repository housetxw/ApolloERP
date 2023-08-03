using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using Ae.ShopOrder.Service.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderPackageCardRepository : AbstractRepository<OrderPackageCardDO>, IOrderPackageCardRepository
    {
        public OrderPackageCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<bool> ActiveByOrderNo(string orderNo)
        {

            var sql = "update order_package_card set is_deleted=0 where source_order_no=@OrderNo";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNo = orderNo });
            });
            return count > 0;

        }

        /// <summary>
        ///  得到套餐卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderPackageCardDO>> GetOrderPackageCard(GetOrderPackageCardRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();


            condition.AppendLine("where   is_deleted=0");

            if (request.SourceOrderNos?.Count > 0)
            {
                condition.AppendLine(" and source_order_no in @SourceOrderNos");
                parameters.Add("@SourceOrderNos", request.SourceOrderNos);
            }

            if (request.VerifyOrderNos?.Count > 0)
            {
                condition.AppendLine(" and verify_order_no in @VerifyOrderNos");
                parameters.Add("@VerifyOrderNos", request.VerifyOrderNos);
            }
            
            var getListAsync = await GetListAsync<OrderPackageCardDO>(condition.ToString(), parameters, false);

            return getListAsync.AsList();
        }

    }
}
