using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IFlashSaleConfigRepository : IRepository<FlashSaleConfigDO>
    {
        Task<bool> AutoOffFlashSaleConfig();
    }
}
