using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public interface IUserCarPropertyRepository
    {
        /// <summary>
        /// 新增五级车型属性
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task<bool> AddUserCarPropertyAsync(List<UserCarPropertyDO> properties);

        /// <summary>
        /// 查用户车型五级属性
        /// </summary>
        /// <param name="carIds"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<UserCarPropertyDO>> GetUserCarPropertiesAsync(List<string> carIds,
            bool readOnly = true);

        /// <summary>
        /// 编辑用户车型五级属性
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="carId"></param>
        /// <param name="submitter"></param>
        /// <returns></returns>
        Task<bool> EditCarPropertiesAsync(List<UserCarPropertyDO> properties, string carId,
            string submitter);
    }
}
