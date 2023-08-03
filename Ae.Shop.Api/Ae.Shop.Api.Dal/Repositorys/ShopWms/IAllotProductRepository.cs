using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IAllotProductRepository : IRepository<AllotProductDO>
    {
        //Task<int> DeleteAllotProduct(DeleteAllotProductRequest request);

        Task<int> UpdateAllotProductStock(AllotProductDO request);
    }
}
