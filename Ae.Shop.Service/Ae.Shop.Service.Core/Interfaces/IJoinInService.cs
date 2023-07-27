using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.BOSS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IJoinInService
    {
        /// <summary>
        /// 加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> JoinInAsync(JoinInRequest request);

        /// <summary>
        /// 加盟列表
        /// </summary>
        /// <param name="request"></param>
        Task<ApiPagedResultData<JoinInVO>> GetJoinInList(JoinInListRequest request);

        Task<bool> ShopJoinAsync(ShopJoinRequest request);
        Task<ApiPagedResultData<ShopJoinDTO>> GetShopJointListAsync(GetShopJoinListRequest request);
        Task<bool> UpdateShopJoinAsync(ShopJoinRequest request);
        Task<ShopJoinDTO> GetShopJoinByIdAsync(GetShopJoinByIdRequest request);
        Task<bool> CheckShopJoinAsync(CheckShopJoinRequest request);
    }
}
