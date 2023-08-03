using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Common.Extension;
using Ae.Order.Service.Core.Enums;
using Ae.Order.Service.Dal.Condition.ShopOrder;
using Ae.Order.Service.Dal.Model;

namespace Ae.Order.Service.Dal.Repository.ShopOrder
{
    public class OrderForAppRepository : AbstractRepository<OrderDO>, IOrderForAppRepository
    {
        public OrderForAppRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        /// <summary>
        /// 得到订单主表的信息根据查询条件 For  App
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderDO>> GetMainOrdersForSearch(GetMainOrdersForSearchCondition request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();

            if (string.IsNullOrEmpty(request.Content) || request.ShopId <= 0)
                return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));
            else
            {
                var parameters = new DynamicParameters();
                var builder = new StringBuilder();
                builder.AppendLine("where 1=1");
           

                if (request.Channels != null && request.Channels.Any())
                {
                    builder.AppendLine(" and channel in @Channel");
                    parameters.Add("@Channel", request.Channels);
                }
                if (request.SearchType == GetOrdersTypeEnum.OrderNo)
                {
                    builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");
                    builder.AppendLine(" and shop_Id=@ShopId");
                    parameters.Add("@ShopId", request.ShopId);
                    builder.AppendLine(" and order_no=@OrderNo");
                    parameters.Add("@OrderNo", request.Content.Trim());
                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                        parameters, false);
                }
                if (request.SearchType == GetOrdersTypeEnum.Telephone)
                {
                    builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");
                    builder.AppendLine(" and shop_Id=@ShopId");
                    parameters.Add("@ShopId", request.ShopId);
                    builder.AppendLine(" and user_phone=@UserTelephone");
                    parameters.Add("@UserTelephone", request.Content.Trim());
                    response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                        parameters, false);
                }

                if (request.SearchType == GetOrdersTypeEnum.ProductName)
                {
                    builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                    var sqlCount = @"select Count(1) FROM (
                                        SELECT DISTINCT A.* FROM `order` A
                                        INNER JOIN `order_product` B
                                        ON A.id=B.order_id
                                        AND A.is_deleted=0
                                        AND B.is_deleted=0
                                        AND A.shop_id=@ShopId " + builder.ToString() + @"
                                        AND B.product_name like CONCAT('%',@Content,'%') 
                                       ) t";

                    parameters.Add("@ShopId", request.ShopId);
                    parameters.Add("@Content", request.Content.Trim());
                    var total = 0;
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                    });

                    var sql = @" SELECT DISTINCT A.`id` AS `Id`,
	                                    A.`order_no` AS `OrderNo`,
	                                    A.`channel` AS `Channel`,
	                                    A.`terminal_type` AS `TerminalType`,
	                                    A.`app_version` AS `AppVersion`,
	                                    A.`order_type` AS `OrderType`,
	                                    A.`order_status` AS `OrderStatus`,
	                                    A.`order_time` AS `OrderTime`,
	                                    A.`user_id` AS `UserId`,
	                                    A.`user_name` AS `UserName`,
	                                    A.`user_phone` AS `UserPhone`,
	                                    A.`contact_id` AS `ContactId`,
	                                    A.`contact_name` AS `ContactName`,
	                                    A.`contact_phone` AS `ContactPhone`,
	                                    A.`total_product_num` AS `TotalProductNum`,
	                                    A.`total_product_amount` AS `TotalProductAmount`,
	                                    A.`service_fee` AS `ServiceFee`,
	                                    A.`delivery_fee` AS `DeliveryFee`,
	                                    A.`total_coupon_amount` AS `TotalCouponAmount`,
	                                    A.`actual_amount` AS `TotalAmount`,
	                                    A.`pay_method` AS `PayMethod`,
	                                    A.`pay_channel` AS `PayChannel`,
	                                    A.`pay_status` AS `PayStatus`,
	                                    A.`stock_status` AS `StockStatus`,
	                                    A.`is_need_invoice` AS `IsNeedInvoice`,
	                                    A.`is_need_delivery` AS `IsNeedDelivery`,
	                                    ' ' AS `DeliveryCode`,
	                                    A.`delivery_type` AS `DeliveryType`,
	                                    A.`delivery_method` AS `DeliveryMethod`,
	                                    A.`delivery_status` AS `DeliveryStatus`,
	                                    A.`sign_status` AS `SignStatus`,
	                                    A.`is_need_install` AS `IsNeedInstall`,
	                                    A.`company_id` AS `CompanyId`,
	                                    A.`shop_id` AS `ShopId`,
	                                    A.`dispatch_status` AS `DispatchStatus`,
	                                    A.`install_status` AS `InstallStatus`,
	                                    A.`comment_status` AS `CommentStatus`,
	                                    ' ' AS `IsOccurRefund`,
	                                    A.`refund_status` AS `RefundStatus`,
	                                    A.`settle_status` AS `SettleStatus`,
	                                    A.`remark` AS `Remark`,
	                                    A.`is_deleted` AS `IsDeleted`,
	                                    A.`create_by` AS `CreateBy`,
	                                    A.`create_time` AS `CreateTime`,
	                                    A.`update_by` AS `UpdateBy`,
	                                    A.`update_time` AS `UpdateTime`,
	                                    A.`install_time` AS `InstallTime`,
	                                    A.`confirm_time` AS `ConfirmTime`,
	                                    A.`pay_time` AS `PayTime`,
	                                    A.`sign_time` AS `SignTime`,
	                                    A.`dispatch_time` AS `DispatchTime`,
	                                    A.`settle_time` AS `SettleTime`,
	                                    A.`delivery_time` AS `DeliveryTime`,
	                                    A.`cancel_time` AS `CancelTime` FROM `order` A
                                        INNER JOIN `order_product` B
                                        ON A.id=B.order_id
                                        AND A.is_deleted=0
                                        AND B.is_deleted=0
                                        AND A.shop_id=@ShopId " + builder.ToString() + @"
                                        AND B.product_name like CONCAT('%',@Content,'%')
                                        Order by A.create_time DESC limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";


                    IEnumerable<OrderDO> orderDos = null;
                    await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<OrderDO>(sql, parameters));

                    response.TotalItems = total;
                    response.Items = orderDos.ToList();
                }

                return response;
            }

        }

        /// <summary>
        ///  得到订单主表的信息根据业务状态 For App
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderDO>> GetMainOrdersForStatus(GetMainOrdersForStatusCondition request)
        {
            PagedEntity<OrderDO> response = new PagedEntity<OrderDO>();
            if (request.ShopId <= 0)
                return await Task.FromResult(new PagedEntity<OrderDO>(totalItems: 0, items: null));
            else
            {
                if (request.SearchType == GetOrdersTypeEnum.OrderNo ||
                    request.SearchType == GetOrdersTypeEnum.Telephone ||
                    request.SearchType == GetOrdersTypeEnum.NotSearch)
                {
                    var parameters = new DynamicParameters();
                    var builder = new StringBuilder();
                    builder.AppendLine("where 1=1");
                    if (request.Channels != null && request.Channels.Any())
                    {
                        builder.AppendLine(" and channel in @Channel");
                        parameters.Add("@Channel", request.Channels);
                    }
                    builder.AppendLine(" and shop_Id=@ShopId");
                    parameters.Add("@ShopId", request.ShopId);
                    builder.AppendLine(" and delivery_type=@DeliveryType");
                    parameters.Add("@DeliveryType", DeliveryTypeEnum.DeliveryToShop);

                    if (request.SearchType == GetOrdersTypeEnum.Telephone)
                    {
                        builder.AppendLine(" and user_phone=@Telephone");
                        parameters.Add("@Telephone", request.Content.Trim());
                    }

                    if (request.SearchType == GetOrdersTypeEnum.OrderNo)
                    {
                        builder.AppendLine(" and order_no=@OrderNo");
                        parameters.Add("@OrderNo", request.Content.Trim());
                    }

                    if (!string.IsNullOrEmpty(request.StartOrderTime))
                    {
                        bool isSuccess = DateTime.TryParse(request.StartOrderTime, out var startOrderTime);
                        if (isSuccess)
                        {
                            builder.AppendLine(" and order_time>=@OrderTime");
                            parameters.Add("@OrderTime", startOrderTime);
                        }
                    }

                    if (!string.IsNullOrEmpty(request.EndOrderTime))
                    {
                        bool isSuccess = DateTime.TryParse(request.EndOrderTime, out var endDateTime);
                        if (isSuccess)
                        {
                            builder.AppendLine(" and order_time<@EndTime");
                            parameters.Add("@EndTime", endDateTime);
                        }
                    }

                    if (request.ProductTypes != null && request.ProductTypes.Any())
                    {
                        builder.AppendLine(" and produce_type in @ProductType");
                        parameters.Add("@ProductType", request.ProductTypes);
                    }


                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingSign)
                    {
                       // builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine($" and order_status in ({OrderStatusEnum.Submitted.ToInt()},{OrderStatusEnum.Confirmed.ToInt()})");
                        //parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        //builder.AppendLine(" and delivery_status=@DeliveryStatus");
                        //parameters.Add("@DeliveryStatus", (int)DeliveryStatusEnum.HaveSend);

                        builder.AppendLine(" and sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.NotSign);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "order_time desc",
                            parameters, false);
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingDispatch)
                    {
                     //   builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine(" and sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.HaveSign);

                        builder.AppendLine("and dispatch_status=@DispatchStatus");
                        parameters.Add("@DispatchStatus", DispatchStatusEnum.NotDispatch);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "sign_time desc",
                            parameters, false);

                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.ABuilding)
                    {
                       // builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine("and dispatch_status=@DispatchStatus");
                        parameters.Add("@DispatchStatus", (int)DispatchStatusEnum.HaveDispatch);

                        //builder.AppendLine("and pay_status=@PayStatus");
                        //parameters.Add("@PayStatus", (int)PayStatusEnum.NotPay);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "dispatch_time desc",
                            parameters, false);

                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingInstall)
                    {
                      //  builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine(" and sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.HaveSign);

                        //builder.AppendLine(" and pay_status=@PayStatus");
                        //parameters.Add("@PayStatus", (int)PayStatusEnum.HavePay);

                        builder.AppendLine("and dispatch_status=@DispatchStatus");
                        parameters.Add("@DispatchStatus", (int)DispatchStatusEnum.HaveDispatch);

                        builder.AppendLine("and install_status=@InstallStatus");
                        parameters.Add("@InstallStatus", (int)InstallStatusEnum.NotInstall);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "dispatch_time desc",
                            parameters, false);
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Installed)
                    {
                        if (!string.IsNullOrEmpty(request.StartInstalledTime))
                        {
                            bool isSuccess = DateTime.TryParse(request.StartInstalledTime, out var startInstalledTime);
                            if (isSuccess)
                            {
                                builder.AppendLine(" and install_time>=@StartInstallTime");
                                parameters.Add("@StartInstallTime", startInstalledTime);
                            }
                        }

                        if (!string.IsNullOrEmpty(request.EndInstalledTime))
                        {
                            bool isSuccess = DateTime.TryParse(request.EndInstalledTime, out var endInstalledTime);
                            if (isSuccess)
                            {
                                builder.AppendLine(" and install_time<@EndInstallTime");
                                parameters.Add("@EndInstallTime", endInstalledTime);
                            }
                        }

                      //  builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Completed);

                        builder.AppendLine("and install_status=@InstallStatus");
                        parameters.Add("@InstallStatus", (int)InstallStatusEnum.HaveInstall);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "install_time desc",
                            parameters, false);

                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Canceled)
                    {
                      //  builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Canceled);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "cancel_time desc",
                            parameters, false);
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingReconciliation)
                    {
                        builder.AppendLine(" and pay_status=@PayStatus");
                        parameters.Add("@PayStatus", (int)PayStatusEnum.HavePay);

                        builder.AppendLine(" and money_arrive_status=@MoneyArriveStatus");
                        parameters.Add("@MoneyArriveStatus", (int)MoneyArriveStatusEnum.Arrive);

                        builder.AppendLine(" and install_status=@InstallStatus");
                        parameters.Add("@InstallStatus", (int)InstallStatusEnum.HaveInstall);

                        builder.AppendLine(" and reconciliation_status=@ReconciliationStatus");
                        parameters.Add("@ReconciliationStatus", (int)ReconciliationStatusEnum.NotReconciliation);

                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "pay_time desc",
                            parameters, false);
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.All)
                    {
                      //  builder.AppendLine($" and ( produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");
                        response = await GetListPagedAsync<OrderDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                            parameters, false);
                    }

                    return response;

                }

                else
                {
                    var parameters = new DynamicParameters();
                    var builder = new StringBuilder();

                    if (!string.IsNullOrEmpty(request.StartOrderTime))
                    {
                        bool isSuccess = DateTime.TryParse(request.StartOrderTime, out var startOrderTime);
                        if (isSuccess)
                        {
                            builder.AppendLine(" and A.order_time>=@OrderTime");
                            parameters.Add("@OrderTime", startOrderTime);
                        }
                    }

                    if (!string.IsNullOrEmpty(request.EndOrderTime))
                    {
                        bool isSuccess = DateTime.TryParse(request.EndOrderTime, out var endDateTime);
                        if (isSuccess)
                        {
                            builder.AppendLine(" and A.order_time<@EndTime");
                            parameters.Add("@EndTime", endDateTime);
                        }
                    }

                    parameters.Add("@ProductName", request.Content.Trim());
                    parameters.Add("@ShopId", request.ShopId);
                    if (request.Channels != null && request.Channels.Any())
                    {
                        builder.AppendLine(" and A.channel in @Channel");
                        parameters.Add("@Channel", request.Channels);
                    }

                    if (request.ProductTypes != null && request.ProductTypes.Any())
                    {
                        builder.AppendLine(" and A.produce_type in @ProductType");
                        parameters.Add("@ProductType", request.ProductTypes);
                    }

                    string orderby = string.Empty;

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingSign)
                    {
                        //builder.AppendLine(" and order_status=@OrderStatus");
                        //parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine($" and order_status in ({OrderStatusEnum.Submitted.ToInt()},{OrderStatusEnum.Confirmed.ToInt()})");

                        //builder.AppendLine(" and delivery_status=@DeliveryStatus");
                        //parameters.Add("@DeliveryStatus", (int)DeliveryStatusEnum.HaveSend);

                        builder.AppendLine(" and sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.NotSign);

                        orderby = " A.delivery_time desc";
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingDispatch)
                    {
                     //   builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and A.order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine(" and A.sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.HaveSign);

                        builder.AppendLine("and A.dispatch_status=@DispatchStatus");
                        parameters.Add("@DispatchStatus", DispatchStatusEnum.NotDispatch);

                        orderby = " A.sign_time desc desc";
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.ABuilding)
                    {

                       // builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and A.order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine("and A.dispatch_status=@DispatchStatus");
                        parameters.Add("@DispatchStatus", (int)DispatchStatusEnum.HaveDispatch);

                        //builder.AppendLine("and A.pay_status=@PayStatus");
                        //parameters.Add("@PayStatus", (int)PayStatusEnum.NotPay);

                        orderby = " A.dispatch_time DESC";

                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingInstall)
                    {
                      //  builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and A.order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Confirmed);

                        builder.AppendLine(" and A.sign_status=@SignStatus");
                        parameters.Add("@SignStatus", (int)SignStatusEnum.HaveSign);

                        //builder.AppendLine(" and A.pay_status=@PayStatus");
                        //parameters.Add("@PayStatus", (int)PayStatusEnum.HavePay);

                        builder.AppendLine("and A.install_status=@InstallStatus");
                        parameters.Add("@InstallStatus", (int)InstallStatusEnum.NotInstall);


                        orderby = " A.pay_time DESC";
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Installed)
                    {

                      //  builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");
                        builder.AppendLine(" and A.order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Completed);

                        builder.AppendLine("and A.install_status=@InstallStatus");
                        parameters.Add("@InstallStatus", (int)InstallStatusEnum.HaveInstall);

                        if (!string.IsNullOrEmpty(request.StartInstalledTime))
                        {
                            bool isSuccess = DateTime.TryParse(request.StartInstalledTime, out var startInstalledTime);
                            if (isSuccess)
                            {
                                builder.AppendLine(" and A.install_time>=@StartInstallTime");
                                parameters.Add("@StartInstallTime", startInstalledTime);
                            }
                        }
                        if (!string.IsNullOrEmpty(request.EndInstalledTime))
                        {
                            bool isSuccess = DateTime.TryParse(request.EndInstalledTime, out var endInstalledTime);
                            if (isSuccess)
                            {
                                builder.AppendLine(" and A.install_time<@EndInstallTime");
                                parameters.Add("@EndInstallTime", endInstalledTime);
                            }
                        }

                        orderby = " A.install_time DESC";



                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.Canceled)
                    {
                     //   builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        builder.AppendLine(" and A.order_status=@OrderStatus");
                        parameters.Add("@OrderStatus", (int)OrderStatusEnum.Canceled);

                        orderby = " A.cancel_time DESC";

                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.WaitingReconciliation)
                    {
                        builder.AppendLine(" and A.pay_status=@PayStatus");
                        parameters.Add("@PayStatus", (int)PayStatusEnum.HavePay);

                        builder.AppendLine(" and A.money_arrive_status=@MoneyArriveStatus");
                        parameters.Add("@MoneyArriveStatus", (int)MoneyArriveStatusEnum.Arrive);

                        builder.AppendLine(" and A.reconciliation_status=@ReconciliationStatus");
                        parameters.Add("@ReconciliationStatus", (int)ReconciliationStatusEnum.NotReconciliation);

                        orderby = " A.pay_time DESC";
                    }

                    if (request.OrderBusinessStatus == OrderBusinessStatusEnum.All)
                    {
                     //   builder.AppendLine($" and ( A.produce_type !={OrderProductTypeEnum.BuyVerification.ToInt()} AND A.produce_type !={OrderProductTypeEnum.UseVerification.ToInt()} )");

                        orderby = " A.create_time DESC";
                    }

                    var sqlCount = @"select Count(1) FROM (
                                        SELECT DISTINCT A.* FROM `order` A
                                        INNER JOIN `order_product` B
                                        ON A.id=B.order_id
                                        AND A.is_deleted=0
                                        AND B.is_deleted=0
                                        AND A.shop_id=@ShopId
                                        AND 1=1 " + builder.ToString() + @"
                                        AND B.product_name like CONCAT('%',@ProductName,'%') ) t";

                    var total = 0;
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                    });

                    parameters.Add("@OrderBy", orderby);

                    var sql = @" SELECT DISTINCT A.`id` AS `Id`,
	                                    A.`order_no` AS `OrderNo`,
	                                    A.`channel` AS `Channel`,
	                                    A.`terminal_type` AS `TerminalType`,
	                                    A.`app_version` AS `AppVersion`,
	                                    A.`order_type` AS `OrderType`,
	                                    A.`order_status` AS `OrderStatus`,
	                                    A.`order_time` AS `OrderTime`,
	                                    A.`user_id` AS `UserId`,
	                                    A.`user_name` AS `UserName`,
	                                    A.`user_phone` AS `UserPhone`,
	                                    A.`contact_id` AS `ContactId`,
	                                    A.`contact_name` AS `ContactName`,
	                                    A.`contact_phone` AS `ContactPhone`,
	                                    A.`total_product_num` AS `TotalProductNum`,
	                                    A.`total_product_amount` AS `TotalProductAmount`,
	                                    A.`service_fee` AS `ServiceFee`,
	                                    A.`delivery_fee` AS `DeliveryFee`,
	                                    A.`total_coupon_amount` AS `TotalCouponAmount`,
	                                    A.`actual_amount` AS `TotalAmount`,
	                                    A.`pay_method` AS `PayMethod`,
	                                    A.`pay_channel` AS `PayChannel`,
	                                    A.`pay_status` AS `PayStatus`,
	                                    A.`stock_status` AS `StockStatus`,
	                                    A.`is_need_invoice` AS `IsNeedInvoice`,
	                                    A.`is_need_delivery` AS `IsNeedDelivery`,
	                                    ' ' AS `DeliveryCode`,
	                                    A.`delivery_type` AS `DeliveryType`,
	                                    A.`delivery_method` AS `DeliveryMethod`,
	                                    A.`delivery_status` AS `DeliveryStatus`,
	                                    A.`sign_status` AS `SignStatus`,
	                                    A.`is_need_install` AS `IsNeedInstall`,
	                                    A.`company_id` AS `CompanyId`,
	                                    A.`shop_id` AS `ShopId`,
	                                    A.`dispatch_status` AS `DispatchStatus`,
	                                    A.`install_status` AS `InstallStatus`,
	                                    A.`comment_status` AS `CommentStatus`,
	                                    ' ' AS `IsOccurRefund`,
	                                    A.`refund_status` AS `RefundStatus`,
	                                    A.`settle_status` AS `SettleStatus`,
	                                    A.`remark` AS `Remark`,
	                                    A.`is_deleted` AS `IsDeleted`,
	                                    A.`create_by` AS `CreateBy`,
	                                    A.`create_time` AS `CreateTime`,
	                                    A.`update_by` AS `UpdateBy`,
	                                    A.`update_time` AS `UpdateTime`,
	                                    A.`install_time` AS `InstallTime`,
	                                    A.`confirm_time` AS `ConfirmTime`,
	                                    A.`pay_time` AS `PayTime`,
	                                    A.`sign_time` AS `SignTime`,
	                                    A.`dispatch_time` AS `DispatchTime`,
	                                    A.`settle_time` AS `SettleTime`,
	                                    A.`delivery_time` AS `DeliveryTime`,
	                                    A.`cancel_time` AS `CancelTime` FROM `order` A 
                                        INNER JOIN `order_product` B
                                        ON A.id=B.order_id
                                        AND A.is_deleted=0
                                        AND B.is_deleted=0
                                        AND A.shop_id=@ShopId
                                        AND 1=1 " + builder.ToString() + @"
                                        AND B.product_name like CONCAT('%',@ProductName,'%')
                                        order by @OrderBy  limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize;


                    IEnumerable<OrderDO> orderDos = null;
                    await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<OrderDO>(sql, parameters));

                    response.TotalItems = total;
                    response.Items = orderDos.ToList();
                    return response;

                }
            }
        }

    }
}
