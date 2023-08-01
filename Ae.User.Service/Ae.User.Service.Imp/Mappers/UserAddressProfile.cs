using AutoMapper;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Imp.Mappers
{
   public class UserAddressProfile: Profile
    {
        public UserAddressProfile() {
            CreateMap<CreateUserAddressRequest, UserAddressDO>();

            CreateMap<UserAddressDO, UserAddressVO>();

            CreateMap<UserAddressTagVO, UserAddressTagDO>();

            CreateMap< UserAddressTagDO, UserAddressTagVO>();

            CreateMap<UpdateUserAddressRequest, UserAddressDO>();

            CreateMap<UserAddressDO, UserAddressDTO>();
            
        }
    }
}
