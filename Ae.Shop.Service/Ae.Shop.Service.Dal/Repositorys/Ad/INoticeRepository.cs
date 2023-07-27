using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Ad
{
    public interface INoticeRepository : IRepository<NoticeDO>
    {
        Task<NoticeDO> GetTopNewNoticeAsync();
        Task<List<NoticeInfoDO>> GetNoticeListByShopAsync(GetNoticeListRequest request);

        Task<bool> AddUserNoticeAsync(GetNoticeInfoRequest request);
    }
}
