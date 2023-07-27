using System.Collections.Generic;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Model.Vehicle;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Model.OpeningGuide;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Response.ShopServer;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Imp.Mappers
{
    public class ShopManageProfile : Profile
    {
        public ShopManageProfile()
        {
            CreateMap<Purchase, PurchaseInfo>().ReverseMap();

            // ---------------------------------- Employee --------------------------------------
            CreateMap<EmployeeDO, EmployeeDTO>().ReverseMap();
            CreateMap<EmployeeDO, EmployeeResDTO>().ReverseMap();
            CreateMap<EmployeeDO, EmployeeInfoDTO>().ReverseMap();
            CreateMap<EmployeeListDTO, EmployeeResDTO>().ReverseMap();
            CreateMap<EmployeeCustomDO, EmployeeInfoDTO>().ReverseMap();

            CreateMap<EmployeeDO, EmployeeSimpleInfoResponse>().ReverseMap();
            CreateMap<EmployeeCustomDO, EmployeeSimpleInfoResponse>().ReverseMap();

            CreateMap<EmployeeCustomDO, TechnicianPageDTO>().ReverseMap();
            CreateMap<PagedEntity<EmployeeCustomDO>, ApiPagedResultData<TechnicianPageDTO>>().ReverseMap();

            CreateMap<RoleListReqVO, RoleListReqDTO>();
            CreateMap<RoleDTO, RoleListResVO>();
            CreateMap<ApiResult<List<RoleDTO>>, ApiResult<List<RoleListResVO>>>();

            CreateMap<RoleListReqVO, RoleListReqsDTO>();
            CreateMap<RoleDTO, RoleVO>();
            CreateMap<OrgRangeRolesDTO, OrgRangeRolesVO>();

            // ---------------------------------- Organization --------------------------------------
            CreateMap<UnitDTO, UnitSelDTO>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name));

            // ---------------------------------- Department --------------------------------------
            CreateMap<DepartmentDO, DepartmentDTO>();
            CreateMap<DepartmentDO, DepartmentSelDTO>()
                .ForMember(dest => dest.Value, src => src.MapFrom(f => f.Id))
                .ForMember(dest => dest.Label, src => src.MapFrom(f => f.Name));
            CreateMap<DepartmentDO, DepartmentTreeDTO>();
            CreateMap<DepartmentTreeDTO, ElementDepartmentTree>()
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name));


            // ---------------------------------- Job --------------------------------------
            CreateMap<JobDO, JobListResDTO>().ReverseMap();
            CreateMap<JobDO, JobSelDTO>()
                .ForMember(dest => dest.Value, src => src.MapFrom(f => f.Id))
                .ForMember(dest => dest.Label, src => src.MapFrom(f => f.Name));
            CreateMap<WorkKindDO, JobSelDTO>()
                .ForMember(dest => dest.Value, src => src.MapFrom(f => f.Id))
                .ForMember(dest => dest.Label, src => src.MapFrom(f => f.Name));
            CreateMap<EmployeeLevelListResDTO, JobSelDTO>()
                .ForMember(dest => dest.Value, src => src.MapFrom(f => f.Id))
                .ForMember(dest => dest.Label, src => src.MapFrom(f => f.Name));

            // ---------------------------------- WorkKind --------------------------------------
            CreateMap<WorkKindDO, WorkKindListResDTO>().ReverseMap();


            CreateMap<ShopTagDTO, ShopTagDO>();
            CreateMap<ShopTagDO, ShopTagDTO>();

            CreateMap<ShopServiceTypeNewDTO, ShopServiceTypeDO>();

            CreateMap<ShopServiceTypeDO, ShopServiceTypeNewDTO>();
            CreateMap<PagedEntity<ShopServiceTypeDO>, ApiPagedResultData<ShopServiceTypeNewDTO>>();

            CreateMap<AddShopRequest, ShopDO>().ReverseMap();
            CreateMap<ShopConfigDTO, ShopConfigDO>().ReverseMap();
            CreateMap<ShopConfigDO, ShopConfigDTO>().ReverseMap();
            CreateMap<ShopConfigDTO, ModifyShopConfigInfoRequest>();
            CreateMap<ModifyShopConfigInfoRequest, ShopConfigDO>();
            CreateMap<ModifyShopConfigExpenseRequest, ShopConfigDO>();


            CreateMap<ShopDO, ShopDTO>().ReverseMap();
            CreateMap<ShopDTO, ShopDO>().ReverseMap();
            CreateMap<NearShopInfoDO, NearShopInfoDTO>().ReverseMap();
            CreateMap<NearShopInfoDO, GetShopDetailResponse>().ReverseMap();
            CreateMap<ShopDO, GetShopSimpleInfoResponse>().ReverseMap();
            CreateMap<ShopSimpleInfoDO, ShopSimpleInfoDTO>().ReverseMap();
            CreateMap<ShopSimpleInfoDO, GetShopSimpleInfoResponse>().ReverseMap();
            CreateMap<JoinInRequest, JoinInDO>().ReverseMap();
            CreateMap<CompanySimpleInfoDO, CompanySimpleInfoDTO>().ReverseMap();
            CreateMap<GetShopListForBOSSRequest, GetShopListModel>().ReverseMap();
            CreateMap<ShopSimpleInfoModel, ShopSimpleInfoForBOSSDTO>().ReverseMap();
            CreateMap<RegionChinaDTO, RegionDTO>().ReverseMap();
            CreateMap<CompanySimpleInfoDO, CompanyInfoDTO>().ReverseMap();

            CreateMap<ShopJoinRequest, ShopJoinDO>().ReverseMap();
            CreateMap<ShopJoinDO, ShopJoinDTO>().ReverseMap();
            CreateMap<BaseServiceDO, BaseServiceDTO>().ReverseMap();
            CreateMap<BaseServiceDTO, BaseServiceDO>().ReverseMap();
            CreateMap<ShopServiceInfoVO, ShopServiceDTO>().ReverseMap();
            CreateMap<ShopBankCardInfoDO, ShopBankCardDTO>().ReverseMap();
            CreateMap<AdvertisementDO, AdvertisementDTO>().ReverseMap();
            CreateMap<NoticeDO, NoticeDTO>().ReverseMap();
            CreateMap<ShopServiceInfoVO, ShopServiceInfoDTO>().ReverseMap();
            CreateMap<ShopScoreDTO, ShopScoreDO>().ReverseMap();
            CreateMap<RegionChinaDTO, RegionChinaVO>().ReverseMap();
            CreateMap<FaqDO, FaqDTO>().ReverseMap();
            CreateMap<FaqDTO, FaqDO>().ReverseMap();
            CreateMap<ModifyFAQRequest, FaqDO>().ReverseMap();
            CreateMap<FaqInfoDO, FaqDTO>().ReverseMap();
            CreateMap<ApiPagedResultData<FaqInfoDO>, ApiPagedResultData<FaqDTO>>().ReverseMap();
            CreateMap<FaqChannelDO, FaqChannelDTO>().ReverseMap();
            CreateMap<FaqCategoryDO, FaqCategoryDTO>().ReverseMap();
            CreateMap<ShopCategoryServiceDO, ShopCategoryServiceDTO>().ReverseMap();
            CreateMap<ShopEmployeeDO, ShopEmployeeDTO>().ReverseMap();
            CreateMap<FaqInfoDO, FaqDTO>().ReverseMap();
            CreateMap<PagedEntity<ShopEmployeeDO>, ApiPagedResultData<ShopEmployeeDTO>>().ReverseMap();
            CreateMap<ModifyShopBankAccountRequest, ShopBankCardDO>().ReverseMap();

            CreateMap<GetVehicleBrandDTO, GetVehicleBrandVo>();
            CreateMap<ShopServiceBrandDO, GetVehicleBrandVo>()
                .ForMember(dest => dest.BrandSuffix, opt => opt.MapFrom(o => o.Brand));
            CreateMap<ShopServiceBrandDTO, ShopServiceBrandDO>();

            CreateMap<ShopServiceBrandDO, ShopServiceBrandDTO>();

            CreateMap<AddShopRegisterRequest, ShopDO>()
                .ForMember(dest => dest.Nature, opt => opt.MapFrom(src => (sbyte)src.Nature));

            CreateMap<ShopDO, ShopBaseInfoVO>();
            CreateMap<ShopDO, ShopAddressInfoVO>();

            // ---------------------------------- Bank --------------------------------------
            CreateMap<ShopBankCardDO, ShopBankCardDTO>().ReverseMap();
            CreateMap<ShopBankCardDTO, ShopBankCardDO>().ReverseMap();
            CreateMap<ShopBankCardInfoDO, ShopBankInfoVO>().ReverseMap();
            CreateMap<BankInformationDO, BankInformationDTO>();

            #region 门店车辆

            CreateMap<ShopCarDTO, ShopCarDO>().ReverseMap();
            CreateMap<ShopCarDO, ShopCarDTO>().ReverseMap();
            CreateMap<ShopCarDO, SimpleShopCarDTO>().ReverseMap();
            CreateMap<ShopCarRecordDO, ShopCarRecordDTO>().ReverseMap();
            CreateMap<ShopCarRecordDTO, ShopCarRecordDO>().ReverseMap();
            CreateMap<AddTechTripRecordRequest, TechTripRecordDO>().ReverseMap();
            CreateMap<TechTripRecordDO, TechTripRecordDTO>().ReverseMap();
            CreateMap<AddTechTripReturnRequest, TechTripRecordDO>().ReverseMap();
            CreateMap<TechTripRecordModel, TechTripRecordInfo>().ReverseMap();
            CreateMap<TechTripRecordModel, GetTripRecordListResponse>().ReverseMap();


            #endregion


            CreateMap<ShopDO, GetNearCityShopInfoResponse>();

            CreateMap<ShopDeliveryConfigDO, ShopDeliveryConfigDTO>();
            CreateMap<ShopDeliveryConfigDTO, ShopDeliveryConfigDO>();

            CreateMap<ShopDeliveryRecordDTO, ShopDeliveryRecordDO>();
            CreateMap<ShopDeliveryRecordDO, ShopDeliveryRecordDTO>();

            CreateMap<ShopGrouponProductDO, ShopGrouponProductDTO>();

            CreateMap<EmployeeGroupDO, EmployeeGroupDTO>().ReverseMap();
        }
    }
}
