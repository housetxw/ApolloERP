using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IOrderPackageCardRepository : IRepository<OrderPackageCardDO>
    {
        /// <summary>
        /// 根据订单号激活核销码
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<bool> ActiveByOrderNo(string orderNo);


        /// <summary>
        ///  得到套餐卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderPackageCardDO>> GetOrderPackageCard(GetOrderPackageCardRequest request);
    }
}
