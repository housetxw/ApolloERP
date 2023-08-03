using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Company;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Dal.Model.Company;
using Ae.Shop.Api.Dal.Repositorys.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private string Domain = string.Empty;//文件管理 外链默认域名
        public CompanyService(ICompanyRepository companyRepository,
            IConfiguration configuration,
            IMapper mapper,
            IIdentityService identityService
            )
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _configuration = configuration;
            _identityService = identityService;
        }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> AddCompanyAsync(AddCompanyRequest request)
        {
            var result = Result.Failed<bool>("新增公司异常");
            //判断名称是否存在
            var companyInfo = await _companyRepository.GetCompanyByName(request.Name, request.SimpleName);
            if (companyInfo != null)
            {
                return result = Result.Failed<bool>("公司名称已存在");
            }

            CompanyDO company = _mapper.Map<CompanyDO>(request);
            company.BusinessLicense = company.BusinessLicense.Replace($"{_configuration["QiNiuImageDomain"]}", "").Replace("http://m.aerp.com.cn/", "");
            company.AccountOpeningLicense = company.AccountOpeningLicense.Replace($"{_configuration["QiNiuImageDomain"]}", "").Replace("http://m.aerp.com.cn/", "");
            company.CreateTime = DateTime.Now;
            company.CreateBy = _identityService.GetUserName();
            company.RegisterTime = DateTime.Now;
            if (company.Status ==0||company.Status==1)//待审核
            {
                company.Status = 2;
            }
            var id = await _companyRepository.AddCompanyAsync(company);
            if (id > 0)
            {
                result = new ApiResult<bool>()
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else
            {
                result = new ApiResult<bool>()
                {
                    Data = false,
                    Code = ResultCode.Failed,
                    Message = CommonConstant.OperateFailed
                };
            }

            return result;
        }

        /// <summary>
        /// 根据公司ID查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompanyDTO> GetCompanyInfoAsync(GetCompanyInfoRequest request)
        {
            var companyInfo = await _companyRepository.GetAsync(request.Id);
            return _mapper.Map<CompanyDTO>(companyInfo);
        }

        /// <summary>
        /// 查询二级公司列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request)
        {
            var data = await _companyRepository.GetPageCompanyListForShopAsync(request);
            List<CompanyPageListForShopDTO> companyList = _mapper.Map<List<CompanyPageListForShopDTO>>(data.Items);
            var companyIds = companyList.Select(o => o.Id).ToList();
            //查询公司下的门店数量
            var shopCountList = await _companyRepository.GetShopCountUnderTheCompanyAsync(companyIds);
            foreach (var itemCount in shopCountList)
            {
                foreach (var item in companyList)
                {
                    if (item.Id == itemCount.CompanyId)
                    {
                        item.ShopTotalCount = itemCount.TotalCount;
                    }
                }
            }
            ApiPagedResultData<CompanyPageListForShopDTO> result = new ApiPagedResultData<CompanyPageListForShopDTO>()
            {
                TotalItems = data.TotalItems,
                Items = companyList
            };
            return result;
        }

        /// <summary>
        /// 查询区域合伙列表 - 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPartnershipCompanyListAsync(GetPageCompanyListRequest request)
        {
            //request.PageSize = 100;
            request.Status = 2;
            request.SystemType = 3;
            request.Type = 0;
            var data = await _companyRepository.GetPageCompanyListForShopAsync(request);
            List<CompanyPageListForShopDTO> companyList = _mapper.Map<List<CompanyPageListForShopDTO>>(data.Items);
           
            ApiPagedResultData<CompanyPageListForShopDTO> result = new ApiPagedResultData<CompanyPageListForShopDTO>()
            {
                TotalItems = data.TotalItems,
                Items = companyList
            };
            return result;
        }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateCompanyAsync(AddCompanyRequest request)
        {
            var result = Result.Failed<bool>("公司信息异常");
            //判断名称是否存在
            var companyTotal = await _companyRepository.GetCompanyTotalByName(request.SimpleName, request.Id);
            if (companyTotal > 0)
            {
                return Result.Failed<bool>("公司名称已存在");
            }
            var companyInfo = await _companyRepository.GetAsync(request.Id);
            if (companyInfo == null)
            {
                return result;
            }
            CompanyDO company = _mapper.Map<CompanyDO>(request);
            company.Name = companyInfo.Name;
            company.Status = companyInfo.Status;
            company.BusinessLicense = company.BusinessLicense.Replace($"{_configuration["QiNiuImageDomain"]}", "").Replace("http://m.aerp.com.cn/", "");
            company.AccountOpeningLicense = company.AccountOpeningLicense.Replace($"{_configuration["QiNiuImageDomain"]}", "").Replace("http://m.aerp.com.cn/", "");
            company.CreateBy = companyInfo.CreateBy;
            company.CreateTime = companyInfo.CreateTime;
            company.UpdateTime = DateTime.Now;
            company.UpdateBy = _identityService.GetUserName();
            if (company.Status == 0 || company.Status == 1)///待审核
            {
                company.Status = 2;
            }
            var count = await _companyRepository.UpdateAsync(company);
            if (count > 0)
            {
                result = new ApiResult<bool>
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            else
            {
                result = new ApiResult<bool>
                {
                    Data = false,
                    Code = ResultCode.Failed,
                    Message = CommonConstant.OperateFailed
                };
            }

            return result;
        }
    }
}
