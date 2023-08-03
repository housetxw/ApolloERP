using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Core.Request;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Model;

namespace Ae.OrderComment.Service.Imp.Mappers
{
   public class ShopManageProfile:Profile
    {
        public ShopManageProfile()
        {
            CreateMap<ShopSimpleInfoClientDTO, ShopSimpleInfoDTO>();

            CreateMap<ApiPagedResultData<ShopSimpleInfoClientDTO>, ApiPagedResultData<ShopSimpleInfoDTO>>();
            CreateMap<ApiPagedResult<ShopSimpleInfoClientDTO>, ApiPagedResult<ShopSimpleInfoDTO>>();

        }
    }
}
