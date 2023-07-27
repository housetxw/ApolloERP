using System;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Model;
using ApolloErp.Log;
using Newtonsoft.Json;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Constant;
using ApolloErp.Login.Auth;
using Microsoft.AspNetCore.Authorization;
using Ae.Shop.Service.Core.Request.BOSS;
using Ae.Shop.Service.Core.Request.Shop;
using Ae.Shop.Service.Core.Response.APP;
using Ae.Shop.Service.Core.Request.APP;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IShopConfigService _shopConfigService;
        private readonly IJoinInService _joinInService;
        private readonly ApolloErpLogger<ShopController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService identityService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public ShopController(IShopService shopService,
            IShopConfigService shopConfigService,
            IJoinInService joinInService,
            ApolloErpLogger<ShopController> logger,
            IEmployeeService employeeService, IIdentityService identityService
            )
        {
            _shopService = shopService;
            _shopConfigService = shopConfigService;
            _joinInService = joinInService;
            _logger = logger;
            _employeeService = employeeService;
            this.identityService = identityService;
        }



        #region 门店信息---BOSS端
        /// <summary>
        /// 新增门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddShop([FromBody] ApiRequest<AddShopRequest> request)
        {
            //_logger.Info($"AddShop 请求参数：{JsonConvert.SerializeObject(request)}");
            request.Data.Shop.FullName = request.Data.Shop.SimpleName;
            var shopId = await _shopService.AddShopAsync(request.Data);
            if (shopId > 0 && request.Data.ShopConfig.IsCreateAccount == 1)
            {
                var userId = identityService.GetUserId();
                var userName = identityService.GetUserName();
                //创建门店初始账号和员工
                Task.Run(() => _employeeService.CreateEmployee(new EmployeeEntityReqDTO()
                {
                    OrganizationId = shopId,
                    Type = Core.Enums.EmployeeType.Shop,
                    Name = request.Data.Shop.OwnerName,
                    Mobile = request.Data.Shop.OwnerPhone,
                    CreateBy = userName,
                    UserId = userId
                }, true, request.Data.ShopConfig.IsSendMessage == 1));
            }
            return Result.Success(true);
        }

        /// <summary>
        /// 查询门店专修品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ApiResult<List<ShopServiceBrandDTO>>> GetShopServiceBrands([FromQuery] ShopServiceBrandDTO request)
        {
            var result = await _shopService.GetShopServiceBrands(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 根据门店ID查询门店配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopResponse>> GetShopForBOSSAsync([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopAsync(request);
            return Result.Success(result);
        }



        /// <summary>
        /// 查询门店列表---BOSS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoForBOSSDTO>> GetShopListForBOSSAsync([FromQuery] GetShopListForBOSSRequest request)
        {
            var result = await _shopService.GetShopListForBOSSAsync(request);
            ApiPagedResult<ShopSimpleInfoForBOSSDTO> response = new ApiPagedResult<ShopSimpleInfoForBOSSDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = "查询成功!"
            };
            return response;
        }

        /// <summary>
        /// 修改暂停营业时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<string>> ModifyShopSuspendTime([FromBody] ApiRequest<ModifyShopSuspendTimeRequest> request)
        {
            var result = await _shopConfigService.ModifyShopSuspendTime(request.Data);
            return result;
        }

        /// <summary>
        /// 修改门店基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> ModifyShopBaseInfoAsync(
            [FromBody] ApiRequest<ModifyShopBaseInfoRequest> request)
        {
            request.Data.FullName = request.Data.SimpleName;
            var result = await _shopService.ModifyShopBaseInfoAsync(request.Data);

            if (request.Data.IsCreateAccount == 1)
            {
                var userId = identityService.GetUserId();
                var userName = identityService.GetUserName();

                var shopInfo = await _shopService.GetShopAsync(new GetShopRequest()
                {
                    ShopId = request.Data.ShopId
                });
                //创建门店初始账号和员工
                Task.Run(() => _employeeService.CreateEmployee(new EmployeeEntityReqDTO()
                {
                    OrganizationId = request.Data.ShopId,
                    Type = Core.Enums.EmployeeType.Shop,
                    Name = shopInfo?.Shop?.OwnerName,
                    Mobile = shopInfo?.Shop?.OwnerPhone,
                    CreateBy = userName,
                    UserId = userId
                }, true, request.Data.IsSendMessage == 1));
            }
            return Result.Success(result);
        }

        /// <summary>
        /// 修改门店地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> ModifyShopAddressAsync([FromBody] ApiRequest<ModifyShopAddressRequest> request)
        {
            var result = await _shopService.ModifyShopAddressAsync(request.Data);
            return Result.Success(result);
        }
        /// <summary>
        /// 修改门店配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> ModifyShopConfigInfoAsync([FromBody] ApiRequest<ModifyShopConfigInfoRequest> request)
        {
            var result = await _shopConfigService.ModifyShopConfigInfoAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改门店配置服务费用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> ModifyShopConfigExpenseAsync([FromBody] ApiRequest<ModifyShopConfigExpenseRequest> request)
        {
            var result = await _shopConfigService.ModifyShopConfigExpenseAsync(request.Data);
            return Result.Success(result);
        }


        /// <summary>
        /// 修改门店银行账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> ModifyShopBankAccountAsync([FromBody] ApiRequest<ModifyShopBankAccountRequest> request)
        {
            //_logger.Info($"修改门店银行账户信息 请求参数：{JsonConvert.SerializeObject(request)}");
            var result = await _shopService.ModifyShopBankAccountAsync(request.Data);
            return Result.Success(result);
        }
        /// <summary>
        /// 添加门店图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddShopImgs([FromBody] ApiRequest<AddShopImgsRequest> request)
        {
            var result = await _shopService.AddShopImgs(request.Data);
            return Result.Success(result);
        }
        /// <summary>
        /// 删除门店图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> DeleteShopImgAsync([FromBody] ApiRequest<DeleteShopImgRequest> request)
        {
            var result = await _shopService.DeleteShopImgAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店专职顾问信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopSimpleInfoDTO>>> GetShopHeaderListByAsync()
        {
            var result = await _shopService.GetShopHeaderListByAsync();
            return Result.Success(result);
        }

        /// <summary>
        /// 修改门店上下架状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<string>> UpdateShopOnline([FromBody] ApiRequest<UpdateOnlineRequest> request)
        {
            return await _shopService.UpdateShopOnline(request.Data);
        }


        /// <summary>
        /// 根据服务PID查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ShopSimpleInfoForBOSSDTO>> GetShopListForServiceAsync([FromBody] ApiRequest<GetShopListForServiceRequest> request)
        {
            var result = await _shopService.GetShopListForServiceAsync(request.Data);
            ApiPagedResult<ShopSimpleInfoForBOSSDTO> response = new ApiPagedResult<ShopSimpleInfoForBOSSDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = "查询成功!"
            };
            return response;
        }

        #endregion


        #region 内部调用

        /// <summary>
        /// 获取门店主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopDTO>> GetShopById([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopByIdAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店配置表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopConfigDTO>> GetShopConfigByShopId([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopConfigByShopIdAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 根据门店ID查询门店银行账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopBankCardDTO>> GetShopBandInfoByShopId([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopBandInfoByShopId(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopSimpleInfoResponse>> GetShopSimpleInfoAsync([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopSimpleInfoAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店简单信息列表---分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync([FromBody] GetShopListRequest request)
        {
            var result = await _shopService.GetShopListAsync(request);
            ApiPagedResult<ShopSimpleInfoDTO> response = new ApiPagedResult<ShopSimpleInfoDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 查询门店仓库列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopSimpleInfoDTO>>> GetShopWareHouseListAsync([FromBody] GetShopListRequest request)
        {
            var result = await _shopService.GetShopWareHouseListAsync(request);
            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListNewAsync([FromBody] BaseEntityPostRequest<GetShopListRequest> request)
        {
            var result = await _shopService.GetShopListAsync(request.Data);
            ApiPagedResult<ShopSimpleInfoDTO> response = new ApiPagedResult<ShopSimpleInfoDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 查询门店列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopSimpleInfoDTO>>> GetShopListByIdsAsync([FromBody] GetShopListByIdsRequest request)
        {
            var result = await _shopService.GetShopListByIdsAsync(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 更新门店好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateShopScoreAsync([FromBody] UpdateShopScoreByShopIdsRequest request)
        {
            var result = await _shopService.UpdateShopScoreAsync(request);
            return Result.Success();
        }


        /// <summary>
        /// 查询银行列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BankInformationDTO>>> GetBankListAsync()
        {
            var result = await _shopService.GetBankListAsync();
            return Result.Success(result);
        }

        #endregion

        #region 加盟
        /// <summary>
        /// 加盟平台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> JoinIn([FromBody] JoinInRequest request)
        {
            var result = await _joinInService.JoinInAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 加盟列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<JoinInVO>> GetJoinInList([FromQuery] JoinInListRequest request)
        {
            var result = await _joinInService.GetJoinInList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 门店加盟---新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ShopJoinAsync([FromBody] ShopJoinRequest request)
        {
            //_logger.Info($"GetEmpAndOrgPageForLoginByAccountIdAsync 请求参数：{JsonConvert.SerializeObject(request)}");
            var result = await _joinInService.ShopJoinAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店加盟列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopJoinDTO>> GetShopJointListAsync(GetShopJoinListRequest request)
        {
            if (request.Status < 0 || request.Status > 2)
            {
                throw new CustomException("审核状态错误");
            }
            var result = await _joinInService.GetShopJointListAsync(request);
            ApiPagedResult<ShopJoinDTO> response = new ApiPagedResult<ShopJoinDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 查询门店加盟详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopJoinDTO>> GetShopJoinByIdAsync(GetShopJoinByIdRequest request)
        {
            var result = await _joinInService.GetShopJoinByIdAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 门店加盟---更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateShopJoinAsync([FromBody] ShopJoinRequest request)
        {
            var result = await _joinInService.UpdateShopJoinAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 审核-门店加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CheckShopJoinAsync([FromBody] CheckShopJoinRequest request)
        {
            if (request.IsConfirmed != 1 && request.IsConfirmed != 2)
            {
                throw new CustomException("审核状态错误");
            }
            //_logger.Info($"CheckShopJoinAsync 请求参数：{JsonConvert.SerializeObject(request)}");
            var result = await _joinInService.CheckShopJoinAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取网站备案号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetWebSiteInfoResponse>> GetWebSiteInfo()
        {
            var host = HttpContext.Request.Host.Value;
            //_logger.Info("域名：" + host);
            string beiAnHao = "皖ICP备19025011号-1";
            if (host.Contains("ApolloErp.cn"))
            {
                beiAnHao = "皖ICP备19025011号-1";
            }
            else if (host.Contains("ApolloErp.work"))
            {
                beiAnHao = "皖ICP备19025011号-3";
            }
            else if (host.Contains("ApolloErp.com.cn"))
            {
                beiAnHao = "皖ICP备19025011号-4";
            }
            else if (host.Contains("ApolloErp.net.cn"))
            {
                beiAnHao = "皖ICP备19025011号-5";
            }
            else if (host.Contains("ApolloErp.net"))
            {
                beiAnHao = "皖ICP备19025011号-2";
            }
            var result = new GetWebSiteInfoResponse()
            {
                BeiAnHao = beiAnHao
            };
            return Result.Success(result);
        }

        [HttpGet]
        public async Task<ApiResult<string[]>> GetShopTags([FromQuery] ShopTagDTO request)
        {
            var result = await _shopService.GetShopTags(request);
            var tags = result.Select(r => r.TagName).ToArray();
            return Result.Success(tags);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetVehicleBrandVo>>> GetVehicleBrandList([FromQuery] GetVehicleBrandVo request)
        {
            var result = await _shopService.GetVehicleBrandList(request);
            return Result.Success(result);
        }


        [HttpPost]
        public async Task<ApiResult<List<string>>> ReGenerateAppletCode([FromBody] ApiRequest<GenerateAppletCodeRequest> request)
        {
            var result = await _shopService.ReGenerateAppletCode(request.Data);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateExaminedStatus([FromBody] ApiRequest<UpdateExaminedStatusRequest> request)
        {
            return await _shopService.UpdateExaminedStatus(request.Data);
        }

        #endregion


        #region 小程序服务
        /// <summary>
        /// 微信小程序查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<NearShopInfoDTO>> GetNearShopListAsync([FromBody] GetNearShopListRequest request)
        {
            //_logger.Info($"附近门店列表 data: {JsonConvert.SerializeObject(request)}");
            var result = await _shopService.GetNearShopListAsync(request);
            ApiPagedResult<NearShopInfoDTO> response = new ApiPagedResult<NearShopInfoDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 微信小程序获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopDetailResponse>> GetShopDetailAsync([FromQuery] GetShopDetailRequest request)
        {
            var result = await _shopService.GetShopDetailAsync(request);
            if (result != null)
            {
                return Result.Success(result, CommonConstant.QuerySuccess);
            }
            else
            {
                return new ApiResult<GetShopDetailResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.NoData
                };
            }

        }



        /// <summary>
        /// 根据市查询市下的区县
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopLocationVO>>> GetRegionByCityId([FromQuery] GetRegionByCityIdRequest request)
        {
            var result = await _shopService.RegionChinaReqByRegionId(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetCityListResponse>> GetAllCitys()
        {
            var result = await _shopService.GetAllCitys();
            return Result.Success(result);
        }

        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOptimalShopResponse>> GetOptimalShop([FromQuery] GetOptimalShopRequest request)
        {
            //_logger.Info($"GetOptimalShop data: {JsonConvert.SerializeObject(request)}");
            var result = await _shopService.GetOptimalShop(request);
            return Result.Success(result);
        }

        #endregion


        #region APP服务

        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopSimpleInfoResponse>> GetShopInfoForAppAsync([FromQuery] BaseGetShopRequest request)
        {
            var result = await _shopService.GetShopInfoForAppAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店详情信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopDetailForAppResponse>> GetShopDetailForApp([FromQuery] GetShopRequest request)
        {
            var result = await _shopService.GetShopDetailForApp(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询城市门店列表。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetNearCityShopInfoResponse>> GetNearCityShopInfo([FromBody] ApiRequest<GetNearCityShopInfoRequest> request)
        {
            return await _shopService.GetNearCityShopInfo(request.Data);
        }


        #endregion

        #region  SHOP

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddOrModifyShopInfoForShopAsync([FromBody] ApiRequest<AddShopRequest> request)
        {
            //_logger.Info($"AddOrModifyShopInfoForShopAsync 请求参数：{JsonConvert.SerializeObject(request.Data)}");
            if (request.Data.Shop.Id > 0)
            {

                if (request.Data.ShopConfig.IsCreateAccount == 1)
                {
                    var userId = identityService.GetUserId();
                    var userName = identityService.GetUserName();
                    //创建门店初始账号和员工
                    Task.Run(() => _employeeService.CreateEmployee(new EmployeeEntityReqDTO()
                    {
                        OrganizationId = request.Data.Shop.Id,
                        Type = Core.Enums.EmployeeType.Shop,
                        Name = request.Data.Shop.OwnerName,
                        Mobile = request.Data.Shop.OwnerPhone,
                        CreateBy = userName,
                        UserId = userId
                    }, true, true));
                }
                //修改门店
                return await _shopService.ModifyShopInfoForShopAsync(request.Data);
            }
            else
            {
                //创建门店
                var shopId = await _shopService.AddShopAsync(request.Data);
                if (shopId > 0 && request.Data.ShopConfig.IsCreateAccount == 1)
                {
                    var userId = identityService.GetUserId();
                    var userName = identityService.GetUserName();
                    //创建门店初始账号和员工
                    Task.Run(() => _employeeService.CreateEmployee(new EmployeeEntityReqDTO()
                    {
                        OrganizationId = shopId,
                        Type = Core.Enums.EmployeeType.Shop,
                        Name = request.Data.Shop.OwnerName,
                        Mobile = request.Data.Shop.OwnerPhone,
                        CreateBy = userName,
                        UserId = userId
                    }, true, true));
                }
                return Result.Success(true);
            }

        }

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ModifyShopInfoAsync([FromBody] ApiRequest<AddShopRequest> request)
        {
            //_logger.Info($"AddOrModifyShopInfoForShopAsync 请求参数：{JsonConvert.SerializeObject(request.Data)}");

            //修改门店
            return await _shopService.ModifyShopInfoAsync(request.Data);



        }

        /// <summary>
        /// 门店列表查询（平台先生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopListPageResponse>> GetShopListPage([FromQuery] ShopListPageRequest request)
        {
            var result = await _shopService.GetShopListPage(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 平台先生门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopDetailForShopResponse>> GetShopDetailForShop(
            [FromQuery] ShopDetailForShopRequest request)
        {
            var result = await _shopService.GetShopDetailForShop(request);

            return Result.Success(result);
        }

        #endregion




        [HttpGet]
        public async Task<ApiResult<bool>> AddShopAppletCode()
        {
            var result = await _shopService.AddShopAppletCode();
            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopLogDTO>>> GetShopLog([FromQuery] GetShopLogRequest request)
        {
            return await _shopService.GetShopLog(request);
        }


        /// <summary>
        /// 更新门店押金
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateShopDeposit([FromBody] UpdateShopDepositRequest request)
        {
            return await _shopService.UpdateShopDeposit(request);
        }



        #region Config

        /// <summary>
        /// 更新门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateShopServiceArea([FromBody] UpdateShopServiceAreaRequest request)
        {
            var result = await _shopService.UpdateShopServiceArea(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopServiceAreaResponse>> GetShopServiceArea(
            [FromQuery] ShopServiceAreaRequest request)
        {
            var result = await _shopService.GetShopServiceArea(request);

            return Result.Success(result);
        }

        #endregion


        #region 团购
        [HttpPost]
        public async Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct([FromBody]GetShopGrouponProductRequest request)
        {
            var data= await _shopService.GetShopGrouponProduct(request);

            return Result.Success(data);
        }

        #endregion
    }
}
