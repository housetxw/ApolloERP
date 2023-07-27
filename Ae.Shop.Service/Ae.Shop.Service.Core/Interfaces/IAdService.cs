using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IAdService
    {
        Task<List<AdvertisementDTO>> GetAdListAsync();
        Task<NoticeDTO> GetTopNewNoticeAsync(GetTopNewNoticeRequest request);
        Task<NoticeDTO> GetNoticeInfoAsync(GetNoticeInfoRequest request);
        Task<GetNoticeListResponse> GetNoticeListAsync(GetNoticeListRequest request);
    }
}
