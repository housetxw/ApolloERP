using AutoMapper;
using Ae.C.Login.Api.Core.Model;
using Ae.C.Login.Api.Dal.Model;

namespace Ae.C.Login.Api.Imp.Mappers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<UserInfoDO, UserInfoVO>().ReverseMap();
            CreateMap<UserThirdDO, UserInfoVO>().ReverseMap();
        }
    }
}
