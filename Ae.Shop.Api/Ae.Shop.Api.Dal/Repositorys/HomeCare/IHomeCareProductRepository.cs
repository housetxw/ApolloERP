using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IHomeCareProductRepository : IRepository<HomeCareProductDO>
    {
        Task<List<HomeCareProductDO>> GetHomeCareProductsByTechId(HomeCareRecordDO request);

        /// <summary>
        /// 订单安装更新数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateProductInstallNum(HomeCareProductDO request);

        /// <summary>
        /// 回写领料表记录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type">1->实收 2->货损 3->缺少</param>
        /// <returns></returns>
        Task<int> UpdateProductReturnNum(HomeCareProductDO request,int type);
    }
}
