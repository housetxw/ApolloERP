using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IConfigAdvertisementRepository : IRepository<ConfigAdvertisementDo>
    {
        Task<List<ConfigAdvertisementDo>> GetValidConfigAdvertisement(string terminalType);
    }
}
