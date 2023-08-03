using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface ITransferTaskRepository : IRepository<WarehouseTransferPackageDO>
    {
        Task<int> UpdatePackageStatus(WarehouseTransferPackageDO request);

        Task<List<WarehouseTransferProductDO>> GetWarehouseTransferProducts(WarehouseTransferProductDO request);

        Task<List<WarehouseTransferPackageDO>> GetWarehouseTransferPackages(WarehouseTransferPackageDO request);

        Task<WarehouseTransferTaskDO> GetWarehouseTransferTask(WarehouseTransferTaskDO request);

        /// <summary>
        /// 更新收货数量
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateProductPackageRelation(ProductPackageRelationDO request);

        /// <summary>
        /// 更新收货数量
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateWarehouseTransferProduct(WarehouseTransferProductDO request);

        Task<int> UpdateProductPackageRelationStatus(ProductPackageRelationDO request);

        Task<int> UpdateWarehouseTransferStatus(WarehouseTransferTaskDO request);

    }
}




