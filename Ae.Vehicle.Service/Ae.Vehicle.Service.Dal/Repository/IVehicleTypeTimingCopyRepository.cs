using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IVehicleTypeTimingCopyRepository : IRepository<VehicleTypeTimingCopyDO>
    {
        Task<List<VehicleTypeTimingCopyDO>> GetAAllVehicleTypeTiming();
    }
}
