using AutoMapper;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Model.BaoYang;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;
using Ae.B.Product.Api.Core.Response;
using System.Collections.Generic;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class BaoYangConfigProfile : Profile
    {
        public BaoYangConfigProfile()
        {
            CreateMap<VehicleInfo, VehicleDetail>().ReverseMap();
            CreateMap<GetPartNameResponse, PartNameListResponse>().ReverseMap();
            CreateMap<BaoYangPartsDto, BaoYangPartsVo>().ReverseMap();
            CreateMap<BaoYangPackageRefDto, BaoYangPackageRefVo>().ReverseMap();
            CreateMap<BaoYangAccessoryDto, BaoYangAccessoryVo>().ReverseMap();
            CreateMap<ItemDto, Item>().ReverseMap();
            CreateMap<StandardDto, Standard>().ReverseMap();
            CreateMap<PartAccessoryDto, PartAccessoryVo>().ReverseMap();
            CreateMap<UpdateAccessory, UpdateAccessoryClientRequest>().ReverseMap();
            CreateMap<UpdateAccessory, UpdateAccessoryClientRequest>().ReverseMap();
            CreateMap<BatchEditAccessoryRequest, BatchEditAccessoryClientRequest>().ReverseMap();
            CreateMap<PartAttribute, PartAttributeClient>().ReverseMap();
            CreateMap<BaoYangPartProductDto, BaoYangPartProductVo>();

            CreateMap<EditPackageTypeConfigRequest, EditPackageTypeConfigClientRequest>();
            CreateMap<BaoYangPackageConfigDto, BaoYangPackageConfigVo>();

            CreateMap<AddPackageTypeConfigRequest, AddPackageTypeConfigClientRequest>();

            CreateMap<BaoYangTypeConfigDetailDto, BaoYangTypeConfigDetailVo>();
            CreateMap<EditBaoYangTypeConfigRequest, EditBaoYangTypeConfigClientRequest>();
            CreateMap<AddBaoYangTypeConfigRequest, AddBaoYangTypeConfigClientRequest>();

            CreateMap<InstallAddFeeConfigDto, InstallAddFeeConfigVo>().ForMember(dest => dest.CarMinPrice,
                opt => opt.MapFrom(src => src.CarMinPrice.ToString("#0.00"))).ForMember(dest => dest.CarMaxPrice,
                opt => opt.MapFrom(src => src.CarMaxPrice.ToString("#0.00"))).ForMember(dest => dest.OriginalPrice,
                opt => opt.MapFrom(src => src.OriginalPrice.ToString("#0.00"))).ForMember(dest => dest.AdditionalPrice,
                opt => opt.MapFrom(src => src.AdditionalPrice.ToString("#0.00")));
        }
    }
}
