using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model.Promotion;
using Ae.C.MiniApp.Api.Core.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class PromotionProfile: Profile
    {
        public PromotionProfile()
        {
            CreateMap<ProductPromotionDetailDto, PromotionDetailVo>();
        }
    }
}
