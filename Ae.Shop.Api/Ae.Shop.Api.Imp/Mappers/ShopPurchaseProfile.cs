using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopPurchaseProfile : Profile
    {
        public ShopPurchaseProfile()
        {
            CreateMap<VenderDTO, VenderDO>().ReverseMap();
            CreateMap<PurchaseOrderDO, PurchaseOrderDTO>().ReverseMap();
            CreateMap<PurchaseOrderProductDO, PurchaseOrderProductDTO>().ReverseMap();
            CreateMap<ProductSearchRequest, ProductSearchClientRequest>();
            CreateMap<ProductSearchRequest, ShopProductSearchClientRequest>();
            CreateMap<VenderDO, VenderVO>().ReverseMap();

            CreateMap<PurchaseOrderLogDO, PurchaseOrderLogDTO>();
            CreateMap<PurchaseOrderLogDTO, PurchaseOrderLogDO>();


            CreateMap<PurchaseMonthPayDO, PurchaseMonthPayDTO>();
            CreateMap<PurchaseMonthPayDTO, PurchaseMonthPayDO>();

            CreateMap<PagedEntity<PurchaseMonthPayDO>, ApiPagedResultData<PurchaseMonthPayDTO>>();

        }
    }
}
