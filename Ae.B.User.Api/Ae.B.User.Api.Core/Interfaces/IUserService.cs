using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Core.Model.User;
using Ae.B.User.Api.Core.Model.Vehicle;
using Ae.B.User.Api.Core.Request.Address;
using Ae.B.User.Api.Core.Request.User;
using Ae.B.User.Api.Core.Request.Vehicle;
using Ae.B.User.Api.Core.Response.User;

namespace Ae.B.User.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<UserBaseInfoVo>, int>> GetUserList(UserListRequest request);

        /// <summary>
        /// 用户基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserInfoResponse> GetUserInfo(GetUserInfoRequest request);

        /// <summary>
        /// 手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserInfoResponse> GetUserInfoByUserTel(UserInfoByUserTelRequest request);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> CreateUser(CreateUserRequest request);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserInfo(EditUserInfoRequest request);

        /// <summary>
        /// 搜索用户 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserInfoVo>> SearchUserInfo(SearchUserInfoRequest request);

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddUserCar(AddUserCarRequest request);

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleVO> GetUserVehicleByCarId(UserVehicleByCarIdRequest request);

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteUserCar(DeleteUserCarRequest request);

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserCar(EditUserCarRequest request);

        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserAddressVo>> GetAllUserAddress(AllUserAddressRequest request);

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddUserAddress(AddUserAddressRequest request);

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUserAddress(EditUserAddressRequest request);

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAddress(DeleteUserAddressRequest request);
    }
}
