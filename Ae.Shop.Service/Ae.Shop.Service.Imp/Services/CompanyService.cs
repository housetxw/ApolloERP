using AutoMapper;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Helper;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Company;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Model.Company;
using Ae.Shop.Service.Core.Request.Company;
using ApolloErp.Login.Auth;
using System.Transactions;
using Ae.Shop.Service.Core.Response.OpeningGuide;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Inteface;
using ApolloErp.Log;
using Newtonsoft.Json;

namespace Ae.Shop.Service.Imp.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private readonly ICompanyDepositRecordRepository _companyDepositRecordRepository;
        private readonly IPurchaseClient _purchaseClient;
        private string Domain = string.Empty; //文件管理 外链默认域名
        private readonly ApolloErpLogger<CompanyService> logger;


        public CompanyService(ICompanyRepository companyRepository,
            IShopRepository shopRepository,
            IConfiguration configuration,
            IIdentityService identityService,
            IMapper mapper, ICompanyDepositRecordRepository companyDepositRecordRepository,
            IPurchaseClient purchaseClient, ApolloErpLogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
            _configuration = configuration;
            Domain = configuration["QiNiuImageDomain"]?.ToString() ?? "";
            _identityService = identityService;
            _companyDepositRecordRepository = companyDepositRecordRepository;
            this._purchaseClient = purchaseClient;
            this.logger = logger;
        }


        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> AddCompanyAsync(CompanyDTO request)
        {
            var result = Result.Failed<bool>("新增公司异常");
            //判断名称是否存在
            var companyInfo = await _companyRepository.GetCompanyByName(request.Name, request.SimpleName);
            if (companyInfo != null)
            {
                return Result.Failed<bool>("公司名称已存在");
            }

            CompanyDO company = _mapper.Map<CompanyDO>(request);

            //company.BusinessLicense = company.BusinessLicense.Substring(company.BusinessLicense.IndexOf(".cn/") + 4, company.BusinessLicense.Length - company.BusinessLicense.IndexOf(".cn/") - 4);
            company.BusinessLicense = company.BusinessLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");
            //company.AccountOpeningLicense = company.AccountOpeningLicense.Substring(company.AccountOpeningLicense.IndexOf(".cn/") + 4, company.AccountOpeningLicense.Length - company.AccountOpeningLicense.IndexOf(".cn/") - 4);
            company.AccountOpeningLicense = company.AccountOpeningLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");
            company.CreateBy = string.IsNullOrEmpty(company.CreateBy) ? _identityService.GetUserName() : company.CreateBy;
            company.CreateTime = DateTime.Now;
            company.DepositAmount = 0;
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

        public async Task<ApiResult<long>> AddCompanyReturnIdAsync(CompanyDTO request)
        {
            var result = Result.Failed<long>("新增公司异常");
            //判断名称是否存在
            var companyInfo = await _companyRepository.GetCompanyByName(request.Name, request.SimpleName);
            if (companyInfo != null)
            {
                return Result.Failed<long>("公司名称已存在");
            }

            CompanyDO company = _mapper.Map<CompanyDO>(request);

            //company.BusinessLicense = company.BusinessLicense.Substring(company.BusinessLicense.IndexOf(".cn/") + 4, company.BusinessLicense.Length - company.BusinessLicense.IndexOf(".cn/") - 4);
            company.BusinessLicense = company.BusinessLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");
            //company.AccountOpeningLicense = company.AccountOpeningLicense.Substring(company.AccountOpeningLicense.IndexOf(".cn/") + 4, company.AccountOpeningLicense.Length - company.AccountOpeningLicense.IndexOf(".cn/") - 4);
            company.AccountOpeningLicense = company.AccountOpeningLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");
            company.CreateBy = string.IsNullOrEmpty(company.CreateBy) ? _identityService.GetUserName() : company.CreateBy;
            company.CreateTime = DateTime.Now;
            company.DepositAmount = 0;
            var id = await _companyRepository.AddCompanyAsync(company);

            result = new ApiResult<long>()
            {
                Data = id,
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };
            return result;
        }

        /// <summary>
        /// 注册公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddCompanyRegisterResponse> AddCompanyRegister(AddCompanyRegisterRequest request)
        {

            string companyName = $"{request.Province}{request.City}{request.UserName}";
            var companyInfo = await _companyRepository.GetCompanyByName(companyName);
            int tag = companyInfo.Exists(t => t.Name == companyName || t.SimpleName == companyName) ? 1 : 0;
            while (tag > 0)
            {
                var defaultName = $"{companyName}{tag}";
                var defaultCompany = companyInfo.Exists(t => t.Name == defaultName || t.SimpleName == defaultName);
                if (defaultCompany)
                {
                    tag++;
                }
                else
                {
                    companyName = defaultName;
                    tag = 0;
                }
            }

            CompanyDO company = new CompanyDO()
            {
                SimpleName = companyName,
                Name = companyName,
                Status = 1,
                Type = 0,
                Level = 0,
                Province = request.Province,
                City = request.City,
                District = request.District,
                Address = request.Address,
                ProvinceId = request.ProvinceId,
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                Mobile = request.Mobile,
                LegalPerson = request.UserName,
                LegalPersonPhone = request.Mobile,
                ServiceLevel = request.ServiceLevel,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now
            };
            var id = await _companyRepository.AddCompanyAsync(company);
            return new AddCompanyRegisterResponse()
            {
                CompanyId = id
            };
        }

        /// <summary>
        /// 根据公司ID查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompanyDTO> GetCompanyInfoByIdAsync(GetCompanyInfoByIdRequest request)
        {
            var companyInfo = await _companyRepository.GetAsync(request.Id);
            return _mapper.Map<CompanyDTO>(companyInfo);
        }

        /// <summary>
        /// 获取所以公司和门店简单信息
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllUnitResponse> GetAllUnitAsync()
        {
            List<UnitDTO> units = new List<UnitDTO>();
            //获取所以公司简单信息
            var companyAll = await _companyRepository.GetAllCompanyAsync();
            companyAll.ForEach(o =>
                units.Add(new UnitDTO() { Id = o.Id, Name = o.Name, Type = o.Type, Status = o.Status }
                ));
            //获取所以门店简单信息
            var shopAll = await _shopRepository.GetAllShopAsync();
            shopAll.ForEach(o =>
                units.Add(new UnitDTO() { Id = o.Id, Name = o.SimpleName, Type = 1, Status = o.Status }
                ));
            GetAllUnitResponse response = new GetAllUnitResponse();
            response.List = units;
            return response;
        }


        /// <summary>
        /// 查询一级公司列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPageCompanyListForShopAsync(
            GetPageCompanyListRequest request)
        {
            var data = await _companyRepository.GetPageCompanyListForShopAsync(request);
            List<CompanyPageListForShopDTO> companyList = _mapper.Map<List<CompanyPageListForShopDTO>>(data.Items);
            var companyIds = companyList.Select(o => o.Id).ToList();
            //查询公司下的门店数量
            var shopCountList = await _shopRepository.GetShopCountUnderTheCompanyAsync(companyIds);
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
        /// 修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateCompanyAsync(CompanyDTO request)
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
            company.BusinessLicense = company.BusinessLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");

            //company.BusinessLicense = company.BusinessLicense.Substring(company.BusinessLicense.IndexOf(".cn/") + 4, company.BusinessLicense.Length - company.BusinessLicense.IndexOf(".cn/") - 4);
            //company.AccountOpeningLicense = company.AccountOpeningLicense.Substring(company.AccountOpeningLicense.IndexOf(".cn/") + 4, company.AccountOpeningLicense.Length - company.AccountOpeningLicense.IndexOf(".cn/") - 4);
            company.AccountOpeningLicense = company.AccountOpeningLicense.Replace("https://m.aerp.com.cn/", "")
                .Replace("http://m.aerp.com.cn/", "");
            company.CreateBy = companyInfo.CreateBy;
            company.CreateTime = companyInfo.CreateTime;
            company.UpdateBy = string.IsNullOrEmpty(company.UpdateBy) ? _identityService.GetUserName() : company.UpdateBy;
            company.UpdateTime = DateTime.Now;
            var count = await _companyRepository.UpdateAsync(company, new List<string>()
            {
                "SimpleName", "Name", "ParentId", "Code", "Status", "Type", "Level", "ProvinceId", "CityId",
                "DistrictId", "Province", "City", "District", "Address", "Mobile", "Email", "Head", "HeadPhone",
                "RegisterMoney", "RegisterTime", "LegalPerson", "LegalPersonPhone", "BusinessLicense", "OpeningBank",
                "BankAccount", "AccountOpeningLicense", "Introduction", "License", "Examiner", "FailedExaminedReason",
                "IsDeleted", "UpdateBy", "UpdateTime","VenderId","SystemType"
            });
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


        public async Task<ApiResult<bool>> UpdateCompanyForSupplierAsync(CompanyDTO request)
        {
            var result = Result.Failed<bool>("公司信息异常");


            var companyInfo = await _companyRepository.GetAsync(request.Id);
            if (companyInfo == null)
            {
                return result;
            }

            CompanyDO company = _mapper.Map<CompanyDO>(request);
            company.UpdateBy = string.IsNullOrEmpty(company.UpdateBy) ? _identityService.GetUserName() : company.UpdateBy;
            company.UpdateTime = DateTime.Now;
            var count = await _companyRepository.UpdateAsync(company, new List<string>()
            {
                  "SimpleName", "Name",
                "ProvinceId", "CityId",
                "DistrictId", "Province", "City", "District", "Address", "Mobile", "Head", "HeadPhone",

            });
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

        /// <summary>
        /// 根据公司ID查询公司信息---微信小程序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompanySimpleInfoDTO> MiniGetCompanyInfo(MiniGetCompanyInfoRequest request)
        {
            var companyInfo = await _companyRepository.GetCompanyInfo(request.CompanyId);
            var result = _mapper.Map<CompanySimpleInfoDTO>(companyInfo);
            if (result != null)
            {
                result.ImgUrl = Domain + result.ImgUrl;
                result.Type = CommonHelper.GetDescriptionByEnum((CompanyTypeEnum)companyInfo.Type);
            }

            return result;
        }

        /// <summary>
        /// 根据门店ID查询公司信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<CompanyInfoDTO> GetCompanyInfoByShopId(GetCompanyByShopIdRequest request)
        {
            var companyInfo = await _companyRepository.GetCompanyInfoByShopId(request.ShopId);
            var result = _mapper.Map<CompanyInfoDTO>(companyInfo);
            return result;
        }

        /// <summary>
        /// 查询公司下的子公司和门店
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<CompanyLessInfoDTO>> GetCompanyAndShopByParentId(GetCompanyInfoByIdRequest request)
        {
            var items = await _companyRepository.GetCompanyAndShopByParentId(request.Id);
            var result = _mapper.Map<List<CompanyLessInfoDTO>>(items);
            return result;
        }

        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateCompanyStatus(UpdateCompanyStatusRequest request)
        {
            var res = Result.Failed<bool>("公司信息审核异常");
            var companyInfo = await _companyRepository.GetAsync(request.CompanyId);
            if (companyInfo == null)
            {
                throw new CustomException("公司信息异常");
            }

            if (request.Status == 4 && string.IsNullOrEmpty(request.FailedExaminedReason))
            {
                throw new CustomException("请填写审核原因");
            }

            request.UpdateBy = string.IsNullOrWhiteSpace(_identityService.GetUserName())
                ? string.Empty
                : _identityService.GetUserName();
            var num = await _companyRepository.UpdateCompanyStatus(request);
            if (num > 0)
            {
                res.Data = true;
                res.Code = ResultCode.Success;
                res.Message = CommonConstant.OperateSuccess;
            }

            return res;
        }

        /// <summary>
        /// OperateCompanyDeposit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> OperateCompanyDeposit(OperateCompanyDepositRequest request)
        {
            var company = await _companyRepository.GetAsync<CompanyDO>(request.CompanyId, true);
            if (company == null)
            {
                throw new CustomException($"公司Id：{request.CompanyId} 不存在");
            }

            if (request.DepositAmount + company.DepositAmount < 0)
            {
                throw new CustomException("提取金额大于押金剩余金额");
            }

            using (TransactionScope ts = new TransactionScope())
            {
                await _companyDepositRecordRepository.InsertAsync(new CompanyDepositRecordDO()
                {
                    CompanyId = request.CompanyId,
                    OperationType = request.OperateType.ToString(),
                    Description = EnumHelper.GetEnumDescription(request.OperateType),
                    Amount = request.DepositAmount,
                    Remark = request.Remark ?? string.Empty,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.SubmitBy,
                    UpdateTime = DateTime.Now
                });
                await _companyRepository.UpdateCompanyDeposit(request.CompanyId, request.DepositAmount,
                    request.SubmitBy);
                ts.Complete();
            }

            return true;
        }

        public async Task<List<VenderDTO>> GetVenders()
        {
            var result = await _purchaseClient.GetVenders();
            result.Add(new VenderDTO { Id = 0, VenderShortName = "-请选择-" });
            result = result.OrderBy(r => r.Id).ToList();
            return result;
        }

        /// <summary>
        /// 获取公司下的子公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<CompanyPageListForShopDTO>>> GetCompanySubs(GetPageCompanyListRequest request)
        {
            var res = new ApiResult<List<CompanyPageListForShopDTO>>();
            try
            {
                var companys = await _companyRepository.GetListAsync("where is_deleted = 0 and parent_id = @parent_id", new { parent_id = request.ParentId });

                var newComanys = await RecursionCompanies(companys, companys?.ToList());
                

                List<CompanyPageListForShopDTO> companyList = _mapper.Map<List<CompanyPageListForShopDTO>>(newComanys.OrderBy(_=>_.ParentId));
                res.Data = companyList;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                logger.Error($"GetCompanySubs_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        private async Task<IEnumerable<CompanyDO>> RecursionCompanies(IEnumerable<CompanyDO> companies, List<CompanyDO> result)
        {
            var parentIds = companies?.Select(_ => _.Id)?.ToList();
            if (parentIds?.Count() > 0)
            {
                var companys = await _companyRepository.GetListAsync("where is_deleted = 0 and parent_id in @ParentIds", new { ParentIds = parentIds });

                if (companys?.Count() > 0)
                {
                    result.AddRange(companys);
                    await RecursionCompanies(companys, result);
                }
            }
            return result;

        }

        public async Task<List<CompanyDTO>> GetFirstCompanyList(GetFirstCompanyListRequest request)
        {
            var companys = await _companyRepository.GetListAsync("where is_deleted = 0 and parent_id = 0 and status=2", null);

            List<CompanyDTO> companyList = _mapper.Map<List<CompanyDTO>>(companys);

            return companyList;
        }


    }
}
