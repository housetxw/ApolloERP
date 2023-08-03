using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Common.Extension;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    /// <summary>
    /// For Shop
    /// </summary>
    public class OrderQueryForShopRepository : AbstractRepository<OrderDO>, IOrderQueryForShopRepository
    {
        public OrderQueryForShopRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderDO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();
            if (request.ShopId <= 0)
                return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));
            else
            {
                var parameters = new DynamicParameters();
                var builder = new StringBuilder();
                builder.AppendLine("where 1=1");

                var shopIds = request.ShopIds ?? new List<int>();
                if (shopIds?.Count<= 0) {
                    shopIds.Add(request.ShopId);
                }


                //builder.AppendLine(" and shop_Id=@ShopId");
                //parameters.Add("@ShopId", request.ShopId);

                builder.AppendLine(" and shop_Id in @ShopIds");
                parameters.Add("@ShopIds", shopIds);

                if (request.ProductType > -1)
                {
                    builder.AppendLine(" And produce_type=@ProductType ");
                    parameters.Add("@ProductType", request.ProductType);
                }

                if (!string.IsNullOrEmpty(request?.OrderNo))
                {
                    builder.AppendLine(" and order_no like CONCAT('%',@OrderNo,'%')");
                    parameters.Add("@OrderNo", request.OrderNo.Trim());
                }
                if (!string.IsNullOrEmpty(request.OrderStatus))
                {
                    var orderStatus = request.OrderStatus.CovertToEnum<OrderStatusEnum>();
                    builder.AppendLine(" and order_status = @OrderStatus");
                    parameters.Add("@OrderStatus", orderStatus.ToInt());
                }
                if (!string.IsNullOrEmpty(request.OrderChannel))
                {
                    var channelEnum = request.OrderChannel.CovertToEnum<ChannelEnum>();
                    builder.AppendLine(" and channel = @Channel");
                    parameters.Add("@Channel", channelEnum.ToInt());
                }

                if (!string.IsNullOrEmpty(request?.OrderType))
                {
                    builder.AppendLine(" and order_type=@OrderType");
                    var orderTypeEnum = request.OrderType.CovertToEnum<OrderTypeEnum>();
                    parameters.Add("@OrderType", orderTypeEnum.ToInt());
                }


                if (!string.IsNullOrEmpty(request?.InstallStatus))
                {
                    builder.AppendLine(" and install_status=@InstallStatus");
                    var orderTypeEnum = request.InstallStatus.CovertToEnum<InstallStatusEnum>();
                    parameters.Add("@InstallStatus", orderTypeEnum.ToInt());
                }

                if (!string.IsNullOrEmpty(request?.PayStatus))
                {
                    builder.AppendLine(" and pay_status=@PayStatus");
                    var payStatusEnum = request.PayStatus.CovertToEnum<PayStatusEnum>();
                    parameters.Add("@PayStatus", payStatusEnum.ToInt());
                }

                if (!string.IsNullOrEmpty(request?.CustomerName))
                {
                    builder.AppendLine(" and user_name like CONCAT('%',@UserName,'%') ");
                    parameters.Add("@UserName", request.CustomerName.Trim());
                }

                if (!string.IsNullOrEmpty(request?.ContactPhone))
                {
                    builder.AppendLine(" And contact_phone=@ContactPhone ");
                    parameters.Add("@ContactPhone", request.ContactPhone.Trim());
                }

                if (!string.IsNullOrEmpty(request.CreateStartTime))
                {
                    bool isSuccess = DateTime.TryParse(request.CreateStartTime, out var startOrderTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and create_time>=@CreateTime");
                        parameters.Add("@CreateTime", startOrderTime);
                    }
                }

                if (!string.IsNullOrEmpty(request.CreateEndTime))
                {
                    bool isSuccess = DateTime.TryParse(request.CreateEndTime, out var endDateTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and create_time<@EndTime");
                        parameters.Add("@EndTime", endDateTime);
                    }
                }


                if (!string.IsNullOrEmpty(request.InstallStartTime))
                {
                    bool isSuccess = DateTime.TryParse(request.InstallStartTime, out var startInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and install_time>=@StartInstallTime");
                        parameters.Add("@StartInstallTime", startInstalledTime);
                    }
                }
                if (!string.IsNullOrEmpty(request.InstallEndTime))
                {
                    bool isSuccess = DateTime.TryParse(request.InstallEndTime, out var endInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and install_time<@EndInstallTime");
                        parameters.Add("@EndInstallTime", endInstalledTime);
                    }
                }

                if (string.IsNullOrEmpty(request.ExceptionOrder))
                {
                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                            parameters, false);
                }
                else
                {
                    var parametersOrder = new DynamicParameters();
                    var builderOrder = new StringBuilder();


                    builderOrder.AppendLine("where 1=1");
                    //builderOrder.AppendLine(" and shop_Id=@ShopId");
                    //parametersOrder.Add("@ShopId", request.ShopId);

                    builderOrder.AppendLine(" and shop_Id in @ShopIds");
                    parametersOrder.Add("@ShopIds", shopIds);

                    var channelEnum = request.ExceptionOrder.CovertToEnum<ExceptionOrderEnum>();

                    if (channelEnum.ToInt() == ExceptionOrderEnum.Yes.ToInt())
                    {
                        builderOrder.AppendLine(" and pay_status=@PayStatus");
                        parametersOrder.Add("@PayStatus", PayStatusEnum.HavePay.ToInt());

                        builderOrder.AppendLine(" and order_status=@OrderStatus");
                        parametersOrder.Add("@OrderStatus", OrderStatusEnum.Confirmed.ToInt());

                        builderOrder.AppendLine(" and install_status=@InstallStatus");
                        parametersOrder.Add("@InstallStatus", InstallStatusEnum.NotInstall.ToInt());

                        builder.AppendLine(" and create_time<=@CurrentTime");
                        parameters.Add("@CurrentTime", DateTime.Now.AddDays(-15));

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builderOrder.ToString(), "create_time desc",
                         parametersOrder, false);

                    }
                    else
                    {
                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                       parameters, false);
                    }

                }
                
                return response;

            }
        }
    }
}
