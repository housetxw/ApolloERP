using AutoMapper;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class ReverseProfile : Profile
    {
        public ReverseProfile()
        {
            CreateMap<GetReverseReasonsRequest, GetReverseReasonConfigsClientRequest>();
            CreateMap<CreateReverseOrderBaseRequest, CreateReverseOrderBaseClientRequest>();
            CreateMap<CancelOrderRequest, CreateReverseOrderForCancelClientRequest>();
            CreateMap<ReverseReasonConfigDTO, ReverseReasonVO>();
            CreateMap<CreateReverseOrderBaseClientResponse, CreateReverseOrderBaseResponse>();
        }
    }
}
