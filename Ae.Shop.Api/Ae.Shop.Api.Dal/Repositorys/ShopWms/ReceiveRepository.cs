using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class ReceiveRepository : AbstractRepository<SignDetailDO>, IReceiveRepository
    {
        public ReceiveRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }

        public async Task<List<SignDetailDO>> GetSignList(List<string> packageNos, long shopId)
        {
            var result = await GetListAsync(" where package_no IN @PackageNos and shop_Id=@ShopId and is_deleted=0", new
            {
                PackageNos = packageNos,
                shopId = shopId
            });

            return result?.ToList() ?? new List<SignDetailDO>();
        }

        /// <summary>
        /// 获取今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<SignDetailDO>> GetTodaySignList(GetTodayReceivePackageRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                var condition = new StringBuilder();
                var parameters = new DynamicParameters();

                condition.AppendLine(" where shop_Id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);

                condition.AppendLine(" and sign_time between @Today  and @Tomorrow");

                parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

                parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

                return await GetListPagedAsync<SignDetailDO>(request.PageIndex, request.PageSize, condition.ToString()
                     , "sign_time desc", parameters, false);

            }
        }

        public async Task<long> SaveSign(SignDO signDo, List<SignDetailDO> signDetail)
        {
            long id = 0;
            await OpenConnectionAsync(async r =>
            {               
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
                        var signDoId = await InsertAsync(signDo);
                        signDetail?.ForEach(x => { x.SignId = signDoId; });
                        await InsertBatchAsync(signDetail);

                        id = signDoId;
                    }
                    else
                    {
                        id = 1;
                        signDetail?.ForEach(x => { x.SignId = dalSignDo.Id; });
                        await InsertBatchAsync(signDetail);
                       
                    }
                }
                catch (Exception e)
                {
                    id = 0;
                  
                }                
            });
            return id;
        }

        public async  Task<int> UpdateSignStatus(SignDO request)
        {
            var result = -1;
            var param = new DynamicParameters();
            param.Add("@update_by",request.UpdateBy);
            param.Add("@have_sign_num", request.HaveSignNum);
            param.Add("@sign_status", request.SignStatus);
            param.Add("@relation_no", request.RelationNo);

            var sql = @"UPDATE sign set
                        sign_status = @sign_status,have_sign_num=@have_sign_num,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        relation_no = @relation_no 
	                        AND is_deleted =0";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));

            return result;
        }
    }
}




