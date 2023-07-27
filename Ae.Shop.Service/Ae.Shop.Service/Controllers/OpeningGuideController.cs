using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Core.Response.OpeningGuide;
using Ae.Shop.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class OpeningGuideController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IShopPointService _shopPointService;
        private readonly IEmployeeService _employeeService;
        private readonly ApolloErpLogger<OpeningGuideController> _logger;
        private readonly ICompanyService _companyService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public OpeningGuideController(IShopService shopService, IShopPointService shopPointService,
            IEmployeeService employeeService, ApolloErpLogger<OpeningGuideController> logger,
            ICompanyService companyService)
        {
            _shopService = shopService;
            _shopPointService = shopPointService;
            _employeeService = employeeService;
            _logger = logger;
            _companyService = companyService;
        }


        /// <summary>
        /// 获取门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopPointVo>> GetShopPoint([FromQuery]ShopPointRequest request)
        {
            var result = await _shopPointService.GetShopPoint(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 操作门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopPointRecord([FromBody]AddShopPointRecordRequest request)
        {
            var result = await _shopPointService.AddShopPointRecord(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增门店注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AddShopRegisterResponse>> AddShopRegister([FromBody]ApiRequest<AddShopRegisterRequest> request)
        {
            var result = await _shopService.AddShopRegister(request);
            if (result.IsNotNullSuccess())
            {
                var requestData = request.Data;
                var shopId = result.GetSuccessData()?.ShopId ?? 0;
                if (shopId > 0)
                {
                    #region 创建门店初始账号和员工
                    var employeeEntityReqDTO = new EmployeeEntityReqDTO()
                    {
                        OrganizationId = shopId,
                        Type = EmployeeType.Shop,
                        Name = requestData.OwnerName,
                        Mobile = requestData.OwnerPhone,
                        CreateBy = requestData.OwnerName,
                        UserId = CommonConstant.DefaultCustomGuidStr,
                        JobId=2//店长身份
                    };
                    _ = Task.Factory.StartNew(async () =>
                    {
                        try
                        {
                            var createEmployeeResult = await _employeeService.CreateEmployee(employeeEntityReqDTO, true,true);
                            //_logger.Info($"AddShopRegister_CreateEmployee employeeEntityReqDTO={JsonConvert.SerializeObject(employeeEntityReqDTO)} createEmployeeResult={JsonConvert.SerializeObject(createEmployeeResult)}");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("AddShopRegister_CreateEmployeeEx", ex);
                        }
                    });
                    #endregion

                    #region 操作门店积分
                    if (requestData.ShareShopId > 0)
                    {
                        var addShopPointRecordRequest = new AddShopPointRecordRequest()
                        {
                            ShopId = (int)requestData.ShareShopId,
                            OperateType = ShopPointOperateTypeEnum.InviteRegister,
                            OperateTypeDescription = $"邀请{requestData.OwnerName}注册",
                            PointValue = 500,
                            SubmitBy = requestData.OwnerName,
                            Remark = shopId.ToString()
                        };
                        _ = Task.Factory.StartNew(async () =>
                        {
                            try
                            {
                                var addShopPointRecordResult = await _shopPointService.AddShopPointRecord(addShopPointRecordRequest);
                                //_logger.Info($"AddShopRegister_AddShopPointRecord addShopPointRecordRequest={JsonConvert.SerializeObject(addShopPointRecordRequest)} addShopPointRecordResult={JsonConvert.SerializeObject(addShopPointRecordResult)}");
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("AddShopRegister_AddShopPointRecordEx", ex);
                            }
                        });
                    }
                    #endregion
                }
            }
            return result;
        }


        /// <summary>
        /// 得到引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<object>> GetGuideData([FromQuery]GetGuideDataRequest request)
        {
            return await _shopService.GetGuideData(request);
        }

        /// <summary>
        /// 保存引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveOpeningGuide([FromBody] ApiRequest<SaveOpeningGuideRequest> request)
        {
            return await _shopService.SaveOpeningGuide(request);
        }
        /// <summary>
        /// 获取门店审核信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopCheckInfoResponse>> GetShopCheckInfo([FromQuery] BaseGetShopRequest request)
        {
            return await _shopService.GetShopCheckInfo(request);
        }

        #region 公司

        /// <summary>
        /// 注册公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AddCompanyRegisterResponse>> AddCompanyRegister(
            [FromBody] AddCompanyRegisterRequest request)
        {
            var result = await _companyService.AddCompanyRegister(request);

            return Result.Success(result);
        }

        #endregion
    }
}
