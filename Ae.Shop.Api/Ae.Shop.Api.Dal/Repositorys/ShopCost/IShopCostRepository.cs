using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopCost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Model.ShopCost;
using Ae.Shop.Api.Core.Request.ShopCost;

namespace Ae.Shop.Api.Dal.Repositorys.ShopCost
{
    public interface IShopCostRepository
    {
        /// <summary>
        /// 得到门店费用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopCostDO>> GetShopCostList(ShopCostRequest request);

        /// <summary>
        /// 得到门店费用详情
        /// </summary>
        /// <param name="costId"></param>
        /// <returns></returns>
        Task<ShopCostDetailDO> GetShopCostDetail(long costId);

        /// <summary>
        /// 获取费用类别查询条件
        /// </summary>
        /// <param name="arrivalIds"></param>
        /// <returns></returns>
        Task<List<ShopCostTypeDO>> GetShopCostTypeListCondition();


        /// <summary>
        ///新增费用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddShopCost(AddShopCostDo request);

        /// <summary>
        ///编辑费用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateShopCost(AddShopCostDo request);

        /// <summary>
        /// 删除费用
        /// </summary>
        /// <param name="shopCostDO"></param>
        /// <returns></returns>
        Task<bool> DeleteShopCost(AddShopCostDo shopCostDO);
    }
}
