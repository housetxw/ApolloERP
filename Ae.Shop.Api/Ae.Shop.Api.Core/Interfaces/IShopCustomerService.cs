using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using Ae.Shop.Api.Core.Request.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopCustomerService
    {
        /// <summary>
        /// 门店客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCustomerListVo>> GetShopCustomerList(ShopCustomerListRequest request);

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopCustomerDetailVo> GetShopCustomerDetail(ShopCustomerDetailRequest request);
    }
}
