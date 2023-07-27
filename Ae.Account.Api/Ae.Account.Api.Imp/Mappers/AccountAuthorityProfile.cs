using System.Collections.Generic;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Core.CommonModel;

namespace Ae.Account.Api.Imp.Mappers
{
    public class AccountAuthorityProfile : Profile
    {
        public AccountAuthorityProfile()
        {
            //CreateMap<Purchase, PurchaseInfo>().ReverseMap();

            // ---------------------------------- Account --------------------------------------
            CreateMap<EmployeePageResVO, AccountPageResVO>().ReverseMap();
            CreateMap<ApiPagedResultData<AccountPageDTO>, ApiPagedResultData<AccountPageResVO>>().ReverseMap();
            CreateMap<ApiPagedResultData<EmployeePageResVO>, ApiPagedResultData<AccountPageResVO>>().ReverseMap();
            CreateMap<AccountUnlockReqByIdVO, AccountUnlockReqByIdDTO>().ReverseMap();
            CreateMap<AccountLockReqByIdVO, AccountLockReqByIdDTO>().ReverseMap();
            CreateMap<AccountBatchDeleteReqByIdVO, AccountBatchDeleteReqByIdDTO>().ReverseMap();
            CreateMap<AccountResetPasswordReqByIdVO, AccountResetPasswordReqByIdDTO>().ReverseMap();
            CreateMap<AccountResetPasswordResByIdDTO, AccountResetPasswordResByIdVO>().ReverseMap();
            CreateMap<AccountUpdatePasswordEntityVO, AccountUpdatePasswordEntityDTO>();


            // ---------------------------------- Application --------------------------------------
            CreateMap<ApplicationVO, ApplicationDTO>().ReverseMap();

            CreateMap<AppReqVO, AppReqDTO>().ReverseMap();
            CreateMap<AppResDTO, AppResVO>().ReverseMap();
            CreateMap<ApplicationVO, AppReqVO>().ReverseMap();

            CreateMap<AppListReqVO, AppListReqDTO>().ReverseMap();
            CreateMap<AppListResDTO, AppListResVO>().ReverseMap();

            CreateMap<AppResDTO, AppSelectResVO>().ReverseMap();


            // ---------------------------------- EmployeeRole --------------------------------------
            #region ！！！授权Model！！！

            CreateMap<AuthorizationReqVO, AuthorizationReqDTO>().ReverseMap();

            CreateMap<AuthorizationResDTO, AuthorizationWebResDTO>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Route))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AuthorityName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.AuthorityType));
            CreateMap<AuthorizationWebResDTO, AuthorizationWebResVO>().ReverseMap();


            #endregion ！！！授权Model！！！

            CreateMap<EmployeeListReqVO, EmployeeListReqDTO>().ReverseMap();
            CreateMap<EmployeeResDTO, EmployeeResVO>().ReverseMap();
            CreateMap<EmployeePageReqVO, EmployeePageReqDTO>().ReverseMap();
            CreateMap<EmployeePageDTO, EmployeePageResVO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.EmployeeType));

            CreateMap<EmployeeRoleListReqVO, EmployeeRoleListReqDTO>().ReverseMap();
            CreateMap<EmployeeRoleListDTO, EmployeeRoleLisVO>().ReverseMap();

            CreateMap<EmployeeRoleDialogListReqVO, RoleListReqDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId));
            CreateMap<RoleDTO, EmployeeRoleLisVO>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => src.Type));


            // ---------------------------------- Role --------------------------------------
            CreateMap<RoleResVO, RoleResDTO>().ReverseMap();

            CreateMap<RoleListReqVO, RoleListReqDTO>().ReverseMap();
            CreateMap<RolePageResDTO, RolePageResVO>().ReverseMap();
            CreateMap<RolePageListResDTO, RolePageListResVO>().ReverseMap();

            CreateMap<RoleVO, RoleDTO>().ReverseMap();
            CreateMap<RoleVO, RoleListInternalReqDTO>().ReverseMap();
            CreateMap<RoleDTO, RoleSelectResVO>().ReverseMap();

            CreateMap<RoleAuthorityReqVO, RoleAuthorityReqDTO>().ReverseMap();
            CreateMap<RoleAuthorityVO, RoleAuthorityDTO>().ReverseMap();
            CreateMap<RoleAuthorityListReqByRoleIdVO, RoleAuthorityListReqByRoleIdDTO>().ReverseMap();

            CreateMap<UnitDTO, OrganizationSel>().ReverseMap();


            // ---------------------------------- Authority --------------------------------------
            CreateMap<AuthorityVO, AuthorityDTO>().ReverseMap();
            CreateMap<AuthorityVO, AuthorityListInternalReqDTO>().ReverseMap();
            CreateMap<AuthorityDTO, AuthorityVO>().ReverseMap();
            CreateMap<AuthorityDTO, AuthoritySelectResVO>().ReverseMap();

            CreateMap<AuthorityListReqVO, AuthorityListReqDTO>().ReverseMap();
            CreateMap<AuthorityPageResDTO, AuthorityPageResVO>().ReverseMap();
            CreateMap<AuthorityPageListResDTO, AuthorityPageListResVO>().ReverseMap();

            CreateMap<AuthorityPageResDTO, AuthorityTreeVO>().ReverseMap();
            CreateMap<AuthorityTreeVO, ElementAuthorityTree>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name));
            //.ReverseMap()


        }
    }
}
