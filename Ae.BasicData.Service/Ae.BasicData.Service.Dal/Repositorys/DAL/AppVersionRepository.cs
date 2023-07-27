using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;

namespace Ae.BasicData.Service.Dal.Repositorys.DAL
{
    public class AppVersionRepository : AbstractRepository<AppVersionDO>, IAppVersionRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AppVersionRepository> logger;

        public AppVersionRepository(ApolloErpLogger<AppVersionRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<AppVersionDO>> GetAppVersionHistory()
        {
            var res = await GetListAsync("WHERE is_release = 1");
            return res.OrderByDescending(o => o.Code).ToList();
        }

        public async Task<List<AppVersionDO>> GetAppVersionListByCode(AppVersionEntityReqDTO req)
        {
            var param = new DynamicParameters();
            param.Add("@VersionCode", req.VersionCode);

            var res = await GetListAsync("WHERE code > @VersionCode ", param);
            return res.ToList();
        }

        public async Task<AppVersionGrayDO> GetAppVersionGrayInfoByCodeAndId(AppVersionEntityReqDTO req)
        {
            var res = new AppVersionGrayDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                var param = new DynamicParameters();
                param.Add("@VersionCode", req.VersionCode);
                param.Add("@ShopId", req.ShopId);

                var sql = @"SELECT version_code versionCode, shop_id shopId FROM app_version_gray
                            WHERE version_code = @VersionCode AND shop_id = @ShopId";
                res = await conn.QueryFirstOrDefaultAsync<AppVersionGrayDO>(sql, param);
            });
            return res;


            // ---------------------------------- 私有方法 --------------------------------------

        }
    }
}
