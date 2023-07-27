using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys;
using Ae.Shop.Service.Dal.Repositorys.EmployeePerformance;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using Ae.Shop.Service.Dal.Repositorys.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class EmployeePerformanceService : IEmployeePerformanceService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<EmployeePerformanceService> _logger;
        private readonly IBasicPerformanceConfigRepository basicPerformanceConfigRepository;
        private readonly IShopPerformanceConfigRepository shopPerformanceConfigRepository;
        private readonly IEmployeePerformanceReportRepository employeePerformanceReportRepository;
        private readonly IPullNewConfigRepository pullNewConfigRepository;
        private readonly IShopServiceTypeRepository shopServiceTypeRepository;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IShopRepository shopRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IShopUserRelationRepository shopUserRelationRepository;
        private readonly IOrderClient orderClient;
        private readonly IShopOrderClient shopOrderClient;
        private readonly IReceiveClient receiveClient;
        private readonly IJobRepository jobRepository;
        private readonly IProductClient _productClient;
        public EmployeePerformanceService(IMapper mapper,
            ApolloErpLogger<EmployeePerformanceService> logger,
           IBasicPerformanceConfigRepository basicPerformanceConfigRepository, IShopPerformanceConfigRepository shopPerformanceConfigRepository,
           IEmployeePerformanceReportRepository employeePerformanceReportRepository,
           IPullNewConfigRepository pullNewConfigRepository,
           IShopServiceTypeRepository shopServiceTypeRepository,
           IBaoYangClient baoYangClient, IShopRepository shopRepository,
           IShopUserRelationRepository shopUserRelationRepository, IOrderClient orderClient,
          IShopOrderClient shopOrderClient, IReceiveClient receiveClient, IEmployeeRepository employeeRepository, 
          IJobRepository jobRepository, IProductClient productClient)
        {
            this._mapper = mapper;
            this._logger = logger;
            this.basicPerformanceConfigRepository = basicPerformanceConfigRepository;
            this.shopPerformanceConfigRepository = shopPerformanceConfigRepository;
            this.employeePerformanceReportRepository = employeePerformanceReportRepository;
            this.pullNewConfigRepository = pullNewConfigRepository;
            this.shopServiceTypeRepository = shopServiceTypeRepository;
            this._baoYangClient = baoYangClient;
            this.shopRepository = shopRepository;
            this.shopUserRelationRepository = shopUserRelationRepository;
            this.orderClient = orderClient;
            this.shopOrderClient = shopOrderClient;
            this.receiveClient = receiveClient;
            this.employeeRepository = employeeRepository;
            this.jobRepository = jobRepository;
            _productClient = productClient;
        }


        #region  个人绩效配置
        public async Task<ApiResult<string>> CreateOrUpdateBasicPerformanceConfig(CreateBasicPerformanceConfigRequest request)
        {
            _logger.Info($"CreateOrUpdateBasicPerformanceConfig request= " + JsonConvert.SerializeObject(request));
            var res = new ApiResult<string>();
            try
            {
                var vo = await basicPerformanceConfigRepository.GetBasicPerformanceConfigs(new GetBasicPerformanceConfigRequest
                {
                    ShopId = request.ShopId
                });
                if (vo.Any())
                {
                    foreach (var item in request.Configs)
                    {
                        if (vo.Any(r => r.ServiceType == item.ServiceType))
                        {
                            await basicPerformanceConfigRepository.UpdateBasicPerformancePoint(new BasicPerformanceConfigDO
                            {
                                UpdateBy = request.UpdateBy,
                                ShopId = request.ShopId,
                                ServiceType = item.ServiceType,
                                ConfigType = item.ConfigType,
                                ConfigPoint = item.ConfigPoint,
                                ConfigFlag = Convert.ToSByte(request.ConfigFlag)
                            });
                        }
                        else
                        {
                            await basicPerformanceConfigRepository.InsertAsync(new BasicPerformanceConfigDO
                            {
                                ShopId = request.ShopId,
                                ServiceType = item.ServiceType,
                                ConfigType = item.ConfigType,
                                ConfigPoint = item.ConfigPoint,
                                ServiceConfigFlag = 1,
                                ConfigFlag = Convert.ToSByte(request.ConfigFlag),
                                ConfigFlagTime = DateTime.Now,
                                IsDeleted = 0,
                                CreateBy = request.UpdateBy,
                                CreateTime = DateTime.Now
                            });
                        }
                            
                    }
                }
                //组装数据
                else
                {
                    foreach (var item in request.Configs)
                    {
                        await basicPerformanceConfigRepository.InsertAsync(new BasicPerformanceConfigDO
                        {
                            ShopId = request.ShopId,
                            ServiceType = item.ServiceType,
                            ConfigType = item.ConfigType,
                            ConfigPoint = item.ConfigPoint,
                            ServiceConfigFlag = 1,
                            ConfigFlag = Convert.ToSByte(request.ConfigFlag),
                            ConfigFlagTime = DateTime.Now,
                            IsDeleted = 0,
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateOrUpdateBasicPerformanceConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }


        public async Task<ApiResult<GetBasicPerformanceConfigResponse>> GetBasicPerformanceConfig(GetBasicPerformanceConfigRequest request)
        {
            var res = new ApiResult<GetBasicPerformanceConfigResponse>();
            try
            {
                var vo = await basicPerformanceConfigRepository.GetBasicPerformanceConfigs(new GetBasicPerformanceConfigRequest
                {
                    ShopId = request.ShopId
                });
                if (vo.Any())
                {
                    //目前不存在单个服务开关  所以暂时取一个                  
                    var configRes = new GetBasicPerformanceConfigResponse
                    {
                        ShopId = request.ShopId,
                        ActiveText = "保存的配置需要次日才能生效"
                    };

                    #region  汇总门店服务项目记录
                    var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                    var serviceTypeDic = serviceTypes.ToDictionary(r => r.ServiceType, r => r);
                    var shopServiceTypes = await shopServiceTypeRepository.GetShopServiceTypeListAsync(request.ShopId);
                    shopServiceTypes = shopServiceTypes.Where(r => r.IsDeleted == 0).ToList();

                    if (!shopServiceTypes.Any())
                    {
                        res.Code = ResultCode.Success;
                        return res;
                    }
                    var activeShopServices = new List<ShopServiceTypeDTO>();
                    foreach (var item in shopServiceTypes)
                    {
                        if (serviceTypeDic.ContainsKey(item.ServiceType))
                        {
                            activeShopServices.Add(new ShopServiceTypeDTO
                            {
                                DisplayName = serviceTypeDic[item.ServiceType].DisplayName,
                                ServiceType = item.ServiceType,
                                ShopId = request.ShopId
                            });
                        }
                    }
                    #endregion


                    var serviceConfigs = _mapper.Map<List<ShopServiceConfigDTO>>(vo);
                    foreach (var item in activeShopServices)
                    {
                        if (serviceConfigs.Any(r => r.ServiceType == item.ServiceType))
                        {
                            var serviceConfig = serviceConfigs.First(r => r.ServiceType == item.ServiceType);

                            configRes.Configs.Add(new ShopServiceConfigDTO
                            {
                                ServiceType = serviceConfig.ServiceType,
                                ServiceTypeLabel = item.DisplayName,
                                ConfigPoint = serviceConfig.ConfigPoint,
                                ConfigType = serviceConfig.ConfigType
                            });
                        }
                        else
                        {
                            configRes.Configs.Add(new ShopServiceConfigDTO
                            {
                                ServiceType = item.ServiceType,
                                ServiceTypeLabel = item.DisplayName,
                                ConfigPoint = 0,
                                ConfigType = -1 //以实际类型为主 标记特殊值!!
                            });
                        }
                    }
                    if (configRes.Configs.Any())
                    {
                        var flag = vo.OrderByDescending(r => r.ConfigFlagTime).FirstOrDefault()?.ConfigFlag;
                        configRes.ConfigFlag = Convert.ToSByte(flag);
                        if (configRes.Configs.Any(r => r.ConfigType == -1))
                        {
                            var configItemType = configRes.Configs.FirstOrDefault()?.ConfigType;
                            foreach (var item in configRes.Configs.Where(r => r.ConfigType == -1))
                            {
                                item.ConfigType = Convert.ToSByte(configItemType);
                            }
                        }
                        configRes.ConfigType = configRes.Configs.First().ConfigType;
                        res.Data = configRes;
                    }
                }
                else
                {
                    var configRes = new GetBasicPerformanceConfigResponse
                    {
                        ShopId = request.ShopId,
                        ConfigFlag = 1,
                        ConfigType = 1
                    };

                    //获取已上架的服务项目组装数据
                    var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                    var serviceTypeDic = serviceTypes.ToDictionary(r => r.ServiceType, r => r);

                    var shopServiceTypes = await shopServiceTypeRepository.GetShopServiceTypeListAsync(request.ShopId);
                    shopServiceTypes = shopServiceTypes.Where(r => r.IsDeleted == 0).ToList();

                    if (shopServiceTypes.Any())
                    {
                        //取状态有效的记录
                        foreach (var item in shopServiceTypes)
                        {
                            configRes.Configs.Add(new ShopServiceConfigDTO
                            {
                                ServiceType = item.ServiceType,
                                ServiceTypeLabel = serviceTypeDic.ContainsKey(item.ServiceType) ? serviceTypeDic[item.ServiceType].DisplayName : string.Empty,
                                ConfigPoint = 0,
                                ConfigType = 1
                            });
                        }
                        res.Data = configRes;
                    }
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetBasicPerformanceConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        public async Task<ApiResult<string>> UpdateBasicPerformanceFlag(UpdateBasicPerformanceFlagRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                //判断是否存在记录 如果不存在 系统默认添加一条记录 
                //如果存在 需要判断更新了什么开关 修改相应的时间

                var configs = await basicPerformanceConfigRepository.GetBasicPerformanceConfigs(new GetBasicPerformanceConfigRequest
                {
                    ShopId = request.ShopId
                });
                if (configs.Any())
                {
                    await basicPerformanceConfigRepository.UpdateBasicPerformanceConfig(request);
                }
                else
                {
                    //初始化生成记录
                    #region  汇总门店服务项目记录
                    var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                    var serviceTypeDic = serviceTypes.ToDictionary(r => r.ServiceType, r => r);
                    var shopServiceTypes = await shopServiceTypeRepository.GetShopServiceTypeListAsync(request.ShopId);
                    shopServiceTypes = shopServiceTypes.Where(r => r.IsDeleted == 0).ToList();

                    if (!shopServiceTypes.Any())
                    {
                        res.Message = $"门店{request.ShopId}服务项目都下架!";
                        res.Code = ResultCode.Success;
                    }
                    var activeShopServices = new List<ShopServiceTypeDTO>();
                    foreach (var item in shopServiceTypes)
                    {
                        if (serviceTypeDic.ContainsKey(item.ServiceType))
                        {
                            activeShopServices.Add(new ShopServiceTypeDTO
                            {
                                DisplayName = serviceTypeDic[item.ServiceType].DisplayName,
                                ServiceType = item.ServiceType,
                                ShopId = request.ShopId
                            });
                        }
                    }
                    #endregion

                    //批量初始化数据
                    foreach (var item in activeShopServices)
                    {
                        await basicPerformanceConfigRepository.InsertAsync(new BasicPerformanceConfigDO
                        {
                            ShopId = request.ShopId,
                            ServiceType = item.DisplayName,
                            ConfigFlag = Convert.ToSByte(request.ConfigFlag),
                            ConfigPoint = 0,
                            ServiceConfigFlag = 1,
                            IsDeleted = 0,
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateBasicPerformanceFlag_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        #endregion

        #region 拉新配置
        /// <summary>
        /// 获取拉新配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetPullNewConfigResponse>> GetPullNewConfig(GetPullNewConfigRequest request)
        {
            var res = new ApiResult<GetPullNewConfigResponse>();
            try
            {
                var result = await pullNewConfigRepository.GetPullNewConfig(request);
                if (result != null)
                {
                    var vo = _mapper.Map<GetPullNewConfigResponse>(result);
                    res.Code = ResultCode.Success;
                    res.Data = vo;
                }
                else
                {
                    //不存在拉新数据 需要组装参数
                    var vo = new GetPullNewConfigResponse
                    {
                        PullNewFlag = true,
                        PullNewPoint = 0,
                        FirstConsumeFlag = true,
                        FirstConsumePoint = 0,
                        FirstConsumeType = 1,
                        ShopId = request.ShopId
                    };
                    res.Code = ResultCode.Success;
                    res.Data = vo;
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"GetPullNewConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        /// <summary>
        /// 修改拉新开关
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdatePullNewFlag(UpdatePullNewFlagRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                //判断是否存在记录 如果不存在 系统默认添加一条记录 
                //如果存在 需要判断更新了什么开关 修改相应的时间
                var result = await pullNewConfigRepository.GetPullNewConfig(new GetPullNewConfigRequest
                {
                    ShopId = request.ShopId
                });
                if (result != null)
                {
                    //更新相应的开关
                    if (request.FirstConsumeFlag != result.FirstConsumeFlag)
                    {
                        request.Type = 2;
                        await pullNewConfigRepository.UpdatePullNewConfigFlag(request);
                    }
                    else if (request.PullNewFlag != result.PullNewFlag)
                    {
                        request.Type = 1;
                        await pullNewConfigRepository.UpdatePullNewConfigFlag(request);
                    }
                    else {
                        request.Type = 3;
                        await pullNewConfigRepository.UpdatePullNewConfigFlag(request);
                    }
                }
                else
                {
                    //新增一条记录 开关记录以前端为主
                    var pullConfig = new PullNewConfigDO
                    {
                        CreateBy = request.UpdateBy,
                        CreateTime = DateTime.Now,
                        ShopId = request.ShopId,
                        IsActive = 1,
                        IsDeleted = 0
                    };
                    if (request.PullNewFlag != 1)
                    {
                        pullConfig.PullNewFlag = request.PullNewFlag;
                        pullConfig.PullNewTime = DateTime.Now;
                    }
                    else if (request.FirstConsumeFlag != 1)
                    {
                        pullConfig.FirstConsumeFlag = request.FirstConsumeFlag;
                        pullConfig.FirstConsumeTime = DateTime.Now;
                    }
                    await pullNewConfigRepository.InsertAsync(pullConfig);
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateBasicPerformanceFlag_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        /// <summary>
        /// 新增/修改拉新配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateOrUpdatePullNewConfig(CreateOrUpdatePullNewConfigRequest request)
        {
            var res = new ApiResult<string>();
            try
            {
                var result = await pullNewConfigRepository.GetPullNewConfig(new GetPullNewConfigRequest
                {
                    ShopId = request.ShopId
                });
                if (result != null)
                {
                    var vo = _mapper.Map<PullNewConfigDO>(request);
                    vo.UpdateBy = request.CreateBy;
                    await pullNewConfigRepository.UpdatePullNewConfig(vo);
                }
                else
                {
                    var vo = _mapper.Map<PullNewConfigDO>(request);
                    vo.CreateBy = request.CreateBy;
                    vo.CreateTime = DateTime.Now;
                    vo.IsDeleted = 0;
                    vo.IsActive = 1;
                    vo.PullNewTime = DateTime.Now;
                    vo.FirstConsumeTime = DateTime.Now;
                    await pullNewConfigRepository.InsertAsync(vo);
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateOrUpdatePullNewConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        #endregion


        #region 新门店绩效配置

        /// <summary>
        /// 获取门店绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopPerformanceConfigDTO>>> GetShopPerformanceConfig(GetBasicPerformanceConfigRequest request)
        {
            var res = new ApiResult<List<ShopPerformanceConfigDTO>>();
            try
            {
                var result = await shopPerformanceConfigRepository.GetShopPerformanceConfig(request);
                //_logger.Info($"GetShopPerformanceConfig result:{JsonConvert.SerializeObject(result)}");
                if (result != null)
                {
                    var vo = _mapper.Map<List<ShopPerformanceConfigDTO>>(result);
                    //_logger.Info($"GetShopPerformanceConfig vo:{JsonConvert.SerializeObject(vo)}");
                    res.Data = vo;
                }

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetShopPerformanceConfig Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        /// <summary>
        /// 新增门店绩效服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateShopPerformanceConfig(ShopPerformanceConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var vo = _mapper.Map<ShopPerformanceConfigDO>(request);

                vo.CreateTime = DateTime.Now;
                vo.IsDeleted = 0;
                await shopPerformanceConfigRepository.InsertAsync(vo);

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateShopPerformanceConfig Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        /// <summary>
        /// 更新门店绩效配置
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateShopPerformanceConfig(ShopPerformanceConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var vo = _mapper.Map<ShopPerformanceConfigDO>(request);

                vo.UpdateTime = DateTime.Now;
          
                await shopPerformanceConfigRepository.UpdateShopPerformanceConfig(vo);

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateShopPerformanceFlag Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        /// <summary>
        /// 更新门店绩效配置
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteShopPerformanceConfig(ShopPerformanceConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var vo = _mapper.Map<ShopPerformanceConfigDO>(request);

                vo.UpdateTime = DateTime.Now;

                await shopPerformanceConfigRepository.DeleteShopPerformanceConfig(vo);

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteShopPerformanceConfig Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
                res.Message = "操作失败!";
            }
            return res;
        }

        #endregion

        #region 员工绩效报表汇总

        /// <summary>
        /// 员工绩效报表汇总统计
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> CollectEmployeePerformanceReport()
        {
            var res = new ApiResult<string>();
            try
            {
                #region 1.获取有效的门店 状态非终止 审核通过 已上架
                var shops = await shopRepository.GetShopPerformances();
                #endregion

                #region 2.获取门店的在职技师
                var shopIds = shops.Select(r => r.Id).ToList();
                var employeeDic = await GetTechDic(shopIds);
                #endregion

                #region 3.获取所有门店安装绩效配置
                var basicPerformanceConfigDic = await GetBasicPerformanceConfigs(shopIds);
                #endregion

                #region 4.获取拉新绩效配置
                var pullNewConfigs = await pullNewConfigRepository.GetListAsync($" where shop_id in ({string.Join(", ", shopIds)}) and is_deleted =0");
                var pullNewConfigDic = new Dictionary<long, PullNewConfigDO>();
                if (pullNewConfigs.Any())
                {
                    pullNewConfigDic = pullNewConfigs.ToDictionary(r => r.ShopId, r => r);
                }
                #endregion

                #region 5.获取技师昨日邀请注册的用户
                var registerUserDic = await GetTechPullNewUserDics(shopIds);
                #endregion

                #region  6.获取技师邀请的新客但未消费的用户
                var unConsumeUsers = await shopUserRelationRepository.GetShopUserUnConsumes(shopIds);
                var unConsumeUserDic = new Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>>();
                if (unConsumeUsers != null && unConsumeUsers.Any())
                {
                    unConsumeUserDic = GetShopUserDic(unConsumeUsers);
                }

                //获取首次消费的绩效点
                var firstConsumeDic = await GetFirstConsumeDic(unConsumeUserDic, pullNewConfigDic);

                #endregion

                #region 7.组装数据
                foreach (var item in employeeDic)
                {
                    var techOrderDic = new Dictionary<string, List<OrderClientDTO>>();

                    //如果当前门店的安装绩效配置总开关 关闭 不需要计算
                    if (basicPerformanceConfigDic.ContainsKey(item.Key))
                    {
                        if (basicPerformanceConfigDic[item.Key].OrderByDescending(r => r.ConfigFlagTime).FirstOrDefault().ConfigFlag == 0)
                        {
                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                            {
                                Remark = "安装绩效点计算！",
                                LogLevel = "Info",
                                CreateTime = DateTime.Now,
                                CreateBy = "SystemTask",
                                ObjectType = "员工绩效",
                                BeforeValue = $"门店{item.Key}的安装绩效开关已关闭!"
                            });
                        }
                        else
                        {
                            var techIds = item.Value.Select(r => r.Id).Distinct().ToList();
                            //获取该技师邀请的用户下的订单信息
                            techOrderDic = await GetTechInstallOrderDic(techIds, item.Key);
                        }
                    }
                    else
                    {
                        await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                        {
                            Remark = "安装绩效点计算！",
                            LogLevel = "Info",
                            CreateTime = DateTime.Now,
                            CreateBy = "SystemTask",
                            ObjectType = "员工绩效",
                            BeforeValue = $"门店{item.Key}的安装绩效配置为空!"
                        });
                    }


                    //门店下的所有技师
                    foreach (var employee in item.Value)
                    {
                        var reportModel = new EmployeePerformanceReportDO
                        {
                            ShopId = item.Key,
                            EmployeeId = employee.Id,
                            EmployeeName = employee.Name,
                            EmployeePhone = employee.Mobile,
                            InstallPoint = 0,
                            PullNewPoint = 0,
                            CunsumePoint = 0,
                            TotalPoint = 0,//汇总计算
                            ReportTime = DateTime.Now,
                            IsDeleted = 0,
                            CreateBy = "System",
                            CreateTime = DateTime.Now
                        };

                        #region  技师拉新点数
                        if (registerUserDic.ContainsKey(employee.OrganizationId) &&
                            registerUserDic[employee.OrganizationId].ContainsKey(employee.Id))
                        {
                            //获取拉新配置
                            var pullNewConfig = pullNewConfigDic.ContainsKey(employee.OrganizationId) ?
                                pullNewConfigDic[employee.OrganizationId] : null;
                            if (pullNewConfig != null && pullNewConfig.PullNewFlag == 1)
                            {
                                //计算拉新绩效点数
                                reportModel.PullNewPoint = pullNewConfig.PullNewPoint * registerUserDic[employee.OrganizationId][employee.Id].Count;
                                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                {
                                    Remark = "拉新绩效点计算！",
                                    LogLevel = "Info",
                                    CreateTime = DateTime.Now,
                                    CreateBy = "SystemTask",
                                    ObjectType = "员工绩效",
                                    BeforeValue = $"门店{item.Key}的技师{employee.Id}计算绩效点数:{reportModel.PullNewPoint},邀请的新客记录${JsonConvert.SerializeObject(registerUserDic[employee.OrganizationId][employee.Id])}",
                                    AfterValue = $"拉新配置:{JsonConvert.SerializeObject(pullNewConfig)}"
                                });
                            }
                            else
                            {
                                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                {
                                    Remark = "拉新绩效点计算！",
                                    LogLevel = "Info",
                                    CreateTime = DateTime.Now,
                                    CreateBy = "SystemTask",
                                    ObjectType = "员工绩效",
                                    BeforeValue = $"门店{item.Key}拉新配置关闭!,门店{item.Key}的技师{employee.Id}邀请的新客记录${JsonConvert.SerializeObject(registerUserDic[employee.OrganizationId][employee.Id])}"
                                });
                            }
                        }
                        else
                        {
                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                            {
                                Remark = "拉新绩效点计算！",
                                LogLevel = "Info",
                                CreateTime = DateTime.Now,
                                CreateBy = "SystemTask",
                                ObjectType = "员工绩效",
                                BeforeValue = $"门店{item.Key}的技师{employee.Id}邀请的新客记录为空"
                            });
                        }
                        #endregion

                        #region 技师安装绩效点数

                        //计算安装订单金额
                        if (techOrderDic.ContainsKey(employee.Id) &&
                            techOrderDic[employee.Id].Any())
                        {
                            var basicPerformanceConfigList = basicPerformanceConfigDic.ContainsKey(employee.OrganizationId) ?
                              basicPerformanceConfigDic[employee.OrganizationId] : new List<BasicPerformanceConfigDO>();
                            if (basicPerformanceConfigList != null && basicPerformanceConfigList.Any())
                            {
                                var techOrderList = techOrderDic[employee.Id];
                                //计算六大类订单的绩效点
                                //轮胎
                                decimal tirePoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 1, "轮胎安装");
                                //保养
                                decimal baoyangPoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 2, "保养服务");
                                //美容
                                decimal washPoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 3, "美容洗车");
                                //钣金维修
                                decimal sheetMetalPoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 4, "钣金维修");
                                //汽车改装
                                decimal carModificationPoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 5, "汽车改装");
                                //其他
                                decimal otherPoint = await GetServicePoint(techOrderList, basicPerformanceConfigList, 6, "其他");

                                reportModel.InstallPoint = tirePoint + baoyangPoint + washPoint + sheetMetalPoint + carModificationPoint + otherPoint;

                                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                {
                                    Remark = "安装绩效点计算！",
                                    LogLevel = "Info",
                                    CreateTime = DateTime.Now,
                                    CreateBy = "SystemTask",
                                    ObjectType = "员工绩效",
                                    BeforeValue = $"门店{item.Key}的技师{employee.Id}昨日安装订单绩效点:轮胎点:{tirePoint},保养点:{baoyangPoint},美容店:{washPoint},钣金点:{sheetMetalPoint},汽车改装点:{carModificationPoint},其他点:{otherPoint}!",
                                    AfterValue = $"门店{item.Key}的技师{employee.Id}昨日安装订单:{JsonConvert.SerializeObject(JsonConvert.SerializeObject(techOrderList))}，安装绩效配置:{JsonConvert.SerializeObject(basicPerformanceConfigList)}"
                                });
                            }
                        }
                        else
                        {
                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                            {
                                Remark = "安装绩效点计算！",
                                LogLevel = "Info",
                                CreateTime = DateTime.Now,
                                CreateBy = "SystemTask",
                                ObjectType = "员工绩效",
                                BeforeValue = $"门店{item.Key}的技师{employee.Id}昨日安装订单为空!"
                            });
                        }

                        #endregion

                        #region  新客首销绩效点数

                        //计算新客首销点数
                        if (firstConsumeDic.ContainsKey(item.Key) && 
                            firstConsumeDic[item.Key].ContainsKey(employee.Id))
                        {
                            reportModel.CunsumePoint = firstConsumeDic[item.Key][employee.Id].Sum(r => r.Point);
                        }
                        else
                        {
                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                            {
                                Remark = "新客首销绩效点为空！",
                                LogLevel = "Info",
                                CreateTime = DateTime.Now,
                                CreateBy = "SystemTask",
                                ObjectType = "员工绩效",
                                BeforeValue = $"门店{item.Key}的技师{employee.Id}邀请的新客首销记录为空!"
                            });
                        }
                        #endregion

                        reportModel.TotalPoint = reportModel.PullNewPoint + reportModel.CunsumePoint + reportModel.InstallPoint;
                        await employeePerformanceReportRepository.InsertAsync(reportModel);
                    }
                }
                #endregion
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"CollectEmployeePerformanceReport_Error", ex);
            }
            return res;
        }

        #region 获取技师昨日邀请新客

        public async Task<Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>>> GetTechPullNewUserDics(List<long> shopIds)
        {
            //昨日邀请的新客 type=9
            var registerUsers = await shopUserRelationRepository.GetShopUserRegisterList(new GetShopNewCustomerRequest
            {
                ShopIds = shopIds
            });
            var registerUserDic = new Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>>();
            if (registerUsers.Any())
            {
                registerUserDic = GetShopUserDic(registerUsers);
            }
            return registerUserDic;
        }

        #endregion

        #region 获取门店下的技师昨日安装订单
        public async Task<Dictionary<string, List<OrderClientDTO>>> GetTechInstallOrderDic(List<string> techIds, long shopId)
        {
            var startTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("D").ToString());
            var endTime = Convert.ToDateTime(DateTime.Now.ToString("D").ToString()).AddSeconds(-1);

            //获取昨日技师安装的订单
            var orderDispatchList = await shopOrderClient.GetOrderDispatch(new GetOrderDispatchClientRequest
            {
                TechIds = techIds,
                StartTime = startTime.ToString(),//昨日的0点
                EndTime = endTime.ToString()  //昨日的24点
            });

            var techOrderDic = new Dictionary<string, List<OrderClientDTO>>();

            if (orderDispatchList != null && orderDispatchList.Any())
            {
                //根据订单号取订单的支付金额
                var orderNos = orderDispatchList.Where(r => r.IsDeleted == 0).Select(r => r.OrderNo).Distinct().ToList();

                //获取订单信息 必须是已付款 已安装的 必须是该门店的订单
                var orderInfos = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest
                {
                    OrderNos = orderNos,
                    ShopId = Convert.ToInt32(shopId)
                });

                //var orderProudcts = await orderClient.GetOrderProduct(new GetOrderProductRequest()
                //{
                //    OrderNos = orderNos
                //});

                //var productCodes = orderProudcts?.Data?.Select(_ => _.ProductId);
                //_productClient.GetProductsByProductCodes(new ProductDetailSearchClientRequest()
                //{
                //})

                if (orderInfos != null && orderInfos.Any())
                {
                    //添加其他状态限制
                    orderInfos = orderInfos.Where(r => r.InstallStatus == 1 && r.PayStatus == 1 && r.IsDeleted ==0).ToList();

                    //组合每个技师安装的订单记录
                    if (orderInfos.Any())
                    {
                        //获取每个技师安装的订单数据
                        foreach (var orderDispatch in orderDispatchList)
                        {
                            if (techOrderDic.ContainsKey(orderDispatch.TechId))
                            {
                                var orderInfo = orderInfos.FirstOrDefault(r => r.OrderNo == orderDispatch.OrderNo);
                                if (orderInfo != null)
                                {
                                    techOrderDic[orderDispatch.TechId].Add(orderInfo);
                                }
                            }
                            else
                            {
                                techOrderDic[orderDispatch.TechId] = new List<OrderClientDTO>();
                                var orderInfo = orderInfos.FirstOrDefault(r => r.OrderNo == orderDispatch.OrderNo);
                                if (orderInfo != null)
                                {
                                    techOrderDic[orderDispatch.TechId].Add(orderInfo);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                {
                    Remark = "新客首销的绩效点计算开始！",
                    LogLevel = "Info",
                    CreateTime = DateTime.Now,
                    CreateBy = "SystemTask",
                    ObjectType = "员工绩效",
                    BeforeValue = $"门店{shopId}的技师{JsonConvert.SerializeObject(techIds)}无安装订单!"
                });
            }
            return techOrderDic;

        }

        #endregion

        #region  获取新客首销的绩效点 

        #region  备用逻辑  这个是拿用户首日的 不管配置什么时候生效的
        /// <summary>
        /// 获取新客首销的绩效点 
        /// Case 1: 1日配置为0 并消费了订单  2日配置了10  消费了订单 
        /// 3日计算时 以2日的结果为主 不用拿之前所有的订单计算
        /// </summary>
        /// <param name="unConsumeUserDic">注册未消费的用户</param>
        /// <param name="pullNewConfigDic">拉新配置</param>
        /// <returns></returns>
        //public async Task<Dictionary<long, Dictionary<string, List<FirstConsumeDTO>>>> GetFirstConsumeDic
        //    (Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>> unConsumeUserDic,
        //    Dictionary<long, PullNewConfigDO> pullNewConfigDic)
        //{
        //    var firstConsumeDic = new Dictionary<long, Dictionary<string, List<FirstConsumeDTO>>>();
        //    if (unConsumeUserDic.Any())
        //    {
        //        foreach (var item in unConsumeUserDic)
        //        {
        //            //门店是否添加拉新配置
        //            if (pullNewConfigDic.ContainsKey(item.Key))
        //            {
        //                var pullNewConfig = pullNewConfigDic[item.Key];

        //                if (pullNewConfig.FirstConsumeFlag == 1)
        //                {
        //                    if (!firstConsumeDic.ContainsKey(item.Key))
        //                    {
        //                        firstConsumeDic[item.Key] = new Dictionary<string, List<FirstConsumeDTO>>();
        //                    }

        //                    foreach (var employee in item.Value)
        //                    {
        //                        //技师邀请的新客
        //                        if (employee.Value.Any())
        //                        {
        //                            var userIds = employee.Value.Select(r => r.UserId.ToString()).ToList();

        //                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                            {
        //                                Remark = "新客首销的绩效点计算开始！",
        //                                LogLevel = "Info",
        //                                CreateTime = DateTime.Now,
        //                                CreateBy = "SystemTask",
        //                                ObjectType = "员工绩效",
        //                                BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的新客未消费用户{JsonConvert.SerializeObject(userIds)}"
        //                            });

        //                            //新客的到店记录
        //                            var receiveList = await receiveClient.GetShopArrivalOrderForStatic(new GetShopArrivalForStaticClientRequest
        //                            {
        //                                UserId = userIds
        //                            });

        //                            //判断门店->技师->客户 是否消费  做Sum ！！！TODO
        //                            if (receiveList != null && receiveList.Any())
        //                            {
        //                                var orderNos = receiveList.Select(r => r.OrderNo).ToList();
        //                                //到店记录 关联的订单信息
        //                                var orderInfos = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest
        //                                {
        //                                    ShopId = Convert.ToInt32(item.Key),
        //                                    OrderNos = orderNos
        //                                });

        //                                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                                {
        //                                    Remark = "新客首销的绩效点关联的订单号！",
        //                                    LogLevel = "Info",
        //                                    CreateTime = DateTime.Now,
        //                                    CreateBy = "SystemTask",
        //                                    ObjectType = "员工绩效",
        //                                    BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的所有新客{JsonConvert.SerializeObject(userIds)}消费的订单{JsonConvert.SerializeObject(orderNos)}"
        //                                });

        //                                // 技师邀请的客户下的已付款 已安装的订单
        //                                orderInfos = orderInfos.Where(r => r.InstallStatus == 1 && r.PayStatus == 1).ToList();

        //                                var filterDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        //                                //新客下的订单 订单的时间是昨日的 model中可能存在0是产生的订单 不参与计算
        //                                //订单必须是已付款的
        //                                //这个orderInfos 可能是技师邀请的客户下的订单  有多笔记录 还需要做过滤 按照user_id Group By TODO！！！
        //                                if (orderInfos.Any(r => r.OrderTime.ToString("yyyy-MM-dd") == filterDate))
        //                                {
        //                                    firstConsumeDic[item.Key][employee.Key] = new List<FirstConsumeDTO>();

        //                                    //取得昨日下的订单
        //                                    var lastDayOrders = orderInfos.Where(r => r.OrderTime.ToString("yyyy-MM-dd") == filterDate);
        //                                    foreach (var order in lastDayOrders.GroupBy(r => r.UserId))
        //                                    {
        //                                        var allOrderNos = order.Select(r => r.OrderNo); //昨日消费的所有订单
        //                                        await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                                        {
        //                                            Remark = "新客首销的绩效点关联的订单号！",
        //                                            LogLevel = "Info",
        //                                            CreateTime = DateTime.Now,
        //                                            CreateBy = "SystemTask",
        //                                            ObjectType = "员工绩效",
        //                                            BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的单个新客{order.Key}消费的订单{string.Join(",", allOrderNos)}"
        //                                        });

        //                                        decimal point = 0;
        //                                        //取用户下的第一单
        //                                        var firstConsumeOrder = order.OrderBy(r => r.OrderTime).FirstOrDefault();
        //                                        if (firstConsumeOrder != null)
        //                                        {
        //                                            if (pullNewConfig.FirstConsumeType == 1)
        //                                            {
        //                                                point = Math.Round((firstConsumeOrder.ActualAmount * pullNewConfig.FirstConsumePoint) / 100, 2); //四舍五入
        //                                            }
        //                                            else if (pullNewConfig.FirstConsumeType == 2)
        //                                            {
        //                                                point = pullNewConfig.FirstConsumePoint;
        //                                            }
        //                                            //计算绩效  打上标签 更新记录    
        //                                            firstConsumeDic[item.Key][employee.Key].Add(new FirstConsumeDTO
        //                                            {
        //                                                Point = point,
        //                                                UserId = order.Key,
        //                                            });

        //                                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                                            {
        //                                                Remark = "新客首销的绩效点计算结果！",
        //                                                LogLevel = "Info",
        //                                                CreateTime = DateTime.Now,
        //                                                CreateBy = "SystemTask",
        //                                                ObjectType = "员工绩效",
        //                                                BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的单个新客{order.Key}消费的第一笔订单{firstConsumeOrder.OrderNo},绩效点:{point}",
        //                                                AfterValue = $"拉新配置:{JsonConvert.SerializeObject(pullNewConfig)}"
        //                                            });
        //                                        }
        //                                    }

        //                                }
        //                            }
        //                            else
        //                            {
        //                                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                                {
        //                                    Remark = "新客首销的绩效点不计算！",
        //                                    LogLevel = "Info",
        //                                    CreateTime = DateTime.Now,
        //                                    CreateBy = "SystemTask",
        //                                    ObjectType = "员工绩效",
        //                                    BeforeValue = $"门店{item.Key}的技师{employee.Key}的新客无到店消费记录"
        //                                });
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                    {
        //                        Remark = "新客首销的绩效点不计算！",
        //                        LogLevel = "Info",
        //                        CreateTime = DateTime.Now,
        //                        CreateBy = "SystemTask",
        //                        ObjectType = "员工绩效",
        //                        BeforeValue = $"门店{item.Key}首销开关关闭,数据:{JsonConvert.SerializeObject(pullNewConfig)}"
        //                    });
        //                }
        //            }
        //            else
        //            {
        //                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
        //                {
        //                    Remark = "新客首销的绩效点不计算！",
        //                    LogLevel = "Info",
        //                    CreateTime = DateTime.Now,
        //                    CreateBy = "SystemTask",
        //                    ObjectType = "员工绩效",
        //                    BeforeValue = $"门店{item.Key}拉新配置为空!"
        //                });
        //            }
        //        }
        //    }
        //    return firstConsumeDic;
        //}
        #endregion

        ///获取新客首销的绩效点
        public async Task<Dictionary<long, Dictionary<string, List<FirstConsumeDTO>>>> GetFirstConsumeDic
            (Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>> unConsumeUserDic,
            Dictionary<long, PullNewConfigDO> pullNewConfigDic)
        {
            //计算规则
            //1.只要是未消费的用户都算,不管是不是今日注册的 
            //2.如果配置的绩效点为0 默认就是不生效的  即使有消费订单
            //3.以到店记录为主(绑定多笔订单) 如果之前有消费 但是开关未生效 也是不统计的

            var firstConsumeDic = new Dictionary<long, Dictionary<string, List<FirstConsumeDTO>>>();
            if (unConsumeUserDic.Any())
            {
                foreach (var item in unConsumeUserDic)
                {
                    //门店是否添加拉新配置
                    if (pullNewConfigDic.ContainsKey(item.Key))
                    {
                        var pullNewConfig = pullNewConfigDic[item.Key];

                        //必须是有效的点数
                        if (pullNewConfig.FirstConsumeFlag == 1 && pullNewConfig.FirstConsumePoint > 0)
                        {
                            if (!firstConsumeDic.ContainsKey(item.Key))
                            {
                                firstConsumeDic[item.Key] = new Dictionary<string, List<FirstConsumeDTO>>();
                            }

                            foreach (var employee in item.Value)
                            {
                                //技师邀请的新客
                                if (employee.Value.Any())
                                {
                                    //技师邀请的用户
                                    var userIds = employee.Value.Select(r => r.UserId.ToString()).ToList();

                                    await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                    {
                                        Remark = "新客首销的绩效点计算开始！",
                                        LogLevel = "Info",
                                        CreateTime = DateTime.Now,
                                        CreateBy = "SystemTask",
                                        ObjectType = "员工绩效",
                                        BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的新客未消费用户{JsonConvert.SerializeObject(userIds)}"
                                    });

                                    //var startTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("D").ToString());
                                    //var endTime = Convert.ToDateTime(DateTime.Now.ToString("D").ToString()).AddSeconds(-1);

                                    //新客的到店记录
                                    var arrivalResponse = await receiveClient.GetShopArrivalOrderForStatic(new GetShopArrivalForStaticClientRequest
                                    {
                                        UserId = userIds
                                    });

                                    if (arrivalResponse.UserIds.Any())
                                    {
                                        //这部分用户 需要打标签
                                        foreach (var userId in arrivalResponse.UserIds)
                                        {
                                            await shopUserRelationRepository.UpdateShopUserFirstOrderFlag(new ShopUserRelationDO
                                            {
                                                ReferrerShopId = item.Key,
                                                ReferrerUserId = employee.Key
                                            }, userId);
                                        }
                                    }

                                    if (arrivalResponse.ShopArrivalOrders.Any())
                                    {
                                        var shopArrivalOrders = arrivalResponse.ShopArrivalOrders;

                                        var orderNos = shopArrivalOrders.Select(r => r.OrderNo).ToList();
                                        //到店记录 关联的订单信息
                                        var orderInfos = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest
                                        {
                                            ShopId = Convert.ToInt32(item.Key),
                                            OrderNos = orderNos
                                        });

                                        // 技师邀请的客户下的已付款 已安装的订单
                                        var lastDayOrders = orderInfos.Where(r => r.InstallStatus == 1 && r.PayStatus == 1 && r.IsDeleted==0)?.ToList();
                                        if (lastDayOrders != null && lastDayOrders.Any())
                                        {
                                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                            {
                                                Remark = "新客首销的绩效点关联的订单号！",
                                                LogLevel = "Info",
                                                CreateTime = DateTime.Now,
                                                CreateBy = "SystemTask",
                                                ObjectType = "员工绩效",
                                                BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的所有新客{JsonConvert.SerializeObject(userIds)}消费的订单{JsonConvert.SerializeObject(orderNos)}"
                                            });

                                            firstConsumeDic[item.Key][employee.Key] = new List<FirstConsumeDTO>();
                                            decimal point = 0;
                                            if (pullNewConfig.FirstConsumeType == 1)
                                            {
                                                point = Math.Round((lastDayOrders.Sum(r => r.ActualAmount) * pullNewConfig.FirstConsumePoint) / 100, 2); //四舍五入
                                            }
                                            else if (pullNewConfig.FirstConsumeType == 2)
                                            {
                                                point = pullNewConfig.FirstConsumePoint * lastDayOrders.Count;
                                            }
                                            //计算绩效  打上标签 更新记录    
                                            firstConsumeDic[item.Key][employee.Key].Add(new FirstConsumeDTO
                                            {
                                                Point = point
                                            });

                                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                                            {
                                                Remark = "新客首销的绩效点计算结果！",
                                                LogLevel = "Info",
                                                CreateTime = DateTime.Now,
                                                CreateBy = "SystemTask",
                                                ObjectType = "员工绩效",
                                                BeforeValue = $"门店{item.Key}的技师{employee.Key}邀请的新客绩效点:{point}",
                                                AfterValue = $"拉新配置:{JsonConvert.SerializeObject(pullNewConfig)}"
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                            {
                                Remark = "新客首销的绩效点不计算！",
                                LogLevel = "Info",
                                CreateTime = DateTime.Now,
                                CreateBy = "SystemTask",
                                ObjectType = "员工绩效",
                                BeforeValue = $"门店{item.Key}首销开关关闭,数据:{JsonConvert.SerializeObject(pullNewConfig)}"
                            });
                        }
                    }
                    else
                    {
                        await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                        {
                            Remark = "新客首销的绩效点不计算！",
                            LogLevel = "Info",
                            CreateTime = DateTime.Now,
                            CreateBy = "SystemTask",
                            ObjectType = "员工绩效",
                            BeforeValue = $"门店{item.Key}拉新配置为空!"
                        });
                    }
                }
            }
            return firstConsumeDic;
        }
        #endregion

        #region    获取门店安装绩效配置
        public async Task<Dictionary<long, List<BasicPerformanceConfigDO>>> GetBasicPerformanceConfigs(List<long> shopIds)
        {
            var basicPerformanceConfigs = await basicPerformanceConfigRepository.GetListAsync($" where shop_id in ({string.Join(",", shopIds)}) and is_deleted =0");
            var basicPerformanceConfigDic = new Dictionary<long, List<BasicPerformanceConfigDO>>();
            if (basicPerformanceConfigs.Any())
            {
                foreach (var item in basicPerformanceConfigs)
                {
                    if (basicPerformanceConfigDic.ContainsKey(item.ShopId))
                    {
                        basicPerformanceConfigDic[item.ShopId].Add(item);
                    }
                    else
                    {
                        basicPerformanceConfigDic[item.ShopId] = new List<BasicPerformanceConfigDO>();
                        basicPerformanceConfigDic[item.ShopId].Add(item);
                    }
                }
            }
            return basicPerformanceConfigDic;
        }

        #endregion

        #region  获取门店在职技师
        public async Task<Dictionary<long, List<EmployeeDO>>> GetTechDic(List<long> shopIds)
        {
            //在职的技师
            var employeeList = await employeeRepository.GetEmployeesByShopIds(shopIds);
            var employeeDic = new Dictionary<long, List<EmployeeDO>>();
            foreach (var item in employeeList)
            {
                if (employeeDic.ContainsKey(item.OrganizationId))
                {
                    employeeDic[item.OrganizationId].Add(item);
                }
                else
                {
                    employeeDic[item.OrganizationId] = new List<EmployeeDO>();
                    employeeDic[item.OrganizationId].Add(item);
                }
            }

            await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
            {
                Remark = "获取有效门店的在职技师",
                LogLevel = "Info",
                CreateTime = DateTime.Now,
                CreateBy = "SystemTask",
                ObjectType = "员工绩效",
                BeforeValue = $"{JsonConvert.SerializeObject(employeeDic)}"
            });
            return employeeDic;
        }

        #endregion

        #region 获取六大类订单的绩效点
        public async Task<decimal> GetServicePoint(List<OrderClientDTO> orders,
            List<BasicPerformanceConfigDO> configs, int orderType, string serviceName)
        {
            decimal point = 0;
            if (orders.Where(r => r.OrderType == orderType).Any())
            {
                var filterOrders = orders.Where(r => r.OrderType == orderType);

                var config = configs.FirstOrDefault(r => r.ServiceType == serviceName && r.ServiceConfigFlag == 1);
                if (config != null)
                {
                    if (config.ConfigType == 1)
                    {
                        point = Math.Round((filterOrders.Sum(r => r.ActualAmount) * config.ConfigPoint) / 100, 2);
                    }
                    else if (config.ConfigType == 2)
                    {
                        point = filterOrders.Count() * config.ConfigPoint;
                    }

                    var orderNos = filterOrders.Select(r => r.OrderNo);
                    await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                    {
                        Remark = "安装服务绩效计算完成！",
                        LogLevel = "Info",
                        CreateTime = DateTime.Now,
                        CreateBy = "SystemTask",
                        ObjectType = "员工绩效",
                        BeforeValue = $"该门店的{serviceName}类绩效点{point}!",
                        AfterValue = $"配置:{JsonConvert.SerializeObject(config)},订单信息:{string.Join(",", orderNos)}"
                    });
                }
                else
                {
                    await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                    {
                        Remark = "安装服务绩效不计算！",
                        LogLevel = "Info",
                        CreateTime = DateTime.Now,
                        CreateBy = "SystemTask",
                        ObjectType = "员工绩效",
                        BeforeValue = $"该门店的{serviceName}类的配置为空!"
                    });
                }
            }
            else
            {
                await basicPerformanceConfigRepository.InsertEmployeePerformanceLog(new EmployeePerformanceLogDO
                {
                    Remark = "安装服务绩效不计算！",
                    LogLevel = "Info",
                    CreateTime = DateTime.Now,
                    CreateBy = "SystemTask",
                    ObjectType = "员工绩效",
                    BeforeValue = $"该门店的{serviceName}类安装订单为空!"
                });
            }
            return point;
        }

        #endregion

        #region 获取门店技师邀请的新客字典
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employees">门店技师</param>
        /// <returns></returns>
        public Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>> GetShopUserDic(List<ShopUserRelationDO> employees)
        {
            var employeeDic = new Dictionary<long, Dictionary<string, List<ShopUserRelationDO>>>();
            foreach (var item in employees)
            {
                if (employeeDic.ContainsKey(item.ReferrerShopId))
                {
                    if (employeeDic[item.ReferrerShopId].ContainsKey(item.ReferrerUserId))
                    {
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId].Add(item);
                    }
                    else
                    {
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId] = new List<ShopUserRelationDO>();
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId].Add(item);
                    }
                }
                else
                {
                    employeeDic[item.ReferrerShopId] = new Dictionary<string, List<ShopUserRelationDO>>();
                    if (employeeDic[item.ReferrerShopId].ContainsKey(item.ReferrerUserId))
                    {
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId].Add(item);
                    }
                    else
                    {
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId] = new List<ShopUserRelationDO>();
                        employeeDic[item.ReferrerShopId][item.ReferrerUserId].Add(item);
                    }
                }
            }
            return employeeDic;
        }
        #endregion

        #endregion



        #region 员工绩效报表
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeePerformanceDto>> GetEmployeePerformanceList(EmployeePerformanceRequest request)
        {
            var result = await employeePerformanceReportRepository.GetEmployeePerformanceList(request);
            return result;
        }

        /// <summary>
        /// 获取员工绩效详情
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeePerformanceDetialDto> GetEmployeePerformanceDetial(EmployeePerformanceDetialRequest request)
        {
            var result = await employeePerformanceReportRepository.GetEmployeePerformanceDetial(request);
            if (string.IsNullOrEmpty(result?.EmployeeId))
            {
                result=new EmployeePerformanceDetialDto();
            }
            var employee = employeeRepository.Get(request.EmployeeId);
            if (string.IsNullOrEmpty(employee.Id))
            {
                throw new CustomException("员工信息不存！");
            }
            result.EmployeeName = employee.Name;
            result.Mobile = employee.Mobile;
            result.Avatar = employee.Avatar;
            result.Description = employee.Name + "绩效统计";
            var jobDo = jobRepository.Get(employee.JobId);
            result.Job = jobDo?.Name;
            var startDate = request.StartDate;
            var endDate = request.EndDate;
            request.StartDate = startDate.AddYears(-1);
            request.EndDate = endDate.AddYears(-1);
            //计算同比
            var lastYearResult = await employeePerformanceReportRepository.GetEmployeePerformanceDetial(request);
            if (lastYearResult?.TotalPoint > 0 && result.TotalPoint > 0)
            {
                var subLastYear = (result.TotalPoint - lastYearResult.TotalPoint) / lastYearResult.TotalPoint;
                result.ComparedLastYearPointPercent =Math.Round(Math.Round(subLastYear,4) * 100, 0);
            }
            else
            {
                result.ComparedLastYearPointPercent =0;
            }
            request.StartDate = startDate.AddDays(-7);
            request.EndDate = endDate.AddDays(-7);
            //计算环比
            var lastweekResult = await employeePerformanceReportRepository.GetEmployeePerformanceDetial(request);
            if (lastweekResult?.TotalPoint > 0 && result.TotalPoint > 0)
            {
                var subLastWeek = (result.TotalPoint - lastweekResult.TotalPoint) / lastweekResult.TotalPoint;
                result.ComparedLastWeekPointPercent = Math.Round(Math.Round(subLastWeek, 4) * 100, 0);
            }
            else
            {
                result.ComparedLastWeekPointPercent = 0;
            }

            return result;
        }
        /// <summary>
        /// 获取技师绩效列表V2
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDto>> GetTechPerformanceList(TechPerformanceRequest request)
        {
            var result = new List<TechPerformanceDto>();
            if (request.StartDate >= Convert.ToDateTime("2022-07-01"))
            {
                var employee = employeeRepository.Get(request.EmployeeId);
                if (string.IsNullOrEmpty(employee.Id))
                {
                    throw new CustomException("员工信息不存在！");
                }
                request.EmployeeName = employee.Name;
                request.Mobile = employee.Mobile;

                _logger.Info($"GetTechPerformanceListV4 request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceListV4(request); 
            }
            else if (request.StartDate >= Convert.ToDateTime("2022-05-01"))
            {
                var employee = employeeRepository.Get(request.EmployeeId);
                if (string.IsNullOrEmpty(employee.Id))
                {
                    throw new CustomException("员工信息不存在！");
                }
                request.EmployeeName = employee.Name;
                request.Mobile = employee.Mobile;

                _logger.Info($"GetTechPerformanceListV3 request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceListV3(request);
            }
            else
            {
                _logger.Info($"GetTechPerformanceList request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceList(request);
            }
            return result;
        }
        /// <summary>
        /// 获取技师绩效明细V2
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetail(TechPerformanceDetailRequest request)
        {
            var result = new List<TechPerformanceDetailDto>();
            if (request.StartDate >= Convert.ToDateTime("2022-07-01"))
            {
                var employee = employeeRepository.Get(request.EmployeeId);
                if (string.IsNullOrEmpty(employee.Id))
                {
                    throw new CustomException("员工信息不存在！");
                }
                request.EmployeeName = employee.Name;
                request.Mobile = employee.Mobile;

                _logger.Info($"GetTechPerformanceDetailV4 request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceDetailV4(request);
            }
            else if (request.StartDate >= Convert.ToDateTime("2022-05-01"))
            {
                var employee = employeeRepository.Get(request.EmployeeId);
                if (string.IsNullOrEmpty(employee.Id))
                {
                    throw new CustomException("员工信息不存在！");
                }
                request.EmployeeName = employee.Name;
                request.Mobile = employee.Mobile;

                _logger.Info($"GetTechPerformanceDetailV3 request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceDetailV3(request);
            }
            else
            {
                _logger.Info($"GetTechPerformanceDetail request= " + JsonConvert.SerializeObject(request));
                result = await employeePerformanceReportRepository.GetTechPerformanceDetail(request);
            }
     
            return result;
        }

        #endregion
    }
}
