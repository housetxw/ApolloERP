using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.JoinIn
{
    public class ShopJoinRepository : AbstractRepository<ShopJoinDO>, IShopJoinRepository
    {
        public ShopJoinRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 查询门店加盟列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopJoinDO>> GetShopJointListAsync(GetShopJoinListRequest request)
        {
            //查询数量
            string sqlCount = @"SELECT COUNT(*) FROM shop_join ";
            //查询列表
            string sql = @"SELECT
    id,
	shop_name ShopName,
    province,
    city,
    district,
	CONCAT(province,city,district,shop_address) ShopAddress,
    contact_name ContactName,
    contact_tel ContactTel,
    create_time CreateTime,
    update_by UpdateBy
    FROM shop_join ";

            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE is_deleted = 0");

            var para = new DynamicParameters();
            

            if (!string.IsNullOrEmpty(request.CreatorPhone))
            {
                condition.Append(" AND creator_Phone = @Phone");
                para.Add("@Phone", request.CreatorPhone);
            }
            if (request.Status >= 0) 
            {
                para.Add("@Status", request.Status);
                condition.Append(" AND is_confirmed = @Status");
            }

            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            PagedEntity<ShopJoinDO> result = new PagedEntity<ShopJoinDO>();
            IEnumerable<ShopJoinDO> Items = new List<ShopJoinDO>();
            if (count > 0)
            {
                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY id desc ");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                try
                {
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        Items = await conn.QueryAsync<ShopJoinDO>(sql, para);
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
                
                result.Items = Items.ToList();
            }
            result.TotalItems = count;
            return result;
        }


        /// <summary>
        /// 审核-门店加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CheckShopJoinAsync(CheckShopJoinRequest request)
        {
            var dt = DateTime.Now;
            string sql = @"UPDATE shop_join 
SET is_confirmed = @IsConfirmed,
confirm_user = @ConfirmUser,
confirm_user_phone = @ConfirmUserPhone,
confirm_remark = @ConfirmRemark,
confir_time = @ConfirTime,
update_by = @ConfirmUser,
update_time = @ConfirTime
WHERE is_deleted = 0 
AND id = @Id ";

            var para = new DynamicParameters();
            para.Add("@Id", request.Id);
            para.Add("@IsConfirmed", request.IsConfirmed);
            para.Add("@ConfirmUser", request.ConfirmUser);
            para.Add("@ConfirmUserPhone", request.ConfirmUserPhone);
            para.Add("@ConfirmRemark", request.ConfirmRemark);
            para.Add("@ConfirTime", dt);

            var result = 0;
            try
            {
                await OpenConnectionAsync(async conn =>
                {
                    result = await conn.ExecuteAsync(sql, para);
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return result > 0;
        }
    }
}
