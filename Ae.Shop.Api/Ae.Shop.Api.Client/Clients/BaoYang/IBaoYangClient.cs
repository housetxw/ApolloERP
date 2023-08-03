using Ae.Shop.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.BaoYang
{
    public interface IBaoYangClient
    {
        /// <summary>
        /// 获取服务项目
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceTypeEnumDto>> GetServiceTypeEnum();
    }
}
