using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IUserCarComponentsResultRepository : IRepository<UserCarComponentsResultDo>
    {
        /// <summary>
        /// 获取车辆部件检查状况
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        Task<List<UserCarComponentsResultDo>> GetUserCarComponentsResult(string carId, int resultValue);
    }
}
