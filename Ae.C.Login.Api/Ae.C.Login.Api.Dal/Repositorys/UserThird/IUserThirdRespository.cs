using ApolloErp.Data.DapperExtensions;
using Ae.C.Login.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.Login.Api.Dal.Repositorys.UserThird
{
    public interface IUserThirdRespository
    {
        Task<bool> UpdateOpenIdById(UserThirdDO req);
        Task<bool> InsertUserThird(UserThirdDO req);
    }
}
