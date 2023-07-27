using AutoMapper;
using ApolloErp.Log;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Repositorys.Ad;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Response;

namespace Ae.Shop.Service.Imp.Services
{
    public class AdService : IAdService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<AdService> _logger;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly INoticeRepository _noticeRepository;

        public AdService(IMapper mapper,
            ApolloErpLogger<AdService> ApolloErpLogger,
            IAdvertisementRepository advertisementRepository,
            INoticeRepository noticeRepository
        ) 
        {
            _mapper = mapper;
            _advertisementRepository = advertisementRepository;
            _logger = ApolloErpLogger;
            _noticeRepository = noticeRepository;
        }

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdvertisementDTO>> GetAdListAsync() 
        {
            var dt = DateTime.Now;
            var paras = new
            {
                Dt = dt
            };
            var conditon = "where is_deleted = 0 AND is_display = 1 AND @Dt BETWEEN start_date and end_date";
            var list = await _advertisementRepository.GetListAsync(conditon.ToString(), paras);
            list = list.OrderByDescending(s => s.Id);
            return _mapper.Map<List<AdvertisementDTO>>(list);
        }

        /// <summary>
        /// 获取最新公告
        /// </summary>
        /// <returns></returns>
        public async Task<NoticeDTO> GetTopNewNoticeAsync(GetTopNewNoticeRequest request) 
        {
            //查询最新公告
            var notice = await _noticeRepository.GetTopNewNoticeAsync();
            var noticeInfo = _mapper.Map<NoticeDTO>(notice);
            //查询所有公告
            var conditon = "where is_deleted = 0 AND is_display = 1";
            var list = await _noticeRepository.GetListAsync(conditon);
            //查询门店已读公告
            var shopNoticeList = await _noticeRepository.GetNoticeListByShopAsync(new GetNoticeListRequest() { ShopId = request.ShopId, UserId = request.UserId });
            noticeInfo.UnreadCount = list.Count() - shopNoticeList.Count;
            return noticeInfo;
        }
        /// <summary>
        /// 获取公告详情
        /// </summary>
        /// <returns></returns>
        public async Task<NoticeDTO> GetNoticeInfoAsync(GetNoticeInfoRequest request)
        {
            var conditon = "where is_deleted = 0 AND is_display = 1 AND id= @Id";
            var paras = new
            {
                Id = request.Id
            };
            var info = (await _noticeRepository.GetListAsync(conditon.ToString(), paras)).FirstOrDefault();
            if (info != null)
            {
                var ss = await _noticeRepository.AddUserNoticeAsync(request);
            }
            return _mapper.Map<NoticeDTO>(info);
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <returns></returns>
        public async Task<GetNoticeListResponse> GetNoticeListAsync(GetNoticeListRequest request)
        {
            //查询所有公告
            var conditon = "where is_deleted = 0 AND is_display = 1";
            //公告分页
            var pageList = await _noticeRepository.GetListPagedAsync(request.PageIndex, request.PageSize,conditon.ToString()," id desc");
            
            //查询门店已读公告
            var shopNoticeList = await _noticeRepository.GetNoticeListByShopAsync(request);

            var response = new GetNoticeListResponse();
            response.Items = _mapper.Map<List<NoticeDTO>>(pageList.Items);
            response.TotalItems = pageList.TotalItems;
            response.ReadTotal = shopNoticeList.Count;
            response.UnReadTotal = pageList.TotalItems - shopNoticeList.Count;
            return response;
        }

    }
}
