using AutoMapper;
using Ae.User.Service.Core.Interfaces;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Core.Response;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ae.User.Service.Dal.Model.Request;
using ApolloErp.Web.WebApi;
using System.Text.RegularExpressions;
using Ae.User.Service.Common.Constant;
using Ae.User.Service.Common.Exceptions;

namespace Ae.User.Service.Imp.Services
{
   public class UserAddressService: IUserAddressService
    {
        private readonly IMapper _mapper;
        private readonly IUserAddressRepository userAddressRepository;

        public UserAddressService(IMapper mapper, IUserAddressRepository userAddressRepository) {
            this.userAddressRepository = userAddressRepository;
            this._mapper = mapper;
        }

        /// <summary>
        /// 创建用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> CreateUserAddress(CreateUserAddressRequest request)
        {
            int res = 0;

            //做地址验证 是否重复，
            var repeatFlag = await userAddressRepository.CheckUserAddressRepeat(new UserAddressDO
            {
                UserId = new Guid(request.UserId),
                ProvinceId = request.ProvinceId,
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                AddressLine = request.AddressLine,
                UserName = request.UserName,
                MobileNumber = request.MobileNumber
            });

            if (repeatFlag > 0)
            {
                throw new CustomException("用户地址添加重复!");
            }

            var vo = _mapper.Map<UserAddressDO>(request);
            res = await CreateUserAddressRes(vo);
            if (res > 0 && request.DefaultAddress)
            {
                await userAddressRepository.SetUserDefaultAddressAsync(request.UserId, res,
                    request.CreateBy);
            }

            return res;
        }

        public async Task<int> CreateUserAddressRes(UserAddressDO vo)
        {
            var result = await userAddressRepository.CreateUserAddress(vo);

            return result;
        }

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request)
        {
            var res =new ApiResult<string>();

            var repeatFlag = await userAddressRepository.CheckUserAddressTagRepeat(new UserAddressTagDO
            {
                UserId =  new Guid(request.UserId),
                TagName = request.TagName
            });

            if (repeatFlag > 0)
            {
                res.Code = ResultCode.Failed;
                res.Message = "用户地址标签已添加!";
            }
            else {
                var vo = new UserAddressTagDO
                {
                    IsDeleted = 0,
                    CreateBy = request.CreateBy,
                    CreateTime = DateTime.Now,
                    TagName = request.TagName,
                    UserId =  new Guid(request.UserId),
                };

                //标签Id系统重新算一下
                var tagList = await userAddressRepository.GetUserAddressTags(request.UserId);
                
                if (tagList.Any())
                {
                    vo.TagId = tagList.Count + 1;
                }
                
                var result = await userAddressRepository.CreateUserAddressTag(vo);
                if (result > 0)
                {
                    res.Code = ResultCode.Success;
                    res.Message = "标签添加成功!";
                }
                else {
                    res.Code = ResultCode.Failed;
                    res.Message = "标签添加失败!";
                }
            }
            return res;
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<ApiResult<bool>> DeleteUserAddress(DeleteUserAddressRequest request)
        {
            var result = await userAddressRepository.DeleteUserAddress(request.AddressId, request.UpdateBy);

            ApiResult<bool> res = new ApiResult<bool>();
            if (result > 0)
            {
                res.Data = true;
                res.Code = ResultCode.Success;
                res.Message = "删除地址成功!";
            }
            else {
                res.Code = ResultCode.Failed;
                res.Message = "删除地址失败!";
               
            }
            return res;
        }

        /// <summary>
        /// 查询用户地址详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<UserAddressDTO> GetUserAddressDetail(GetUserAddressRequest request)
        {
            var result = await userAddressRepository.GetUserAddressDetail(request.AddressId);
            if (result != null) {
                var vo = _mapper.Map<UserAddressDTO>(result);
                vo.AddressId = vo.Id;
                vo.MobileNumberDes = FormatHelper.FormatTel(vo.MobileNumber);
                return vo;
            }
            return null;
        }

        /// <summary>
        /// 获取用户地址列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<UserAddressResponse> GetUserAddressList(GetUserAddressRequest request)
        {
            var result = await userAddressRepository.GetUserAddressAsync(request.UserId);
            var vo = _mapper.Map<List<UserAddressDTO>>(result);
            foreach (var item in vo)
            {
                item.MobileNumberDes = FormatHelper.FormatTel(item.MobileNumber);
                item.AddressId = item.Id;
            }
           
            var res = new UserAddressResponse
            {
                AddressVOs = vo
            };
            return res;
        }

        /// <summary>
        /// 获取用户标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<UserAddressTagResponse> GetUserAddressTagList(GetUserAddressTagRequest request)
        {
            var vo = await userAddressRepository.GetUserAddressTags(request.UserId);
            var res = new UserAddressTagResponse();
            var tagList = _mapper.Map<List<UserAddressTagVO>>(vo);
            res.TagList = tagList;
            return res;
        }

        /// <summary>
        ///更新用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAddress(UpdateUserAddressRequest request)
        {
            //是否修改了标签
            bool res = false;
            var vo = _mapper.Map<UserAddressDO>(request);

            //做地址验证 是否重复，
            var repeatFlag = await userAddressRepository.CheckUserAddressRepeat(new UserAddressDO
            {
                UserId = new Guid(request.UserId),
                ProvinceId = request.ProvinceId,
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                AddressLine = request.AddressLine,
                UserName = request.UserName,
                MobileNumber = request.MobileNumber
            });
            if (repeatFlag > 0)
            {
                throw new CustomException("编辑用户地址已存在!");
            }

            res = await UpdateUserAddressRes(vo);
            if (res && request.DefaultAddress)
            {
                await userAddressRepository.SetUserDefaultAddressAsync(request.UserId, request.Id,
                    request.UpdateBy);
            }

            return res;
        }

        public async Task<bool> UpdateUserAddressRes(UserAddressDO vo)
        {
            var result = await userAddressRepository.UpdateUserAddress(vo);
            return result > 0;
        }

        /// <summary>
        /// 设为默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SetDefaultAddress(SetDefaultAddressRequest request)
        {
            var addressResult =
                await userAddressRepository.GetUserAddressAsync(request.UserId, request.AddressId, false);
            if (addressResult != null)
            {
                var result =
                    await userAddressRepository.SetUserDefaultAddressAsync(request.UserId, request.AddressId,
                        request.SubmitBy);

                return result;
            }
            else
            {
                throw new CustomException("此地址不存在！");
            }
        }

    }
}
