using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public interface IDimSequenceRepository : IRepository<DimSequenceDo>
    {
        /// <summary>
        ///  获取自增Code
        /// </summary>
        /// <param name="seqName">业务标识</param>
        /// <returns></returns>
        Task<int> GenerateProductCode(string seqName);

        Task<DimSequenceDo> GetSequenceBySeqName(string seqName);
    }
}
