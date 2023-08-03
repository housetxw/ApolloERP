using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{

    [Route("[controller]/[action]")]
    //[Filter(nameof(ReceiveController))]
    public class ReceiveController : ControllerBase
    {
        public ITransferTaskService _transferTaskService;
        private IIdentityService identityService;
        public ReceiveController(ITransferTaskService transferTaskService,
            IIdentityService identityService)
        {
            this._transferTaskService = transferTaskService;
            this.identityService = identityService;
        }

        /// <summary>
        /// 获取今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiPagedResult<GetTodayReceivePackageDTO>> GetTodayReceivePackage([FromBody]GetTodayReceivePackageRequest request)
        {
            var geTodaySignPackage = await _transferTaskService.GetTodayReceivePackage(request);
            if (geTodaySignPackage?.Items != null && geTodaySignPackage.Items.Any())
            {
                return new ApiPagedResult<GetTodayReceivePackageDTO>()
                {
                    Code = ResultCode.Success,
                    Data = geTodaySignPackage
                };
            }
            else
            {
                return new ApiPagedResult<GetTodayReceivePackageDTO>()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.NullData,
                    Data = new ApiPagedResultData<GetTodayReceivePackageDTO>()
                    {
                        TotalItems = 0,
                        Items = new List<GetTodayReceivePackageDTO>()
                    }
                };
            }
        }


        /// <summary>
        /// 清点入库 自营门店使用接口！！！
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckInStock([FromBody]CheckInStockRequest request)
        {
            return await _transferTaskService.CheckInStock(request);
        }

        /// <summary>
        /// 签收去清点操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<ReceivePackageDTO>> SignInStock([FromBody] SignInStockRequest request)
        {
            return await _transferTaskService.SignInStock(request);
        }

        /// <summary>
        /// 公司下的合作门店使用(扩展卖机油)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckInStockForShop([FromBody]CompanyInStockDTO request)
        {
            return await _transferTaskService.CheckInStockForShop(request);
        }

        /// <summary>
        /// 代理商/供应商发货，门店入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> VenderCheckInStock([FromBody]AcceptCompanyStockRequest request)
        {
            return await _transferTaskService.VenderCheckInStock(request);
        }

        ///// <summary>
        ///// 包裹签收清点
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ApiResult<ReceivePackageDTO>> GetReceivePackagesByPackageNos([FromBody]GetReceivePackageRequest request)
        //{
        //    return await _transferTaskService.GetReceivePackagesByPackageNos(request);
        //}
    }
}