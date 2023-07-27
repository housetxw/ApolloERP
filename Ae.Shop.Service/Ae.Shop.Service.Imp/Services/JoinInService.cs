using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.JoinIn;
using Ae.Shop.Service.Core.Interfaces;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Request.BOSS;

namespace Ae.Shop.Service.Imp.Services
{
    public class JoinInService : IJoinInService
    {
        private readonly IJoinInRespository _joinInRespository;
        private readonly IMapper _mapper;
        private readonly IShopJoinRepository _shopJoinRepository;

        public JoinInService(IJoinInRespository joinInRespository,
            IMapper mapper,
            IShopJoinRepository shopJoinRepository
            )
        {
            _joinInRespository = joinInRespository;
            _mapper = mapper;
            _shopJoinRepository = shopJoinRepository;
        }


        /// <summary>
        /// 加盟平台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> JoinInAsync(JoinInRequest request)
        {
            JoinInDO joinIn = _mapper.Map<JoinInDO>(request);
            joinIn.CreateTime = DateTime.Now;
            var id = await _joinInRespository.InsertAsync(joinIn);
            return id > 0;
        }

        /// <summary>
        /// 加盟列表
        /// </summary>
        /// <param name="request"></param>
        public async Task<ApiPagedResultData<JoinInVO>> GetJoinInList(JoinInListRequest request)
        {
            ApiPagedResultData<JoinInVO> data = new ApiPagedResultData<JoinInVO>()
            {
                Items = new List<JoinInVO>()
            };
            int provinceId = 0;
            int cityId = 0;
            int districtId = 0;
            if (!string.IsNullOrEmpty(request.ProvinceId))
            {
                Int32.TryParse(request.ProvinceId, out provinceId);
            }

            if (!string.IsNullOrEmpty(request.CityId))
            {
                Int32.TryParse(request.CityId, out cityId);
            }

            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                Int32.TryParse(request.DistrictId, out districtId);
            }

            var result = await _joinInRespository.GetJoinInList(new JoinInListCondition()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Name = request.Name,
                Phone = request.Phone,
                ProvinceId = provinceId,
                CityId = cityId,
                DistrictId = districtId
            });

            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null)
                {
                    data.Items = result.Items.Select(_ => new JoinInVO
                    {
                        Id = _.Id,
                        Name = _.Name,
                        Phone = _.Phone,
                        Email = _.Email,
                        ShortAddress = _.ShortAddress,
                        Remark = _.Remark,
                        CreateTime = _.CreateTime,
                        CreateBy = _.CreateBy
                    }).ToList();
                }
            }

            return data;
        }

        /// <summary>
        /// 门店加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ShopJoinAsync(ShopJoinRequest request) 
        {
            ShopJoinDO join = _mapper.Map<ShopJoinDO>(request);
            join.CreateTime = DateTime.Now;
            var id = await _shopJoinRepository.InsertAsync(join);
            return id > 0;
        }

        /// <summary>
        /// 查询门店加盟列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopJoinDTO>> GetShopJointListAsync(GetShopJoinListRequest request) 
        {
            var result = await _shopJoinRepository.GetShopJointListAsync(request);
            ApiPagedResultData<ShopJoinDTO> resultData = new ApiPagedResultData<ShopJoinDTO>()
            {
                Items = _mapper.Map<List<ShopJoinDTO>>(result.Items),
                TotalItems = result.TotalItems
            };
            return resultData;
        }
        /// <summary>
        /// 查询门店加盟详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopJoinDTO> GetShopJoinByIdAsync(GetShopJoinByIdRequest request) 
        {
            var result = await _shopJoinRepository.GetAsync(request.Id);
            return _mapper.Map<ShopJoinDTO>(result);
        }

        /// <summary>
        /// 更新门店加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShopJoinAsync(ShopJoinRequest request) 
        {

            var shopJoinInfo = await _shopJoinRepository.GetAsync(request.Id);
            var id = 0;
            if (shopJoinInfo != null) 
            {
                ShopJoinDO shopJoinDO = _mapper.Map<ShopJoinDO>(request);
                id = await _shopJoinRepository.UpdateAsync(shopJoinDO);
                
            }
            return id > 0;
        }

        /// <summary>
        /// 审核-门店加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CheckShopJoinAsync(CheckShopJoinRequest request)
        {
            var result = await _shopJoinRepository.CheckShopJoinAsync(request);
            return result;
        }
    }
}
