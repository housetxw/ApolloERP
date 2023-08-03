using AutoMapper;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.Promotion;
using Ae.C.MiniApp.Api.Core.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionClient _promotionClient;
        private readonly IMapper _mapper;

        public PromotionService(IPromotionClient promotionClient, IMapper mapper)
        {
            _promotionClient = promotionClient;
            _mapper = mapper;
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request)
        {
            var result = await _promotionClient.GetProductPromotionDetail(new Client.Request.Promotion.ProductPromotionDetailClientRequest
            {
                ActivityId = request.ActivityId,
                Pid = request.Pid
            });

            if (result != null)
            {
                return _mapper.Map<PromotionDetailVo>(result);
            }

            return null;
        }
    }
}
