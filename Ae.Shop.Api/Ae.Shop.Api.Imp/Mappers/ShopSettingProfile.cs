using AutoMapper;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopSettingProfile : Profile
    {
        public ShopSettingProfile()
        {
            CreateMap<ShopAssetDO, ShopAssetVO>().ReverseMap();
            CreateMap<ShopAssetTypeDO, ShopAssetTypeVO>().
                ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Value)).
                ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Label));
            CreateMap<ShopAssetTypeDO, ShopAssetTypeOptionsResponse>().
                ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Value)).
                ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Label));
            CreateMap<ShopAssetMaintainDO, ShopAssetMaintainVO>().ReverseMap();
            CreateMap<ShopAssetDiscardDO, ShopAssetDiscardVO>().ReverseMap();
        }
    }
}
