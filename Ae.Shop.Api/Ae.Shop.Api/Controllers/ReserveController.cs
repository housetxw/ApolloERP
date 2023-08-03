using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using Ae.Shop.Api.Core.Response.Reserve;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 预约
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[Filter(nameof(ReserveController))]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService reserveService;
        private readonly IVehicleService vehicleService;
        private readonly IUserService userService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="reserveService"></param>
        /// <param name="vehicleService"></param>
        /// <param name="userService"></param>
        public ReserveController(IReserveService reserveService, IVehicleService vehicleService,
            IUserService userService)
        {
            this.reserveService = reserveService;
            this.vehicleService = vehicleService;
            this.userService = userService;
        }

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopReserveListVo>> GetReserveListPage([FromQuery]ReserveListPageRequest request)
        {
            var result = await reserveService.GetReserveListPage(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveDetailForWebVo>> GetReserveDetailForWeb([FromQuery]ReserveDetailForWebRequest request)
        {
            var result = await reserveService.GetReserveDetailForWeb(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约列表查询条件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ArrivalListConditionResponse>> GetReserveListCondition()
        {
            ArrivalListConditionResponse result = new ArrivalListConditionResponse();
            result.Status = new List<StatusEnum> {
                new StatusEnum { Value = 0, DisplayName = "全部" },
                new StatusEnum { Value = 1, DisplayName = "待到店" },
                new StatusEnum { Value = 2, DisplayName = "已正常到店" },
                new StatusEnum { Value = 3, DisplayName = "逾期已到店" },
                new StatusEnum { Value = 4, DisplayName = "逾期未到店" },
                new StatusEnum { Value = 5, DisplayName = "已取消" }
            };
            result.Channel = new List<StatusEnum>
            {
                new StatusEnum { Value = 0, DisplayName = "全部" },
                new StatusEnum { Value = 1, DisplayName = "客户预约" },
                new StatusEnum { Value = 2, DisplayName = "门店预约" },
            };

            var serviceType = await reserveService.GetShopServiceType();

            serviceType.Insert(0, new StatusEnum { Value = "", DisplayName = "全部" });

            result.Type = serviceType;

            var technician = await reserveService.GetReserveTechnician();

            result.Technician = technician.Select(_ => new StatusEnum
            {
                Value = _.Id,
                DisplayName = _.Name
            }).ToList();

            return Result.Success(result);
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveDateForWebResponse>> GetReserveDateForWeb()
        {
            var result = await reserveService.GetReserveDateForWeb();

            return Result.Success(result);
        }

        /// <summary>
        /// 用户车型列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserVehicleSimpleVo>>> GetAllUserVehicles(
            [FromQuery] UserVehiclesRequest request)
        {
            var result = await vehicleService.GetAllUserVehicles(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelReserve([FromBody] ApiRequest<CancelReserveRequest> request)
        {
            var result = await reserveService.CancelReserve(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditReserve([FromBody] ApiRequest<EditReserveRequest> request)
        {
            var result = await reserveService.EditReserve(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveListVo>>> GetValidReserve([FromQuery] ValidReserveRequest request)
        {
            var result = await reserveService.GetValidReserve(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据手机号查询客户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserDetailVo>> GetUserDetailByUserTel([FromQuery] UserDetailByUserTelRequest request)
        {
            var result = await userService.GetUserDetailByUserTel(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddReserve([FromBody] ApiRequest<AddReserveRequest> request)
        {
            var result = await reserveService.AddReserve(request.Data);

            return Result.Success(result);
        }
    }
}