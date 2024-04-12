using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using ApolloErp.Message.Sms;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys;
using Ae.Shop.Service.Dal.Repositorys.Employee;

namespace Ae.Shop.Service.Imp.Services
{
    public class EmployeeService : IEmployeeService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<EmployeeService> logger;
        private readonly IEmployeeRepository empRepo;
        private readonly IAccountAuthorityClient accountAuthorityClient;
        private readonly ICompanyService comSvc;
        private readonly IShopService shopSvc;
        private readonly SmsClient smsClient;
        private readonly IEmployeeGroupRepository _employeeGroupRepository;
        private readonly ITechnicianRepository techRepo;

        public EmployeeService(IMapper mapper, IConfiguration configuration, ApolloErpLogger<EmployeeService> logger,
            IEmployeeRepository empRepo,
            IAccountAuthorityClient accountAuthorityClient,
            ICompanyService comSvc,
            IShopService shopSvc,
            SmsClient smsClient, IEmployeeGroupRepository employeeGroupRepository,
            ITechnicianRepository techRepo
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;

            this.empRepo = empRepo;

            this.accountAuthorityClient = accountAuthorityClient;
            this.comSvc = comSvc;
            this.shopSvc = shopSvc;
            this.smsClient = smsClient;
            _employeeGroupRepository = employeeGroupRepository;
            this.techRepo = techRepo;
        }

        // ---------------------------------- 接口实现 --------------------------------------

        #region ！！！登录接口相关，请勿轻易对其修改！！！

