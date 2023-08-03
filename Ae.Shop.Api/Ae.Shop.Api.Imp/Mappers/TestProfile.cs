using AutoMapper;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<GetShopRequest, GetShopClientRequest>();
            CreateMap<ShopDTO, ShopVO>().ReverseMap();
        }
    }
}
