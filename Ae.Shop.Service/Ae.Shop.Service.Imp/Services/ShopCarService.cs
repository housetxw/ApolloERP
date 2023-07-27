using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Helper;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Transactions;

namespace Ae.Shop.Service.Imp.Services
{
    public class ShopCarService : IShopCarService
    {
        private readonly IMapper _mapper;
        private readonly IShopCarRepository _shopCarRepository;
        private readonly IIdentityService _identityService;
        private readonly IShopCarRecordRepository _shopCarRecordRepository;
        private readonly ITechTripRecordRepository _techTripRecordRepository;

        public ShopCarService(IShopCarRepository shopCarRepository, 
            IMapper mapper, 
            IIdentityService identityService,
            IShopCarRecordRepository shopCarRecordRepository,
            ITechTripRecordRepository techTripRecordRepository
            ) 
        {
            _mapper = mapper;
            _shopCarRepository = shopCarRepository;
            _identityService = identityService;
            _shopCarRecordRepository = shopCarRecordRepository;
            _techTripRecordRepository = techTripRecordRepository;
        }

        /// <summary>
        /// 新增或修改门店车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> AddOrModifyShopCar(ShopCarDTO request) 
        {
            var num = 0;
            var dt = DateTime.Now;
            var shopCarDO = _mapper.Map<ShopCarDO>(request);
            if (shopCarDO.Id > 0)
            {
                shopCarDO.UpdateBy = _identityService.GetUserName();
                shopCarDO.UpdateTime = dt;
                num = await _shopCarRepository.ModifyShopCar(shopCarDO);
            }
            else 
            {
                shopCarDO.ShopId = long.Parse(_identityService.GetOrganizationId());
                shopCarDO.CreateBy = _identityService.GetUserName();
                shopCarDO.CreateTime = dt;
                shopCarDO.Status = 0;
                num = await _shopCarRepository.InsertAsync(shopCarDO);
            }
            if (num > 0)
            {
                return new ApiResult<bool>()
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else 
            {
                return new ApiResult<bool>()
                {
                    Data = false,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateFailed
                };
            }
        }


        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<SimpleShopCarDTO>> GetShopCarListByShopId(GetShopCarPageListRequest request) 
        {
            if (request.ShopId <= 0) 
            {
                request.ShopId = long.Parse(_identityService.GetOrganizationId());
            }
            var response = await _shopCarRepository.GetShopCarListByShopId(request);
            var list = _mapper.Map<List<SimpleShopCarDTO>>(response.Items);
            foreach (var item in list)
            {
                item.VehicleCar = CommonHelper.GetCarVehicle(item.Vehicle, item.PaiLiang, item.Nian, item.SalesName);
            }
            return new ApiPagedResultData<SimpleShopCarDTO>() 
            {
                TotalItems = response.TotalItems,
                Items = list,
            };
        }


        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateShopCarStatus(UpdateShopCarStatusRequest request)
        {
            request.UpdateBy = _identityService.GetUserName();
            var model = new ShopCarDO() { Id = request.Id,Status = request.Status,UpdateBy = request.UpdateBy,UpdateTime = DateTime.Now };
            var num = await _shopCarRepository.UpdateAsync(model, new[] { "Status", "UpdateBy", "UpdateTime" });
            if (num > 0)
            {
                return new ApiResult<bool>()
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else
            {
                return new ApiResult<bool>()
                {
                    Data = false,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateFailed
                };
            }
        }

        /// <summary>
        /// 查询门店车辆详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopCarDTO> GetShopCarInfoById(BaseGetInfoRequest request)
        {
            var shopCarDO = await _shopCarRepository.GetAsync(request.Id);
            return _mapper.Map<ShopCarDTO>(shopCarDO);
        }


        /// <summary>
        /// 查询门店车辆记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCarRecordDTO>> GetShopCarRecordListByShopId(GetShopCarRecordPageListRequest request) 
        {
            request.ShopId = long.Parse(_identityService.GetOrganizationId());
            var response = await _shopCarRecordRepository.GetShopCarRecordListByShopId(request);
            var list = _mapper.Map<List<ShopCarRecordDTO>>(response.Items);
            return new ApiPagedResultData<ShopCarRecordDTO>()
            {
                TotalItems = response.TotalItems,
                Items = list,
            };
        }


        /// <summary>
        /// 新增门/修改店车辆使用信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> AddShopCarRecordInfo(ShopCarRecordDTO request)
        {
            var num = 0;
            var dt = DateTime.Now;
            var shopCarRecordDO = _mapper.Map<ShopCarRecordDO>(request);
            shopCarRecordDO.ShopId = long.Parse(_identityService.GetOrganizationId());
            if (request.Id > 0)
            {
                shopCarRecordDO.UpdateBy = _identityService.GetUserName();
                shopCarRecordDO.UpdateTime = dt;
                num = await _shopCarRecordRepository.ModifyShopCarRecordInfo(shopCarRecordDO);
            }
            else 
            {
                shopCarRecordDO.CreateBy = _identityService.GetUserName();
                shopCarRecordDO.CreateTime = dt;
                num = await _shopCarRecordRepository.InsertAsync(shopCarRecordDO);
            }
            
            if (num > 0)
            {
                return new ApiResult<bool>()
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else
            {
                return new ApiResult<bool>()
                {
                    Data = false,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateFailed
                };
            }
        }


        /// <summary>
        /// 查询门店车辆使用详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopCarRecordDTO> GetShopCarRecordInfoById(BaseGetInfoRequest request)
        {
            var shopCarRecordDO = await _shopCarRecordRepository.GetAsync(request.Id);
            return _mapper.Map<ShopCarRecordDTO>(shopCarRecordDO);
        }

        /// <summary>
        /// 删除车辆使用记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> DeleteShopCarRecordStatus(BaseGetInfoRequest request) 
        {
            var model = new ShopCarRecordDO() { Id = request.Id, IsDeleted = 1, UpdateTime = DateTime.Now };
            model.UpdateBy = _identityService.GetUserName();
            var num = await _shopCarRecordRepository.UpdateAsync(model, new[] { "IsDeleted", "UpdateBy", "UpdateTime" });
            if (num > 0)
            {
                return new ApiResult<bool>()
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else
            {
                return new ApiResult<bool>()
                {
                    Data = false,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateFailed
                };
            }
        }


        /// <summary>
        /// 查询门店技师出行记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetTripRecordListResponse>> GetTripRecordPageListForShop(GetTripRecordPageListRequest request)
        {
            if (request.ShopId == 0)
            {
                request.ShopId = long.Parse(_identityService.GetOrganizationId());
            }

            var result = await _techTripRecordRepository.GetTripRecordPageList(request);

            ApiPagedResultData<GetTripRecordListResponse> response = new ApiPagedResultData<GetTripRecordListResponse>();
            response.Items = _mapper.Map<List<GetTripRecordListResponse>>(result.Items);
            foreach (var item in response.Items)
            {
                item.TripTime = item.StartTime.ToString("yyyy-MM-dd");
                item.StatusName = item.Status == 0 ? "未还车" : "已还车";
            }
            response.TotalItems = result.TotalItems;
            return response;

        }

        /// <summary>
        /// 查询门店技师出行记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetTripRecordListResponse>> GetTripRecordPageList(GetTripRecordPageListRequest request)
        {
            var response = await _techTripRecordRepository.GetTripRecordPageList(request);

            var list = new List<GetTripRecordListResponse>();
            response.Items.ToList().ForEach(_ => list.Add(new GetTripRecordListResponse()
            {
                Id = _.Id,
                StatusName = _.Status == 0 ? "未还车" : "已还车",
                CarNumber = _.CarNumber,
                OrderNo = _.OrderNo,
                Remark = _.Remark,
                TripTime = _.StartTime.ToString("yyyy-MM-dd")
            }));
            return new ApiPagedResultData<GetTripRecordListResponse>()
            {
                TotalItems = response.TotalItems,
                Items = list,
            };
        }


        /// <summary>
        /// 新增出行记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddTechTripRecord(AddTechTripRecordRequest request)
        {
            var result = Result.Failed();
            var carInfo = await _shopCarRepository.GetAsync(request.CarId);
            if (carInfo != null)
            {
                if (carInfo.Status == 1)
                {
                    return Result.Failed("车辆禁用");
                } 
                else if (carInfo.Status == 2) 
                {
                    return Result.Failed("车辆未归还");
                }
            }
            else 
            {
                return result;
            }
            var modle = _mapper.Map<TechTripRecordDO>(request);
            modle.CreateTime = DateTime.Now;
            modle.StartTime = modle.CreateTime;
            modle.StartMileageImg = request.StartMileageImg.Substring(request.StartMileageImg.IndexOf(".cn/") + 4);
            modle.StartOilImg = request.StartOilImg.Substring(request.StartOilImg.IndexOf(".cn/") + 4);
            var id = 0;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
            {
                id = await _techTripRecordRepository.InsertAsync(modle);
                bool a = await _shopCarRepository.UpdateShopCarStatus(request.CarId, 2);
                ts.Complete();
            }
            if (id > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            return result;
        }


        /// <summary>
        /// 添加还车
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddTechTripReturn(AddTechTripReturnRequest request)
        {
            var result = Result.Failed("还车失败");
            var info = await _techTripRecordRepository.GetAsync(request.Id);
            if (info == null) 
            {
                return result;
            }
            var modle = _mapper.Map<TechTripRecordDO>(request);
            modle.UpdateTime = DateTime.Now;
            modle.ReturnTime = DateTime.Now;
            modle.EndMileageImg = request.EndMileageImg.Substring(request.EndMileageImg.IndexOf(".cn/") + 4);
            modle.EndOilImg = request.EndOilImg.Substring(request.EndOilImg.IndexOf(".cn/") + 4);
            modle.Status = 1;
            var id = 0;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                id = await _techTripRecordRepository.UpdateAsync(modle, new[] { "Status", "EndMileage", "EndMileageImg", "EndOil", "EndOilImg", "Refuelled", "ReturnTime",
                     "OrderNo", "Remark","UpdateBy", "UpdateTime" });
                await _shopCarRepository.UpdateShopCarStatus(info.CarId,0);
                ts.Complete();
            }
            if (id > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            return result;
        }


        /// <summary>
        /// 查询技师出行记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetTripRecordInfoResponse>> GetTechTripRecordInfo(BaseGetInfoRequest request) 
        {
            var response = Result.Failed();
            var info = await _techTripRecordRepository.GetTechTripRecordInfo(request);
            if (info != null) 
            {
                var techTripRecordInfo = _mapper.Map<TechTripRecordInfo>(info);
                techTripRecordInfo.CarVehicle = info.Brand + info.SalesName;
                techTripRecordInfo.CreateTimeStr = info.CreateTime.ToString("yyyy-MM-dd HH:mm");
                techTripRecordInfo.StartTimeStr = info.StartTime == new DateTime(1900, 1, 1) ? "" : info.StartTime.ToString("yyyy-MM-dd HH:mm");
                techTripRecordInfo.ReturnTimeStr = info.ReturnTime == new DateTime(1900, 1, 1) ? "" : info.ReturnTime.ToString("yyyy-MM-dd HH:mm");
                return new ApiResult<GetTripRecordInfoResponse>()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess,
                    Data = new GetTripRecordInfoResponse() { TechTripRecordVO = techTripRecordInfo }
                };
            }
            return new ApiResult<GetTripRecordInfoResponse>() 
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.OperateFailed
            };
        }
    }
}
