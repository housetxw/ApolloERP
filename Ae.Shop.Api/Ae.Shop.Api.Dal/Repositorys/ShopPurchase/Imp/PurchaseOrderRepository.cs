using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.Shop.Api.Core.Request.ShopPurchase;
using System.Linq;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase.Imp
{
    public class PurchaseOrderRepository : AbstractRepository<PurchaseOrderDO>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSqlReadOnly");
        }

        public async Task<int> BatchReleasePurchasePayStatus(List<string> purchaseIds, string updateBy)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", updateBy);

            param.Add("@ids", purchaseIds);
            var result = -1;
            var sql = @"update purchase_order
	                    set mouth_pay_flag='已失效',update_by = @update_by,update_time = SYSDATE( )
	                    where id in @ids";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> BatchUpdatePurchasePayStatus(List<string> purchaseIds, PurchaseOrderDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);

            param.Add("@serial_number", request.SerialNumber);
            param.Add("@payer", request.Payer);
            param.Add("@pay_time", request.PayTime);
            param.Add("@pay_method", request.PayMethod);

            param.Add("@ids", purchaseIds);
            var result = -1;

            var sql = @"UPDATE purchase_order 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        pay_status = 3,
                        mouth_pay_flag = '已结算',
                        serial_number = @serial_number,
                        payer = @payer,
                        pay_time = @pay_time,
                        pay_method = @pay_method 
                        WHERE
	                        id IN @ids";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> BatchUpdatePurchasePayStatusToAudit(List<string> purchaseIds, string updateBy)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", updateBy);

            param.Add("@ids", purchaseIds);
            var result = -1;
            var sql = @"UPDATE purchase_order 
                        SET update_by = @update_by,update_time = SYSDATE( ),mouth_pay_flag='审核中'
                        WHERE
	                        id in @ids";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> DeletePurchaseOrder(PurchaseOrderDO request)
        {

            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);

            param.Add("@id", request.Id);
            var result = -1;
            var sql = @"UPDATE purchase_order 
                        SET update_by = @update_by,update_time = SYSDATE( ),status=3
                        WHERE
	                        id = @id";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        

        public async Task<List<PurchaseOrderViewDTO>> SelectPurchaseOrderViewPages(SelectOutPurchaseOrdersRequest request)
        {
            var parames = new DynamicParameters();
            var condtions = new StringBuilder();
            parames.Add("@shop_id", request.shopId);
            condtions.Append(" and po.shop_id=@shop_id");
            var sql = @"SELECT
	                    po.id PoId,po.create_by CreateBy,
	                    pop.product_code ProductCode,
	                    pop.product_name ProductName,
	                    pop.purchase_price PurchasePrice,pop.id PurchaseProductId,
	                    pop.num Num,
	                    po.vender_short_name VenderShortName,pop.create_time  CreateTime,pop.remark Remark
                    FROM
	                    purchase_order po
	                    INNER JOIN purchase_order_product pop ON po.id = pop.po_id 
                        AND po.is_deleted = 0 
	                    AND pop.is_deleted = 0 
                    WHERE
	                    po.STATUS = 2" + condtions.ToString() + " order by po.id desc";

            IEnumerable<PurchaseOrderViewDTO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<PurchaseOrderViewDTO>(sql, parames));

            return productDOs?.ToList() ?? new List<PurchaseOrderViewDTO>();
        }

        public async Task<List<TempPurchaseOrderInfo>> SelectPurchasePayInfos(List<long> shopIds, long venderId,List<string> times)
        {
          
            var parames = new DynamicParameters();

            parames.Add("@vender_id", venderId);
            parames.Add("@shop_ids", shopIds);

            var conditions = new StringBuilder();

            if (times != null && times.Any()) {
                var startTime = Convert.ToDateTime(times[0]);
                var endTime = Convert.ToDateTime(times[1]);

                conditions.Append(" and po.create_time>= @startTime and po.create_time<=@endTime");
                parames.Add("@startTime",startTime);

                parames.Add("@endTime", endTime);
            }

            var sql = @"SELECT
	                    po.id PurchaseId,po.relation_purchase_id relationPurchaseId,po.hc_type hcType,
	                    pop.product_code ProductId,
	                    pop.product_name ProductName,
	                    pop.purchase_price Price,
	                    pop.num Num ,po.shop_id  shopId,po.create_time createTime ,po.waybill_number deliveryCode
                    FROM
	                    purchase_order po
	                    INNER JOIN purchase_order_product pop ON po.id = pop.po_id 
	                    AND po.is_deleted = 0 
	                    AND pop.is_deleted = 0 
	                    AND po.`status` <> 3 
	                    AND pop.`status` <> 3 and po.pay_status=1 and po.pay_type =3
                    WHERE
	                    po.vender_id = @vender_id 
	                    AND po.shop_id in @shop_ids and (po.mouth_pay_flag<>'审核中' and po.mouth_pay_flag<>'已结算') "+ conditions.ToString();
            IEnumerable<TempPurchaseOrderInfo> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<TempPurchaseOrderInfo>(sql, parames));

            return productDOs?.ToList() ?? new List<TempPurchaseOrderInfo>();

        }

        public async Task<int> UpdatePurchasePrice(List<PurchaseOrderProductDO> request)
        {
            var result = 1;

            foreach (var item in request)
            {
                var param = new DynamicParameters();
                param.Add("@Update_by", item.UpdateBy);
                param.Add("@Price", item.PurchaseCost);
                param.Add("@PoId", item.PoId);
                param.Add("@Id", item.Id);

                var sql = @"UPDATE purchase_order_product 
                        SET update_by = @Update_by,
                        update_time = SYSDATE( ),
                        purchase_cost = @Price, price = @Price,
                        total_cost = num * @Price, amount = num * @Price,
                        purchase_price = @Price,
                        total_price =  num * @Price
                        WHERE
	                        id = @Id and po_id = @PoId";

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            }

            var po = request.FirstOrDefault();
            if (po != null)
            {
                var param = new DynamicParameters();
                param.Add("@Update_by", po.UpdateBy);
                param.Add("@PoId", po.PoId);

                var sql = @"UPDATE purchase_order 
                        SET update_by = @Update_by,
                        update_time = SYSDATE( ),
                        total_amount = (SELECT sum(amount) FROM `purchase_order_product`
                                where po_id = @PoId and is_deleted = 0 ),
                        actual_amount = (SELECT sum(amount) FROM `purchase_order_product`
                                where po_id = @PoId and is_deleted = 0 ) + own_freight - coupon_amount
                  
                        WHERE
	                        id = @PoId";

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            }

            return result;
        }

        public async Task<int> UpdatePurchaseOrderDeliveryInfo(PurchaseOrderDO request)
        {
            //填写发货信息后，采购单变成已发货
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@waybill_number", request.WaybillNumber);
            param.Add("@own_freight", request.OwnFreight);
            param.Add("@id", request.Id);
            var result = -1;
            var sql = @"UPDATE purchase_order 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        waybill_number = @waybill_number,actual_amount = total_amount - coupon_amount + @own_freight, 
                        own_freight = @own_freight,status=4
                        WHERE
	                        id = @id";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdatePurchaseOrderPayType(List<long> purchaseIds, PurchaseOrderDO request)
        {
            var result = -1;

            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@ids", purchaseIds);
            param.Add("@pay_status", request.PayStatus);
           // param.Add("@pay_type", request.PayType);
            param.Add("@waybill_number", request.WaybillNumber);

            var sql = @"UPDATE purchase_order 
                        SET update_by = @update_by,
                        update_time = SYSDATE(),
                        pay_status = @pay_status,status = 6,waybill_number =@waybill_number
                        WHERE
                            id in @ids";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<List<TempPurchaseOrderInfo>> GetTargetPurchaseOrders(List<string> purchaseIds)
        {
            var sql = @"SELECT
	                    po.id PurchaseId,po.relation_purchase_id relationPurchaseId,po.hc_type hcType,
	                    pop.product_code ProductId,
	                    pop.product_name ProductName,
	                    pop.purchase_price Price,
	                    pop.num Num ,po.shop_id  shopId,po.create_time createTime ,po.waybill_number deliveryCode
                    FROM
	                    purchase_order po
	                    INNER JOIN purchase_order_product pop ON po.id = pop.po_id 
	                    AND po.is_deleted = 0 
	                    AND pop.is_deleted = 0 
	                    AND po.`status` <> 3 
	                    AND pop.`status` <> 3
                    WHERE
	                    po.id in @poIds ";
            var parames = new DynamicParameters();

            parames.Add("@poIds", purchaseIds);

            IEnumerable<TempPurchaseOrderInfo> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<TempPurchaseOrderInfo>(sql, parames));

            return productDOs?.ToList() ?? new List<TempPurchaseOrderInfo>();


        }
    }
}


