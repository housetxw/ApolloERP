using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IDimSequenceRepository : IRepository<DimSequenceDO>
    {
        /// <summary>
        ///  获取自增Code
        /// </summary>
        /// <param name="seqName">业务标识</param>
        /// <returns></returns>
        int GenerateProductCode(string seqName);

        DimSequenceDO GetSequenceBySeqName(string seqName);
    }
}
