using System;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Dapper;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderAddressRepository : AbstractRepository<OrderAddressDO>, IOrderAddressRepository
    {
        public OrderAddressRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<OrderAddressDO> GetOrderAddress(long orderId)
        {
            var list = await GetListAsync("where order_id=@OrderId", new { OrderId = orderId });
            return list?.FirstOrDefault();
        }

        public async Task<bool> UpdateOrderAddress(UpdateOrderAddressRequest request)
        {
            int id = 0;
            StringBuilder condition = new StringBuilder();
            condition.Append("Update order_address set is_deleted=0");
            if (!string.IsNullOrEmpty(request.ReceiverName))
            {
                condition.Append(",receiver_name =@RecevieName");
            }
            if (!string.IsNullOrEmpty(request.ReceiverPhone))
            {
                condition.Append(",receiver_phone =@ReceivePhone");
            }

            condition.Append(",province_id=@ProvinceId,province=@Province,city_id=@CityId,city=@City,district_id=@DistinctId,district=@District,detail_address=@DetailAddress,update_by=@UpdateBy,update_time=Now(3) where order_id=@OrderId");

            var parameters = new DynamicParameters();
            parameters.Add("@RecevieName", request.ReceiverName);
            parameters.Add("@ReceivePhone", request.ReceiverPhone);
            parameters.Add("@province", request.Province);
            parameters.Add("@ProvinceId", request.ProvinceId);
            parameters.Add("@CityId", request.CityId);
            parameters.Add("@City", request.City);
            parameters.Add("@DistinctId", request.DistrictId);
            parameters.Add("@District", request.District);
            parameters.Add("@DetailAddress", request.DetailAddress);
            parameters.Add("@UpdateBy", request.UpdateBy);
            int.TryParse(request.OrderNo, out var orderId);
            parameters.Add("@OrderId", orderId);
            await OpenSlaveConnectionAsync(async conn => { id = await conn.ExecuteAsync(condition.ToString(), parameters); });
            return id>0;
        }
    }
}
