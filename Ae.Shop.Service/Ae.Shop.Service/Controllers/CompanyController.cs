using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Model.Company;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.Company;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.Shop.Service.Client.Model;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ApolloErpLogger<CompanyController> _logger;
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService,
            ApolloErpLogger<CompanyController> logger, IEmployeeService employeeService
            )
        {
            _companyService = companyService;
            _logger = logger;
            _employeeService = employeeService;
        }

        #region 公共

        /// <summary>
        /// 查询公司详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CompanyDTO>> GetCompanyInfoById([FromQuery] GetCompanyInfoByIdRequest request)
        {
            var result = await _companyService.GetCompanyInfoByIdAsync(request);
            return Result.Success(result);
        }

        #endregion




        #region BOSS端

        /// <summary>
        /// 新增或修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddOrUpdateCompany([FromBody] ApiRequest<CompanyDTO> request)
        {
            //_logger.Info($"AddOrUpdateCompany ：{JsonConvert.SerializeObject(request)}");
            var requestData = request.Data;
            if (requestData.Id > 0)
            {
                return await _companyService.UpdateCompanyAsync(requestData);
            }
            else
            {
                return await _companyService.AddCompanyAsync(requestData);
            }
        }

        /// <summary>
        /// 查询一级公司列表 - 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CompanyPageListForShopDTO>> GetPageCompanyListForShopAsync([FromQuery] GetPageCompanyListRequest request)
        {
            var result = await _companyService.GetPageCompanyListForShopAsync(request);
            ApiPagedResult<CompanyPageListForShopDTO> response = new ApiPagedResult<CompanyPageListForShopDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = "查询成功!"
            };
            return response;
        }

        [HttpGet]
        public async Task<ApiResult<List<CompanyPageListForShopDTO>>> GetCompanySubs([FromQuery] GetPageCompanyListRequest request)
        {
            return await _companyService.GetCompanySubs(request);
        }


        /// <summary>
        /// 查询一级公司列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CompanyDTO>>> GetFirstCompanyList([FromQuery] GetFirstCompanyListRequest request)
        {
            var result = await _companyService.GetFirstCompanyList(request);
            ApiResult<List<CompanyDTO>> response = new ApiResult<List<CompanyDTO>>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = "查询成功!"
            };
            return response;
        }

        #endregion



        /// <summary>
        /// 获取所有公司和门店简单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetAllUnitResponse>> GetAllUnitAsync()
        {
            var result = await _companyService.GetAllUnitAsync();
            return Result.Success(result);
        }


        /// <summary>
        /// 查询公司详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CompanySimpleInfoDTO>> MiniGetCompanyInfo([FromQuery] MiniGetCompanyInfoRequest request)
        {
            var result = await _companyService.MiniGetCompanyInfo(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 根据门店ID查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CompanyInfoDTO>> GetCompanyInfoByShopId([FromQuery] GetCompanyByShopIdRequest request)
        {
            var result = await _companyService.GetCompanyInfoByShopId(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 查询公司下的子公司和门店
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CompanyLessInfoDTO>>> GetCompanyAndShopByParentId(GetCompanyInfoByIdRequest request)
        {
            var result = await _companyService.GetCompanyAndShopByParentId(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> UpdateCompanyStatus([FromBody] ApiRequest<UpdateCompanyStatusRequest> request)
        {
            //_logger.Info($"UpdateCompanyStatus: {JsonConvert.SerializeObject(request)}");
            return await _companyService.UpdateCompanyStatus(request.Data);
        }

        /// <summary>
        /// OperateCompanyDeposit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> OperateCompanyDeposit([FromBody] OperateCompanyDepositRequest request)
        {
            var result = await _companyService.OperateCompanyDeposit(request);

            return Result.Success(result);
        }

        [HttpGet]
        public async Task<ApiResult<List<VenderDTO>>> GetVenders()
        {
            var result = await _companyService.GetVenders();
            return Result.Success(result);
        }

        /// <summary>
        /// 保存公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveCompany([FromBody] ApiRequest<CompanyDTO> request)
        {
            //_logger.Info($"AddOrUpdateCompany ：{JsonConvert.SerializeObject(request)}");
            var requestData = request.Data;
            if (requestData.Id > 0)
            {
                return await _companyService.UpdateCompanyForSupplierAsync(requestData);
            }
            else
            {
                var addCompanyResult = await _companyService.AddCompanyReturnIdAsync(requestData);
                var userId = request.Data.CreateBy;
                var userName = request.Data.CreateBy;
                if (addCompanyResult.Code == ResultCode.Success && addCompanyResult.Data > 0)
                {
                    //创建初始账号和员工
                    Task.Run(() => _employeeService.CreateEmployee(new EmployeeEntityReqDTO()
                    {
                        OrganizationId = addCompanyResult.Data,
                        Type = (Core.Enums.EmployeeType)request.Data.Type,
                        Name = request.Data.Head,
                        Mobile = request.Data.HeadPhone,
                        CreateBy = userName,
                        UserId = userId
                    }, true, true, "区域合伙公司-代理商角色"));

                    return new ApiResult<bool>()
                    {
                        Code = ResultCode.Success,
                        Data = true,
                        Message = "保存成功"
                    };
                }

                return new ApiResult<bool>()
                {
                    Code = ResultCode.Failed,
                    Data = false,
                    Message = "保存失败"
                };
            }
        }
    }
}
