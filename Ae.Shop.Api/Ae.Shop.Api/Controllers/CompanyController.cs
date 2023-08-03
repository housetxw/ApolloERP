using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Company;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(CompanyController))]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ApolloErpLogger<CompanyController> _logger;
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService,
            ApolloErpLogger<CompanyController> logger, IIdentityService identityService
            )
        {
            _companyService = companyService;
            _logger = logger;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ApiResult<string>> GetOrgType()
        {
            return Result.Success<string>(_identityService.GetOrgType());
        }

        [HttpGet]
        public async Task<ApiResult<string>> GetCurrentUser()
        {
            return Result.Success<string>(_identityService.GetUserName());
        }


        #region SHOP端

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddOrUpdateCompany([FromBody] ApiRequest<AddCompanyRequest> request)
        {
            try
            {
                _logger.Info($"AddOrUpdateCompany ：{JsonConvert.SerializeObject(request)}");
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
            catch (Exception ex)
            {
                _logger.Error($"AddOrUpdateCompany Error ：{JsonConvert.SerializeObject(ex.Message)}");
                throw;
            }
            
        }

        /// <summary>
        /// 查询公司详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CompanyDTO>> GetCompanyInfo([FromQuery] GetCompanyInfoRequest request)
        {
            var result = await _companyService.GetCompanyInfoAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询二级公司列表 - 分页
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

        /// <summary>
        /// 查询区域合伙列表 - 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CompanyPageListForShopDTO>> GetPartnershipCompanyListAsync([FromQuery] GetPageCompanyListRequest request)
        {
            var result = await _companyService.GetPartnershipCompanyListAsync(request);
            ApiPagedResult<CompanyPageListForShopDTO> response = new ApiPagedResult<CompanyPageListForShopDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = "查询成功!"
            };
            return response;
        }

        #endregion
    }
}
