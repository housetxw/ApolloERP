using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;
using Ae.B.Login.Api.Dal.Model;

namespace Ae.B.Login.Api.Imp.Mappers
{
    public class LoginAuthProfile : Profile
    {
        public LoginAuthProfile()
        {
            // ---------------------------------- ShopManage --------------------------------------
            CreateMap<OrganizationPageListReqVO, EmployeePageForLoginListReqDTO>().ReverseMap();
            CreateMap<OrganizationPageListByTokenReqVO, EmployeePageForLoginListByTokenReqDTO>().ReverseMap();
            CreateMap<ApiPagedResult<OrganizationPageListReqVO>, ApiPagedResult<EmployeePageForLoginListReqDTO>>().ReverseMap();

            CreateMap<EmployeeResDTO, OrganizationVO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name));

            CreateMap<ShopTypeDTO, ShopTypeVO>().ReverseMap();
            CreateMap<EmployeePageDTO, OrganizationVO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name));

            // ---------------------------------- Authentication --------------------------------------
            CreateMap<SysLoginLogVO, SysLoginLogDO>().ReverseMap();

            CreateMap<LoginVO, AccountEntityReqByNameVO>().ReverseMap();

            CreateMap<LoginMessageVO, UpdatePasswordVO>().ReverseMap();

            CreateMap<EmployeeInfoByAuthCodeReq, EmployeeInfoVO>().ReverseMap();
            CreateMap<EmployeeInfoByTokenReq, EmployeeInfoVO>().ReverseMap();

            CreateMap<AccountKeyInfoWithPwdEntityResVO, AccountKeyInfoApiResVO>().ReverseMap();
            CreateMap<AccountDO, AccountKeyInfoEntityResVO>().ReverseMap();
            

        }
    }
}
