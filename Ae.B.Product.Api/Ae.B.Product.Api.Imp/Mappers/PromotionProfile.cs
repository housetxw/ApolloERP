using AutoMapper;
using Ae.B.Product.Api.Client.Model.Promotion;
using Ae.B.Product.Api.Client.Request.Promotion;
using Ae.B.Product.Api.Core.Model.Promotion;
using Ae.B.Product.Api.Core.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            CreateMap<SearchPromotionActivityRequest, SearchPromotionActivityClientRequest>();
            CreateMap<PromotionActivityListDto, PromotionActivityListVo>();
            CreateMap<PromotionActivityDetailDto, PromotionDetailVo>();
            CreateMap<PromotionProductDto, PromotionProductVo>();
        }
    }
}
