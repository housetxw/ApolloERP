using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Imp.Mappers
{
    public class AccountAuthorityProfile : Profile
    {
        public AccountAuthorityProfile()
        {
            CreateMap<Purchase, PurchaseInfo>().ReverseMap();

            #region Account

            #region 创建账号，更新密码和登录操作相关方法

            CreateMap<AccountCreateEntityDTO, AccountDO>().ReverseMap();

            CreateMap<AccountDO, AccountPageResDTO>().ReverseMap();

            CreateMap<AccountUpdatePasswordEntityDTO, AccountDO>().ReverseMap();


            #endregion 创建账号，更新密码和登录操作相关方法

            #region Service相关的方法

            CreateMap<AccountDO, AccountKeyInfoListResDTO>().ReverseMap();
            CreateMap<AccountDO, AccountKeyInfoEntityResDTO>().ReverseMap();
            CreateMap<PagedEntity<AccountPageDTO>, ApiPagedResultData<AccountPageDTO>>().ReverseMap();

            CreateMap<AccountResetPasswordReqByIdDTO, AccountResetPasswordReqByIdIntlDTO>().ReverseMap();


            #endregion Service相关的方法

            #endregion Account

            #region Application

            CreateMap<ApplicationDO, ApplicationDTO>().ReverseMap();
            //CreateMap<ApplicationDO, AppResDTO>()
            //    //.ForMember(dest => dest.AppId, opt => opt.MapFrom(src => src.Id))
            //    //.ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.Name))
            //    .ReverseMap();
            CreateMap<ApplicationDO, AppResDTO>().ReverseMap();

            #endregion Application

            #region EmployeeRole

            #region ！！！授权Model！！！

            //APP
            CreateMap<AuthorizationDO, AuthorizationAuthorityAPPResDTO>()
                .ForMember(dest => dest.RouteKey, opt => opt.MapFrom(src => src.AuthorityName))
                .ForMember(dest => dest.RouteName, opt => opt.MapFrom(src => src.AuthorityName))
                .ForMember(dest => dest.RouteValue, opt => opt.MapFrom(src => src.Route))
                .ForMember(dest => dest.RouteImg, opt => opt.MapFrom(src => src.MenuIcon))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
                    src.AuthorityType == AuthorityType.Page ? "Menu" : src.AuthorityType.ToString()));

            CreateMap<AuthorizationDO, AuthorizationResDTO>().ReverseMap();

            #endregion ！！！授权Model！！！

            CreateMap<EmployeeRoleListDO, EmployeeRoleListDTO>().ReverseMap();

            CreateMap<EmployeeRoleEntityDTO, EmployeeRoleDO>().ReverseMap();
            CreateMap<EmployeeRoleSaveReqDTO, EmployeeRoleSaveReqDO>().ReverseMap();

            #endregion EmployeeRole

            #region Role

            CreateMap<EmployeeDefaultRoleReqDTO, EmployeeRoleSaveReqWithRoleIdDTO>().ReverseMap();

            CreateMap<RoleDO, RoleDTO>().ReverseMap();
            CreateMap<RoleDO, RolePageResDTO>().ReverseMap();

            CreateMap<RoleDTO, RoleDO>().ReverseMap();

            CreateMap<RoleAuthorityDTO, RoleAuthorityDO>().ReverseMap();
            CreateMap<RoleAuthorityReqDTO, RoleAuthorityReqDO>().ReverseMap();
            CreateMap<RoleAuthorityDO, RoleAuthorityDTO>().ReverseMap();
            CreateMap<RoleAuthorityListReqByRoleIdDTO, RoleAuthorityListReqByRoleIdDO>().ReverseMap();


            #endregion Role

            #region Authority

            CreateMap<AuthorityDTO, AuthorityDO>().ReverseMap();
            CreateMap<AuthorityDO, AuthorityDTO>().ReverseMap();
            CreateMap<AuthorityDO, AuthorityResDTO>().ReverseMap();
            CreateMap<AuthorityPageDO, AuthorityPageResDTO>().ReverseMap();
            CreateMap<AuthorityPageDO, AuthorityDTO>().ReverseMap();

            #endregion Authority

        }
    }
}
