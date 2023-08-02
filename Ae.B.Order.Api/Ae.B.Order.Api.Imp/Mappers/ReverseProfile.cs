using AutoMapper;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Imp.Mappers
{
    public class ReverseProfile : Profile
    {
        public ReverseProfile()
        {
            CreateMap<GetReverseReasonsRequest, GetReverseReasonConfigsRequest>();
            CreateMap<ReverseReasonConfigDTO, ReverseReasonVO>();

            CreateMap<CreateReverseOrderBaseRequest, CreateReverseOrderBaseClientRequest>();
            CreateMap<CancelOrderRequest, CreateReverseOrderForCancelClientRequest>();
            CreateMap<RefundApplyRequest, CreateReverseOrderForRefundClientRequest>();

            CreateMap<CreateReverseOrderBaseClientResponse, CreateReverseOrderBaseResponse>();
        }
    }
}
