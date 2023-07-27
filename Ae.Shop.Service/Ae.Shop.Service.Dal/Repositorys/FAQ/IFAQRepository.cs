using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface IFAQRepository : IRepository<FaqDO>
    {
        Task<ApiPagedResultData<FaqInfoDO>> GetFAQListAsync(GetFAQListRequest request);
        Task<List<FaqChannelDO>> GetFaqChannelListAsync();
        Task<List<FaqCategoryDO>> GetFaqCategoryListAsync(long categoryId);
    }
}
