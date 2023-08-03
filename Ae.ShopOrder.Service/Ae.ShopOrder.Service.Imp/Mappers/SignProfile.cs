using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Response.Sign;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Imp.Mappers
{
    public class SignProfile:Profile
    {
        public SignProfile()
        {
            CreateMap<PagedEntity<SignDetailDO>, TodaySignPackageApiPagedResult<GetTodaySignPackageResponse>>();
            CreateMap<SignDetailDO, GetTodaySignPackageResponse>();

            CreateMap<TodayReceiveDO, GetTodayReceiveVO>();
        }
    }
}
