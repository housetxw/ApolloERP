using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Client.Model.BaoYang;

namespace Ae.Receive.Service.Client.Inteface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaoYangClient
    {
        /// <summary>
        /// 获取服务项目
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceTypeEnumDto>> GetServiceTypeEnum();
    }
}
