using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface ITechnicianRepository : IRepository<TechnicianDO>
    {
        Task<bool> UpdateTechnicianById(TechnicianDO req);
        Task<TechnicianDO> GetTechnicianInfo(string accountId);

    }
}
