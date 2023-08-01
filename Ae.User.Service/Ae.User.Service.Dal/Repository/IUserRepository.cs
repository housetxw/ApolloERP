using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Model.Request;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserRepository : IRepository<UserDO>
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<UserDO>, int>> GetUserListAsync(GetUserListRequest request);

        /// <summary>
        /// 批量查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<UserDO>> GetUsersByUserIds(List<string> userIds);

        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDO> GetUserInfo(string userId);

        Task<bool> EditUserInfo(UserDO userDo);

        /// <summary>
        /// 根据手机号查用户
        /// </summary>
        /// <param name="userTel"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserDO> GetUserInfoByUserTel(string userTel, bool readOnly = true);
        Task<List<UserDO>> GetCommonUserInfo(List<string> userIds, string employeeId, string searchContent, string userName, string userTel);

        Task<PagedEntity<UserDO>> SearchUserInfo(SearchUserInfoCondition request);

        /// <summary>
        /// 获取用户分享获得的新客列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<PagedEntity<UserDO>> GetReferrerCustomerList(ReferrerCustomerListRequest req);

        /// <summary>
        /// 获取用户分享获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<int> GetReferrerCustomerNum(ReferrerCustomerRequest req);

        /// <summary>
        /// 获取门店当月获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ShopReferrerCustomerResDTO> GetShopNewCustomerInfo(ShopReferrerCustomerReqDTO req);

        Task<PagedEntity<EmployeeReferrerCustomerResDTO>> GetEmployeeReferrerNewCustomerPage(EmployeeReferrerCustomerPageReqDTO req);

        Task<UserDO> GetUserByRelationShopId(long shopId);
    }
}
