using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public interface IShopArrivalCarReportRepository : IRepository<ShopArrivalCarReportDO>
    {
        Task<ShopArrivalCarReportDO> GetCarReportByRecId(long recId);
    }
}
