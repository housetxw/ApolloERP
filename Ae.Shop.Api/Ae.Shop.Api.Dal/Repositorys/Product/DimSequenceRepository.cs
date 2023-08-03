using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public class DimSequenceRepository: AbstractRepository<DimSequenceDo>, IDimSequenceRepository
    {
        public DimSequenceRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        ///  获取自增Code
        /// </summary>
        /// <param name="seqName">业务标识</param>
        /// <returns></returns>
        public async Task<int> GenerateProductCode(string seqName)
        {
            var paras = new
            {
                v_seq_name = seqName
            };
            var sql = $"select next_val('{seqName}');";
            var cvalue = 0;
            await OpenConnectionAsync(async conn =>
            {
                cvalue = (await conn.ExecuteScalarAsync<int>(sql, paras));

            });

            return cvalue;
        }

        public async Task<DimSequenceDo> GetSequenceBySeqName(string seqName)
        {
            var para = new DynamicParameters();
            para.Add("@seqName", seqName);
            var result = await GetListAsync("WHERE seq_name = @seqName", para, true);
            return result?.FirstOrDefault();
        }
    }
}
