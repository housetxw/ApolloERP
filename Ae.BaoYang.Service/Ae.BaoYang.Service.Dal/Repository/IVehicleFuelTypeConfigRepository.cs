using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IVehicleFuelTypeConfigRepository
    {
        /// <summary>
        /// 获取机油类别保养项目黑名单
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VehicleFuelTypeConfigDO>> GetVehicleFuelTypeConfigAsync();
    }
}
