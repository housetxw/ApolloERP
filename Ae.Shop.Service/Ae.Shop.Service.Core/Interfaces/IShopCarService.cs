using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IShopCarService
    {
        /// <summary>
        /// 新增或修改门店车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddOrModifyShopCar(ShopCarDTO request);
        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<SimpleShopCarDTO>> GetShopCarListByShopId(GetShopCarPageListRequest request);

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateShopCarStatus(UpdateShopCarStatusRequest request);


        /// <summary>
        /// 查询门店车辆详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopCarDTO> GetShopCarInfoById(BaseGetInfoRequest request);

        /// <summary>
        /// 查询门店车辆记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCarRecordDTO>> GetShopCarRecordListByShopId(GetShopCarRecordPageListRequest request);

        /// <summary>
        /// 新增门店车辆使用信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddShopCarRecordInfo(ShopCarRecordDTO request);


        /// <summary>
        /// 查询门店车辆使用详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopCarRecordDTO> GetShopCarRecordInfoById(BaseGetInfoRequest request);

        /// <summary>
        /// 删除车辆使用记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> DeleteShopCarRecordStatus(BaseGetInfoRequest request);

        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetTripRecordListResponse>> GetTripRecordPageList(GetTripRecordPageListRequest request);
        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetTripRecordListResponse>> GetTripRecordPageListForShop(GetTripRecordPageListRequest request);
        /// <summary>
        /// 新增出行记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> AddTechTripRecord(AddTechTripRecordRequest request);
        /// <summary>
        /// 添加还车
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> AddTechTripReturn(AddTechTripReturnRequest request);

        /// <summary>
        /// 查询技术出行记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetTripRecordInfoResponse>> GetTechTripRecordInfo(BaseGetInfoRequest request);
    }
}
