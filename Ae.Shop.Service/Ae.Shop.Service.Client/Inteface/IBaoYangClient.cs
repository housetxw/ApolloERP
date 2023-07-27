using Ae.Shop.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IBaoYangClient
    {
        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceTypeDTO>> GetServiceTypeEnum();
    }
}
