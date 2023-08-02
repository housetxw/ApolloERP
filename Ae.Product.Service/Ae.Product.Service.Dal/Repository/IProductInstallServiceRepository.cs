using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Extend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IProductInstallServiceRepository : IRepository<RelProductInstallserviceDO>
    {
        Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByProductIds(List<string> productIds);

        Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByPidList(List<string> pidList, bool readOnly = true);

        Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByPidList(List<string> pidList,
            string serviceId, bool readOnly = true);

        /// <summary>
        /// 产品安装服务配置页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ProductInstallDTO>> GetProductInstallServicePageList(
            ProductInstallServicePageListCondition request);
    }
}
