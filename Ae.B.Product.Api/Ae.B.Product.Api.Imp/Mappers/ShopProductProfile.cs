using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class ShopProductProfile : Profile
    {
        public ShopProductProfile()
        {
            CreateMap<ShopProductVo, BaseShopProductDto>().ReverseMap();
            CreateMap<ShopProductVo, ShopProductDto>().ReverseMap();
            CreateMap<ShopProductEditRequest, ShopProductEditClientRequest>();
            CreateMap<ShopProductSearchRequest, ShopProductSearchClientRequest>();
            CreateMap<ShopListRequest, ShopListClientRequest>();
            CreateMap<ShopSimpleInfoClientDto, ShopSimpleInfoVo>();
            CreateMap<ApiPagedResultData<ShopSimpleInfoClientDto>, ApiPagedResultData<ShopSimpleInfoVo>>();
            CreateMap<ApiPagedResult<ShopSimpleInfoClientDto>, ApiPagedResult<ShopSimpleInfoVo>>();
            CreateMap<ShopServiceTypeDto, ShopServiceTypeVo>();
            CreateMap<ShopProductImportVo, ShopProductImportDto>();
            CreateMap<ShopProductAuditRequest, ShopProductAuditClientRequest>();
        }
    }
}
