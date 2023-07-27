using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Client.Model.User;
using Ae.Shop.Service.Client.Model.Vehicle;
using Ae.Shop.Service.Client.Request;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IVehicleClient
    {
        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserVehicleDto>> GetUserDefaultVehicleBatch(UserDefaultVehicleBatchRequest request);

        Task<List<GetVehicleBrandDTO>> GetVehicleBrandList();
    }
}
