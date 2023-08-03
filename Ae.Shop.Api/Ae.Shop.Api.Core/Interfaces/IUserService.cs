using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 根据手机号查询客户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserDetailVo> GetUserDetailByUserTel(UserDetailByUserTelRequest request);
    }
}
