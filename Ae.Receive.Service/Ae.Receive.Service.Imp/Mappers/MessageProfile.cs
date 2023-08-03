using AutoMapper;
using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Imp.Mappers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Purchase, PurchaseInfo>().ReverseMap();
            CreateMap<UninstalledOrderProductDTO, ReserveProductDTO>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(dto => dto.TotalNumber));
            CreateMap<ModifyReserveRequest, UpdateReserveDTO>().ReverseMap();
            CreateMap<ReserveTrackDO, ReserveTrackDTO>().ReverseMap();
            CreateMap<ShopReserveTimeConfigDO, ReserveTimeConfigDTO>().ReverseMap();
            CreateMap<ShopReserveOrderDO, ShopReserveOrderDTO>();
            CreateMap<ShopReserveDO, ShopReserveDTO>();
        }
    }
}
