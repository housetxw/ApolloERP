using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IConfigShopPackageCardRepository : IRepository<ConfigShopPackageCardDO>
    {
        Task<PagedEntity<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(GetShopPackageCardProductPageListRequest request);

        Task<GetShopPackageCardProductPageListVo> GetShopCardDetail(GetShopCardDetailRequest request);



    }
}
