using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ReceiveProfile : Profile
    {
        public ReceiveProfile()
        {
            CreateMap<CanReserveOrderDTO, CanReserveOrderVO>().ReverseMap();
            CreateMap<ReserveProductDTO, ReserveProductVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<CanReserveOrderListRequest, CanReserveOrderClientRequest>().ReverseMap();
            CreateMap<CanReserveOrderClientResponse, CanReserveOrderListResponse>().ReverseMap();
            CreateMap<JudgeMyReserveRequest, JudgeMyReserveClientRequest>().ReverseMap();
            CreateMap<JudgeMyReserveClientResponse, JudgeMyReserveResponse>().ReverseMap();
            CreateMap<ReserveInitialRequest, ReserveInitialClientRequest>().ReverseMap();
            CreateMap<ReserveInitialClientResponse, ReserveInitialResponse>().ReverseMap();
            CreateMap<MyCarInfoDTO, MyCarInfoVO>()
                .ForMember(dest => dest.CarLogo, opt => opt.MapFrom(src => src.CarLogo.AddImageDomain()));//图片绝对路径
            CreateMap<ReserveDateDTO, ReserveDateVO>().ReverseMap();
            CreateMap<AddReserveRequest, AddReserveClientRequest>().ReverseMap();
            CreateMap<GetReserveInfoRequest, GetReserveInfoClientRequest>().ReverseMap();
            CreateMap<GetReserveInfoClientResponse, GetReserveInfoResponse>().ReverseMap();
            CreateMap<ReserveShopInfoDTO, ReserveShopInfoVO>().ReverseMap();
            CreateMap<ModifyReserveRequest, ModifyReserveClientRequest>().ReverseMap();
            CreateMap<CanReserveTimeRequest, CanReserveTimeClientRequest>().ReverseMap();
            CreateMap<ReservedListRequest, ReservedListClientRequest>().ReverseMap();
            CreateMap<ReservedInfoDTO, ReservedListResponse>().ReverseMap();
            CreateMap<ReserveTimeDTO, ReserveTimeVO>().ReverseMap();
            CreateMap<ShopServiceDTO, ShopServiceVO>().ReverseMap();
            CreateMap<ApiPagedResultData<CanReserveOrderDTO>, ApiPagedResultData<CanReserveOrderVO>>().ReverseMap();
            CreateMap<AddReserveNewYearRequest, AddReserveV2ClientRequest>().ReverseMap();
            CreateMap<ImgDTO, ImgClientDTO>().ReverseMap();
            CreateMap<RebookReserveProductDTO, ReserveRebookProductVO>().ReverseMap();
            CreateMap<CancelReserveRequest, CancelReserveClientRequest>().ReverseMap();
            CreateMap<ReservedListV2DTO, ReservedListV2Response>().ReverseMap();
            CreateMap<ApiPagedResultData<ReservedListV2DTO>, ApiPagedResultData<ReservedListV2Response>>().ReverseMap();
            CreateMap<CanReserveDateClientResponse, CanReserveDateResponse>().ReverseMap();
            CreateMap<GetRebookReserveClientResponse, GetRebookReserveResponse>().ReverseMap(); 
            CreateMap<ReserveRebookProductDTO, ReserveRebookProductVO>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon.AddImageDomain()));//图片绝对路径
            CreateMap<ReserveProductVO, RebookReserveProductDTO>().ReverseMap();
            CreateMap<GetReserveSurplusClientResponse, GetReserveSurplusResponse>().ReverseMap();
            CreateMap<GetReserveInfoV3ClientResponse, GetReserveInfoV3Response>().ReverseMap();
            CreateMap<ReserveSurplusContentDTO, ReserveSurplusContentVO>().ReverseMap();
            CreateMap<AddReserveV3Request, AddShopReserveV3ClientRequest>().ReverseMap();
            CreateMap<CanReserveTimeV3Request, CanReserveTimeV3ClientRequest>().ReverseMap();
            CreateMap<ModifyReserveTimeRequest, ModifyReserveTimeClientRequest>().ReverseMap();
            CreateMap<GetReserveInfoV3Request, GetReserveInfoV3ClientRequest>().ReverseMap();
        }
    }
}
