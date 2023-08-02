using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class DimSequenceRepository : AbstractRepository<DimSequenceDO>, IDimSequenceRepository
    {
        public DimSequenceRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        ///  获取自增Code
        /// </summary>
        /// <param name="seqName">业务标识</param>
        /// <returns></returns>
        public int GenerateProductCode(string seqName)
        {
            var paras = new
            {
                v_seq_name = seqName
            };
            var sql = $"select next_val('{seqName}');";
            var cvalue = 0;
            OpenConnection(conn =>
            {
                cvalue = conn.ExecuteScalar<int>(sql, paras);
            });
            return cvalue;
        }

        public DimSequenceDO GetSequenceBySeqName(string seqName)
        {
            var para = new DynamicParameters();
            para.Add("@seqName", seqName);
            var result = GetList("WHERE seq_name = @seqName", para, true);
            return result?.FirstOrDefault();
        }

    }
}
