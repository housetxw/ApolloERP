using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopCheckPropertyRepository: AbstractRepository<ShopCheckPropertyDo>, IShopCheckPropertyRepository
    {
        public ShopCheckPropertyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 配置查询
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="parentId">-1 查所有</param>
        /// <returns></returns>
        public async Task<List<ShopCheckPropertyDo>> GetShopCheckProperty(int categoryId, long parentId)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE `category_id` = @categoryId");
            para.Add("@categoryId", categoryId);
            if (parentId >= 0)
            {
                condition.Append(" AND `parent_id` = @parentId");
                para.Add("@parentId", parentId);
            }

            var result = await GetListAsync<ShopCheckPropertyDo>(condition.ToString(), para);

            return result?.OrderBy(_ => _.Rank)?.ToList() ?? new List<ShopCheckPropertyDo>();
        }

        /// <summary>
        /// 根据KeyName查询data
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="categoryId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<ShopCheckPropertyDo> GetPropertyByKeyName(string keyName, int categoryId, int parentId = 0)
        {
            var para = new DynamicParameters();
            para.Add("@keyName", keyName);
            para.Add("@categoryId", categoryId);
            para.Add("@parentId", parentId);

            var result = await GetListAsync<ShopCheckPropertyDo>("WHERE key_name = @keyName AND `category_id` = @categoryId AND `parent_id` = @parentId", para);

            return result?.ToList()?.FirstOrDefault();
        }
    }
}
