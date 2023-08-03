using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    /// <summary>
    /// 签收仓储
    /// </summary>
    public class SignRepository : AbstractRepository<SignDO>, ISignRepository
    {
        public SignRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }

        /// <summary>
        /// 得到签收主表信息
        /// </summary>
        /// <param name="signCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<SignDO> GetSign(string signCode, int shopId)
        {
            if (string.IsNullOrEmpty(signCode) || shopId <= 0)
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();
                condition.AppendLine(" where relation_no=@SignCode");
                parameters.Add("@SignCode", signCode);

                condition.AppendLine(" and shop_Id=@ShopId");
                parameters.Add("@ShopId", shopId);

                var getListAsync = await GetListAsync<SignDO>(condition.ToString(), parameters, false);

                return getListAsync?.FirstOrDefault();
            }
        }

        /// <summary>
        /// 得到用户已签收记录
        /// </summary>
        /// <param name="packageNos"></param>
        /// <param name="shopId"></param>
        /// <param name="transferId"></param>
        /// <returns></returns>
        public async Task<List<SignDetailDO>> GetHaveSignDetail(List<string> packageNos, int shopId, string transferId)
        {
            if ( shopId <= 0)
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();
                if (packageNos!=null&&packageNos.Any())
                {
                    condition.AppendLine(" where package_no IN @PackageNos");
                    parameters.Add("@PackageNos", packageNos);
                }

                if (!string.IsNullOrEmpty(transferId))
                {
                    condition.AppendLine(" where relation_no= @TransferId");
                    parameters.Add("@TransferId", transferId);
                }

                condition.AppendLine(" and shop_Id=@ShopId");
                parameters.Add("@ShopId", shopId);

                var getListAsync = await GetListAsync<SignDetailDO>(condition.ToString(), parameters, true);

                return getListAsync.ToList();
            }
        }

        /// <summary>
        /// 更新签收记录表状态
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="updateBy"></param>
        /// <param name="signStatus"></param>
        /// <returns></returns>
        public async Task<long> UpdateSignStatus(string relationId, string updateBy, int signStatus,int signNum)
        {
            string sql = @"UPDATE `sign` 
                            SET sign_status = @SignStatus,update_by=@UpdateBy,update_time=Now(3),have_sign_num=have_sign_num+@SignNum
                            WHERE
                                relation_no = @RelationID ";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    SignStatus = signStatus,
                    UpdateBy = updateBy,
                    RelationID = relationId,
                    SignNum = signNum
                });
            });
            return count;
        }

        /// <summary>
        /// 得到签收的关联单号
        /// </summary>
        /// <param name="signCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<SignPackageDO>> GetHaveSignPackageRelationNo(string signCode, int shopId)
        {
            if (string.IsNullOrEmpty(signCode) || shopId <= 0)
                return null;
            else
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SignCode", signCode);
                parameters.Add("@ShopId", shopId);

                var sql =
                    @"select Distinct  B.relation_no AS RelationNo,A.package_no AS PackageNo,B.sign_status AS signStatus  from sign_detail A
                                        Inner join sign B
                                        ON A.sign_id = B.id
                                        where A.is_deleted = 0 AND B.is_deleted = 0
                                        AND(A.relation_no = @SignCode OR A.package_no = @SignCode)
                                        AND B.shop_id = @ShopId";

                IEnumerable<SignPackageDO> signPackage = null;
                await OpenConnectionAsync(async conn =>
                    signPackage = await conn.QueryAsync<SignPackageDO>(sql, parameters));

                return signPackage.ToList();


            }
        }

        /// <summary>
        /// 得到今日签收包裹
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<PagedEntity<SignDetailDO>> GetSignDetailList(int shopId, int pageSize, int pageIndex)
        {
            if (shopId <= 0)
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();

                condition.AppendLine(" where shop_Id=@ShopId");
                parameters.Add("@ShopId", shopId);

                condition.AppendLine(" and sign_time between @Today  and @Tomorrow");

                parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

                return await GetListPagedAsync<SignDetailDO>(pageIndex, pageSize, condition.ToString()
                     , "sign_time desc", parameters, false);

            }
        }

        /// <summary>
        /// 今日签收进度
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<TodayReceiveDO>> GetTodayReceiveOrder(int shopId)
        {
            if (shopId <= 0)
                return null;
            else
            {

                var sql =
                    @"  SELECT  OrderNo, SignStatus, SumNum, HaveSignNum, DeliveryInfo, Max(SignTime) AS SignTime, Max(SignUser)  AS SignUser FROM(SELECT
      
                
                    A.relation_no AS OrderNo,
                    A.sign_status AS SignStatus,
                    A.num AS SumNum,
                    A.have_sign_num As HaveSignNum,
                    A.package_nos AS DeliveryInfo,
                    B.sign_time AS SignTime,
                    B.sign_user AS SignUser
      
                    FROM
      
                sign A
      
                INNER JOIN(
                    SELECT
      
                        relation_no,
                    sign_id,
                    sign_time,
                    sign_user
      
                        FROM
      
                sign_detail
      
                WHERE
      
                    is_deleted = 0
      
                AND shop_id =@ShopId
      
                AND sign_time BETWEEN @Today
      
                AND @Tomorrow
      
                ORDER BY
      
                sign_time DESC

                    ) B ON A.id = B.sign_id) Temp
                    GROUP BY  OrderNo, SignStatus, SumNum, HaveSignNum, DeliveryInfo";

                var parameters = new DynamicParameters();


                parameters.Add("@ShopId", shopId);



                parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

                IEnumerable<TodayReceiveDO> orderDos = null;
                await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<TodayReceiveDO>(sql, parameters));

                return orderDos.ToList();
            }

        }

        /// <summary>
        /// 用户签收
        /// </summary>
        /// <param name="signDo"></param>
        /// <param name="signDetail"></param>
        /// <returns></returns>
        public async Task<long> SaveSign(SignDO signDo, List<SignDetailDO> signDetail)
        {
            long id = 0;

            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();

                try
                {
                    var whereCondition = @"
                                where relation_no = @OrderId AND shop_id=@ShopId and is_deleted=0";

                    var parameters = new DynamicParameters();
                    var relationId = signDo?.RelationNo;
                    parameters.Add("@OrderId", relationId);
                    parameters.Add("@ShopId", signDo.ShopId);

                    SignDO dalSignDo = (await GetListAsync<SignDO>(whereCondition, parameters, true))?.FirstOrDefault();
                    if (dalSignDo == null)
                    {
                        var signDoId = await InsertAsync<long>(signDo);
                        signDetail?.ForEach(x => { x.SignId = signDoId; });
                        await InsertBatchAsync(signDetail);

                        id = signDoId;
                    }
                    else
                    {
                        id = 1;
                        signDetail?.ForEach(x => { x.SignId = dalSignDo.Id; });
                        await InsertBatchAsync(signDetail);
                        //var num = signDetail?.Count ?? 0;
                        //var updateSql =
                        //    " UPDATE sign set have_sign_num=have_sign_num+" + num + ",update_time=NOW(3),update_by=@UpdateBy WHERE is_deleted = 0 AND id=@SignID ";

                        //parameters.Add("@UpdateBy", signDetail?.FirstOrDefault()?.SignUser);
                        //parameters.Add("@SignID", dalSignDo.Id);

                        //id = await conn.ExecuteAsync(updateSql, parameters);
                    }
                }
                catch (Exception e)
                {
                    id = 0;
                    tran.Rollback();
                }
                finally
                {
                    tran.Commit();
                }
            });

            return id;
        }
    }
}
