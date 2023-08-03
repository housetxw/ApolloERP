using AutoMapper;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Pay;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class PayProfile: Profile
    {
        public PayProfile()
        {
            CreateMap<CreateWxPrePayOrderRequest, CreateWxPrePayOrderForMiniAppRequest>();
            CreateMap<CreateWxPrePayOrderForMiniAppResponse, CreateWxPrePayOrderResponse>();

            CreateMap<ConfirmPayRequest, ConfirmPayClientRequest>();
            CreateMap<ConfirmPayClientResponse, ConfirmPayResponse>();
        }
    }
}
