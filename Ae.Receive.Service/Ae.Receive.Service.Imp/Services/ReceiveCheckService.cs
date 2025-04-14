using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Common.Helper;
using Ae.Receive.Service.Core.Enums;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Request.ReceiveCheck;
using Ae.Receive.Service.Core.Response.ReceiveCheck;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using Ae.Receive.Service.Dal.Repositorys;
using Ae.Receive.Service.Dal.Repositorys.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ae.Receive.Service.Common.Extension;
using System.Text.RegularExpressions;
using Ae.Receive.Service.Core.Model.ReceiveCheck;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Request.Vehicle;
using Ae.Receive.Service.Client.Request;
using AutoMapper;
using Ae.Receive.Service.Client.Request.Shop;
using Ae.Receive.Service.Client.Model.Shop;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Imp.Services
{
    public class ReceiveCheckService : IReceiveCheckService
    {
        private readonly IShopReceiveRepository shopReceiveRepository;
        private readonly IShopReceiveCheckRepository shopReceiveCheckRepository;
        private readonly IShopCheckPropertyRepository shopCheckPropertyRepository;
        private readonly IShopReceiveCheckLogRepository shopReceiveCheckLogRepository;
        private readonly IShopCheckResultRepository shopCheckResultRepository;
        private readonly IShopCheckResultImageRepository shopCheckResultImageRepository;
        private readonly IShopReceiveResultWordRepository shopReceiveResultWordRepository;
        private readonly IShopReceiveCheckSubItemRepository shopReceiveCheckSubItemRepository;
        private readonly IShopReceiveCheckResultWordRepository shopReceiveCheckResultWordRepository;
        private readonly IShopReceiveResultWordAndSubItemRepository shopReceiveResultWordAndSubItemRepository;
        private readonly IShopReceiveResultWordComputeRuleRepository shopReceiveResultWordComputeRuleRepository;
        private readonly IShopReceiveSubItemComputeRuleRepository shopReceiveSubItemComputeRuleRepository;
        private readonly IShopReceiveCheckReportRepository shopReceiveCheckReportRepository;
        private readonly IArrivalRepository arrivalRepository;
        private readonly IVehicleClient vehicleClient;
        private readonly IUserClient userClient;
        private readonly IShopManageClient shopManageClient;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private const int CategoryId = 1;
        private readonly IShopArrivalCarReportRepository shopArrivalCarReportRepository;

        public ReceiveCheckService(IShopReceiveRepository shopReceiveRepository,
            IShopReceiveCheckRepository shopReceiveCheckRepository,
            IShopCheckPropertyRepository shopCheckPropertyRepository,
            IShopReceiveCheckLogRepository shopReceiveCheckLogRepository,
            IShopCheckResultRepository shopCheckResultRepository,
            IShopCheckResultImageRepository shopCheckResultImageRepository,
            IConfiguration configuration, IShopReceiveResultWordRepository shopReceiveResultWordRepository,
            IShopReceiveCheckSubItemRepository shopReceiveCheckSubItemRepository,
            IShopReceiveCheckResultWordRepository shopReceiveCheckResultWordRepository,
            IShopReceiveResultWordAndSubItemRepository shopReceiveResultWordAndSubItemRepository,
            IShopReceiveResultWordComputeRuleRepository shopReceiveResultWordComputeRuleRepository,
            IShopReceiveSubItemComputeRuleRepository shopReceiveSubItemComputeRuleRepository,
            IVehicleClient vehicleClient, IUserClient userClient, IMapper mapper,
            IShopReceiveCheckReportRepository shopReceiveCheckReportRepository,
            IShopManageClient shopManageClient, IArrivalRepository arrivalRepository,
            IShopArrivalCarReportRepository shopArrivalCarReportRepository)
        {
            this.shopReceiveRepository = shopReceiveRepository;
            this.shopReceiveCheckRepository = shopReceiveCheckRepository;
            this.shopCheckPropertyRepository = shopCheckPropertyRepository;
            this.shopReceiveCheckLogRepository = shopReceiveCheckLogRepository;
            this.shopCheckResultRepository = shopCheckResultRepository;
            this.shopCheckResultImageRepository = shopCheckResultImageRepository;
            this.configuration = configuration;
            this.shopReceiveResultWordRepository = shopReceiveResultWordRepository;
            this.shopReceiveCheckSubItemRepository = shopReceiveCheckSubItemRepository;
            this.shopReceiveCheckResultWordRepository = shopReceiveCheckResultWordRepository;
            this.shopReceiveResultWordAndSubItemRepository = shopReceiveResultWordAndSubItemRepository;
            this.shopReceiveResultWordComputeRuleRepository = shopReceiveResultWordComputeRuleRepository;
            this.shopReceiveSubItemComputeRuleRepository = shopReceiveSubItemComputeRuleRepository;
            this.vehicleClient = vehicleClient;
            this.userClient = userClient;
            this.mapper = mapper;
            this.shopReceiveCheckReportRepository = shopReceiveCheckReportRepository;
            this.shopManageClient = shopManageClient;
            this.arrivalRepository = arrivalRepository;
            this.shopArrivalCarReportRepository = shopArrivalCarReportRepository;
        }

        /// <summary>
        /// 检查项目首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckMainDataResponse> GetCheckMainData(CheckMainDataRequest request)
        {
            CheckMainDataResponse result = new CheckMainDataResponse();
            List<ShopReceiveCheckLogDo> checkLogData = new List<ShopReceiveCheckLogDo>();
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId);//检查报告主数据
            var propertyDataTask = shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, 0);//检查报告大类配置
            await Task.WhenAll(receiveDataTask, checkDataTask, propertyDataTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            var propertyData = propertyDataTask.Result ?? new List<ShopCheckPropertyDo>();
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }
            if (checkData != null)
            {
                checkLogData = await shopReceiveCheckLogRepository.GetShopReceiveCheckLog(checkData.Id, CategoryId);
            }
            result.MainData = new CheckMainDataVo
            {
                CheckId = checkData?.Id ?? 0,
                Status = checkData?.CheckStatus ?? 0,
                UserName = receiveData.UserName,
                UserTel = receiveData.UserTel,
                UserTelDisplay = CommonHelper.FormatTel(receiveData.UserTel),
                CarPlate = receiveData.CarNo,
                BrandUrl = configuration["QiNiuImageDomain"] + CommonHelper.GetLogoUrlByName(receiveData.Brand),
                CarType = receiveData.Vehicle
            };

            result.ProjectClassify = GetProjectClassify(propertyData, checkLogData);

            return result;
        }

        /// <summary>
        /// 获取客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CustomerDescriptionResponse> GetCustomerDescription(CheckMainDataRequest request)
        {
            CustomerDescriptionResponse result = new CustomerDescriptionResponse
            {
                CheckId = 0,
                Narration = "",
                CommonSense = new List<string>()
                {
                    "油耗高","检查刹车片","加速无力","发动机异响","轮胎调整"
                }
            };
            var checkData = await GetReceiveCheckData(request.RecId, request.CheckId);//检查报告主数据
            if (checkData != null)
            {
                result.CheckId = checkData.Id;
                result.Narration = checkData.Narration;
            }

            return result;
        }

        /// <summary>
        /// 获取仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CarDashboardResponse> GetCarDashboard(CheckMainDataRequest request)
        {
            CarDashboardResponse result = new CarDashboardResponse();
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId);//检查报告主数据
            await Task.WhenAll(receiveDataTask, checkDataTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }
            if (checkData != null)
            {
                result.CheckId = checkData.Id;
                result.VinCode = checkData.VinCode;
                result.Mileage = checkData.Mileage;
                result.DashboardImg = checkData.DashboardImg;
            }

            if (string.IsNullOrEmpty(result.VinCode))
            {
                result.VinCode = await shopReceiveCheckRepository.GetVinCodeByCarPlate(receiveData.CarNo);
            }

            result.LightCheckResult = await GetNormalProjectResult(checkData?.Id ?? 0, ReceiveCheckClassfyEnum.Dashboard.ToString());

            return result;
        }

        /// <summary>
        /// 获取附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OtherInspectionResponse> GetOtherInspection(CheckMainDataRequest request)
        {
            OtherInspectionResponse result = new OtherInspectionResponse();

            var checkData = await GetReceiveCheckData(request.RecId, request.CheckId);//检查报告主数据

            long checkId = checkData?.Id ?? 0;

            result.CheckId = checkId;

            var checkResult = await GetNormalProjectResult(checkId, ReceiveCheckClassfyEnum.Other.ToString());

            result.OtherProjectResult = checkResult.Select(_ => new OtherProjectResult
            {
                ProjectId = _.ProjectId,
                ProjectName = _.ProjectName,
                KeyName = _.KeyName,
                ResultValue = _.ResultValue,
                Image = _.Image.Select(t => t.Url).ToList()
            }).ToList();

            return result;
        }

        /// <summary>
        /// 获取内饰/外观
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<InOrOutlookInspectionResponse> GetInOrOutlookInspection(InOrOutlookInspectionRequest request)
        {
            InOrOutlookInspectionResponse result = new InOrOutlookInspectionResponse();
            var checkData = await GetReceiveCheckData(request.RecId, request.CheckId);//检查报告主数据

            long checkId = checkData?.Id ?? 0;

            result.CheckId = checkId;

            result.CheckResult = await GetNormalProjectResult(checkId, request.CheckModuleCode);

            return result;
        }

        /// <summary>
        /// 保存客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveCustomerDescription(SaveCustomerDescriptionRequest request)
        {
            string submitBy = request.SubmitBy ?? string.Empty;
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId, false);//检查报告主数据
            await Task.WhenAll(receiveDataTask, checkDataTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }

            if (checkData == null)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    ReceiveId = receiveData.Id,
                    ShopId = receiveData.ShopId,
                    CheckStatus = 0,
                    Narration = request.Narration,
                    UserId = receiveData.UserId,
                    UserTel = receiveData.UserTel,
                    UserName = receiveData.UserName,
                    CarId = receiveData.CarId,
                    CarPlate = receiveData.CarNo,
                    VersionNum = 1,
                    CreateBy = submitBy,
                    CreateTime = DateTime.Now,
                    UpdateBy = submitBy,
                    UpdateTime = DateTime.Now
                };

                return await InsertReceiveCheck(checkResultDo, ReceiveCheckClassfyEnum.CustomerDescription, new List<NormalProjectRequest>(), submitBy, 0);
            }
            else if (checkData.CheckStatus == 0)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    Id = checkData.Id,
                    Narration = request.Narration,
                    UpdateBy = request.SubmitBy ?? string.Empty,
                    UpdateTime = DateTime.Now
                };

                var updateField = new List<string> { "Narration", "UpdateBy", "UpdateTime" };

                return await UpdateReceiveCheck(checkResultDo, updateField, ReceiveCheckClassfyEnum.CustomerDescription, new List<NormalProjectRequest>(), submitBy, 0);
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 保存仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveCarDashboard(SaveCarDashboardRequest request)
        {
            ReceiveCheckClassfyEnum classfyEnum = ReceiveCheckClassfyEnum.Dashboard;
            var submitBy = request.SubmitBy ?? string.Empty;
            var now = DateTime.Now;
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId, false);//检查报告主数据
            var totalCountTask = GetNormalTotalCountByProject(classfyEnum);//总数量
            await Task.WhenAll(receiveDataTask, checkDataTask, totalCountTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            var totalCount = totalCountTask.Result;
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }

            var checkProject = request.LightCheckResult ?? new List<NormalProjectRequest>();

            if (checkData == null)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    ReceiveId = receiveData.Id,
                    ShopId = receiveData.ShopId,
                    CheckStatus = 0,
                    VinCode = request.VinCode,
                    Mileage = request.Mileage,
                    DashboardImg = request.DashboardImg,
                    UserId = receiveData.UserId,
                    UserTel = receiveData.UserTel,
                    UserName = receiveData.UserName,
                    CarId = receiveData.CarId,
                    CarPlate = receiveData.CarNo,
                    VersionNum = 1,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await InsertReceiveCheck(checkResultDo, classfyEnum, checkProject, submitBy, totalCount);
            }
            else if (checkData.CheckStatus == 0)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    Id = checkData.Id,
                    VinCode = request.VinCode,
                    Mileage = request.Mileage,
                    DashboardImg = request.DashboardImg,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };
                var updateField = new List<string> { "VinCode", "Mileage", "DashboardImg", "UpdateBy", "UpdateTime" };

                return await UpdateReceiveCheck(checkResultDo, updateField, classfyEnum, checkProject, submitBy, totalCount);
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 保存附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveOtherInspection(SaveOtherInspectionRequest request)
        {
            var submitBy = request.SubmitBy ?? string.Empty;
            var now = DateTime.Now;
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId, false);//检查报告主数据
            await Task.WhenAll(receiveDataTask, checkDataTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }

            var checkProject = request.OtherProjectResult ?? new List<OtherProjectResult>();

            var errorProject = checkProject.Select(_ => new NormalProjectRequest
            {
                ProjectId = (int)_.ProjectId,
                ResultValue = _.ResultValue,
                Image = _.Image?.Select(t => new ErrorImageModel { Url = t })?.ToList() ?? new List<ErrorImageModel>()
            }).ToList();

            if (checkData == null)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    ReceiveId = receiveData.Id,
                    ShopId = receiveData.ShopId,
                    CheckStatus = 0,
                    UserId = receiveData.UserId,
                    UserTel = receiveData.UserTel,
                    UserName = receiveData.UserName,
                    CarId = receiveData.CarId,
                    CarPlate = receiveData.CarNo,
                    VersionNum = 1,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await InsertReceiveCheck(checkResultDo, ReceiveCheckClassfyEnum.Other, errorProject, submitBy, 0);
            }
            else if (checkData.CheckStatus == 0)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    Id = checkData.Id,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await UpdateReceiveCheck(checkResultDo, new List<string>(), ReceiveCheckClassfyEnum.Other, errorProject, submitBy, 0);
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 保存外观/内饰
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveInOrOutlookInspection(SaveInOrOutlookInspectionRequest request)
        {
            ReceiveCheckClassfyEnum receiveCheckClassfyEnum = (ReceiveCheckClassfyEnum)Enum.Parse(typeof(ReceiveCheckClassfyEnum), request.CheckModuleCode);
            var submitBy = request.SubmitBy ?? string.Empty;
            var now = DateTime.Now;
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId, false);//检查报告主数据
            var totalCountTask = GetNormalTotalCountByProject(receiveCheckClassfyEnum);//总数量
            await Task.WhenAll(receiveDataTask, checkDataTask, totalCountTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            var totalCount = totalCountTask.Result;
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }

            var checkProject = request.CheckResult ?? new List<NormalProjectRequest>();

            if (checkData == null)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    ReceiveId = receiveData.Id,
                    ShopId = receiveData.ShopId,
                    CheckStatus = 0,
                    UserId = receiveData.UserId,
                    UserTel = receiveData.UserTel,
                    UserName = receiveData.UserName,
                    CarId = receiveData.CarId,
                    CarPlate = receiveData.CarNo,
                    VersionNum = 1,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await InsertReceiveCheck(checkResultDo, receiveCheckClassfyEnum, checkProject, submitBy, totalCount);
            }
            else if (checkData.CheckStatus == 0)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    Id = checkData.Id,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await UpdateReceiveCheck(checkResultDo, new List<string>(), receiveCheckClassfyEnum, checkProject, submitBy, totalCount);
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 升级项目查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UpgradeCheckItemResponse> GetUpgradeCheckItem(UpgradeCheckItemRequest request)
        {
            UpgradeCheckItemResponse result = new UpgradeCheckItemResponse();
            List<ShopCheckResultDo> checkResult = new List<ShopCheckResultDo>();
            List<ShopReceiveCheckResultWordDo> checkResultWord = new List<ShopReceiveCheckResultWordDo>();
            List<ShopCheckResultImageDo> checkResultImage = new List<ShopCheckResultImageDo>();

            var checkData = await GetReceiveCheckData(request.RecId, request.CheckId); //检查报告主数据
            long checkId = checkData?.Id ?? 0;
            result.CheckId = checkId;
            var configTask = shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, -1); //配置
            var subItemTask = shopReceiveCheckSubItemRepository.GetShopReceiveCheckSubItem(); //配置项 关联 结果项
            var subItemWithWordTask =
                shopReceiveResultWordAndSubItemRepository.GetShopReceiveResultWordAndSubItem(); //结果 - 结果词
            var resultWordTask = shopReceiveResultWordRepository.GetShopReceiveResultWord(); //结果词

            if (checkId > 0)
            {
                var checkResultTask = shopCheckResultRepository.GetShopCheckResult(checkId, CategoryId, 1); //结果数据
                var checkResultWordTask =
                    shopReceiveCheckResultWordRepository.GetShopReceiveCheckResultWord(checkId, CategoryId); //结果词结果
                var checkImageTask =
                    shopCheckResultImageRepository.GetCheckResultImageByCheckId(checkId, CategoryId); //图片
                await Task.WhenAll(checkResultTask, checkResultWordTask, checkImageTask);
                checkResult = checkResultTask.Result ?? new List<ShopCheckResultDo>();
                checkResultWord = checkResultWordTask.Result ?? new List<ShopReceiveCheckResultWordDo>();
                checkResultImage = checkImageTask.Result ?? new List<ShopCheckResultImageDo>();
            }

            await Task.WhenAll(configTask, subItemTask, subItemWithWordTask, resultWordTask);
            var config = configTask.Result ?? new List<ShopCheckPropertyDo>();
            var subItem = subItemTask.Result ?? new List<ShopReceiveCheckSubItemDo>();
            var subItemWithWord = subItemWithWordTask.Result ?? new List<ShopReceiveResultWordAndSubItemDo>();
            var resultWord = resultWordTask.Result ?? new List<ShopReceiveResultWordDo>();
            var currentClassfy = config.FirstOrDefault(_ => _.KeyName == request.CheckModuleCode && _.ParentId == 0);
            if (currentClassfy != null)
            {
                result.Id = currentClassfy.Id;
                result.DisplayName = currentClassfy.DisplayName;
                List<UpgradeCheckproject> childClassfy = new List<UpgradeCheckproject>();
                var chaildProject = config.Where(_ => _.ParentId == currentClassfy.Id).ToList();
                chaildProject.ForEach(_ =>
                {
                    UpgradeCheckproject project = new UpgradeCheckproject
                    {
                        Id = _.Id,
                        DisplayName = _.DisplayName,
                        DisplayDes = _.DisplayDes,
                        Required = _.IsRequire,
                        ErrorImages = checkResultImage.Where(d => d.PropertyId == _.Id).Select(d => d.Url).ToList(),
                        CheckItemMain = _.IsCheckItemMain
                    };
                    var subProject = config.Where(x => x.ParentId == _.Id).ToList();
                    if (subProject.Any())
                    {
                        List<UpgradeCheckproject> subClassfy = new List<UpgradeCheckproject>();
                        subProject.ForEach(x =>
                        {
                            var subCheckItem = subItem.Where(t => t.ConfigId == x.Id).ToList();
                            subClassfy.Add(new UpgradeCheckproject
                            {
                                Id = x.Id,
                                DisplayName = x.DisplayName,
                                DisplayDes = x.DisplayDes,
                                Required = x.IsRequire,
                                CheckItemMain = x.IsCheckItemMain,
                                CheckSubItems = subCheckItem.Select(t => new CheckSubItem
                                {
                                    CheckCount = t.CheckCount,
                                    CheckItemMainId = _.Id,
                                    Id = t.Id,
                                    CheckType = t.CheckType,
                                    Chosen = IniCheckChoose(t.Id, checkResult, x.KeyName),
                                    LimitMessage = GetLimitMessage(t.NumberLimit, t.OptType),
                                    NeedCompute = t.IsCompute,
                                    NeedPhoto = t.NeedPhoto,
                                    OptType = t.OptType,
                                    Prefix = t.Prefix,
                                    Suffix = t.Suffix,
                                    Suggestion = t.Suggestion,
                                    TextValue = checkResult.FirstOrDefault(f => f.PropertyId == t.Id)?.TextValue ??
                                                string.Empty,
                                    ResultWords = GetCheckResultWord(t.Id, checkResultWord, subItemWithWord, resultWord)
                                }).ToList()
                            });
                        });
                        project.Children = subClassfy;
                    }
                    else
                    {
                        var subCheckItem = subItem.Where(x => x.ConfigId == _.Id).ToList();
                        project.CheckSubItems = subCheckItem.Select(t => new CheckSubItem
                        {
                            CheckCount = t.CheckCount,
                            CheckItemMainId = _.Id,
                            Id = t.Id,
                            CheckType = t.CheckType,
                            Chosen = IniCheckChoose(t.Id, checkResult, _.KeyName),
                            LimitMessage = GetLimitMessage(t.NumberLimit, t.OptType),
                            NeedCompute = t.IsCompute,
                            NeedPhoto = t.NeedPhoto,
                            OptType = t.OptType,
                            Prefix = t.Prefix,
                            Suffix = t.Suffix,
                            Suggestion = t.Suggestion,
                            TextValue =
                                checkResult.FirstOrDefault(f => f.PropertyId == t.Id)?.TextValue ?? string.Empty,
                            ResultWords = GetCheckResultWord(t.Id, checkResultWord, subItemWithWord, resultWord)
                        }).ToList();
                    }

                    childClassfy.Add(project);
                });
                result.Children = childClassfy;
            }

            return result;
        }

        private bool IniCheckChoose(long id, List<ShopCheckResultDo> checkResult, string keyName)
        {
            if (checkResult.Any(d => d.PropertyId == id))
            {
                return true;
            }

            if (keyName == "UploadPageReport")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 升级项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveUpgradeCheckItem(SaveUpgradeCheckItemRequest request)
        {
            var submitBy = request.SubmitBy ?? string.Empty;
            var now = DateTime.Now;
            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(request.RecId);//查到店记录
            var checkDataTask = GetReceiveCheckData(request.RecId, request.CheckId, false);//检查报告主数据
            var subItemTask = shopReceiveCheckSubItemRepository.GetShopReceiveCheckSubItem();//subItem
            var ruleWordTask = shopReceiveResultWordComputeRuleRepository.GetResultWordAndRule();
            await Task.WhenAll(receiveDataTask, checkDataTask, subItemTask, ruleWordTask);
            var receiveData = receiveDataTask.Result;
            var checkData = checkDataTask.Result;
            var subItem = subItemTask.Result ?? new List<ShopReceiveCheckSubItemDo>();
            var ruleWord = ruleWordTask.Result ?? new List<ShopCheckRuleWordDo>();
            if (receiveData == null)
            {
                throw new CustomException($"到店记录查询失败,{request.RecId}");
            }

            var checkResult = request.CheckResult ?? new List<UpgradeProjectRequest>();
            var checkResultImage = request.CheckItemImages ?? new List<UpgradeProjectImageRequest>();

            ReceiveCheckClassfyEnum receiveCheckClassfyEnum = (ReceiveCheckClassfyEnum)Enum.Parse(typeof(ReceiveCheckClassfyEnum), request.CheckModuleCode);

            if (checkData == null)
            {
                var checkResultDo = new ShopReceiveCheckDo
                {
                    ReceiveId = receiveData.Id,
                    ShopId = receiveData.ShopId,
                    CheckStatus = 0,
                    UserId = receiveData.UserId,
                    UserTel = receiveData.UserTel,
                    UserName = receiveData.UserName,
                    CarId = receiveData.CarId,
                    CarPlate = receiveData.CarNo,
                    VersionNum = 1,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                };

                return await InsertUpgradeReceiveCheck(checkResultDo, receiveCheckClassfyEnum, checkResult, checkResultImage, submitBy, subItem, ruleWord);
            }
            else if (checkData.CheckStatus == 0)
            {
                return await UpdateUpgradeReceiveCheck(checkData.Id, receiveCheckClassfyEnum, checkResult, checkResultImage, submitBy, subItem, ruleWord);
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 检查项统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckStatisticsResultResponse> GetCheckStatisticsResult(CheckStatisticsResultRequest request)
        {
            CheckStatisticsResultResponse result = new CheckStatisticsResultResponse();
            var checkLogDataTask = shopReceiveCheckLogRepository.GetShopReceiveCheckLog(request.CheckId, CategoryId);//日志
            var totalCountTask = GetTotalCount();//总数量
            await Task.WhenAll(checkLogDataTask, totalCountTask);
            var checkLogData = checkLogDataTask.Result ?? new List<ShopReceiveCheckLogDo>();
            var totalCount = totalCountTask.Result;

            var errorCheckCount = checkLogData.Distinct(_ => _.CheckModuleCode).Sum(_ => _.ErrorCount);

            var normalCheckCount = checkLogData.Distinct(_ => _.CheckModuleCode).Sum(_ => _.NormalCount);

            result.NormalCount = normalCheckCount;
            result.AbNormalCount = errorCheckCount;
            result.UncheckCount = totalCount - normalCheckCount - errorCheckCount;
            return result;
        }

        /// <summary>
        /// 根据到店记录查询检查报告统计
        /// </summary>
        /// <param name="recId"></param>
        /// <returns></returns>
        public async Task<CheckStatisticsVo> GetCheckStatisticsByRecId(long recId)
        {
            var receiveDataTask = shopReceiveCheckRepository.GetReceiveCheckByRecId(recId);
            var totalCountTask = GetTotalCount();//总数量
            await Task.WhenAll(receiveDataTask, totalCountTask);
            var receiveData = receiveDataTask.Result;
            var totalCount = totalCountTask.Result;
            if (receiveData != null)
            {
                CheckStatisticsVo result = new CheckStatisticsVo
                {
                    CheckId = receiveData.Id,
                    Status = receiveData.CheckStatus
                };
                var checkLogData = await shopReceiveCheckLogRepository.GetShopReceiveCheckLog(result.CheckId, CategoryId);//日志
                var errorCheckCount = checkLogData.Distinct(_ => _.CheckModuleCode).Sum(_ => _.ErrorCount);
                var normalCheckCount = checkLogData.Distinct(_ => _.CheckModuleCode).Sum(_ => _.NormalCount);
                result.NormalCount = normalCheckCount;
                result.AbNormalCount = errorCheckCount;
                result.UncheckCount = totalCount - normalCheckCount - errorCheckCount;
                return result;
            }
            return null;
        }

        /// <summary>
        /// 技师Or客户签字
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveSignatureData(SaveSignatureDataRequest request)
        {
            string submitBy = request.SubmitBy ?? string.Empty;
            var checkDataTask = GetReceiveCheckData(0, request.CheckId, false);//检查报告主数据
            var checkLogDataTask = shopReceiveCheckLogRepository.GetShopReceiveCheckLog(request.CheckId, CategoryId);//日志
            await Task.WhenAll(checkDataTask, checkLogDataTask);
            var checkData = checkDataTask.Result;
            var checkLogData = checkLogDataTask.Result ?? new List<ShopReceiveCheckLogDo>();
            if (checkData == null)
            {
                throw new CustomException($"您还未检查任何项目");
            }
            else if (checkData.CheckStatus == 0)
            {
                if (request.CheckModuleCode == ReceiveCheckClassfyEnum.TechnicianSignature.ToString())
                {
                    var shopReceiveCheckDo = new ShopReceiveCheckDo
                    {
                        Id = checkData.Id,
                        TechnicianSignature = request.SignatureUrl,
                        UpdateBy = request.SubmitBy ?? string.Empty,
                        UpdateTime = DateTime.Now
                    };

                    var updateField = new List<string> { "TechnicianSignature", "UpdateBy", "UpdateTime" };

                    return await UpdateSignature(shopReceiveCheckDo, updateField, ReceiveCheckClassfyEnum.TechnicianSignature, submitBy);

                }
                else if (request.CheckModuleCode == ReceiveCheckClassfyEnum.ZhiJianSignature.ToString())
                {

                    var existTechSign = checkLogData.Any(_ => _.CheckModuleCode == ReceiveCheckClassfyEnum.TechnicianSignature.ToString());
                    if (!existTechSign)
                    {
                        throw new CustomException($"需要技师确认后客户才能签字");
                    }

                    var shopReceiveCheckDo = new ShopReceiveCheckDo
                    {
                        Id = checkData.Id,
                        ZhiJianSignature = request.SignatureUrl,
                        UpdateBy = request.SubmitBy ?? string.Empty,
                        UpdateTime = DateTime.Now
                    };

                    var updateField = new List<string> { "ZhiJianSignature", "UpdateBy", "UpdateTime" };

                    return await UpdateSignature(shopReceiveCheckDo, updateField, ReceiveCheckClassfyEnum.ZhiJianSignature, submitBy);

                }
                else if (request.CheckModuleCode == ReceiveCheckClassfyEnum.CustomerSignature.ToString())
                {
                    var existTechSign = checkLogData.Any(_ => _.CheckModuleCode == ReceiveCheckClassfyEnum.TechnicianSignature.ToString());
                    if (!existTechSign)
                    {
                        throw new CustomException($"需要技师确认后客户才能签字");
                    }
                    var shopReceiveCheckDo = new ShopReceiveCheckDo
                    {
                        Id = checkData.Id,
                        CustomerSignature = request.SignatureUrl,
                        UpdateBy = request.SubmitBy ?? string.Empty,
                        UpdateTime = DateTime.Now
                    };

                    var updateField = new List<string> { "CustomerSignature", "UpdateBy", "UpdateTime" };

                    return await UpdateSignature(shopReceiveCheckDo, updateField, ReceiveCheckClassfyEnum.CustomerSignature, submitBy);
                }
                else
                {
                    throw new CustomException($"参数错误");
                }
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 提交检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SubmitCheckResult(SubmitCheckResultRequest request)
        {
            long checkId = request.CheckId;
            string submitBy = request.SubmitBy ?? string.Empty;
            var checkData = await GetReceiveCheckData(0, checkId, false);//检查报告主数据
            if (checkData == null)
            {
                throw new CustomException($"您还未检查任何项目");
            }

            var receiveDataTask = shopReceiveRepository.GetAsync<ShopReceiveDO>(checkData.ReceiveId);//查到店记录
            var checkLogDataTask = shopReceiveCheckLogRepository.GetShopReceiveCheckLog(checkId, CategoryId);//日志
            var configTask = shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, -1);//property配置
            var checkResultTask = shopCheckResultRepository.GetShopCheckResult(checkId, CategoryId, -1);//检查结果
            var subItemTask = shopReceiveCheckSubItemRepository.GetShopReceiveCheckSubItem();//subItem
            var checkResultImageTask = shopCheckResultImageRepository.GetCheckResultImageByCheckId(checkId, CategoryId);//图片
            var resultWordTask = shopReceiveResultWordRepository.GetShopReceiveResultWord();//结果词
            var exampleImageTask = shopCheckResultImageRepository.GetCheckResultImageByCheckId(-100, CategoryId);//仪表盘故障灯示例图
            await Task.WhenAll(checkLogDataTask, configTask, checkResultTask, subItemTask, receiveDataTask, checkResultImageTask, resultWordTask, exampleImageTask);
            var checkLogData = checkLogDataTask.Result ?? new List<ShopReceiveCheckLogDo>();
            var config = configTask.Result ?? new List<ShopCheckPropertyDo>();
            var checkResult = checkResultTask.Result ?? new List<ShopCheckResultDo>();
            var subItem = subItemTask.Result ?? new List<ShopReceiveCheckSubItemDo>();
            var receiveData = receiveDataTask.Result;
            var checkResultImage = checkResultImageTask.Result ?? new List<ShopCheckResultImageDo>();
            var resultWord = resultWordTask.Result ?? new List<ShopReceiveResultWordDo>();
            var exampleImage = exampleImageTask.Result ?? new List<ShopCheckResultImageDo>();
            if (receiveData == null)
            {
                throw new CustomException("到店记录查询失败");
            }

            if (checkData.CheckStatus == 0)
            {
                var existTechSign = checkLogData.Any(_ => _.CheckModuleCode == ReceiveCheckClassfyEnum.TechnicianSignature.ToString());
                if (!existTechSign)
                {
                    throw new CustomException($"签字确认后才能提交");
                }
                var existCusSign = checkLogData.Any(_ => _.CheckModuleCode == ReceiveCheckClassfyEnum.CustomerSignature.ToString());
                if (!existCusSign)
                {
                    throw new CustomException($"签字确认后才能提交");
                }
                var shopReceiveCheckDo = new ShopReceiveCheckDo
                {
                    Id = checkData.Id,
                    CheckStatus = 1,
                    SubmitChannel = 0,
                    PersonId = request.PersonId ?? string.Empty,
                    PersonName = request.PersonName ?? string.Empty,
                    UpdateBy = request.SubmitBy ?? string.Empty,
                    UpdateTime = DateTime.Now
                };

                var updateField = new List<string> { "CheckStatus", "SubmitChannel", "PersonId", "PersonName", "UpdateBy", "UpdateTime" };

                var result = await UpdateSignature(shopReceiveCheckDo, updateField, ReceiveCheckClassfyEnum.Submit, submitBy);

                if (result)
                {
                    #region 存储报告数据

                    var resultJson = GetCheckReportMobileSummary(receiveData, checkData, config, checkResult, checkResultImage, resultWord, checkLogData, exampleImage, subItem);
                    await shopReceiveCheckReportRepository.InsertAsync(new ShopReceiveCheckReportDo
                    {
                        CheckId = checkId,
                        CategoryId = CategoryId,
                        ReceiveId = receiveData.Id,
                        NormalCount = resultJson.Items.FirstOrDefault(_ => _.ResultWordCode == "Normal")?.Count ?? 0,
                        AbnormalCount = resultJson.Items.FirstOrDefault(_ => _.ResultWordCode == "AbNormal")?.Count ?? 0,
                        UncheckCount = resultJson.Items.FirstOrDefault(_ => _.ResultWordCode == "None")?.Count ?? 0,
                        VersionNum = checkData.VersionNum,
                        MobileSummary = JsonConvert.SerializeObject(resultJson),
                        CreateBy = submitBy,
                        CreateTime = DateTime.Now
                    });

                    #endregion

                    #region 更新C端车辆档案 - 公里数和VIN码

                    bool editFlag = false;
                    EditUserCarClientRequest editCarRequest = new EditUserCarClientRequest
                    {
                        UserId = checkData.UserId,
                        CarId = checkData.CarId,
                        Submitter = submitBy
                    };
                    if (!string.IsNullOrEmpty(checkData.VinCode))
                    {
                        editCarRequest.VinCode = checkData.VinCode;
                        editFlag = true;
                    }

                    if (checkData.Mileage > 0)
                    {
                        editCarRequest.TotalMileage = checkData.Mileage;
                        editFlag = true;
                    }

                    if (editFlag)
                    {
                        await vehicleClient.EditUserCarAsync(editCarRequest);
                    }

                    #endregion

                    #region 更新C端车辆档案 - 车辆部件异常数据

                    var normalError = checkResult.Where(_ => _.PropertyType == 0 && _.NumericValue == 1).Select(_ => _.PropertyId).ToList();
                    var upgradeSubItemId = checkResult.Where(_ => _.PropertyType == 1 && _.NumericValue == 1).Select(_ => _.PropertyId).ToList();
                    var upgradeError = subItem.Where(_ => upgradeSubItemId.Contains(_.Id)).Select(_ => _.CheckItemMainId).ToList();
                    var errorPropertyIds = normalError.Union(upgradeError).ToList();
                    var errorProperty = config.Where(_ => errorPropertyIds.Contains(_.Id) && !string.IsNullOrEmpty(_.CarComponents)).ToList();
                    var errorComponentsRequest = errorProperty.GroupBy(_ => _.CarComponents).Select(_ => new CarComponentsClientRequest
                    {
                        KeyName = _.Key,
                        PropertyId = _.Select(t => t.Id).ToList()
                    }).ToList();

                    if (errorComponentsRequest.Any())
                    {
                        await vehicleClient.UpdateCarPartCheckResult(new UpdateCarPartCheckResultClientRequest
                        {
                            CarId = checkData.CarId,
                            SubmitBy = submitBy,
                            CheckId = checkId,
                            CategoryId = CategoryId,
                            CarErrorComponents = errorComponentsRequest
                        });
                    }

                    #endregion
                }

                return result;
            }
            else
            {
                throw new CustomException($"该检查报告已提交，不能修改！");
            }
        }

        /// <summary>
        /// 计算规则结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ComputeRuleResultResponse> ComputeRuleResult(ComputeRuleResultRequest request)
        {
            ComputeRuleResultResponse result = new ComputeRuleResultResponse() { Value = request.Value };

            var subItem = await shopReceiveCheckSubItemRepository.GetAsync<ShopReceiveCheckSubItemDo>(request.CheckSubItemId);
            if (subItem == null)
            {
                throw new CustomException($"当前项不需要重新计算");
            }
            if (!subItem.IsCompute)
            {
                throw new CustomException($"当前项不需要重新计算");
            }

            if (subItem.OptType == "input-num")
            {
                var computeRuleTask = shopReceiveSubItemComputeRuleRepository.GetSubItemComputeRule(request.CheckSubItemId);
                var ruleWordTask = shopReceiveResultWordComputeRuleRepository.GetResultWordComputeRule();
                var resultWordTask = shopReceiveResultWordRepository.GetShopReceiveResultWord();
                await Task.WhenAll(computeRuleTask, ruleWordTask, resultWordTask);
                var computeRule = computeRuleTask.Result ?? new List<ShopReceiveSubItemComputeRuleDo>();
                var ruleWord = ruleWordTask.Result ?? new List<ShopReceiveResultWordComputeRuleDo>();
                var resultWord = resultWordTask.Result ?? new List<ShopReceiveResultWordDo>();

                var isNum = Regex.IsMatch(request.Value ?? "", @"^(([0-9]{1,10})|(([0]\.\d{1,2}|[1-9][0-9]{0,9}\.\d{1,2})))$");

                if (!isNum)
                {
                    throw new CustomException($"格式有误，请输入数值类型且最多2位小数");
                }
                decimal decimalValue = Convert.ToDecimal(request.Value);
                var currentRule = computeRule.FirstOrDefault(_ => _.MinValue <= decimalValue && _.MaxValue >= decimalValue);
                if (currentRule == null)
                {
                    throw new CustomException($"超出范围，请求确认后重新填写！");
                }
                var wordList = ruleWord.Where(_ => _.RuleId == currentRule.Id).Select(_ => _.ResultWordId).ToList();

                result.ResultWords = resultWord.Where(_ => wordList.Contains(_.Id)).Select(_ => new CheckResultWord
                {
                    Id = _.Id,
                    Code = _.Code,
                    Name = _.Name,
                    Value = _.Value,
                    Type = _.Type
                }).ToList();

                return result;
            }
            else
            {
                throw new CustomException($"当前项不需要重新计算");
            }
        }

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckReportResponse> GetCheckReport(CheckReportRequest request)
        {
            CheckReportResponse result = new CheckReportResponse();
            long checkId = request.CheckId;
            var checkData = await shopReceiveCheckRepository.GetAsync<ShopReceiveCheckDo>(checkId);//检查报告主数据
            if (checkData == null)
            {
                throw new CustomException("检查报告查询失败");
            }

            var receiveData = await shopReceiveRepository.GetAsync<ShopReceiveDO>(checkData.ReceiveId);//查到店记录
            if (receiveData == null)
            {
                throw new CustomException("检查报告查询失败");
            }

            var checkReport = (await shopReceiveCheckReportRepository.GetReceiveCheckReporList(new List<long> { checkId }, CategoryId))?.FirstOrDefault();
            if (checkReport != null)
            {
                CheckReportResponse response = JsonConvert.DeserializeObject<CheckReportResponse>(checkReport.MobileSummary);

                return response;
            }
            else
            {
                var configTask = shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, -1);//配置
                var checkResultTask = shopCheckResultRepository.GetShopCheckResult(checkId, CategoryId, -1);//检查结果
                var checkResultImageTask = shopCheckResultImageRepository.GetCheckResultImageByCheckId(checkId, CategoryId);//图片
                var resultWordTask = shopReceiveResultWordRepository.GetShopReceiveResultWord();//结果词
                var checkLogTask = shopReceiveCheckLogRepository.GetShopReceiveCheckLog(checkId, CategoryId);//日志
                var exampleImageTask = shopCheckResultImageRepository.GetCheckResultImageByCheckId(-100, CategoryId);//仪表盘故障灯示例图
                var subItemTask = shopReceiveCheckSubItemRepository.GetShopReceiveCheckSubItem();//SubItem
                await Task.WhenAll(configTask, checkResultTask, checkResultImageTask, resultWordTask, checkLogTask, exampleImageTask, subItemTask);
                var config = configTask.Result ?? new List<ShopCheckPropertyDo>();
                var checkResult = checkResultTask.Result ?? new List<ShopCheckResultDo>();
                var checkResultImage = checkResultImageTask.Result ?? new List<ShopCheckResultImageDo>();
                var resultWord = resultWordTask.Result ?? new List<ShopReceiveResultWordDo>();
                var checkLog = checkLogTask.Result ?? new List<ShopReceiveCheckLogDo>();
                var exampleImage = exampleImageTask.Result ?? new List<ShopCheckResultImageDo>();
                var subItem = subItemTask.Result ?? new List<ShopReceiveCheckSubItemDo>();

                CheckReportResponse response = GetCheckReportMobileSummary(receiveData, checkData, config, checkResult, checkResultImage, resultWord, checkLog, exampleImage, subItem);

                return response;
            }
        }

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserReceiveCheckVo>> GetUserReceiveCheckList(UserReceiveCheckListRequest request)
        {
            ApiPagedResultData<UserReceiveCheckVo> result = new ApiPagedResultData<UserReceiveCheckVo>
            {
                Items = new List<UserReceiveCheckVo>()
            };
            var receiveData = await shopReceiveRepository.GetUserReceivePageList(request.UserId, request.PageIndex, request.PageSize);
            if (receiveData != null)
            {
                result.TotalItems = receiveData.TotalItems;
                if (receiveData.Items != null && receiveData.Items.Count > 0)
                {
                    var totalCount = await GetTotalCount();//总数量
                    var recIds = receiveData.Items.Select(_ => _.Id).ToList();

                    var checkListTask = shopReceiveCheckRepository.GetReceiveCheckByRecIds(recIds);
                    var carReportTask = shopArrivalCarReportRepository.GetShopArrivalCarReportList(recIds);
                    await Task.WhenAll(checkListTask, carReportTask);

                    var checkList = checkListTask.Result ?? new List<ShopReceiveCheckDo>();
                    var carReport = carReportTask.Result ?? new List<ShopArrivalCarReportDO>();

                    checkList = checkList.Where(_ => _.CheckStatus == 1).ToList();

                    List<ShopReceiveCheckLogDo> checkLogList = new List<ShopReceiveCheckLogDo>();

                    if (checkList.Any())
                    {
                        checkLogList = await shopReceiveCheckLogRepository.GetReceiveCheckLogByCheckIds(checkList.Select(_ => _.Id).ToList(), CategoryId);
                    }
                    foreach (var receiveItem in receiveData.Items)
                    {
                        List<ReceiveCheckStatisticsVo> receiveCheck = new List<ReceiveCheckStatisticsVo>();
                        ReceiveCheckStatisticsVo itemCheck = new ReceiveCheckStatisticsVo
                        {
                            Type = CategoryId,
                            CategoryName = "车况检查",

                        };
                        var firstCheck = checkList.FirstOrDefault(_ => _.ReceiveId == receiveItem.Id);
                        if (firstCheck != null)
                        {
                            var firstLog = checkLogList.Where(_ => _.CheckId == firstCheck.Id).ToList();
                            itemCheck.CheckId = firstCheck.Id;
                            var normalCount = firstLog.Sum(_ => _.NormalCount);
                            var errorCount = firstLog.Sum(_ => _.ErrorCount);
                            itemCheck.StatisticsData = new CheckStatisticsData
                            {
                                NormalCount = normalCount,
                                AbNormalCount = errorCount,
                                UncheckCount = totalCount - normalCount - errorCount
                            };
                        }
                        else
                        {
                            itemCheck.Message = "您的爱车并未做相关检查";
                        }
                        receiveCheck.Add(itemCheck);
                        result.Items.Add(new UserReceiveCheckVo
                        {
                            RecId = receiveItem.Id,
                            Brand = receiveItem.Brand,
                            BrandUrl = configuration["QiNiuImageDomain"] +
                                       CommonHelper.GetLogoUrlByName(receiveItem.Brand),
                            CarPlate = receiveItem.CarNo,
                            ReceiveDate = receiveItem.ArrivalTime.ToString("yyyy-MM-dd"),
                            ReceiveCheck = receiveCheck,
                            CarReportUrl = carReport.FirstOrDefault(_ => _.ArrivalId == receiveItem.Id)?.CarReportUrl ??
                                           string.Empty
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 车辆历史检测报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CarReceiveCheckVo>> GetCarReceiveCheckList(CarReceiveCheckListRequest request)
        {
            List<CarReceiveCheckVo> result = new List<CarReceiveCheckVo>();
            var checkList = await shopReceiveCheckRepository.GetReceiveCheckByCarId(request.CarId);
            if (request.Limit > 0)
            {
                checkList = checkList.Take(request.Limit).ToList();
            }

            if (checkList.Any())
            {
                var recIds = checkList.Select(_ => _.ReceiveId).ToList();
                var checkIds = checkList.Select(_ => _.Id).ToList();
                var shopIds = checkList.Select(_ => _.ShopId).Distinct().ToList();

                var receiveListTask = shopReceiveRepository.GetReceiveListByIds(recIds);
                var checkLogListTask = shopReceiveCheckLogRepository.GetReceiveCheckLogByCheckIds(checkIds, CategoryId);
                var shopListTask = shopManageClient.GetShopListByIdsAsync(new ShopListByIdsAsyncClientRequest()
                {
                    ShopIds = shopIds
                });
                var totalCountTask = GetTotalCount(); //总数量
                await Task.WhenAll(receiveListTask, checkLogListTask, shopListTask, totalCountTask);
                var receiveList = receiveListTask.Result ?? new List<ShopReceiveDO>();
                var checkLogList = checkLogListTask.Result ?? new List<ShopReceiveCheckLogDo>();
                var shopList = shopListTask.Result ?? new List<ShopListDto>();
                var totalCount = totalCountTask.Result;
                checkList.ForEach(_ =>
                {
                    var defaultReceive = receiveList.FirstOrDefault(t => t.Id == _.ReceiveId);
                    var inDate = string.Empty;
                    if (defaultReceive != null)
                    {
                        inDate = defaultReceive.ArrivalTime.ToString("yyyy-MM-dd");
                    }

                    var firstLog = checkLogList.Where(t => t.CheckId == _.Id).ToList();
                    var normalCount = firstLog.Sum(t => t.NormalCount);
                    var errorCount = firstLog.Sum(t => t.ErrorCount);
                    CarReceiveCheckVo itemCheck = new CarReceiveCheckVo()
                    {
                        RecId = _.ReceiveId,
                        ReceiveDate = inDate,
                        ShopSimpleName = shopList.FirstOrDefault(t => t.Id == _.ShopId)?.SimpleName ?? string.Empty,
                        CheckReport = new CarCheckReportVo()
                        {
                            CheckId = _.Id,
                            NormalCount = normalCount,
                            AbNormalCount = errorCount,
                            UncheckCount = totalCount - normalCount - errorCount
                        }
                    };
                    result.Add(itemCheck);
                });
            }

            return result;
        }

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleFileResponse> GetUserVehicleFile(UserVehicleFileRequest request)
        {
            UserVehicleFileResponse result = new UserVehicleFileResponse();
            var carFileTask = vehicleClient.GetUserCarFile(new UserCarFileClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId
            });//车型档案数据
            var userInfoTask = userClient.GetUserInfo(new GetUserInfoClientRequest
            {
                UserId = request.UserId
            });//用户信息
            var lastArriveTimeTask = arrivalRepository.GetLastArriveTimeByCarId(new Core.Request.Arrival.GetArrialMaintenanceRequest
            {
                CarId = request.CarId
            });//最后到店时间
            var arriveCountTask = arrivalRepository.GetArriveCountByCarId(new Core.Request.Arrival.GetArrialMaintenanceRequest
            {
                CarId = request.CarId
            });//到店总次数
            var sumMoneyTask = arrivalRepository.GetArriveConsumptionAmountByCarId(new Core.Request.Arrival.GetArrialMaintenanceRequest
            {
                CarId = request.CarId
            });
            await Task.WhenAll(carFileTask, userInfoTask, lastArriveTimeTask, arriveCountTask, sumMoneyTask);
            var carFile = carFileTask.Result;
            var userInfo = userInfoTask.Result;
            var lastArriveTime = lastArriveTimeTask.Result ?? string.Empty;
            var arriveCount = arriveCountTask.Result;
            var sumMoney = sumMoneyTask.Result;
            if (userInfo == null)
            {
                throw new CustomException("用户信息不存在");
            }

            if (carFile == null)
            {
                throw new CustomException("用户车辆不存在");
            }

            result.MainData = new VehicleFileMainData
            {
                UserName = userInfo.UserName,
                UserTel = userInfo.UserTel,
                UserTelDisplay = userInfo.UserTelDes,
                CarPlate = carFile.CarNumber,
                BrandUrl = configuration["QiNiuImageDomain"] + CommonHelper.GetLogoUrlByName(carFile.Brand),
                CarType = CommonHelper.GetCarVehicle(carFile.Vehicle, carFile.PaiLiang, carFile.Nian, carFile.SalesName),
                LastInTime = lastArriveTime,
                Mileage = carFile.TotalMileage,
                ReceiveCount = arriveCount,
                TotalMoney = (decimal)sumMoney,
                InsuranceCompany = carFile.InsuranceCompany,
                InsureExpireDate = carFile.InsureExpireDate?.ToString("yyyy-MM-dd") ?? string.Empty
            };
            List<CarPartsSituation> carPartsSituation = new List<CarPartsSituation>();
            if (carFile.CarPartsSituation != null && carFile.CarPartsSituation.Any())
            {
                carPartsSituation = mapper.Map<List<CarPartsSituation>>(carFile.CarPartsSituation);
            }
            result.CarPartsSituation = carPartsSituation;

            return result;
        }

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CheckErrorDetailVo>> GetCheckErrorDetail(CheckErrorDetailRequest request)
        {
            List<CheckErrorDetailVo> result = new List<CheckErrorDetailVo>();
            var errorRecord = await vehicleClient.GetUserCarComponentsErrorCheck(new UserCarComponentsErrorClientCheckRequest
            {
                CarId = request.CarId,
                KeyName = request.KeyName
            });
            if (errorRecord != null && errorRecord.Any())
            {
                var checkIdList = errorRecord.Where(_ => _.CategoryId == CategoryId).Select(_ => _.CheckId).ToList();
                var checkListTask = shopReceiveCheckRepository.GetReceiveCheckList(checkIdList);//检查记录
                var checkReportListTask = shopReceiveCheckReportRepository.GetReceiveCheckReporList(checkIdList, CategoryId);//检查报告
                await Task.WhenAll(checkListTask, checkReportListTask);
                var checkList = checkListTask.Result ?? new List<ShopReceiveCheckDo>();
                var checkReportList = checkReportListTask.Result ?? new List<ShopReceiveCheckReportDo>();
                var shopIdList = checkList.Select(_ => _.ShopId).Distinct().ToList();
                List<ShopListDto> shopList = new List<ShopListDto>();
                if (shopIdList.Any())
                {
                    shopList = (await shopManageClient.GetShopListByIdsAsync(new ShopListByIdsAsyncClientRequest { ShopIds = shopIdList })) ?? new List<ShopListDto>();
                }
                checkList.ForEach(_ =>
                {
                    var propertyList = errorRecord.FirstOrDefault(t => t.CheckId == _.Id)?.PropertyIds ?? new List<long>();
                    List<CheckResultItem> checkItem = new List<CheckResultItem>();
                    var defaultReport = checkReportList.FirstOrDefault(t => t.CheckId == _.Id);
                    if (defaultReport != null)
                    {
                        var itemResult = JsonConvert.DeserializeObject<CheckReportResponse>(defaultReport.MobileSummary);
                        if (itemResult != null)
                        {
                            var defaultResult = itemResult.Items?.FirstOrDefault(t => t.ResultWordCode == "AbNormal");
                            if (defaultResult != null)
                            {
                                checkItem = defaultResult.CheckClassfy.SelectMany(t => t.ResultItems).ToList();
                            }
                        }
                    }
                    CheckErrorDetailVo itemCheck = new CheckErrorDetailVo
                    {
                        CheckDate = _.CreateTime.ToString("yyyy-MM-dd"),
                        ShopSimpleName = shopList.FirstOrDefault(t => t.Id == _.ShopId)?.SimpleName ?? string.Empty,
                        ResultItems = checkItem.Where(t => propertyList.Contains(t.CheckItemId)).Select(t => new CheckPropertyResult
                        {
                            PropertyId = t.CheckItemId,
                            DisplayName = t.DisplayName,
                            Images = t.Images,
                            ErrorDesList = t.ErrorDesList
                        }).ToList()
                    };

                    result.Add(itemCheck);
                });

            }

            return result;
        }

        /// <summary>
        /// 根据到店记录批量查询检查报告
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        public async Task<List<ReceiveCheckListVo>> GetReceiveCheckListByRecIds(List<long> recIds)
        {
            var receiveList = await shopReceiveCheckRepository.GetReceiveCheckByRecIds(recIds);

            receiveList = receiveList.Where(_ => _.CheckStatus == 1).ToList();

            return receiveList.Select(_ => new ReceiveCheckListVo
            {
                RecId = _.ReceiveId,
                Mileage = _.Mileage
            }).ToList();
        }

        #region Private

        /// <summary>
        /// 检查报告数据
        /// </summary>
        /// <param name="receiveData"></param>
        /// <param name="checkData"></param>
        /// <param name="config"></param>
        /// <param name="checkResult"></param>
        /// <param name="checkResultImage"></param>
        /// <param name="resultWord"></param>
        /// <param name="checkLog"></param>
        /// <param name="exampleImage"></param>
        /// <param name="subItem"></param>
        /// <returns></returns>
        private CheckReportResponse GetCheckReportMobileSummary(ShopReceiveDO receiveData, ShopReceiveCheckDo checkData,
            List<ShopCheckPropertyDo> config, List<ShopCheckResultDo> checkResult, List<ShopCheckResultImageDo> checkResultImage,
            List<ShopReceiveResultWordDo> resultWord, List<ShopReceiveCheckLogDo> checkLog, List<ShopCheckResultImageDo> exampleImage,
            List<ShopReceiveCheckSubItemDo> subItem)
        {
            CheckReportMainData mainData = new CheckReportMainData
            {
                CheckId = checkData.Id,
                Status = checkData.CheckStatus,
                UserName = receiveData.UserName,
                UserTel = receiveData.UserTel,
                UserTelDisplay = CommonHelper.FormatTel(receiveData.UserTel),
                CarPlate = receiveData.CarNo,
                BrandUrl = configuration["QiNiuImageDomain"] + CommonHelper.GetLogoUrlByName(receiveData.Brand),
                CarType = receiveData.Vehicle,
                InTime = receiveData.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss"),
                VinCode = checkData.VinCode,
                Mileage = checkData.Mileage,
                DashboardImg = checkData.DashboardImg,
                Narration = checkData.Narration,
                TechnicianSignature = checkData.TechnicianSignature,
                CustomerSignature = checkData.CustomerSignature,
                ZhiJianSignature=checkData.ZhiJianSignature,
            };

            List<OtherProjectResult> otherProject = new List<OtherProjectResult>();
            var otherParent = config.FirstOrDefault(_ => _.KeyName == ReceiveCheckClassfyEnum.Other.ToString() && _.ParentId == 0);
            if (otherParent != null)
            {
                var otherConfig = config.Where(_ => _.ParentId == otherParent.Id).ToList();
                otherProject = otherConfig.Select(_ => new OtherProjectResult
                {
                    ProjectId = _.Id,
                    ProjectName = _.DisplayName,
                    KeyName = _.KeyName,
                    ResultValue = checkResult.FirstOrDefault(t => t.PropertyType == 0 && t.PropertyId == _.Id)?.NumericValue ?? 0,
                    Image = checkResultImage.Where(t => t.PropertyId == _.Id).Select(t => t.Url).ToList()
                }).ToList();
            }

            return new CheckReportResponse
            {
                MainData = mainData,
                OtherProjectResult = otherProject,
                Items = GetCheckResultClassify(resultWord, checkLog, exampleImage, config, checkResult, checkResultImage, subItem)
            };
        }

        /// <summary>
        /// 获取检查总数量
        /// </summary>
        /// <returns></returns>
        private async Task<int> GetTotalCount()
        {
            var config = (await shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, -1)) ?? new List<ShopCheckPropertyDo>();//配置
            List<string> normalProject = new List<string> { ReceiveCheckClassfyEnum.Inlook.ToString(), ReceiveCheckClassfyEnum.Outlook.ToString(), ReceiveCheckClassfyEnum.Dashboard.ToString(),ReceiveCheckClassfyEnum.ZhiJian.ToString() };
            var normalProjectId = config.Where(_ => normalProject.Contains(_.KeyName) && _.ParentId == 0).Select(_ => _.Id).ToList();
            var normalCount = config.Count(_ => normalProjectId.Contains(_.ParentId));
            var upgradeProjectId = config.Where(_ => _.ParentId == 0 && _.Condition == "10000").Select(_ => _.Id).ToList();
            var upgradeCount = config.Count(_ => upgradeProjectId.Contains(_.ParentId) && _.IsCheckItemMain);
            var totalCount = normalCount + upgradeCount;//总数量
            return totalCount;
        }


        /// <summary>
        /// 异常描述/建议 赋值
        /// </summary>
        /// <param name="checkResult"></param>
        /// <param name="subItem"></param>
        /// <param name="config"></param>
        /// <param name="checkResult"></param>
        private void SetErrorDes(List<ShopCheckResultDo> checkResult, List<ShopReceiveCheckSubItemDo> subItem, List<ShopCheckPropertyDo> config, CheckResultItem checkItem)
        {
            var normalSubIds = checkResult.Select(_ => _.PropertyId).ToList();

            var checkMainId = checkItem.CheckItemId;
            List<string> suggestion = new List<string>();
            foreach (var _ in checkResult)
            {
                ResultWordAndSuggestion resultWord = JsonConvert.DeserializeObject<ResultWordAndSuggestion>(_.ResultWordsJson);

                if (!string.IsNullOrEmpty(resultWord?.Suggestion))
                {
                    suggestion.Add(resultWord?.Suggestion);
                }
            }

            List<ErrorDesList> errorDes = new List<ErrorDesList>();
            var childItem = config.Where(_ => _.ParentId == checkMainId).ToList();
            if (childItem.Any())
            {
                foreach (var childSub in childItem)
                {
                    errorDes.Add(new ErrorDesList
                    {
                        DisplayName = childSub.DisplayName,
                        ErrorDes = GetNormalDes(checkResult, subItem.Where(_ => _.ConfigId == childSub.Id).ToList())
                    });
                }
            }
            else
            {
                errorDes.Add(new ErrorDesList
                {
                    ErrorDes = GetNormalDes(checkResult, subItem)
                });
            }

            checkItem.Suggestions = suggestion;
            checkItem.ErrorDesList = errorDes;
        }

        private List<string> GetNormalDes(List<ShopCheckResultDo> checkResult, List<ShopReceiveCheckSubItemDo> subItem)
        {
            List<string> msg = new List<string>();
            foreach (var itemSub in subItem)
            {
                var str = $"{itemSub.Prefix}{checkResult.FirstOrDefault(_ => _.PropertyId == itemSub.Id)?.TextValue ?? string.Empty}{itemSub.Suffix}";
                if (!msg.Contains(str)) {
                    msg.Add(str);
                }
            }
            return msg;
        }

        /// <summary>
        /// 常规项目 总数量
        /// </summary>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <returns></returns>
        private async Task<int> GetNormalTotalCountByProject(ReceiveCheckClassfyEnum receiveCheckClassfyEnum)
        {
            var config = await shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, -1);//配置
            var normalProjectId = config.FirstOrDefault(_ => _.KeyName == receiveCheckClassfyEnum.ToString() && _.ParentId == 0);
            if (normalProjectId != null)
            {
                return config.Count(_ => _.ParentId == normalProjectId.Id);
            }

            return 0;
        }

        /// <summary>
        /// 获取ReultWord
        /// </summary>
        /// <param name="subItemId"></param>
        /// <param name="resultWord"></param>
        /// <param name="subItemWithWod"></param>
        /// <param name="resultWordEnum"></param>
        /// <returns></returns>
        private List<CheckResultWord> GetCheckResultWord(long subItemId, List<ShopReceiveCheckResultWordDo> resultWord, List<ShopReceiveResultWordAndSubItemDo> subItemWithWod,
            List<ShopReceiveResultWordDo> resultWordEnum)
        {
            var resultWordCheck = resultWord.Where(d => d.CheckResultId == subItemId).Select(_ => _.ResultWordId).ToList();
            if (resultWordCheck.Any())
            {
                return resultWordEnum.Where(_ => resultWordCheck.Contains(_.Id)).Select(_ => new CheckResultWord
                {
                    Id = _.Id,
                    Code = _.Code,
                    Name = _.Name,
                    Value = _.Value,
                    Type = _.Type
                }).ToList();
            }
            else
            {
                var wordIds = subItemWithWod.Where(_ => _.CheckSubItemId == subItemId).Select(_ => _.ResultWordId).ToList();

                return resultWordEnum.Where(_ => wordIds.Contains(_.Id)).Select(_ => new CheckResultWord
                {
                    Id = _.Id,
                    Code = _.Code,
                    Name = _.Name,
                    Value = _.Value,
                    Type = _.Type
                }).ToList();
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="numberLimit"></param>
        /// <param name="optType"></param>
        /// <returns></returns>
        private string GetLimitMessage(string numberLimit, string optType)
        {
            string message = string.Empty;
            if (!string.IsNullOrEmpty(numberLimit))
            {
                switch (optType)
                {
                    case "input-num":
                        message = $"请输入，数值范围{numberLimit}";
                        break;
                    case "input-txt":
                        message = $"请输入，字符范围{numberLimit}";
                        break;
                }
            }
            return message;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="shopReceiveCheckDo"></param>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <param name="checkResult"></param>
        /// <param name="submitBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private async Task<bool> InsertReceiveCheck(ShopReceiveCheckDo shopReceiveCheckDo, ReceiveCheckClassfyEnum receiveCheckClassfyEnum, List<NormalProjectRequest> checkResult, string submitBy, int totalCount)
        {
            DateTime now = DateTime.Now;
            int errorCount = 0;
            int normalCount = 0;
            List<NormalProjectRequest> errorProject = new List<NormalProjectRequest>();

            switch (receiveCheckClassfyEnum)
            {
                case ReceiveCheckClassfyEnum.Dashboard:
                case ReceiveCheckClassfyEnum.Inlook:
                case ReceiveCheckClassfyEnum.Outlook:
                case ReceiveCheckClassfyEnum.ZhiJian:
                    errorProject = checkResult.Where(_ => _.ResultValue == 1).ToList();
                    errorCount = errorProject.Count;
                    normalCount = totalCount - errorCount;
                    break;
                case ReceiveCheckClassfyEnum.Other:
                    errorProject = checkResult.Where(_ => _.ResultValue > 0).ToList();
                    break;
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var checkId = await shopReceiveCheckRepository.InsertAsync<long>(shopReceiveCheckDo);

                var batchId = await shopReceiveCheckLogRepository.InsertAsync<long>(new ShopReceiveCheckLogDo
                {
                    CheckId = checkId,
                    CheckModule = EnumHelper.GetEnumDescription(receiveCheckClassfyEnum),
                    CheckModuleCode = receiveCheckClassfyEnum.ToString(),
                    CategoryId = CategoryId,
                    ErrorCount = errorCount,
                    NormalCount = normalCount,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                });

                if (errorProject.Any())
                {
                    List<ShopCheckResultDo> shopCheckResultList = errorProject.Select(_ => new ShopCheckResultDo
                    {
                        CheckId = checkId,
                        PropertyId = _.ProjectId,
                        NumericValue = _.ResultValue,
                        CategoryId = CategoryId,
                        SubmitBatchId = batchId,
                        CreateBy = submitBy,
                        CreateTime = now,
                        UpdateBy = submitBy,
                        UpdateTime = now
                    }).ToList();

                    await shopCheckResultRepository.InsertBatchAsync(shopCheckResultList);

                    List<ShopCheckResultImageDo> shopCheckResultImage = new List<ShopCheckResultImageDo>();
                    foreach (var itemP in errorProject)
                    {
                        if (itemP.Image != null && itemP.Image.Any())
                        {
                            shopCheckResultImage.AddRange(itemP.Image.Where(_ => !string.IsNullOrEmpty(_.Url)).Select(x => new ShopCheckResultImageDo
                            {
                                CheckId = checkId,
                                CategoryId = CategoryId,
                                Url = x.Url,
                                DisplayName = string.Join("，", x.Tips ?? new List<string>()),
                                PropertyId = itemP.ProjectId,
                                SubmitBatchId = batchId,
                                CreateBy = submitBy,
                                CreateTime = now,
                                UpdateBy = submitBy,
                                UpdateTime = now
                            }));
                        }
                    }

                    if (shopCheckResultImage.Any())
                    {
                        await shopCheckResultImageRepository.InsertBatchAsync(shopCheckResultImage);
                    }
                }

                ts.Complete();

                return true;
            }
        }

        /// <summary>
        /// 升级项保存
        /// </summary>
        /// <param name="shopReceiveCheckDo"></param>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <param name="checkResult"></param>
        /// <param name="checkResultImage"></param>
        /// <param name="submitBy"></param>
        /// <param name="subItem"></param>
        /// <param name="ruleWord"></param>
        /// <returns></returns>
        private async Task<bool> InsertUpgradeReceiveCheck(ShopReceiveCheckDo shopReceiveCheckDo, ReceiveCheckClassfyEnum receiveCheckClassfyEnum, List<UpgradeProjectRequest> checkResult,
            List<UpgradeProjectImageRequest> checkResultImage, string submitBy, List<ShopReceiveCheckSubItemDo> subItem, List<ShopCheckRuleWordDo> ruleWord)
        {
            DateTime now = DateTime.Now;
            var errorProject = checkResult.Where(_ => _.ResultWords.Any(t => t.Id == 3)).ToList();
            var errorCount = errorProject.Select(_ => _.CheckItemMainId).Distinct().ToList().Count;

            var normalProject = checkResult.Where(_ => !_.ResultWords.Any(t => t.Id == 3)).ToList();
            var normalCount = normalProject.Select(_ => _.CheckItemMainId).Distinct().ToList().Count;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var checkId = await shopReceiveCheckRepository.InsertAsync<long>(shopReceiveCheckDo);

                var batchId = await shopReceiveCheckLogRepository.InsertAsync<long>(new ShopReceiveCheckLogDo
                {
                    CheckId = checkId,
                    CheckModule = EnumHelper.GetEnumDescription(receiveCheckClassfyEnum),
                    CheckModuleCode = receiveCheckClassfyEnum.ToString(),
                    CategoryId = CategoryId,
                    ErrorCount = errorCount,
                    NormalCount = normalCount,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                });

                if (checkResult.Any())
                {
                    List<ShopCheckResultDo> shopCheckResultList = new List<ShopCheckResultDo>();
                    List<ShopReceiveCheckResultWordDo> resultWordList = new List<ShopReceiveCheckResultWordDo>();
                    checkResult.ForEach(_ =>
                    {
                        shopCheckResultList.Add(new ShopCheckResultDo
                        {
                            CheckId = checkId,
                            PropertyId = _.CheckSubItemId,
                            TextValue = _.TextValue,
                            NumericValue = _.ResultWords.Any(t => t.Id == 3) ? 1 : 0,
                            ResultWordsJson = GetResultWordsJson(subItem, ruleWord, _),
                            CategoryId = CategoryId,
                            PropertyType = 1,
                            SubmitBatchId = batchId,
                            CreateBy = submitBy,
                            CreateTime = now,
                            UpdateBy = submitBy,
                            UpdateTime = now
                        });

                        resultWordList.AddRange(_.ResultWords.Select(t => new ShopReceiveCheckResultWordDo
                        {
                            CheckId = checkId,
                            CheckResultId = _.CheckSubItemId,
                            CategoryId = CategoryId,
                            ResultWordId = t.Id,
                            SubmitBatchId = batchId,
                            CreateBy = submitBy,
                            CreateTime = now,
                            UpdateBy = submitBy,
                            UpdateTime = now
                        }));
                    });

                    await shopCheckResultRepository.InsertBatchAsync(shopCheckResultList);

                    await shopReceiveCheckResultWordRepository.InsertBatchAsync(resultWordList);

                    List<ShopCheckResultImageDo> shopCheckResultImage = new List<ShopCheckResultImageDo>();

                    foreach (var itemP in checkResultImage)
                    {
                        if (itemP.Images != null && itemP.Images.Any())
                        {
                            shopCheckResultImage.AddRange(itemP.Images.Where(_ => !string.IsNullOrEmpty(_)).Select(x => new ShopCheckResultImageDo
                            {
                                CheckId = checkId,
                                CategoryId = CategoryId,
                                Url = x,
                                PropertyId = itemP.CheckItemMainId,
                                SubmitBatchId = batchId,
                                CreateBy = submitBy,
                                CreateTime = now,
                                UpdateBy = submitBy,
                                UpdateTime = now
                            }));
                        }
                    }

                    if (shopCheckResultImage.Any())
                    {
                        await shopCheckResultImageRepository.InsertBatchAsync(shopCheckResultImage);
                    }
                }

                ts.Complete();

                return true;
            }
        }

        private string GetResultWordsJson(List<ShopReceiveCheckSubItemDo> subItem, List<ShopCheckRuleWordDo> ruleWord, UpgradeProjectRequest upgradeProjectRequest)
        {
            var resultWord = upgradeProjectRequest.ResultWords;
            var subItemId = upgradeProjectRequest.CheckSubItemId;
            string suggestion = string.Empty;
            var defaultItem = subItem.FirstOrDefault(_ => _.Id == subItemId);
            if (defaultItem != null)
            {
                if (!defaultItem.IsCompute)
                {
                    suggestion = defaultItem.Suggestion;
                }
                else
                {
                    List<int> wordIds = resultWord.Select(_ => _.Id).ToList();
                    suggestion = ruleWord.FirstOrDefault(_ => _.SubItemId == subItemId && wordIds.Contains(_.ResultWordId))?.Suggestion ?? string.Empty;
                }
            }
            ResultWordAndSuggestion jsonData = new ResultWordAndSuggestion
            {
                ResultWords = resultWord,
                Suggestion = suggestion
            };

            return JsonConvert.SerializeObject(jsonData);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="shopReceiveCheckDo"></param>
        /// <param name="updateField"></param>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <param name="checkResult"></param>
        /// <param name="submitBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>

        private async Task<bool> UpdateReceiveCheck(ShopReceiveCheckDo shopReceiveCheckDo, List<string> updateField, ReceiveCheckClassfyEnum receiveCheckClassfyEnum, List<NormalProjectRequest> checkResult, string submitBy, int totalCount)
        {
            DateTime now = DateTime.Now;
            int errorCount = 0;
            int normalCount = 0;

            List<NormalProjectRequest> errorProject = new List<NormalProjectRequest>();
            switch (receiveCheckClassfyEnum)
            {
                case ReceiveCheckClassfyEnum.Dashboard:
                case ReceiveCheckClassfyEnum.Inlook:
                case ReceiveCheckClassfyEnum.Outlook:
                    errorProject = checkResult.Where(_ => _.ResultValue == 1).ToList();
                    errorCount = errorProject.Count;
                    normalCount = totalCount - errorCount;
                    break;
                case ReceiveCheckClassfyEnum.Other:
                    errorProject = checkResult.Where(_ => _.ResultValue > 0).ToList();
                    break;
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (updateField != null && updateField.Any())
                {
                    var updateMain = await shopReceiveCheckRepository.UpdateAsync(shopReceiveCheckDo, updateField);
                }

                await shopReceiveCheckLogRepository.BatchDeleteLastData(shopReceiveCheckDo.Id, CategoryId, receiveCheckClassfyEnum.ToString(), submitBy);

                var batchId = await shopReceiveCheckLogRepository.InsertAsync<long>(new ShopReceiveCheckLogDo
                {
                    CheckId = shopReceiveCheckDo.Id,
                    CheckModule = EnumHelper.GetEnumDescription(receiveCheckClassfyEnum),
                    CheckModuleCode = receiveCheckClassfyEnum.ToString(),
                    CategoryId = CategoryId,
                    ErrorCount = errorCount,
                    NormalCount = normalCount,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                });

                if (errorProject.Any())
                {
                    List<ShopCheckResultDo> shopCheckResultList = errorProject.Select(_ => new ShopCheckResultDo
                    {
                        CheckId = shopReceiveCheckDo.Id,
                        PropertyId = _.ProjectId,
                        NumericValue = _.ResultValue,
                        CategoryId = CategoryId,
                        SubmitBatchId = batchId,
                        CreateBy = submitBy,
                        CreateTime = now,
                        UpdateBy = submitBy,
                        UpdateTime = now
                    }).ToList();

                    await shopCheckResultRepository.InsertBatchAsync(shopCheckResultList);

                    List<ShopCheckResultImageDo> shopCheckResultImage = new List<ShopCheckResultImageDo>();
                    foreach (var itemP in errorProject)
                    {
                        if (itemP.Image != null && itemP.Image.Any())
                        {
                            shopCheckResultImage.AddRange(itemP.Image.Where(_ => !string.IsNullOrEmpty(_.Url)).Select(x => new ShopCheckResultImageDo
                            {
                                CheckId = shopReceiveCheckDo.Id,
                                CategoryId = CategoryId,
                                Url = x.Url,
                                DisplayName = string.Join("，", x.Tips ?? new List<string>()),
                                PropertyId = itemP.ProjectId,
                                SubmitBatchId = batchId,
                                CreateBy = submitBy,
                                CreateTime = now,
                                UpdateBy = submitBy,
                                UpdateTime = now
                            }));
                        }
                    }

                    if (shopCheckResultImage.Any())
                    {
                        await shopCheckResultImageRepository.InsertBatchAsync(shopCheckResultImage);
                    }
                }

                ts.Complete();

                return true;
            }

        }

        /// <summary>
        /// 更新签字
        /// </summary>
        /// <param name="shopReceiveCheckDo"></param>
        /// <param name="updateField"></param>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>

        private async Task<bool> UpdateSignature(ShopReceiveCheckDo shopReceiveCheckDo, List<string> updateField, ReceiveCheckClassfyEnum receiveCheckClassfyEnum, string submitBy)
        {
            DateTime now = DateTime.Now;

            //using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (updateField != null && updateField.Any())
                {
                    var updateMain = await shopReceiveCheckRepository.UpdateAsync(shopReceiveCheckDo, updateField);
                }

                await shopReceiveCheckLogRepository.BatchDeleteLastSignatureData(shopReceiveCheckDo.Id, CategoryId, receiveCheckClassfyEnum.ToString(), submitBy);

                var batchId = await shopReceiveCheckLogRepository.InsertAsync<long>(new ShopReceiveCheckLogDo
                {
                    CheckId = shopReceiveCheckDo.Id,
                    CheckModule = EnumHelper.GetEnumDescription(receiveCheckClassfyEnum),
                    CheckModuleCode = receiveCheckClassfyEnum.ToString(),
                    CategoryId = CategoryId,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                });

                //ts.Complete();

                return true;
            }
        }

        /// <summary>
        /// 升级项更新
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="receiveCheckClassfyEnum"></param>
        /// <param name="checkResult"></param>
        /// <param name="checkResultImage"></param>
        /// <param name="submitBy"></param>
        /// <param name="subItem"></param>
        /// <param name="ruleWord"></param>
        /// <returns></returns>
        private async Task<bool> UpdateUpgradeReceiveCheck(long checkId, ReceiveCheckClassfyEnum receiveCheckClassfyEnum, List<UpgradeProjectRequest> checkResult,
            List<UpgradeProjectImageRequest> checkResultImage, string submitBy, List<ShopReceiveCheckSubItemDo> subItem, List<ShopCheckRuleWordDo> ruleWord)
        {
            DateTime now = DateTime.Now;
            var errorProject = checkResult.Where(_ => _.ResultWords.Any(t => t.Id == 3)).ToList();
            var errorCount = errorProject.Select(_ => _.CheckItemMainId).Distinct().ToList().Count;

            var normalProject = checkResult.Where(_ => !_.ResultWords.Any(t => t.Id == 3)).ToList();
            var normalCount = normalProject.Select(_ => _.CheckItemMainId).Distinct().ToList().Count;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await shopReceiveCheckLogRepository.BatchDeleteLastUpgradeData(checkId, CategoryId, receiveCheckClassfyEnum.ToString(), submitBy);

                var batchId = await shopReceiveCheckLogRepository.InsertAsync<long>(new ShopReceiveCheckLogDo
                {
                    CheckId = checkId,
                    CheckModule = EnumHelper.GetEnumDescription(receiveCheckClassfyEnum),
                    CheckModuleCode = receiveCheckClassfyEnum.ToString(),
                    CategoryId = CategoryId,
                    ErrorCount = errorCount,
                    NormalCount = normalCount,
                    CreateBy = submitBy,
                    CreateTime = now,
                    UpdateBy = submitBy,
                    UpdateTime = now
                });

                if (checkResult.Any())
                {
                    List<ShopCheckResultDo> shopCheckResultList = new List<ShopCheckResultDo>();
                    List<ShopReceiveCheckResultWordDo> resultWordList = new List<ShopReceiveCheckResultWordDo>();
                    checkResult.ForEach(_ =>
                    {
                        shopCheckResultList.Add(new ShopCheckResultDo
                        {
                            CheckId = checkId,
                            PropertyId = _.CheckSubItemId,
                            TextValue = _.TextValue,
                            NumericValue = _.ResultWords.Any(t => t.Id == 3) ? 1 : 0,
                            ResultWordsJson = GetResultWordsJson(subItem, ruleWord, _),
                            CategoryId = CategoryId,
                            PropertyType = 1,
                            SubmitBatchId = batchId,
                            CreateBy = submitBy,
                            CreateTime = now,
                            UpdateBy = submitBy,
                            UpdateTime = now
                        });

                        resultWordList.AddRange(_.ResultWords.Select(t => new ShopReceiveCheckResultWordDo
                        {
                            CheckId = checkId,
                            CheckResultId = _.CheckSubItemId,
                            CategoryId = CategoryId,
                            ResultWordId = t.Id,
                            SubmitBatchId = batchId,
                            CreateBy = submitBy,
                            CreateTime = now,
                            UpdateBy = submitBy,
                            UpdateTime = now
                        }));
                    });

                    await shopCheckResultRepository.InsertBatchAsync(shopCheckResultList);

                    await shopReceiveCheckResultWordRepository.InsertBatchAsync(resultWordList);

                    List<ShopCheckResultImageDo> shopCheckResultImage = new List<ShopCheckResultImageDo>();
                    foreach (var itemP in checkResultImage)
                    {
                        if (itemP.Images != null && itemP.Images.Any())
                        {
                            shopCheckResultImage.AddRange(itemP.Images.Where(_ => !string.IsNullOrEmpty(_)).Select(x => new ShopCheckResultImageDo
                            {
                                CheckId = checkId,
                                CategoryId = CategoryId,
                                Url = x,
                                PropertyId = itemP.CheckItemMainId,
                                SubmitBatchId = batchId,
                                CreateBy = submitBy,
                                CreateTime = now,
                                UpdateBy = submitBy,
                                UpdateTime = now
                            }));
                        }
                    }

                    if (shopCheckResultImage.Any())
                    {
                        await shopCheckResultImageRepository.InsertBatchAsync(shopCheckResultImage);
                    }
                }

                ts.Complete();

                return true;
            }

        }

        /// <summary>
        /// 检查项数据处理 （内饰/外观/仪表.附加）
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="checkModuleCode"></param>
        /// <returns></returns>
        private async Task<List<NormalProjectResult>> GetNormalProjectResult(long checkId, string checkModuleCode)
        {
            List<NormalProjectResult> result = new List<NormalProjectResult>();
            List<ShopCheckPropertyDo> checkProperty = new List<ShopCheckPropertyDo>();
            List<ShopCheckResultDo> checkResult = new List<ShopCheckResultDo>();
            List<ShopCheckResultImageDo> checkResultImage = new List<ShopCheckResultImageDo>();
            List<ShopCheckResultImageDo> exampleImage = new List<ShopCheckResultImageDo>();
            var property = await shopCheckPropertyRepository.GetPropertyByKeyName(checkModuleCode, CategoryId);
            if (property != null)
            {
                checkProperty = await shopCheckPropertyRepository.GetShopCheckProperty(CategoryId, property.Id);
            }

            if (checkId > 0)
            {
                var checkResultTask = shopCheckResultRepository.GetShopCheckResult(checkId, CategoryId, 0);
                var checkResultImageTask = shopCheckResultImageRepository.GetCheckResultImageByCheckId(checkId, CategoryId);
                await Task.WhenAll(checkResultTask, checkResultImageTask);
                checkResult = checkResultTask.Result ?? new List<ShopCheckResultDo>();
                checkResultImage = checkResultImageTask.Result;
            }

            if (checkModuleCode == "Dashboard")
            {
                exampleImage = await shopCheckResultImageRepository.GetCheckResultImageByCheckId(-100, CategoryId);//仪表盘故障灯示例图
            }
            checkProperty.ForEach(_ =>
            {
                var errorItem = checkResult.FirstOrDefault(x => x.PropertyId == _.Id);
                NormalProjectResult itemResult = new NormalProjectResult
                {
                    ProjectId = _.Id,
                    ProjectName = _.DisplayName,
                    KeyName = _.KeyName,
                    ExampleImage = exampleImage.Where(x => x.DisplayName == _.KeyName).Select(x => new ExampleImage
                    {
                        Url = x.Url,
                        Type = x.ImageType
                    }).ToList(),
                    ResultValue = errorItem?.NumericValue ?? 0,
                    AbnormalId = errorItem?.Id ?? 0,
                    Image = checkResultImage.Where(x => x.PropertyId == _.Id).Select(x => new ErrorImageVo
                    {
                        Url = x.Url
                    }).ToList()
                };

                result.Add(itemResult);
            });

            return result;
        }

        /// <summary>
        /// 检查项数据处理
        /// </summary>
        /// <param name="property"></param>
        /// <param name="checkLog"></param>
        /// <returns></returns>
        private List<ProjectClassifyVo> GetProjectClassify(List<ShopCheckPropertyDo> property, List<ShopReceiveCheckLogDo> checkLog)
        {
            List<ProjectClassifyVo> result = new List<ProjectClassifyVo>();
            var normalProjects = property.Where(_ => _.Condition == "0").ToList();
            var upgradeProjects = property.Where(_ => _.Condition == "10000").ToList();
            var signatureProjects = property.Where(_ => _.Condition == "20000").ToList();
            if (normalProjects.Any())
            {
                result.Add(new ProjectClassifyVo
                {
                    Classify = "NormalProjects",
                    ClassifyName = "常规项目",
                    CheckProjects = normalProjects.Select(_ => new CheckProjectVo
                    {
                        CheckModule = _.DisplayName,
                        CheckModuleCode = _.KeyName,
                        Status = checkLog.Any(t => t.CheckModuleCode == _.KeyName) ? 1 : 0,
                        AbnormalCount = checkLog.FirstOrDefault(t => t.CheckModuleCode == _.KeyName)?.ErrorCount ?? 0
                    }).ToList()
                });
            }
            if (upgradeProjects.Any())
            {
                result.Add(new ProjectClassifyVo
                {
                    Classify = "UpgradeProjects",
                    ClassifyName = "升级项目",
                    CheckProjects = upgradeProjects.Select(_ => new CheckProjectVo
                    {
                        CheckModule = _.DisplayName,
                        CheckModuleCode = _.KeyName,
                        Status = checkLog.Any(t => t.CheckModuleCode == _.KeyName) ? 1 : 0,
                        AbnormalCount = checkLog.FirstOrDefault(t => t.CheckModuleCode == _.KeyName)?.ErrorCount ?? 0
                    }).ToList()
                });
            }
            if (signatureProjects.Any())
            {
                result.Add(new ProjectClassifyVo
                {
                    Classify = "SignatureProjects",
                    ClassifyName = "签字确认",
                    CheckProjects = signatureProjects.Select(_ => new CheckProjectVo
                    {
                        CheckModule = _.DisplayName,
                        CheckModuleCode = _.KeyName,
                        Status = checkLog.Any(t => t.CheckModuleCode == _.KeyName) ? 1 : 0
                    }).ToList()
                });
            }

            return result;
        }

        /// <summary>
        /// 查询检查报告主数据
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="checkId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        private async Task<ShopReceiveCheckDo> GetReceiveCheckData(long recId, long checkId, bool readOnly = true)
        {
            if (checkId > 0)
            {
                return await shopReceiveCheckRepository.GetAsync<ShopReceiveCheckDo>(checkId, !readOnly);
            }
            else if (recId > 0)
            {
                return await shopReceiveCheckRepository.GetReceiveCheckByRecId(recId, readOnly);
            }
            return null;
        }

        /// <summary>
        /// 检查报告数据
        /// </summary>
        /// <param name="resultWord"></param>
        /// <param name="checkLog"></param>
        /// <param name="exampleImage"></param>
        /// <param name="config"></param>
        /// <param name="checkResult"></param>
        /// <param name="resultImage"></param>
        /// <returns></returns>
        private List<CheckResultClassify> GetCheckResultClassify(List<ShopReceiveResultWordDo> resultWord, List<ShopReceiveCheckLogDo> checkLog,
            List<ShopCheckResultImageDo> exampleImage, List<ShopCheckPropertyDo> config, List<ShopCheckResultDo> checkResult,
            List<ShopCheckResultImageDo> resultImage, List<ShopReceiveCheckSubItemDo> subItem)
        {
            List<CheckResultClassify> checkItem = resultWord.Where(_ => _.Type == "ResultWord" && _.WorkGroup == "checkresult").OrderBy(_ => _.Rank).Select(_ => new CheckResultClassify
            {
                ResultWordCode = _.Code,
                DisplayName = _.Name
            }).ToList();

            List<CheckResultItem> errorCheckItem = new List<CheckResultItem>();
            List<CheckClassfy> unCheckItem = new List<CheckClassfy>();
            List<CheckClassfy> normalCheckItem = new List<CheckClassfy>();

            var otherParent = config.FirstOrDefault(_ => _.KeyName == ReceiveCheckClassfyEnum.Dashboard.ToString() && _.ParentId == 0);
            if (otherParent != null)
            {
                var itemConfig = config.Where(_ => _.ParentId == otherParent.Id).ToList();
                if (itemConfig.Any())
                {
                    var itemProperty = itemConfig.Select(_ => _.Id).ToList();
                    var defaultLog = checkLog.FirstOrDefault(_ => _.CheckModuleCode == ReceiveCheckClassfyEnum.Dashboard.ToString());
                    if (defaultLog != null)
                    {
                        var errorDashboard = checkResult.Where(_ => _.SubmitBatchId == defaultLog.Id).ToList();
                        var errorProperty = errorDashboard.Select(_ => _.PropertyId).ToList();
                        var errorKeyName = itemConfig.Where(_ => errorProperty.Contains(_.Id)).Select(_ => _.KeyName);
                        if (errorDashboard.Any())
                        {
                            errorCheckItem.Add(new CheckResultItem
                            {
                                DisplayName = $"{EnumHelper.GetEnumDescription(ReceiveCheckClassfyEnum.Dashboard)}-故障灯",
                                KeyName = ReceiveCheckClassfyEnum.Dashboard.ToString(),
                                Images = exampleImage.Where(_ => errorKeyName.Contains(_.DisplayName) && _.ImageType == 1).Select(t => t.Url).ToList()
                            });
                        }

                        var normalProperty = itemProperty.Where(_ => !errorProperty.Contains(_));
                        var normalKeyName = itemConfig.Where(_ => normalProperty.Contains(_.Id)).Select(_ => _.KeyName);
                        if (normalProperty.Any())
                        {
                            normalCheckItem.Add(new CheckClassfy
                            {
                                ClassfyName = $"{EnumHelper.GetEnumDescription(ReceiveCheckClassfyEnum.Dashboard)}-故障灯",
                                ClassfyCode = ReceiveCheckClassfyEnum.Dashboard.ToString(),
                                ResultItems = new List<CheckResultItem>
                                {
                                    new CheckResultItem
                                    {
                                        KeyName = ReceiveCheckClassfyEnum.Dashboard.ToString(),
                                        Images = exampleImage.Where(t => normalKeyName.Contains(t.DisplayName) && t.ImageType == 0).Select(t => t.Url).ToList()
                                    }
                                }
                            });
                        }
                    }
                    else
                    {
                        unCheckItem.Add(new CheckClassfy
                        {
                            ClassfyName = $"{EnumHelper.GetEnumDescription(ReceiveCheckClassfyEnum.Dashboard)}-故障灯",
                            ClassfyCode = ReceiveCheckClassfyEnum.Dashboard.ToString(),
                            ResultItems = new List<CheckResultItem>
                            {
                                new CheckResultItem
                                {
                                    KeyName = ReceiveCheckClassfyEnum.Dashboard.ToString(),
                                    Images = exampleImage.Where(t => t.ImageType == 0).Select(t => t.Url).ToList()
                                }
                            }
                        });
                    }
                }
            }

            List<ReceiveCheckClassfyEnum> lookEnum = new List<ReceiveCheckClassfyEnum>
            {
                ReceiveCheckClassfyEnum.Inlook,
                ReceiveCheckClassfyEnum.Outlook,
                ReceiveCheckClassfyEnum.ZhiJian
            };

            foreach (var typeItem in lookEnum)
            {
                var lookParent = config.FirstOrDefault(_ => _.KeyName == typeItem.ToString() && _.ParentId == 0);
                if (lookParent != null)
                {
                    var itemConfig = config.Where(_ => _.ParentId == lookParent.Id).ToList();
                    if (itemConfig.Any())
                    {
                        var itemProperty = itemConfig.Select(_ => _.Id).ToList();
                        var defaultLog = checkLog.FirstOrDefault(_ => _.CheckModuleCode == typeItem.ToString());
                        if (defaultLog != null)
                        {
                            var errorDashboard = checkResult.Where(_ => _.SubmitBatchId == defaultLog.Id).ToList();
                            var errorProperty = errorDashboard.Select(_ => _.PropertyId).ToList();
                            var errorConfig = itemConfig.Where(_ => errorProperty.Contains(_.Id)).ToList();
                            if (errorConfig.Any())
                            {
                                errorCheckItem.AddRange(errorConfig.Select(_ => new CheckResultItem
                                {
                                    CheckItemId = _.Id,
                                    DisplayName = $"{EnumHelper.GetEnumDescription(typeItem)}-{ _.DisplayName}",
                                    DisplayDes = _.DisplayDes,
                                    KeyName = _.KeyName,
                                    FunctionDes = _.FunctionDes,
                                    Images = resultImage.Where(t => t.PropertyId == _.Id).Select(t => t.Url).ToList(),
                                    ErrorDesList = new List<ErrorDesList> {
                                        new ErrorDesList
                                        {
                                            ErrorDes = resultImage.Where(t => t.PropertyId == _.Id).Select(t => t.DisplayName).ToList()
                                        }
                                    }
                                }));
                            }

                            var normalConfig = itemConfig.Where(_ => !errorProperty.Contains(_.Id)).ToList();
                            if (normalConfig.Any())
                            {
                                normalCheckItem.Add(new CheckClassfy
                                {
                                    ClassfyName = $"{EnumHelper.GetEnumDescription(typeItem)}",
                                    ClassfyCode = typeItem.ToString(),
                                    ResultItems = normalConfig.Select(_ => new CheckResultItem
                                    {
                                        CheckItemId = _.Id,
                                        DisplayName = _.DisplayName,
                                        DisplayDes = _.DisplayDes,
                                        KeyName = _.KeyName,
                                        FunctionDes = _.FunctionDes,
                                        NormalDes = new List<string> { "正常√" }
                                    }).ToList()
                                });
                            }
                        }
                        else
                        {
                            unCheckItem.Add(new CheckClassfy
                            {
                                ClassfyName = $"{EnumHelper.GetEnumDescription(typeItem)}",
                                ClassfyCode = typeItem.ToString(),
                                ResultItems = itemConfig.Select(_ => new CheckResultItem
                                {
                                    CheckItemId = _.Id,
                                    DisplayName = _.DisplayName,
                                    DisplayDes = _.DisplayDes,
                                    KeyName = _.KeyName,
                                    FunctionDes = _.FunctionDes,
                                    NormalDes = new List<string> { "未检查" }
                                }).ToList()
                            });
                        }
                    }
                }
            }

            List<ReceiveCheckClassfyEnum> upgradeEnum = new List<ReceiveCheckClassfyEnum>
            {
                ReceiveCheckClassfyEnum.BatteryPack,
                ReceiveCheckClassfyEnum.HighVoltageWiring,
                ReceiveCheckClassfyEnum.ChargingSystem,
                ReceiveCheckClassfyEnum.DrivingCab,
                ReceiveCheckClassfyEnum.Lamplight,
                ReceiveCheckClassfyEnum.EngineRoom,
                ReceiveCheckClassfyEnum.Tire,
                ReceiveCheckClassfyEnum.BrakeDisc,
                ReceiveCheckClassfyEnum.DropInCheck,
                ReceiveCheckClassfyEnum.DropInPageCheck
            };

            foreach (var typeItem in upgradeEnum)
            {
                CheckClassfy normalClassfy = new CheckClassfy
                {
                    ClassfyName = $"{EnumHelper.GetEnumDescription(typeItem)}",
                    ClassfyCode = typeItem.ToString(),
                    ResultItems = new List<CheckResultItem>()
                };

                CheckClassfy unCheckClassfy = new CheckClassfy
                {
                    ClassfyName = $"{EnumHelper.GetEnumDescription(typeItem)}",
                    ClassfyCode = typeItem.ToString(),
                    ResultItems = new List<CheckResultItem>()
                };

                var upgradeParent = config.FirstOrDefault(_ => _.KeyName == typeItem.ToString() && _.ParentId == 0);
                if (upgradeParent != null)
                {
                    var itemConfig = config.Where(_ => _.ParentId == upgradeParent.Id).ToList();
                    if (itemConfig.Any())
                    {
                        foreach (var itemProperty in itemConfig)
                        {
                            var itemSubItem = subItem.Where(_ => _.CheckItemMainId == itemProperty.Id).ToList();//项所有结果
                            var subItemIds = itemSubItem.Select(_ => _.Id).ToList();
                            var itemResult = checkResult.Where(_ => _.PropertyType == 1 && subItemIds.Contains(_.PropertyId)).ToList();
                            if (itemResult.Any())
                            {
                                var errorItemResult = itemResult.Where(_ => _.NumericValue == 1).ToList();
                                if (errorItemResult.Any())
                                {
                                    var errorSubIds = errorItemResult.Select(_ => _.PropertyId).ToList();
                                    var itemCheck = new CheckResultItem
                                    {
                                        CheckItemId = itemProperty.Id,
                                        DisplayName = $"{EnumHelper.GetEnumDescription(typeItem)}-{ itemProperty.DisplayName}",
                                        DisplayDes = itemProperty.DisplayDes,
                                        KeyName = itemProperty.KeyName,
                                        FunctionDes = itemProperty.FunctionDes,
                                        Images = resultImage.Where(t => t.PropertyId == itemProperty.Id).Select(t => t.Url).ToList(),
                                    };
                                    SetErrorDes(errorItemResult, itemSubItem.Where(_ => errorSubIds.Contains(_.Id)).ToList(), config, itemCheck);
                                    errorCheckItem.Add(itemCheck);
                                }
                                else
                                {
                                    var normalSubIds = itemResult.Select(_ => _.PropertyId).ToList();
                                    normalClassfy.ResultItems.Add(new CheckResultItem
                                    {
                                        CheckItemId = itemProperty.Id,
                                        DisplayName = itemProperty.DisplayName,
                                        DisplayDes = itemProperty.DisplayDes,
                                        KeyName = itemProperty.KeyName,
                                        FunctionDes = itemProperty.FunctionDes,
                                        NormalDes = GetNormalDes(itemResult, itemSubItem.Where(_ => normalSubIds.Contains(_.Id)).ToList())
                                    });
                                }
                            }
                            else
                            {
                                unCheckClassfy.ResultItems.Add(new CheckResultItem
                                {
                                    CheckItemId = itemProperty.Id,
                                    DisplayName = itemProperty.DisplayName,
                                    DisplayDes = itemProperty.DisplayDes,
                                    KeyName = itemProperty.KeyName,
                                    FunctionDes = itemProperty.FunctionDes,
                                    NormalDes = new List<string> { "未检查" }
                                });
                            }
                        }
                    }
                }

                if (normalClassfy.ResultItems.Any())
                {
                    normalCheckItem.Add(normalClassfy);
                }
                if (unCheckClassfy.ResultItems.Any())
                {
                    unCheckItem.Add(unCheckClassfy);
                }
            }

            checkItem.ForEach(_ =>
            {
                if (_.ResultWordCode == "AbNormal")
                {
                    _.CheckClassfy = new List<CheckClassfy>
                    {
                        new CheckClassfy
                        {
                            ResultItems = errorCheckItem
                        }
                    };
                    var dashCount = errorCheckItem.Where(f => f.KeyName == ReceiveCheckClassfyEnum.Dashboard.ToString()).SelectMany(x => x.Images).ToList().Count;
                    var notDashCount = errorCheckItem.Where(f => f.KeyName != ReceiveCheckClassfyEnum.Dashboard.ToString()).ToList().Count;
                    _.Count = notDashCount + dashCount;
                }
                else if (_.ResultWordCode == "Normal")
                {
                    var dashCount = normalCheckItem.Where(f => f.ClassfyCode == ReceiveCheckClassfyEnum.Dashboard.ToString()).SelectMany(t => t.ResultItems).SelectMany(t => t.Images).ToList().Count;
                    var notDashCount = normalCheckItem.Where(f => f.ClassfyCode != ReceiveCheckClassfyEnum.Dashboard.ToString()).SelectMany(t => t.ResultItems).ToList().Count;
                    _.CheckClassfy = normalCheckItem;
                    _.Count = notDashCount + dashCount;
                }
                else if (_.ResultWordCode == "None")
                {
                    var dashCount = unCheckItem.Where(f => f.ClassfyCode == ReceiveCheckClassfyEnum.Dashboard.ToString()).SelectMany(t => t.ResultItems).SelectMany(t => t.Images).ToList().Count;
                    var notDashCount = unCheckItem.Where(f => f.ClassfyCode != ReceiveCheckClassfyEnum.Dashboard.ToString()).SelectMany(t => t.ResultItems).ToList().Count;
                    _.CheckClassfy = unCheckItem;
                    _.Count = notDashCount + dashCount;
                }
            });

            return checkItem;
        }

        #endregion
    }
}
