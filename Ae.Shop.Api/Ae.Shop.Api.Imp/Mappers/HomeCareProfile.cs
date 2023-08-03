using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class HomeCareProfile : Profile
    {
        public HomeCareProfile()
        {
            CreateMap<HomeCareOrderDO, HomeCareOrderDTO>();
            CreateMap<HomeCareProductDO, HomeCareProductDTO>();
            CreateMap<HomeCareRecordDO, HomeCareRecordDTO>();

            CreateMap<HomeCareOrderDTO, HomeCareOrderDO>();
            CreateMap<HomeCareProductDTO, HomeCareProductDO>();
            CreateMap<HomeCareRecordDTO, HomeCareRecordDO>();
            CreateMap<EmployeeDO, EmployeeDTO>();
            CreateMap<PagedEntity<HomeCareRecordDO>, ApiPagedResultData<HomeCareRecordDTO>>();

            CreateMap<PagedEntity<HomeReturnRecordDO>, ApiPagedResultData<HomeReturnRecordDTO>>();
            CreateMap<HomeReturnRecordDO, HomeReturnRecordDTO>();
            CreateMap<HomeReturnRecordDTO, HomeReturnRecordDO>();

            CreateMap<HomeReturnProductDTO, HomeReturnProductDO>();
            CreateMap<HomeReturnProductDO, HomeReturnProductDTO>();


            CreateMap<TechClaimProductDTO, TechClaimProductDO>();
            CreateMap<TechClaimProductDO, TechClaimProductDTO>();


            CreateMap<ProductClaimRecordDTO, ProductClaimRecordDO>();
            CreateMap<ProductClaimRecordDO, ProductClaimRecordDTO>();



            CreateMap<PagedEntity<ProductClaimRecordDO>, ApiPagedResultData<ProductClaimRecordDTO>>();

        }
    }
}
