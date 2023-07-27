using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Ae.Shop.Service.Imp.Services
{

    public class ShopDeliveryRecordService : IShopDeliveryRecordService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<ShopDeliveryRecordService> _logger;
        private readonly IShopDeliveryConfigRepository _shopDeliveryConfigRepository;
        private readonly IShopDeliveryRecordRepository _shopDeliveryRecordRepository;
        private readonly IShopRepository _shopRepository;

        public ShopDeliveryRecordService(IMapper mapper,
            ApolloErpLogger<ShopDeliveryRecordService> ApolloErpLogger,
            IShopDeliveryConfigRepository shopDeliveryConfigRepository,
            IShopDeliveryRecordRepository shopDeliveryRecordRepository,
             IShopRepository shopRepository )
        {
            _mapper = mapper;
            _shopDeliveryConfigRepository = shopDeliveryConfigRepository;
            _logger = ApolloErpLogger;
            _shopDeliveryRecordRepository = shopDeliveryRecordRepository;
            _shopRepository = shopRepository;
        }

        public async Task<ApiResult<string>> AddShopDeliveryConfig(ShopDeliveryConfigDTO request)
        {
            var vo = _mapper.Map<ShopDeliveryConfigDO>(request);

            var shopInfo = await _shopRepository.GetAsync(request.ShopId);
            vo.ShopName = shopInfo.SimpleName;
            vo.IsDeleted = 0;
            vo.CreateTime = DateTime.Now;
            await _shopDeliveryConfigRepository.InsertAsync(vo);

            return new ApiResult<string>
            {
                Code = ResultCode.Success,
                Message = "添加成功!"
            };

        }

        public async Task<ApiResult<string>> AddShopDeliveryRecord(ShopDeliveryRecordDTO request)
        {
            var vo = _mapper.Map<ShopDeliveryRecordDO>(request);

            var shopInfo = await _shopRepository.GetAsync(request.ShopId);

            vo.IsDeleted = 0;
            vo.CreateTime = DateTime.Now;
            await _shopDeliveryRecordRepository.InsertAsync(vo);

            return new ApiResult<string>
            {
                Code = ResultCode.Success,
                Message = "添加成功!"
            };

        }

        public async Task<ApiResult<List<ShopDeliveryConfigDTO>>> GetShopDeliveryConfigs(ShopDeliveryConfigDTO request)
        {
            var res = await _shopDeliveryConfigRepository.GetListAsync(" where is_deleted=0 and shop_id =@shopId", new { shopId = request.ShopId });

            var vo = _mapper.Map<List<ShopDeliveryConfigDTO>>(res);
            return new ApiResult<List<ShopDeliveryConfigDTO>>
            {
                Code = ResultCode.Success,
                Data = vo
            };
        }

        public async Task<ApiResult<ShopDeliveryRecordDTO>> GetShopDeliveryRecord(ShopDeliveryRecordDTO request)
        {
            var res = await _shopDeliveryRecordRepository.GetListAsync(" where is_deleted=0 and order_no=@orderNo", new { orderNo = request.OrderNo });
            if (res.Any())
            {
                var vo = _mapper.Map<ShopDeliveryRecordDTO>(res.First());
                return new ApiResult<ShopDeliveryRecordDTO>
                {
                    Code = ResultCode.Success,
                    Data = vo
                };
            }
            return new ApiResult<ShopDeliveryRecordDTO>
            {
                Code = ResultCode.SUCCESS_NOT_EXIST,
                Data = new ShopDeliveryRecordDTO { }
            };
        }
    }
}
