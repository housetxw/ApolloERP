using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model.ShopCustomer;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopCustomer;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;

namespace Ae.Shop.Service.Controllers
{
    /// <summary>
    /// 门店客户
    /// </summary>
    [Route("[controller]/[action]")]
    public class ShopCustomerController : ControllerBase
    {
        private readonly IShopCustomerService _shopCustomerService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="shopCustomerService"></param>
        public ShopCustomerController(IShopCustomerService shopCustomerService)
        {
            _shopCustomerService = shopCustomerService;
        }

        /// <summary>
        /// 会员标签查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<MemberTagDto>> GetMemberTag([FromQuery] MemberTagRequest request)
        {
            var result = await _shopCustomerService.GetMemberTag(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有会员标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<MemberTagDto>>> GetMemberTagEnum()
        {
            var result = await _shopCustomerService.GetMemberTagEnum();

            return Result.Success(result);
        }

        /// <summary>
        /// 用户关联门店(即创建门店会员信息)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopUserRelation([FromBody] AddShopUserRelationRequest request)
        {
            var result = await _shopCustomerService.AddShopUserRelation(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑会员标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditMemberTag([FromBody] EditMemberTagRequest request)
        {
            var result = await _shopCustomerService.EditMemberTag(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 更新用户最近一次到店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateUserLastReceiveTime(
            [FromBody] UpdateUserLastReceiveTimeRequest request)
        {
            var result = await _shopCustomerService.UpdateUserLastReceiveTime(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopCustomerVo>> GetCustomerList([FromQuery] CustomerListRequest request)
        {
            var result = await _shopCustomerService.GetCustomerList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 门店客户列表（Shop站点）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopCustomerListVo>> GetShopCustomerList([FromQuery] ShopCustomerListRequest request)
        {
            var result = await _shopCustomerService.GetShopCustomerList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 门店客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopCustomerDetailVo>> GetShopCustomerDetail([FromQuery]ShopCustomerDetailRequest request)
        {
            var result = await _shopCustomerService.GetShopCustomerDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店当月获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopReferrerCustomerResDTO>> GetCurMonthShopNewCustomerStatisticsInfo([FromQuery] ShopReferrerCustomerReqDTO req)
        {
            var res = await _shopCustomerService.GetCurMonthShopNewCustomerStatisticsInfo(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据统计类型，获取引流统计信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<DrainageStatisticsResDTO>> GetDrainageStatisticsPage([FromQuery] DrainageStatisticsPageReqDTO req)
        {
            var res = await _shopCustomerService.GetDrainageStatisticsPage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        #region 平台汽配相关

        /// <summary>
        /// 平台汽配下单调用
        /// 绑定客户与店的关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopUserRelationForQp([FromBody] AddShopUserRelationForQpRequest request)
        {
            var result = await _shopCustomerService.AddShopUserRelationForQp(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 平台汽配 - 我的顾客
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopCustomerQpVo>> GetCustomerListForQp(
            [FromQuery] CustomerListForQpRequest request)
        {
            var result = await _shopCustomerService.GetCustomerListForQp(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        #endregion

    }
}