        public async Task<List<EmployeeResDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req)
        {
            var resDo = await empRepo.GetEmpAndOrgListForLoginByAccountIdAsync(req);
            var res = mapper.Map<List<EmployeeResDTO>>(resDo);
            return res;
        }

        public async Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req)
        {
            var res = new EmployeePageResDTO();
            var resDto = await empRepo.GetEmpAndOrgPageForLoginByAccountIdAsync(req);

            if (null == resDto)
            {
                return res;
            }

            res.TotalItems = resDto.TotalItems;
            res.Items = resDto.Items?.ToList();

            if (req.EmployeeType == EmployeeType.Shop || req.EmployeeType == EmployeeType.None)
            {
                res?.Items?.ForEach(f =>
                {
                    if (f.EmployeeType == EmployeeType.Shop)
                    {
                        f.ShopTypeList.AddRange(shopSvc.GetShopTypes(f.ShopType));
                    }
                });
            }

            return res;
        }

        public async Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            var res = new EmployeePageResDTO();
            var resDto = await empRepo.GetEmpAndOrgPageForLoginByEmpIdAsync(req);

            if (null == resDto)
            {
                return res;
            }

            res.TotalItems = resDto.TotalItems;
            res.Items = resDto.Items?.ToList();

            if (req.EmployeeType == EmployeeType.Shop || req.EmployeeType == EmployeeType.None)
            {
                res?.Items?.ForEach(f =>
                {
                    if (f.EmployeeType == EmployeeType.Shop)
                    {
                        f.ShopTypeList.AddRange(shopSvc.GetShopTypes(f.ShopType));
                    }
                });
            }

            return res;
        }

        public async Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            var res = new EmployeePageResDTO();
            var resDto = await empRepo.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(req);

            if (null == resDto)
            {
                return res;
            }

            res.TotalItems = resDto.TotalItems;
            res.Items = resDto.Items?.ToList();

            if (req.EmployeeType == EmployeeType.Shop || req.EmployeeType == EmployeeType.None)
            {
                res?.Items?.ForEach(f =>
                {
                    if (f.EmployeeType == EmployeeType.Shop)
                    {
                        f.ShopTypeList.AddRange(shopSvc.GetShopTypes(f.ShopType));
                    }
                });
            }

            return res;
        }

        public async Task<EmployeePageResDTO> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req)
        {
            var res = new EmployeePageResDTO();
            var resDto = await empRepo.GetOrgRangePageForLoginByEmpIdExcCurOrgId(req);

            if (null == resDto)
            {
                return res;
            }

            res.TotalItems = resDto.TotalItems;
            res.Items = resDto.Items?.ToList();

            if (req.EmployeeType == EmployeeType.Shop || req.EmployeeType == EmployeeType.None)
            {
                res?.Items?.ForEach(f =>
                {
                    if (f.EmployeeType == EmployeeType.Shop)
                    {
                        f.ShopTypeList.AddRange(shopSvc.GetShopTypes(f.ShopType));
                    }
                });
            }

            return res;
        }

        public async Task<List<long>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req)
        {
            var res = new List<long>();
            var resDo = await empRepo.GetOrgRangeRoleIdList(req);

            resDo.ForEach(f =>
            {
                var list = f.RoleIds
                    .Replace("，", CommonConstant.SeparatorComma)
                    .Split(CommonConstant.SeparatorComma)
                    .Distinct().ToList();

                list.ForEach(l =>
                {
                    if (long.TryParse(l, out var tmp) && res.IndexOf(tmp) < CommonConstant.Zero)
                        res.Add(tmp);
                });
            });

            return res;
        }


        #endregion ！！！登录接口相关，请勿轻易对其修改！！！

        public async Task<List<EmployeeResDTO>> GetAllEmployeeListAsync()
        {
            var resDo = await empRepo.GetAllEmployeeListAsync();
            var res = mapper.Map<List<EmployeeResDTO>>(resDo);
            return res;
        }

        public async Task<List<EmployeeResDTO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req)
        {
            var resDo = await empRepo.GetEmployeeListByOrgIdAndTypeAsync(req);
            var res = mapper.Map<List<EmployeeResDTO>>(resDo);
            return res;
        }

        public async Task<EmployeePageResDTO> GetEmployeePage(EmployeePageReqDTO req)
        {
            var res = new EmployeePageResDTO();
            var resDto = await empRepo.GetEmployeePage(req);

            if (null == resDto)
            {
                return res;
            }

            res.Items = resDto.Items?.ToList();
            var employeeIds = res.Items?.Select(o => o.Id).ToList();
            var employeeRoles = await accountAuthorityClient.GetEmployeeRoleListByEmpIds(new EmployeeRoleListByEmpIdsReqDTO { EmployeeIds = employeeIds });
            foreach (var item in res.Items)
            {
                var employeeRole = employeeRoles.Find(r => r.EmployeeId == item.Id);
                if (employeeRole != null)
                {
                    var roles = employeeRole.Roles.Select(n => n.RoleName).ToList();
                    item.RoleName = roles.Any() ? string.Join(",", roles) : string.Empty;
                }
            }

            res.TotalItems = resDto.TotalItems;
            return res;
        }

        /// <summary>
        /// 查询员工基本信息（含角色）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<EmployeeInfoDTO> GetEmployeeInfo(EmployeeInfoReqDTO req)
        {
            var empInfoTask = empRepo.GetEmployeeInfo(req);
            var empRoleTask = accountAuthorityClient.GetEmployeeRoleListByEmpId(new EmployeeRoleListReqDTO { EmployeeId = req.EmployeeId });
            var empRoleRangeTask = empRepo.GetOrgRangeRoleListByEmpId(new OrgRangeRoleListForLoginReqDTO { EmployeeId = req.EmployeeId });

            Task.WaitAll(empInfoTask, empRoleTask, empRoleRangeTask);

            var res = mapper.Map<EmployeeInfoDTO>(empInfoTask.Result);

            var empRole = empRoleTask.Result;
            if (empRole != null && empRole.Any())
            {
                res.RoleIds = empRole.Select(s => s.RoleId).Distinct().ToList();
                //res.RoleIds.AddRange(empRole.Select(s => s.RoleId).Distinct());
            }

            var empRoleRange = empRoleRangeTask.Result;
            if (empRoleRange != null && empRoleRange.Any())
            {
                empRoleRange.ForEach(f =>
                {
                    res.OrgRanges.Add(new OrganizationRangeDTO
                    {
                        OrganizationId = f.OrganizationId,
                        Type = f.EmployeeType,
                        RoleId = f.RoleIds.IsNotNullOrWhiteSpace() ? f.RoleIds.Split(CommonConstant.SeparatorComma).Distinct().Select(long.Parse).ToList() : default
                    });
                });
            }

            return res;
        }

        /// <summary>
        /// 查询员工基本信息（简单）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<EmployeeSimpleInfoResponse> GetEmployeeSimpleInfo(EmployeeInfoReqDTO req)
        {
            var result = await empRepo.GetEmployeeInfo(req);

            var res = mapper.Map<EmployeeSimpleInfoResponse>(result);

            return res;
        }

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<EmployeeInfoDTO>> GetEmployeeListByShopIdAsync(GetEmployeeListByOrganizationIdRequest request)
        {
            var employeeList = await empRepo.GetEmployeeListByShopIdAsync(request.OrganizationId, request.Type);
            var result = mapper.Map<List<EmployeeInfoDTO>>(employeeList);
            return result;
        }

        public async Task<List<EmployeeInfoDTO>> GetEmployeeListByEmpId(List<string> empId)
        {
            var employeeList = await empRepo.GetEmployeeListByEmpId(empId);
            var result = mapper.Map<List<EmployeeInfoDTO>>(employeeList);
            return result;
        }

        public async Task<ApiResult<List<RoleListResVO>>> GetRoleListByOrgIdAndType(RoleListReqVO req)
        {
            ApiResult<List<RoleListResVO>> result = new ApiResult<List<RoleListResVO>>()
            {
                Data = new List<RoleListResVO>()
            };
            var reqDto = mapper.Map<RoleListReqDTO>(req);
            if (req.Type == RoleType.Shop)
            {
                var shopInfo = await shopSvc.GetShopSimpleInfoAsync(new GetShopRequest() { ShopId = req.OrganizationId });
                reqDto.Features = shopInfo.SystemType;
                var resDto = await accountAuthorityClient.GetRoleListByOrgIdAndType(reqDto);
                result = mapper.Map<ApiResult<List<RoleListResVO>>>(resDto);
            }
            else if (req.Type == RoleType.Company || req.Type == RoleType.Extend || req.Type == RoleType.Supplier)
            {
                var companyInfo = await comSvc.GetCompanyInfoByIdAsync(new GetCompanyInfoByIdRequest() { Id = req.OrganizationId });
                var resDto = await accountAuthorityClient.GetRoleListByOrgIdAndType(reqDto);
                //取虚拟角色
                reqDto.OrganizationId = 0;
                reqDto.Features = companyInfo.SystemType;
                var resDtotwo = await accountAuthorityClient.GetRoleListByOrgIdAndType(reqDto);
                foreach (var r in resDtotwo?.Data)
                {
                    resDto.Data.Add(r);
                }
                result = mapper.Map<ApiResult<List<RoleListResVO>>>(resDto);
            }


            return result;
        }

        public async Task<List<OrgRangeRolesVO>> GetRoleListByOrgId(long orgId)
        {
            var res = new List<OrgRangeRolesVO>();

            var orgs = await comSvc.GetCompanyAndShopByParentId(new GetCompanyInfoByIdRequest { Id = orgId });
            if (orgs == null && !orgs.Any()) return res;

            var req = new List<OrgEntityReqDTO>();
            orgs.ForEach(f =>
            {
                req.Add(new OrgEntityReqDTO
                {
                    OrganizationId = f.Id,
                    Type = (RoleType)f.Type,
                    SystemType = f.SystemType
                });
            });

            var resDto = await accountAuthorityClient.GetRoleListByOrgIds(req);
            var resTmp = mapper.Map<List<OrgRangeRolesVO>>(resDto);

            if (resTmp == null) return res;

            var comRoleList = resTmp.FindAll(f => f.Type != RoleType.Shop);
            var orgRoleDic = new Dictionary<string, OrgRangeRolesVO>();
            comRoleList.ForEach(f =>
            {
                var key = $"{f.OrganizationId}{CommonConstant.SeparatorVertical}{(int)f.Type}";
                if (!orgRoleDic.ContainsKey(key))
                {
                    orgRoleDic.Add(key, f);
                }
                else
                {
                    orgRoleDic[key] = f;
                }
            });

            var shopRoleObj = resTmp.Find(f => f.Type == RoleType.Shop);
            var roles = new List<RoleVO>();
            if (shopRoleObj?.Roles != null && shopRoleObj.Roles.Any())
            {
                roles = shopRoleObj?.Roles.Where((x, i) => shopRoleObj?.Roles.FindIndex(z => z.Id == x.Id) == i).ToList();
            }


            orgs.ForEach(f =>
            {
                var tmp = new OrgRangeRolesVO
                {
                    OrganizationId = f.Id,
                    OrganizationName = f.FullName,
                    Type = (RoleType)f.Type,
                };

                if (f.Type == (int)RoleType.Shop)
                {
                    if (roles != null && roles.Any())
                    {
                        var role = roles.FindAll(_ => _.Features == f.SystemType);
                        tmp.Roles.AddRange(role);
                    }
                }
                else
                {
                    var key = $"{f.Id}{CommonConstant.SeparatorVertical}{f.Type}";
                    if (orgRoleDic.ContainsKey(key))
                    {
                        tmp.Roles.AddRange(orgRoleDic[key].Roles);
                    }
                }

                res.Add(tmp);
            });

            return res;
        }

        public async Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName(AccountEntityReqByNameDTO req)
            => await accountAuthorityClient.CheckAccountIsExistByName(req);

        /// <summary>
        /// 创建或是编辑员工信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateOrUpdateEmployee(EmployeeEntityReqDTO req)
            => req.EmployeeId.IsNullOrWhiteSpace() ?
                await CreateEmployee(req) :
                await UpdateEmployee(req);
        public async Task<ApiResult> CreateEmployee(EmployeeEntityReqDTO req, bool isCreateShop = false, bool isSendMessage = false, string defaultRoleName = "")
        {
            var res = new ApiResult
            {
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };

            var employeeInfo = await empRepo.EmployeeVerify(req);
            if (employeeInfo != null)
            {
                res.Code = ResultCode.Failed;
                res.Message = CommonConstant.EmployeeExist;
                return res;
            }

            //创建或是获取员工账号
            var resAcct = await accountAuthorityClient.CreateAccountWithIdDefaultPwdAsync(new AccountCreateRequest
            {
                Name = req.Mobile,
                Password = req.Password,
                CreateBy = req.CreateBy,
                CreateById = req.UserId
            });
            if (resAcct.Code != ResultCode.Success) { return resAcct; }

            //var initPwd = "";
            //initPwd = req.Password.IsNotNullOrWhiteSpace() ? resAccount?.Data.InitPassword : CommonConstant.OperateSuccess;

            var account = resAcct?.Data;

            if (account == null)
            {
                if (isCreateShop)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("创建门店初始账号失败"));
                }

                res.Code = ResultCode.Failed;
                res.Message = CommonConstant.AddEmployeeFailed;
                return res;
            }

            req.AccountId = account.Id;
            var employee = GenEmployeeDO(req, true);

            //新增员工信息
            var empId = await empRepo.InsertAsync<string>(employee);

            if (empId.IsNullOrWhiteSpace())
            {
                if (!account.HasAccount)
                {
                    //添加员工失败，删除账户信息
                    await accountAuthorityClient.DeleteAccountById(new AccountDeleteByIdRequest { Id = account.Id, UpdateBy = req.UserId });
                }
            }

            //同步技师信息
            //await  AddOrEidtTechnician(req);

            //创建门店员工默认角色
            //1. GetEnabledDefaultRole: 配置创建门店员工时，是否添加默认角色
            //2. isCreateShop: 门店初始员工，分配默认角色

            var roleType = -1;
            if (req.Type == EmployeeType.Shop)
            {
                var shop = await shopSvc.GetShopByIdAsync(new GetShopRequest() { ShopId = req.OrganizationId });
                roleType = shop.SystemType;
            }
            else
            {
                var company = await comSvc.GetCompanyInfoByIdAsync(new GetCompanyInfoByIdRequest()
                {
                    Id = req.OrganizationId
                });

                roleType = company.SystemType;
                var getRoles = await accountAuthorityClient.GetRoleListByOrgIdAndType(new RoleListReqDTO()
                {
                    Features = roleType,
                    Type = (RoleType)company.Type,
                    OrganizationId = 0
                });
                if (getRoles?.Data?.Count() > 0)
                {
                    req.RoleIds = new List<long>();
                    var roleId = getRoles?.Data?.Where(_ => _.Name.Trim() == defaultRoleName)?.FirstOrDefault()?.Id ?? 0;
                    if (roleId > 0)
                        req.RoleIds.Add(roleId);
                }
            }

            if (isCreateShop && req.Type == EmployeeType.Shop && GetEnabledDefaultRole())
            {
                var isSuccess = await accountAuthorityClient.AddShopEmployeeDefaultRole(new EmployeeDefaultRoleClientRequest
                {
                    EmployeeId = empId,
                    Operator = req.CreateBy,
                    OperatorId = CommonConstant.DefaultCustomGuidStr,
                    Type = roleType//根据系统版本来判断用什么角色了
                }); ;

                if (!isSuccess)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("创建门店账号员工默认角色失败"));
                }
            }

            //添加员工角色
            if (req.RoleIds.Any())
            {
                var isSucc = await accountAuthorityClient.EditEmployeeRoleWithRoleId(new EmployeeRoleSaveReqWithRoleIdDTO
                {
                    EmployeeId = empId,
                    RoleIds = req.RoleIds.Distinct().ToList(),
                    Operator = req.CreateBy,
                    OperatorId = CommonConstant.DefaultCustomGuidStr
                });

                if (!isSucc)
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("创建员工角色失败"));
            }

            //添加员工管辖范围
            if (req.OrgRanges.Any())
            {
                var isSucc = await empRepo.EditEmployeeRangeRole(GenEmployeeOrganizationRangeRoleDO(req, empId));

                if (!isSucc)
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("创建员工管辖范围失败"));
            }

            #region H5注册门店时，发送短信

            //发送短信通知
            if (isCreateShop && isSendMessage)
            {
                var pwd = req.Mobile.Substring(3);
                var sms = new SmsParameter
                {
                    PhoneNumbers = req.Mobile,
                    SignName = configuration["AliSms:SignName"],
                    TemplateCode = configuration["AliSms:CreateShopUserCode"],
                    TemplateParam = JsonConvert.SerializeObject(new { phone = req.Mobile, password = pwd })
                };
                if (account.HasAccount)
                {
                    pwd = "您的原密码";
                    //sms.TemplateCode = configuration["AliSms:CreateShopUserCode"];
                    sms.TemplateParam = JsonConvert.SerializeObject(new { phone = req.Mobile, password = pwd });

                }
                var sendSmsResult = smsClient.SendSms(sms);
                var isSendSuccess = sendSmsResult.Code.Equals("10000");
            }

            #endregion H5注册门店时，发送短信

            res.Message = CommonConstant.OperateSuccess; //initPwd;
            return res;
            //return new ApiResult
            //{
            //    Code = ResultCode.Success,
            //    Message = initPwd //CommonConstant.OperateSuccess
            //};
        }
        private async Task<ApiResult> UpdateEmployee(EmployeeEntityReqDTO req)
        {
            var res = new ApiResult
            {
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };

            var employeeInfo = await empRepo.EmployeeVerify(req);
            if (employeeInfo != null)
            {
                res.Code = ResultCode.Failed;
                res.Message = "手机号已存在，请重新输入";
                return res;
            }

            //编辑员工信息
            var employee = GenEmployeeDO(req);

            var ud = await empRepo.UpdateEmployeeById(employee);
            if (!ud)
            {
                res.Code = ResultCode.Failed;
                res.Message = CommonConstant.EditEmployeeFailed;
                return res;
            }

            //更新技师信息
            //await AddOrEidtTechnician(req);

            //编辑员工角色
            if (req.RoleIds.Any())
            {
                var isSucc = await accountAuthorityClient.EditEmployeeRoleWithRoleId(new EmployeeRoleSaveReqWithRoleIdDTO
                {
                    EmployeeId = req.EmployeeId,
                    RoleIds = req.RoleIds.Distinct().ToList(),
                    Operator = req.CreateBy,
                    OperatorId = CommonConstant.DefaultCustomGuidStr
                });

                if (!isSucc)
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("编辑员工角色失败"));
            }

            //编辑员工管辖范围
            if (req.OrgRanges.Any())
            {
                var isSucc = await empRepo.EditEmployeeRangeRole(GenEmployeeOrganizationRangeRoleDO(req, req.EmployeeId));

                if (!isSucc)
                    logger.Error(JsonConvert.SerializeObject(req).GenArgumentErrorInfo("编辑员工管辖范围失败"));
            }

            return res;
        }

        /// <summary>
        /// 获取门店员工列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopEmployeeDTO>> GetShopEmployeePagedListAsync(BasePageShopRequest request)
        {
            var result = await empRepo.GetShopEmployeePagedListAsync(request);
            return mapper.Map<ApiPagedResultData<ShopEmployeeDTO>>(result);
        }

        public List<EmployeeLevelListResDTO> GetEmployeeLevelList()
        {
            return (from EmployeeLevelEnum item in Enum.GetValues(typeof(EmployeeLevelEnum))
                    select new EmployeeLevelListResDTO
                    {
                        Id = Convert.ToInt32(item),
                        Name = item.GetEnumDescription()
                    }).ToList();
        }

        public async Task<bool> DeleteEmployeeById(EmployeeDeleteReqByIdDTO req)
        {
            var res = await empRepo.DeleteEmployeeById(req);
            return res;
        }

        public async Task<bool> BatchDeleteEmployeeById(EmployeeBatchDeleteReqByIdDTO req)
        {
            var res = await empRepo.BatchDeleteEmployeeById(req);
            return res;
        }

        public async Task<ApiPagedResultData<TechnicianPageDTO>> GetTechnicianPage(TechnicanPageReqDTO req)
        {
            //TODO: 设置获取技师接口默认PageSize为100，后续若有问题，再行修复（目前每个门店技师的人数不会太多）
            req.PageSize = CommonConstant.OneHundred;

            var resDo = await empRepo.GetTechnicianPage(req);
            if (resDo == null || resDo.TotalItems == CommonConstant.Zero)
            {
                return new ApiPagedResultData<TechnicianPageDTO>();
            }

            var resDto = mapper.Map<ApiPagedResultData<TechnicianPageDTO>>(resDo);

            //TODO: 把最近下工的技师排在最后，即技师按下工时间做升序排列

            var res = new ApiPagedResultData<TechnicianPageDTO>
            {
                Items = new List<TechnicianPageDTO>(),
                TotalItems = resDto.TotalItems
            };
            resDto.Items
                .MoveToFront(f => f.Id == req.EmployeeId)
                .ForEach(f => res.Items.Add(f));

            return res;
        }


        // ---------------------------------- 私有方法 --------------------------------------
        private bool GetEnabledDefaultRole()
        {
            var tmp = configuration["ShopManageBiz:EnabledShopDefaultRole"];
            if (!bool.TryParse(tmp, out var enabled))
            {
                enabled = false;
            }
            return enabled;
        }

        private EmployeeDO GenEmployeeDO(EmployeeEntityReqDTO req, bool isCreate = false)
        {
            var emp = new EmployeeDO
            {
                Name = req.Name,
                Email = req.Email,
                Mobile = req.Mobile,
                Identity = req.Identity,
                Number = req.Number,
                Gender = req.Gender,
                OrganizationId = req.OrganizationId,
                DepartmentId = req.DepartmentId,
                Type = req.Type,
                WeChat = req.WeChat,
                QQ = req.QQ,
                Level = Convert.ToInt32(req.Level),
                WorkKindId = req.WorkKindId,
                JobId = req.JobId,
                Avatar = req.Avatar,
                QualificationCertificate = string.Join(CommonConstant.SeparatorSemicolon, req.QualificationCertificate ?? new List<string>()),
                Description = req.Description,
                IsDeleted = req.IsNeedLogin
            };

            if (isCreate)
            {
                emp.Id = Guid.NewGuid().ToString();
                emp.AccountId = req.AccountId;
                emp.DimissionType = DimissionType.OnJob;
                emp.CreateBy = req.CreateBy;
                emp.CreateTime = DateTime.Now;

                return emp;
            }

            emp.Id = req.EmployeeId;
            emp.UpdateBy = req.UpdateBy;
            emp.UpdateTime = DateTime.Now;
            return emp;
        }

        private EmployeeRangeRoleSaveReqDO GenEmployeeOrganizationRangeRoleDO(EmployeeEntityReqDTO req, string empId)
        {
            var res = new EmployeeRangeRoleSaveReqDO
            {
                EmployeeId = empId,
                Operator = req.EmployeeId.IsNullOrWhiteSpace() ? req.CreateBy : req.UpdateBy,
                UserId = req.UserId
            };

            req.OrgRanges.ForEach(f =>
            {
                res.EmpOrgRoles.Add(new EmployeeOrganizationRangeDO
                {
                    EmployeeId = empId,
                    OrganizationId = f.OrganizationId,
                    Type = f.Type,
                    RoleIds = string.Join(CommonConstant.SeparatorComma, f.RoleId),
                    CreateBy = res.Operator
                });
            });

            return res;
        }

        public async Task<ApiResult> AddOrEidtEmployeeGroup(EmployeeGroupDTO req)
        {
            var employeeDo = mapper.Map<EmployeeGroupDO>(req);


            if (employeeDo.Id > 0)
            {
                employeeDo.UpdateTime = DateTime.Now;
                await _employeeGroupRepository.UpdateAsync(employeeDo, new List<string>() { "GroupName", "GroupLeader", "UpdateBy", "UpdateTime", "EmployeeId" , "Name", "Mobile" });
            }
            else
            {
                //var isRepeate = await _employeeGroupRepository.GetListAsync(" where is_deleted=0 and group_name=@GroupName", new { GroupName = req.GroupName });

                //if (isRepeate?.Count() > 0)
                //{
                //    return Result.Failed("分组组名已存在");
                //}

                //employeeDo.Id = Guid.NewGuid().ToString();
                employeeDo.CreateTime = DateTime.Now;
                employeeDo.ShopId = 7;
                employeeDo.IsDeleted = 0;
                await _employeeGroupRepository.InsertAsync(employeeDo);
            }

            return Result.Success();
        }

        public async Task<ApiResult<List<EmployeeGroupDTO>>> GetEmployeeGroupList(GetEmployeeGroupListRequest request)
        {
            var dalData = await _employeeGroupRepository.GetEmployeeGroupList(request);

            return new ApiResult<List<EmployeeGroupDTO>>()
            {
                Code = ResultCode.Success,
                Data = dalData
            };


        }

        public async Task<ApiResult> AddOrEidtTechnician(EmployeeEntityReqDTO req)
        {
            if (req.AccountId.IsNullOrWhiteSpace() && req.EmployeeId.IsNullOrWhiteSpace())
            {
                return Result.Success(); //不更新
            }
            if (req.AccountId.IsNullOrWhiteSpace())
            {
                var employeeInfo = await empRepo.GetAsync(req.EmployeeId);
                if (employeeInfo == null || employeeInfo.AccountId.IsNullOrWhiteSpace())
                    return Result.Success(); //不更新
                req.AccountId = employeeInfo.AccountId;
            }
            var techInfo = await techRepo.GetTechnicianInfo(req.AccountId);
            var technicianDO = GenTechnicianDO(req, techInfo == null);

            logger.Info("AddOrEidtTechnician:" + JsonConvert.SerializeObject(technicianDO));

            if (techInfo != null)
            {
                technicianDO.Id = techInfo.Id;
                await techRepo.UpdateAsync(technicianDO, new List<string>() { "JobId", "WorkKindId", "UpdateBy", "UpdateTime", "WorkKindLevel" });
            }
            else
            {
                logger.Info("InsertAsync");
                await techRepo.InsertAsync<string>(technicianDO);
            }

            return Result.Success();
        }

        private TechnicianDO GenTechnicianDO(EmployeeEntityReqDTO req, bool isCreate = false)
        {
            var emp = new TechnicianDO
            {
                Name = req.Name,
                Email = req.Email,
                Mobile = req.Mobile,
                Identity = req.Identity,
                //Number = req.Number,
                Gender = (sbyte) req.Gender.ToInt(),
                OrganizationId = req.OrganizationId,
                DepartmentId = req.DepartmentId,
                Type = (sbyte)req.Type.ToInt(),
                WeChat = req.WeChat,
                QQ = req.QQ,
                WorkKindLevel = Convert.ToInt32(req.Level),
                WorkKindId = req.WorkKindId,
                JobId = req.JobId,
                Avatar = req.Avatar,
                //QualificationCertificate = string.Join(CommonConstant.SeparatorSemicolon, req.QualificationCertificate ?? new List<string>()),
                Description = req.Description,
                IsDeleted = 0
            };

            if (isCreate)
            {
                emp.Id = Guid.NewGuid().ToString();
                emp.AccountId = req.AccountId;
                emp.DimissionType = (sbyte)DimissionType.OnJob.ToInt();
                emp.CreateBy = req.CreateBy;
                emp.CreateTime = DateTime.Now;

                return emp;
            }

            //emp.Id = req.EmployeeId;
            emp.UpdateBy = req.UpdateBy;
            emp.UpdateTime = DateTime.Now;
            return emp;
        }


    }
}