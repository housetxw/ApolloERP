using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using Ae.Shop.Service.Core.Interfaces;
using System.Transactions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Response;
using System.Linq;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Common.Helper;
using Ae.Shop.Service.Core.Enums;
using ApolloErp.Log;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Service.Dal.Repositorys;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Client.Clients;
using Ae.Shop.Service.Client;
using Castle.DynamicProxy;
using Ae.Shop.Service.Imp.Helpers;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Core.Response.OpeningGuide;
using Newtonsoft.Json;
using Ae.Shop.Service.Core.Model.OpeningGuide;
using Ae.Shop.Service.Common.Extension;
using ApolloErp.Login.Auth;
using Ae.Shop.Service.Client.Model.OrderComment;
using Ae.Shop.Service.Core.Request.APP;
using Ae.Shop.Service.Core.Request.Shop;
using Ae.Shop.Service.Core.Response.APP;
using Ae.Shop.Service.Dal.Repositorys.Company;
using MimeKit;
using Microsoft.AspNetCore.Hosting;

namespace Ae.Shop.Service.Imp.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IShopConfigRepository _shopConfigRepository;
        private readonly IMapper _mapper;
        private readonly IShopImgRepository _shopImgRepository;
        private readonly IBasicDataClient _basicDataClient;
        private readonly IHotCityRepository _hotCityRepository;
        private readonly IShopBankCardRepository _shopBankCardRepository;
        private readonly ApolloErpLogger<ShopService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBaseServiceRepository _baseServiceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IShopServiceRepository _shopServiceRepository;
        private readonly IOrderCommentClient _orderCommentClient;
        private readonly IOrderClient _orderClient;
        private readonly IActivityClient _activityClient;
        private readonly IFileUploadClient _fileUploadClient;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IShopServiceTypeRepository _shopServiceTypeRepository;
        private readonly IReceiveClient _receiveClient;
        private readonly IVehicleClient _vehicleClient;
        private string Domain = string.Empty;//文件管理 外链默认域名
        private readonly IShopLogRepository _shopLogRepository;
        private readonly IIdentityService identityService;
        private readonly IBankInformationRepository _bankInformationRepository;
        private readonly IShopServiceAreaRepository _shopServiceAreaRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IJobRepository _jobRepository;
        private readonly IShopGrouponProductRepository _shopGrouponProductRepository;

        private readonly ApolloErp.Email.Message.EMailClient _eMailClient;

        public ShopService(IShopRepository shopRepository,
            IMapper mapper,
            IBasicDataClient basicDataClient,
            IShopImgRepository shopImgRepository,
            IShopConfigRepository shopConfigRepository,
            IHotCityRepository hotCityRepository,
            IShopBankCardRepository shopBankCardRepository,
            ApolloErpLogger<ShopService> logger,
            IConfiguration configuration,
            IEmployeeRepository employeeRepository,
            IBaseServiceRepository baseServiceRepository,
            IShopServiceRepository shopServiceRepository,
            IOrderCommentClient orderCommentClient,
            IOrderClient orderClient,
            IActivityClient activityClient,
            IFileUploadClient fileUploadClient,
            IBaoYangClient baoYangClient,
            IShopServiceTypeRepository shopServiceTypeRepository,
            IReceiveClient receiveClient,
            IVehicleClient vehicleClient,
            IShopLogRepository shopLogRepository,
            IIdentityService identityService,
            IBankInformationRepository bankInformationRepository, IShopServiceAreaRepository shopServiceAreaRepository, ICompanyRepository companyRepository, ApolloErp.Email.Message.EMailClient eMailClient, IHostingEnvironment hostingEnvironment, IJobRepository jobRepository, IShopGrouponProductRepository shopGrouponProductRepository
        )
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
            _shopConfigRepository = shopConfigRepository;
            _shopImgRepository = shopImgRepository;
            _basicDataClient = basicDataClient;
            _hotCityRepository = hotCityRepository;
            _shopBankCardRepository = shopBankCardRepository;
            _logger = logger;
            _configuration = configuration;
            Domain = configuration["QiNiuImageDomain"]?.ToString() ?? "";
            _employeeRepository = employeeRepository;
            _baseServiceRepository = baseServiceRepository;
            _shopServiceRepository = shopServiceRepository;
            _orderCommentClient = orderCommentClient;
            _orderClient = orderClient;
            _activityClient = activityClient;
            _fileUploadClient = fileUploadClient;
            _baoYangClient = baoYangClient;
            _shopServiceTypeRepository = shopServiceTypeRepository;
            _receiveClient = receiveClient;
            _vehicleClient = vehicleClient;
            _shopLogRepository = shopLogRepository;
            this.identityService = identityService;
            _bankInformationRepository = bankInformationRepository;
            _shopServiceAreaRepository = shopServiceAreaRepository;
            _companyRepository = companyRepository;
            _eMailClient = eMailClient;
            _hostingEnvironment = hostingEnvironment;
            _jobRepository = jobRepository;
            _shopGrouponProductRepository = shopGrouponProductRepository;
        }


        #region   BOSS端服务
        /// <summary>
        /// 新增门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddShopAsync(AddShopRequest request)
        {
            //判断门店名称是否唯一
            var ShopInfo = await _shopRepository.GetShopByNameAsync(request.Shop.SimpleName, 0);
            if (ShopInfo != null)
            {
                throw new CustomException("门店名称已存在");
            }

            var dt = DateTime.Now;
            request.Shop.AppletCode = string.Empty;
            request.Shop.TagName = request.Shop.TagNames.Any() ? string.Join(",", request.Shop.TagNames) : string.Empty;
            ShopDO shop = _mapper.Map<ShopDO>(request.Shop);
            long shopId = 0;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    //新增门店
                    shop.CreateTime = dt;
                    shop.CreateBy = identityService.GetUserName();
                    LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                    var shopRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                    {
                        Identifier = string.Empty,
                        CreateBy = shop.CreateBy,
                        UpdateBy = shop.UpdateBy,
                        Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                        Type2 = LoggerTypeTwoEnum.ShopMainCreate.GetEnumDescription(),
                        Filter1 = string.Empty,
                        Filter2 = string.Empty,
                        Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                    });
                    shop.CheckStatus = 2;
                    shopId = await shopRepository.AddShopAsync(shop);
                    //新增门店配置
                    //营业时间
                    if (request.ShopConfig != null)
                    {
                        request.ShopConfig.StartWorkTime = DateTime.Parse("1900-01-01 " + request.ShopConfig.StartWorkTime.ToString("HH:mm"));
                        request.ShopConfig.EndWorkTime = DateTime.Parse("1900-01-01 " + request.ShopConfig.EndWorkTime.ToString("HH:mm"));
                        ShopConfigDO shopConfig = _mapper.Map<ShopConfigDO>(request.ShopConfig);
                        shopConfig.CreateTime = dt;
                        shopConfig.ShopId = shopId;
                        LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorShopConfigHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
                        var shopConfigRepository = logInterceptorShopConfigHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                        {
                            Identifier = string.Empty,
                            CreateBy = shop.CreateBy,
                            UpdateBy = shop.UpdateBy,
                            Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.ShopConfigCreate.GetEnumDescription(),
                            Filter1 = shopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                        });
                        await shopConfigRepository.AddAsync(shopConfig);
                    }
                    //新增银行账户
                    if (request.ShopBankCard != null)
                    {
                        ShopBankCardDO shopBankCard = _mapper.Map<ShopBankCardDO>(request.ShopBankCard);
                        shopBankCard.CreateTime = dt;
                        shopBankCard.ShopId = shopId;
                        shopBankCard.CreateBy = shop.CreateBy;
                        LogInterceptorHelpher<IShopBankCardRepository, ShopLogDO> logInterceptorBankCardHelpher = new LogInterceptorHelpher<IShopBankCardRepository, ShopLogDO>();
                        var shopBankCardRepository = logInterceptorBankCardHelpher.CreateInterfaceProxyWithTarget(_shopBankCardRepository, new ShopLogDO()
                        {
                            Identifier = string.Empty,
                            CreateBy = shop.CreateBy,
                            UpdateBy = shop.UpdateBy,
                            Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.ShopBankCreate.GetEnumDescription(),
                            Filter1 = shopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                        });
                        await shopBankCardRepository.InsertAsync(shopBankCard);
                    }

                    //新增图片
                    var shopImgs = new List<ShopImgDO>();
                    var shopInfo = request.Shop;
                    if (shopInfo.HeadImg.Any())
                    {
                        foreach (var item in shopInfo.HeadImg)
                        {
                            var newUrl = item.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", "");
                            shopImgs.Add(new ShopImgDO()
                            {
                                ImgUrl = newUrl,
                                ShopId = shopId,
                                Type = 1,
                                CreateBy = shop.CreateBy,
                                CreateTime = dt
                            });
                        }

                    }
                    if (shopInfo.FrontImg.Any())
                    {
                        foreach (var item in shopInfo.FrontImg)
                        {
                            var newUrl = item.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", "");
                            shopImgs.Add(new ShopImgDO()
                            {
                                ImgUrl = newUrl,
                                ShopId = shopId,
                                Type = 4,
                                CreateBy = shop.CreateBy,
                                CreateTime = dt
                            });
                        }
                    }
                    if (shopInfo.ShopImgs.Any())
                    {
                        shopInfo.ShopImgs.ForEach(o =>
                        shopImgs.Add(new ShopImgDO()
                        {

                            ShopId = shopId,
                            ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),

                            Type = 2,
                            CreateBy = shop.CreateBy,
                            CreateTime = dt
                        }));
                    }
                    if (shopInfo.ShopProofImgs.Any())
                    {
                        shopInfo.ShopProofImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                        {
                            ShopId = shopId,
                            ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                            Type = 3,
                            CreateBy = shop.CreateBy,
                            CreateTime = dt
                        }));
                    }

                    if (shopInfo.BusinessLienseImgs.Any())
                    {
                        shopInfo.BusinessLienseImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                        {
                            ShopId = shopId,
                            ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                            Type = 5,
                            CreateBy = shop.CreateBy,
                            CreateTime = dt
                        }));
                    }

                    if (shopInfo.ManagementLicenseImgs.Any())

                    {
                        shopInfo.ManagementLicenseImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                        {
                            ShopId = shopId,
                            ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                            Type = 7,
                            CreateBy = shop.CreateBy,
                            CreateTime = dt
                        }));
                    }
                    if (shopImgs != null && shopImgs.Any())
                    {
                        LogInterceptorHelpher<IShopImgRepository, ShopLogDO> logInterceptorpImgHelpher = new LogInterceptorHelpher<IShopImgRepository, ShopLogDO>();
                        shopImgs.ForEach(o =>
                        {
                            var shopImgRepository = logInterceptorpImgHelpher.CreateInterfaceProxyWithTarget(_shopImgRepository, new ShopLogDO()
                            {
                                Identifier = string.Empty,
                                CreateBy = shop.CreateBy,
                                UpdateBy = shop.UpdateBy,
                                Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                                Type2 = LoggerTypeTwoEnum.ShopImgCreate.GetEnumDescription(),
                                Filter1 = shopId.ToString(),
                                Filter2 = string.Empty,
                                Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                            });
                            shopImgRepository.InsertAsync(o);
                        });
                    }

                    ts.Complete();
                }
                #region 生成门店专修品牌记录
                if (request.Shop.BrandNames.Any() && shopId > 0)
                {
                    var brandRes = await _vehicleClient.GetVehicleBrandList();
                    if (brandRes.Any())
                    {
                        var brands = request.Shop.BrandNames;
                        foreach (var item in brands)
                        {
                            var vehicleBrand = brandRes.FirstOrDefault(r => r.BrandSuffix == item);
                            var brandDto = new ShopServiceBrandDO
                            {
                                ShopId = shopId,
                                CreateBy = shop.CreateBy,
                                CreateTime = DateTime.Now,
                                IsDeleted = 0,
                                Brand = item,
                                BrandUrl = vehicleBrand != null ? vehicleBrand.BrandUrl : string.Empty
                            };
                            //不符合规范没有修改添加日志记录后期兼容进去
                            await _shopServiceRepository.CreateShopServiceBrand(brandDto);
                        }
                    }
                }

                #endregion

                #region   自动生成门店二维码
                if (shopId > 0)
                {
                    //生成快速排队二维码
                    var res = await GenerateShopAppletCode(shopId, "pages/fastQueuing/main");
                    if (res.Code == ResultCode.Success)
                    {
                        LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorShopHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                        var shopRepository = logInterceptorShopHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                        {
                            Identifier = shopId.ToString(),
                            CreateBy = shop.CreateBy,
                            UpdateBy = shop.UpdateBy,
                            Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.UpdateShopQuickQueueAppletCode.GetEnumDescription(),
                            Filter1 = shopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                        });

                        await shopRepository.UpdateQuickQueueAppletCode(new ShopDO
                        {
                            UpdateBy = shop.CreateBy,
                            AppletCode = res.Data,
                            Id = shopId
                        });
                    }

                    //生成门店二维码
                    res = await GenerateShopAppletCode(shopId, "pages/storeDetails/main");
                    if (res.Code == ResultCode.Success)
                    {
                        LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorShopHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                        var shopRepository = logInterceptorShopHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                        {
                            Identifier = shopId.ToString(),
                            CreateBy = shop.CreateBy,
                            UpdateBy = shop.UpdateBy,
                            Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.UpdateShopAppletCode.GetEnumDescription(),
                            Filter1 = shopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                        });
                        await shopRepository.UpdateShopAppletCode(new ShopDO
                        {
                            UpdateBy = shop.CreateBy,
                            ShopAppletCode = res.Data,
                            Id = shopId
                        });
                    }

                    var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                    if (serviceTypes != null && serviceTypes.Any())
                    {
                        LogInterceptorHelpher<IShopServiceTypeRepository, ShopLogDO> logInterceptorShopServiceTypeHelpher = new LogInterceptorHelpher<IShopServiceTypeRepository, ShopLogDO>();
                        //自动生成门店服务类型记录
                        foreach (var item in serviceTypes)
                        {
                            var shopServiceTypeRepository = logInterceptorShopServiceTypeHelpher.CreateInterfaceProxyWithTarget(_shopServiceTypeRepository, new ShopLogDO()
                            {
                                Identifier = string.Empty,
                                CreateBy = shop.CreateBy,
                                UpdateBy = shop.UpdateBy,
                                Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                                Type2 = LoggerTypeTwoEnum.CreateShopServiceType.GetEnumDescription(),
                                Filter1 = shopId.ToString(),
                                Filter2 = string.Empty,
                                Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                            });
                            await shopServiceTypeRepository.CreateShopServiceType(new ShopServiceTypeDO
                            {
                                ShopId = shopId,
                                IsDeleted = 0,
                                CreateBy = "System",
                                CreateTime = DateTime.Now,
                                ServiceType = item.ServiceType
                            });
                        }
                    }
                }
                #endregion

                #region 初始化员工账户


                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return shopId;
        }

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> ModifyShopInfoForShopAsync(AddShopRequest request)
        {
            var result = Result.Failed<bool>("修改门店信息异常");
            if (request.Shop == null)
            {
                return result;
            }
            //验证门店是否存在
            var ShopInfo = await _shopRepository.GetShopAsync(request.Shop.Id);
            if (ShopInfo == null)
            {
                return result;
            }
            var dt = DateTime.Now;
            request.Shop.TagName = request.Shop.TagNames.Any() ? string.Join(",", request.Shop.TagNames) : string.Empty;
            ShopDO shop = _mapper.Map<ShopDO>(request.Shop);
            int num = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    #region  修改门店信息

                    //修改门店信息
                    shop.UpdateTime = dt;
                    shop.UpdateBy = request.UserId;
                    LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                    var shopRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                    {
                        Identifier = shop.Id.ToString(),
                        CreateBy = shop.UpdateBy,
                        UpdateBy = shop.UpdateBy,
                        Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                        Type2 = LoggerTypeTwoEnum.ModifyShopBaseInfo.GetEnumDescription(),
                        Filter1 = shop.Id.ToString(),
                        Filter2 = string.Empty,
                        Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                    });
                    num = await shopRepository.UpdateShopInfoForShopAsync(shop);

                    #endregion

                    #region  修改门店配置信息

                    if (request.ShopConfig != null)
                    {
                        var shopConfigDTO = request.ShopConfig;
                        shopConfigDTO.ShopId = shop.Id;
                        shopConfigDTO.StartWorkTime = DateTime.Parse("1900-01-01 " + request.ShopConfig.StartWorkTime.ToString("HH:mm"));
                        shopConfigDTO.EndWorkTime = DateTime.Parse("1900-01-01 " + request.ShopConfig.EndWorkTime.ToString("HH:mm"));
                        shopConfigDTO.UpdateBy = identityService.GetUserName();
                        var isSuccess = await ModifyShopConfigAsyne(request.ShopConfig);
                    }

                    #endregion

                    #region  修改门店银行账户信息
                    if (request.ShopBankCard != null)
                    {
                        var shopBankCard = request.ShopBankCard;
                        var bankAccount = new ModifyShopBankAccountRequest()
                        {
                            ShopId = request.Shop.Id,
                            Type = shopBankCard.Type,
                            BankId = shopBankCard.BankId,
                            OpeningBank = shopBankCard.OpeningBank,
                            OpeningBranch = shopBankCard.OpeningBranch,
                            OpeningUserName = shopBankCard.OpeningUserName,
                            BankCardNo = shopBankCard.BankCardNo,
                            CompanyName = shopBankCard.CompanyName,
                            OpeningLicence = shopBankCard.OpeningLicence,
                            UpdateBy = shop.UpdateBy,
                            Source = 2
                        };
                        var isSuccess = await ModifyShopBankAccountAsync(bankAccount);
                    }

                    #endregion

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new ApiResult<bool>
            {
                Data = true,
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };
        }

        /// <summary>
        /// 生成门店二维码接口
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> GenerateShopAppletCode(long shopId, string type)
        {
            var res = new ApiResult<string>();
            try
            {
                var codeResult = await _activityClient.GenMinAppCode(new GenMinAppCodeClientRequest
                {
                    IsHyaline = false,
                    // Scene = $"s={shopId},t=4",
                    Scene = new { PromoteContentType = 4, ShopId = shopId },
                    Page = type
                });

                if (codeResult.Code == ResultCode.Success)
                {
                    var result = codeResult.Data;
                    string fullName = string.Empty;
                    if (result.CodeBytes.Length > 0)
                    {
                        var fileName = DateTime.Now.ToString("yyyyMMdd");
                        fileName += DateTime.Now.Hour.ToString() +
                            DateTime.Now.Minute.ToString()
                            + DateTime.Now.Second.ToString()
                            + DateTime.Now.Millisecond.ToString() + ".png";

                        fullName = "Shops/QRCode/" + fileName;
                        await _fileUploadClient.UploadBytes(new UploadByteRequest
                        {
                            Bytes = result.CodeBytes,
                            FileName = fullName
                        });
                    }
                    res.Code = ResultCode.Success;
                    res.Data = fullName;
                }
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"GenerateShopAppletCode_Error shopId:{shopId}", ex);
            }
            return res;
        }


        /// <summary>
        /// 根据门店ID查询门店配置信息---BOSS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopResponse> GetShopAsync(GetShopRequest request)
        {
            //门店信息
            var shopInfo = _shopRepository.GetShopAsync(request.ShopId);
            //门店配置信息
            var shopConfigInfo = _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
            var shopBankCard = _shopBankCardRepository.GetShopBankCardByShopIdAsync(request.ShopId);


            //获取门店图片
            var imgs = _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);

            await Task.WhenAll(shopInfo, shopConfigInfo, shopBankCard, imgs);

            ShopDTO shopDTO = _mapper.Map<ShopDTO>(shopInfo.Result) ?? new ShopDTO();

            if (!string.IsNullOrWhiteSpace(shopDTO.AppletCode))
            {
                shopDTO.AppletCode = _configuration["QiNiuImageDomain"] + shopDTO.AppletCode;
            }

            if (!string.IsNullOrWhiteSpace(shopDTO.ShopAppletCode))
            {
                shopDTO.ShopAppletCode = _configuration["QiNiuImageDomain"] + shopDTO.ShopAppletCode;
            }

            if (!string.IsNullOrWhiteSpace(shopDTO.TagName))
            {
                shopDTO.TagNames = shopDTO.TagName.Split(',').ToList();
            }
            var shopImgs = imgs.Result;

            var shopCompanyInfo = await _companyRepository.GetAsync(shopDTO?.CompanyId);
            shopDTO.CompanyName = shopCompanyInfo?.Name;
            if (shopImgs != null && shopImgs.Any())
            {
                shopDTO.HeadImg = shopImgs.Where(o => o.Type == 1).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
                shopDTO.ShopImgs = shopImgs.Where(o => o.Type == 2).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
                shopDTO.ShopProofImgs = shopImgs.Where(o => o.Type == 3).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
                shopDTO.FrontImg = shopImgs.Where(o => o.Type == 4).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
                shopDTO.BusinessLienseImgs = shopImgs.Where(o => o.Type == 5).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
                shopDTO.ManagementLicenseImgs = shopImgs.Where(o => o.Type == 7).Select(p => new ShopImgDTO() { ImgId = p.Id, Url = Domain + p.ImgUrl }).ToList() ?? new List<ShopImgDTO>();
            }

            GetShopResponse getShopResponse = new GetShopResponse();
            getShopResponse.Shop = shopDTO;
            getShopResponse.ShopConfig = _mapper.Map<ShopConfigDTO>(shopConfigInfo.Result) ?? new ShopConfigDTO();
            getShopResponse.shopBankCard = _mapper.Map<ShopBankCardDTO>(shopBankCard.Result) ?? new ShopBankCardDTO();
            getShopResponse.shopBankCard.IconUrl = Domain + getShopResponse.shopBankCard.IconUrl;
            return getShopResponse;
        }

        /// <summary>
        /// 查询门店列表---BOSS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopSimpleInfoForBOSSDTO>> GetShopListForBOSSAsync(GetShopListForBOSSRequest request)
        {
            //门店信息列表
            var model = _mapper.Map<GetShopListModel>(request);
            var result = await _shopRepository.GetShopListForBOSSAsync(model);

            ApiPagedResultData<ShopSimpleInfoForBOSSDTO> response = new ApiPagedResultData<ShopSimpleInfoForBOSSDTO>();
            response.Items = _mapper.Map<List<ShopSimpleInfoForBOSSDTO>>(result.Items);
            foreach (var item in response.Items)
            {
                item.Type = GetShopTypeStr(int.Parse(item.Type));
            }
            response.TotalItems = result.TotalItems;
            return response;
        }

        /// <summary>
        /// 修改门店基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopBaseInfoAsync(ModifyShopBaseInfoRequest request)
        {
            request.TagName = request.TagNames.Any() ? string.Join(",", request.TagNames) : string.Empty;
            request.UpdateBy = identityService.GetUserName();
            LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
            var shopRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
            {
                Identifier = request.ShopId.ToString(),
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                Type2 = LoggerTypeTwoEnum.ModifyShopBaseInfo.GetEnumDescription(),
                Filter1 = request.ShopId.ToString(),
                Filter2 = string.Empty,
                Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
            });
            var result = await shopRepository.ModifyShopBaseInfoAsync(request);
            #region 更新专修品牌记录
            //需要判断哪些是新添加  哪些是要删除的记录
            if (request.BrandNames.Any())
            {
                //清除原来的记录
                await _shopServiceRepository.DeleteShopServiceBrand(new ShopServiceBrandDO
                {
                    ShopId = request.ShopId,
                    UpdateBy = request.UpdateBy
                });

                //新增选中的记录
                var brandRes = await _vehicleClient.GetVehicleBrandList();
                if (brandRes.Any())
                {
                    var brands = request.BrandNames;
                    foreach (var item in brands)
                    {
                        var vehicleBrand = brandRes.FirstOrDefault(r => r.BrandSuffix == item);
                        var brandDto = new ShopServiceBrandDO
                        {
                            ShopId = request.ShopId,
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now,
                            IsDeleted = 0,
                            Brand = item,
                            BrandUrl = vehicleBrand != null ? vehicleBrand.BrandUrl : string.Empty
                        };

                        await _shopServiceRepository.CreateShopServiceBrand(brandDto);
                    }
                }
            }
            else
            {
                //清除原来的记录
                await _shopServiceRepository.DeleteShopServiceBrand(new ShopServiceBrandDO
                {
                    ShopId = request.ShopId,
                    UpdateBy = request.UpdateBy
                });
            }

            #endregion


            return result;
        }
        /// <summary>
        /// 修改门店地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopAddressAsync(ModifyShopAddressRequest request)
        {
            request.UpdateBy = identityService.GetUserName();
            var result = await _shopRepository.ModifyShopAddressAsync(request);
            return result;
        }

        /// <summary>
        /// 修改门店银行账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopBankAccountAsync(ModifyShopBankAccountRequest request)
        {
            if (request.Source != 1)
            {
                request.UpdateBy = identityService.GetUserName();
            }
            var shopBankInfo = await _shopBankCardRepository.GetShopBankCardByShopIdAsync(request.ShopId);
            var result = false;
            LogInterceptorHelpher<IShopBankCardRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopBankCardRepository, ShopLogDO>();
            if (shopBankInfo != null)
            {
                var shopBankCardRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopBankCardRepository, new ShopLogDO()
                {
                    Identifier = shopBankInfo?.Id.ToString(),
                    CreateBy = request.UpdateBy,
                    UpdateBy = request.UpdateBy,
                    Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                    Type2 = LoggerTypeTwoEnum.ModifyShopBankAccount.GetEnumDescription(),
                    Filter1 = request.ShopId.ToString(),
                    Filter2 = string.Empty,
                    Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                });
                result = await shopBankCardRepository.ModifyShopBankAccountAsync(request);

            }
            else
            {
                //新增银行账户
                ShopBankCardDO shopBankCard = _mapper.Map<ShopBankCardDO>(request);
                shopBankCard.CreateTime = DateTime.Now;
                shopBankCard.CreateBy = request.UpdateBy;

                var shopBankCardRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopBankCardRepository, new ShopLogDO()
                {
                    Identifier = string.Empty,
                    CreateBy = string.Empty,
                    UpdateBy = request.UpdateBy,
                    Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                    Type2 = LoggerTypeTwoEnum.ShopBankCreate.GetEnumDescription(),
                    Filter1 = request.ShopId.ToString(),
                    Filter2 = string.Empty,
                    Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                });
                var id = await shopBankCardRepository.InsertAsync(shopBankCard);
                if (id > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 添加门店图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopImgs(AddShopImgsRequest request)
        {
            //新增图片
            var shopImgs = new List<ShopImgDO>();
            var dt = DateTime.Now;
            var shopId = request.ShopId;
            var createBy = request.CreateBy;
            if (string.IsNullOrWhiteSpace(createBy))
            {
                createBy = identityService.GetUserName();
            }
            //门头照片
            if (request.HeadImg.Any())
            {
                foreach (var item in request.HeadImg)
                {
                    var newUrl = item.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", "");
                    shopImgs.Add(new ShopImgDO()
                    {
                        ImgUrl = newUrl,
                        ShopId = shopId,
                        Type = 1,
                        CreateBy = createBy,
                        CreateTime = dt
                    });
                }

            }
            //正面图片
            if (request.FrontImg.Any())
            {
                foreach (var item in request.FrontImg)
                {
                    var newUrl = item.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", "");
                    shopImgs.Add(new ShopImgDO()
                    {
                        ImgUrl = newUrl,
                        ShopId = shopId,
                        Type = 4,
                        CreateBy = createBy,
                        CreateTime = dt
                    });
                }
            }
            //门店照片
            if (request.ShopImgs.Any())
            {
                request.ShopImgs.ForEach(o =>
                shopImgs.Add(new ShopImgDO()
                {

                    ShopId = shopId,
                    ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                    Type = 2,
                    CreateBy = createBy,
                    CreateTime = dt
                }));
            }
            //门店资质证明照片
            if (request.ShopProofImgs.Any())
            {
                request.ShopProofImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                {
                    ShopId = shopId,
                    ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                    Type = 3,
                    CreateBy = createBy,
                    CreateTime = dt
                }));
            }
            //营业执照
            if (request.BusinessLicenseImgs.Any())
            {
                request.BusinessLicenseImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                {
                    ShopId = shopId,
                    ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                    Type = 5,
                    CreateBy = createBy,
                    CreateTime = dt
                }));
            }
            //经营许可证
            if (request.ManagementLicenseImgs.Any())
            {
                request.ManagementLicenseImgs.ForEach(o => shopImgs.Add(new ShopImgDO()
                {
                    ShopId = shopId,
                    ImgUrl = o.Url.Replace("https://m.ApolloErp.cn/", "").Replace("http://m.ApolloErp.cn/", ""),
                    Type = 7,
                    CreateBy = createBy,
                    CreateTime = dt
                }));
            }
            if (shopImgs != null && shopImgs.Any())
            {
                LogInterceptorHelpher<IShopImgRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopImgRepository, ShopLogDO>();
                shopImgs.ForEach(o =>
                {
                    var shopImgRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopImgRepository, new ShopLogDO()
                    {
                        Identifier = string.Empty,
                        CreateBy = o.CreateBy,
                        UpdateBy = o.CreateBy,
                        Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                        Type2 = LoggerTypeTwoEnum.ShopImgCreate.GetEnumDescription(),
                        Filter1 = shopId.ToString(),
                        Filter2 = string.Empty,
                        Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                    });
                    shopImgRepository.InsertAsync(o);
                });
            }
            return true;
        }
        /// <summary>
        /// 删除门店图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShopImgAsync(DeleteShopImgRequest request)
        {
            request.UpdateBy = identityService.GetUserName();
            var img = await _shopImgRepository.GetAsync(request.ImgId);
            img.IsDeleted = 1;
            img.UpdateBy = request.UpdateBy;
            img.UpdateTime = DateTime.Now;
            LogInterceptorHelpher<IShopImgRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopImgRepository, ShopLogDO>();
            var shopImgRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopImgRepository, new ShopLogDO()
            {
                Identifier = img.Id.ToString(),
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                Type2 = LoggerTypeTwoEnum.DeleteShopImg.GetEnumDescription(),
                Filter1 = img.ShopId.ToString(),
                Filter2 = string.Empty,
                Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
            });
            var num = await shopImgRepository.UpdateAsync(img, new[] { "IsDeleted", "UpdateTime", "UpdateBy" });
            return num > 0;
        }
        /// <summary>
        /// 获取门店专修品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceBrandDTO>> GetShopServiceBrands(ShopServiceBrandDTO request)
        {
            var result = await _shopServiceRepository.GetShopServiceBrands(new List<long> { request.ShopId });
            var resultVo = _mapper.Map<List<ShopServiceBrandDTO>>(result);
            return resultVo;
        }

        /// <summary>
        /// 查询门店专职顾问信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoDTO>> GetShopHeaderListByAsync()
        {
            var result = await _shopRepository.GetShopHeaderListByAsync();
            return _mapper.Map<List<ShopSimpleInfoDTO>>(result);
        }

        #endregion

        #region 公共服务
        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopSimpleInfoResponse> GetShopSimpleInfoAsync(GetShopRequest request)
        {
            //获取门店详情
            var shopInfo = await _shopRepository.GetShopSimpleInfoAsync(request.ShopId);
            GetShopSimpleInfoResponse response = new GetShopSimpleInfoResponse();
            if (shopInfo != null)
            {
                //获取门店图片
                var shopImgs = await _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
                response = _mapper.Map<GetShopSimpleInfoResponse>(shopInfo);
                response.Type = shopInfo.Type;
                response.ShopType = ((ShopTypeEnum)shopInfo.Type).GetEnumDescription();
                response.ShopImageUrl = Domain + shopInfo.ShopImageUrl;
                response.WorkTime = shopInfo.StartWorkTime.ToShortTimeString() + "-" + shopInfo.EndWorkTime.ToShortTimeString();
                response.Imgs = shopImgs.Where(o => o.Type != 3).Select(p => new ImgDTO() { Url = Domain + p.ImgUrl }).ToList();
            }

            return response;
        }
        /// <summary>
        /// 查询门店主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopDTO> GetShopByIdAsync(GetShopRequest request)
        {
            //获取门店详情
            var shopInfo = await _shopRepository.GetAsync(request.ShopId);
            var shop = _mapper.Map<ShopDTO>(shopInfo);
            if (shop != null)
            {
                if (!string.IsNullOrWhiteSpace(shop.TagName))
                {
                    shop.TagNames = shop.TagName.Split(',').ToList();
                }
            }

            return shop;
        }
        /// <summary>
        /// 查询门店配置表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopConfigDTO> GetShopConfigByShopIdAsync(GetShopRequest request)
        {
            //获取门店详情
            var shopConfig = await _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
            var result = _mapper.Map<ShopConfigDTO>(shopConfig);
            return result;
        }

        /// <summary>
        /// 根据门店ID查询门店银行账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopBankCardDTO> GetShopBandInfoByShopId(GetShopRequest request)
        {
            var shopBankCard = await _shopBankCardRepository.GetShopBankCardByShopIdAsync(request.ShopId);
            var result = _mapper.Map<ShopBankCardDTO>(shopBankCard);
            return result;
        }

        /// <summary>
        /// 查询门店简单信息列表---分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request)
        {
            //门店信息列表
            var result = await _shopRepository.GetShopListAsync(request);
            ApiPagedResultData<ShopSimpleInfoDTO> response = new ApiPagedResultData<ShopSimpleInfoDTO>();
            response.Items = _mapper.Map<List<ShopSimpleInfoDTO>>(result.Items);
            foreach (var item in response.Items)
            {
                item.Type = GetShopTypeStr(int.Parse(item.Type));
            }
            response.TotalItems = result.TotalItems;
            return response;
        }
        public async Task<List<ShopSimpleInfoDTO>> GetShopWareHouseListAsync(GetShopListRequest request)
        {
            //门店仓库列表
            var result = await _shopRepository.GetShopWareHouseListAsync(request);
            return _mapper.Map<List<ShopSimpleInfoDTO>>(result);
        }

        /// <summary>
        /// 查询门店简单信息列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoDTO>> GetShopListByIdsAsync(GetShopListByIdsRequest request)
        {
            //门店信息列表
            var result = await _shopRepository.GetShopListByIdsAsync(request.ShopIds);
            //ApiPagedResultData<ShopSimpleInfoDTO> response = new ApiPagedResultData<ShopSimpleInfoDTO>();
            return _mapper.Map<List<ShopSimpleInfoDTO>>(result);
        }

        /// <summary>
        /// 更新门店好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopScoreAsync(UpdateShopScoreByShopIdsRequest request)
        {
            int num = 0;
            if (request != null && request.ShopScoreList != null)
            {
                try
                {
                    var shopScoreList = _mapper.Map<List<ShopScoreDO>>(request.ShopScoreList);
                    num = await _shopRepository.UpdateShopScoreAsync(shopScoreList);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return num;
        }

        public async Task<List<ShopTagDTO>> GetShopTags(ShopTagDTO request)
        {
            var result = await _shopRepository.GetShopTags(new ShopTagDO
            {
                ShopId = request.ShopId
            });
            var vo = _mapper.Map<List<ShopTagDTO>>(result);
            return vo;
        }

        #endregion


        #region  微信端服务
        /// <summary>
        /// 微信小程序查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<NearShopInfoDTO>> GetNearShopListAsync(GetNearShopListRequest request)
        {
            //门店信息列表
            var result = await _shopRepository.GetNearShopListAsync(request);
            ApiPagedResultData<NearShopInfoDTO> response = new ApiPagedResultData<NearShopInfoDTO>();
            var list = _mapper.Map<List<NearShopInfoDTO>>(result.List);
            if (list != null && list.Any())
            {
                List<long> shopIds = list.Select(o => o.Id).ToList();

                #region 获取门店专修品牌
                var brands = await _shopServiceRepository.GetShopServiceBrands(shopIds);
                var brandDic = new Dictionary<long, List<ShopServiceBrandDTO>>();
                if (brands.Any())
                {
                    var brandVo = _mapper.Map<List<ShopServiceBrandDTO>>(brands);
                    foreach (var item in brandVo)
                    {
                        if (!brandDic.ContainsKey(item.ShopId))
                        {
                            brandDic[item.ShopId] = new List<ShopServiceBrandDTO>();
                            brandDic[item.ShopId].Add(item);
                        }
                        else
                        {
                            brandDic[item.ShopId].Add(item);
                        }
                    }
                }
                #endregion
                list.ForEach(r =>
                {
                    r.TagNames = !string.IsNullOrWhiteSpace(r.TagName) ? r.TagName.Split(',').ToList() : new List<string>();
                    r.Img = !string.IsNullOrWhiteSpace(r.Img) ? r.Img : "shopProductImage/202002251011076245.PNG";
                    r.BrandNames = brandDic.ContainsKey(r.Id) ? brandDic[r.Id] : new List<ShopServiceBrandDTO>();
                });
                //筛选门店开通的服务大类

                //var shopServiceCategory = _shopServiceRepository.GetServiceCategoryWithShops(shopIds);

                var shopServiceTypes = _shopServiceTypeRepository.GetShopServiceTypeListByIdsAsync(shopIds);
                var serviceType = _baoYangClient.GetServiceTypeEnum();
                await Task.WhenAll(shopServiceTypes, serviceType);
                if (shopServiceTypes.Result != null && shopServiceTypes.Result.Any() && serviceType.Result != null && serviceType.Result.Any())
                {
                    List<ShopServiceTypeDTO> shopTypes = new List<ShopServiceTypeDTO>();
                    foreach (var item in shopServiceTypes.Result)
                    {
                        foreach (var itemType in serviceType.Result)
                        {
                            if (itemType.ServiceType == item.ServiceType)
                            {
                                var type = new ShopServiceTypeDTO();
                                type.ShopId = item.ShopId;
                                type.DisplayName = itemType.DisplayName;
                                shopTypes.Add(type);
                            }
                        }
                    }
                    foreach (var item in list)
                    {
                        item.ShopServices = shopTypes.Where(o => o.ShopId == item.Id).Select(r => r.DisplayName).ToList();
                    }
                }

                var orderCommentTask =
                    _orderCommentClient.GetShopScore(new ShopScoreClientRequest() { ShopIdList = shopIds });
                //查询门店订单数量
                var shopOrderCountListTask = _orderClient.BatchGetOrderCountByShopId(new GetOrderCountByShopIdClientRequest() { ShopIds = shopIds });
                await Task.WhenAll(orderCommentTask, shopOrderCountListTask);
                var shopOrderCountList =
                    shopOrderCountListTask.Result ?? new List<GetOrderCountByShopIdClientResponse>();
                var orderComment = orderCommentTask.Result ?? new List<ShopScoreDto>();
                foreach (var item in list)
                {
                    item.Score = orderComment.FirstOrDefault(_ => _.ShopId == item.Id)?.Score ?? 0;
                    item.TotalOrder = shopOrderCountList.FirstOrDefault(_ => _.ShopId == item.Id)?.OrderCount ?? 0;
                }

            }

            response.Items = list;
            response.TotalItems = result.TotalItems;
            return response;
        }
        /// <summary>
        /// 微信小程序获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopDetailResponse> GetShopDetailAsync(GetShopDetailRequest request)
        {
            var shopDetail = new GetShopDetailResponse();
            //获取门店详情
            var shopInfo = await _shopRepository.GetNearShoDetailAsync(request);

            if (shopInfo != null)
            {
                shopDetail = _mapper.Map<GetShopDetailResponse>(shopInfo);
                shopDetail.TagNames = !string.IsNullOrWhiteSpace(shopDetail.TagName) ? shopDetail.TagName.Split(',').ToList() : new List<string>();

                //获取门店专修品牌
                var brands = await _shopServiceRepository.GetShopServiceBrands(new List<long> { request.ShopId });
                if (brands.Any())
                {
                    shopDetail.BrandNames = _mapper.Map<List<ShopServiceBrandDTO>>(brands);
                }

                //获取门店图片
                var shopImgs = await _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
                List<string> Imgs = new List<string>();
                if (shopImgs != null && shopImgs.Any())
                {
                    Imgs = shopImgs.OrderBy(o => o.Id).Where(r => r.Type == 1 || r.Type == 2 || r.Type == 4).Select(a => Domain + a.ImgUrl).ToList();
                }
                else
                {
                    Imgs.Add(Domain + "shopProductImage/202002251011076245.PNG");
                }

                var shopIds = new List<long>();
                shopIds.Add(request.ShopId);

                //获取门店员工列表 
                var shopEmployees = await _employeeRepository.GetEmployeeListByShopIdAsync(request.ShopId, 1);
                var workKinds = await _jobRepository.GetWorkKindList();
                List<TechDTO> Techs = new List<TechDTO>();
                shopEmployees?.Where(_ => _.JobId == 4)?.ToList().ForEach(o => Techs.Add(new TechDTO() { TechId = o.Id, HeadImg = o.Avatar, Name = o.Name, TechLevel = "技术总监" }));

                shopEmployees?.Where(_ => _.JobId == 3)?.ToList().ForEach(o => Techs.Add(new TechDTO() { TechId = o.Id, HeadImg = o.Avatar, Name = o.Name, TechLevel = workKinds?.Where(_ => _.Id == o.WorkKindId)?.FirstOrDefault().Name + (o.Level > 0 ? "-" + CommonHelper.GetDescriptionByEnum((EmployeeLevelEnum)o.Level) : "") }));

                shopEmployees?.Where(_ => _.JobId == 2)?.ToList().ForEach(o => Techs.Add(new TechDTO() { TechId = o.Id, HeadImg = o.Avatar, Name = o.Name, TechLevel = "店长" }));

                Techs.ForEach(o => o.HeadImg = !string.IsNullOrEmpty(o.HeadImg) ? o.HeadImg : "shopProductImage/202002251129368649.png");

                var orderCommentTask =
                    _orderCommentClient.GetShopScore(new ShopScoreClientRequest() { ShopIdList = shopIds });
                //查询门店订单数量
                var shopOrderCountTask =
                    _orderClient.BatchGetOrderCountByShopId(
                        new GetOrderCountByShopIdClientRequest() { ShopIds = shopIds });
                //门店预约数量
                var shopReserveTask =
                    _receiveClient.GetShopTotalReserve(new BaseGetShopClientRequest { ShopId = request.ShopId });
                await Task.WhenAll(orderCommentTask, shopOrderCountTask, shopReserveTask);
                var shopReserve = shopReserveTask.Result;
                shopDetail.TotalReserve = shopReserve?.TotalReserve ?? 0;

                var shopOrderCount = shopOrderCountTask.Result ?? new List<GetOrderCountByShopIdClientResponse>();
                shopDetail.TotalOrder = shopOrderCount.FirstOrDefault(_ => _.ShopId == request.ShopId)?.OrderCount ?? 0;

                var orderComment = orderCommentTask.Result ?? new List<ShopScoreDto>();
                shopDetail.Score = orderComment.FirstOrDefault(_ => _.ShopId == request.ShopId)?.Score ?? 0;

                shopDetail.WorkTime = shopInfo.StartWorkTime.ToShortTimeString() + "-" + shopInfo.EndWorkTime.ToShortTimeString();
                shopDetail.Imgs = Imgs;
                //shopInfo.



                shopDetail.Techs = Techs;
                return shopDetail;
            }
            return null;

        }



        /// <summary>
        /// 根据市查询市下的区县
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopLocationVO>> RegionChinaReqByRegionId(GetRegionByCityIdRequest request)
        {
            var clientRequest = new RegionChinaReqByRegionIdClientRequest()
            {
                RegionId = request.CityId.ToString()
            };
            //区县信息
            var regionlist = await _basicDataClient.RegionChinaReqByRegionId(clientRequest);
            List<ShopLocationVO> shopLocationList = new List<ShopLocationVO>();
            shopLocationList.Add(new ShopLocationVO { DistrictId = 0, Name = "全市" });
            regionlist.ForEach(o =>
                shopLocationList.Add(new ShopLocationVO() { DistrictId = long.Parse(o.RegionId), Name = o.Name }));

            if (request.IsShowShopCount)
            {
                ////注释 各区门店数量查询
                var shopInfolist = await _shopRepository.GetCityShopListAsync(request.CityId);
                //查询每一区的门店数量
                if (shopInfolist != null)
                {
                    foreach (var item in shopLocationList)
                    {
                        item.TotalCount = shopInfolist.Where(s => s.DistrictId == item.DistrictId).Count();
                    }

                    shopLocationList.First().TotalCount = shopLocationList.Sum(c => c.TotalCount);
                }
            }

            return shopLocationList;
        }

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public async Task<GetCityListResponse> GetAllCitys()
        {
            var request = new RegionChinaReqByLevelClientRequest()
            {
                Level = RegionChinaType.City
            };
            //获取所有城市
            var clientResponse = await _basicDataClient.GetAllCitys(request);
            List<GetAllCitysVO> cityList = new List<GetAllCitysVO>();
            //按字母分组
            var groupList = clientResponse.GroupBy(g => g.Initial).ToList().OrderBy(o => o.Key);

            foreach (IGrouping<string, RegionChinaDTO> regionItem in groupList)
            {
                List<CityInfo> cityInfos = new List<CityInfo>();
                regionItem.ToList().ForEach(o => cityInfos.Add(new CityInfo()
                {
                    CityId = o.RegionId,
                    City = o.Name,
                    ProvinceId = o.ParentId
                }));

                GetAllCitysVO city = new GetAllCitysVO()
                {
                    Code = regionItem.Key,
                    Citys = cityInfos
                };
                cityList.Add(city);
            }

            //获取热门城市
            var hotCitys = _hotCityRepository.GetListAsync(" where is_deleted = 0").Result.ToList();
            List<CityInfo> regionDTOs = new List<CityInfo>();
            hotCitys.ForEach(o => regionDTOs.Add(new CityInfo()
            {
                CityId = o.RegionId,
                City = o.Name,
                ProvinceId = o.ParentId
            }));

            var response = new GetCityListResponse()
            {
                Citys = cityList,
                HotCitys = regionDTOs
            };
            return response;
        }

        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetOptimalShopResponse> GetOptimalShop(GetOptimalShopRequest request)
        {
            long shopId = 0;
            if (!string.IsNullOrEmpty(request.UserId))
            {
                shopId = await _receiveClient.GetLastShopForLastArrival(new GetLastShopForLastArrivalClientRequest { UserId = request.UserId });
                if (shopId > 0)
                {
                    var shopInfo = await _shopRepository.GetShopSimpleInfoForApp(shopId);
                    if (shopInfo != null && shopInfo.Id > 0)
                    {
                        shopId = shopInfo.Id;
                    }
                    else
                    {
                        shopId = 0;
                    }
                }
                if (shopId == 0)
                {
                    if (request.CityId.IsNullOrWhiteSpace() && request.Longitude > 0 && request.Latitude > 0)
                    {
                        var position = await _basicDataClient.GetPosition(new GetPositionClientRequest { Longitude = request.Longitude, Latitude = request.Latitude });
                        if (position != null)
                        {
                            request.CityId = position.CityId;
                        }
                    }
                    var shopInfo = await _shopRepository.GetOptimalShopId(request);
                    if (shopInfo != null && shopInfo.Id > 0)
                    {
                        shopId = shopInfo.Id;
                    }
                }
            }
            else
            {
                if (request.CityId.IsNullOrWhiteSpace() && request.Longitude > 0 && request.Latitude > 0)
                {
                    var position = await _basicDataClient.GetPosition(new GetPositionClientRequest { Longitude = request.Longitude, Latitude = request.Latitude });
                    if (position != null)
                    {
                        request.CityId = position.CityId;
                    }
                }
                var shopInfo = await _shopRepository.GetOptimalShopId(request);
                if (shopInfo != null && shopInfo.Id > 0)
                {
                    shopId = shopInfo.Id;
                }
            }

            return new GetOptimalShopResponse { ShopId = shopId };
        }

        #endregion


        #region  APP服务

        /// <summary>
        /// 查询门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopSimpleInfoResponse> GetShopInfoForAppAsync(BaseGetShopRequest request)
        {
            //获取门店详情
            var shopInfo = await _shopRepository.GetShopSimpleInfoAsync(request.ShopId);
            //var shopComment = await _orderCommentClient.GetShopCommentNum(new BaseGetShopClientRequest() { ShopId = request.ShopId });
            GetShopSimpleInfoResponse response = new GetShopSimpleInfoResponse();
            if (shopInfo != null)
            {
                //获取门店图片
                var shopImgs = await _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
                response = _mapper.Map<GetShopSimpleInfoResponse>(shopInfo);
                response.ShopImageUrl = Domain + shopInfo.ShopImageUrl;
                response.WorkTime = shopInfo.StartWorkTime.ToShortTimeString() + "-" +
                                    shopInfo.EndWorkTime.ToShortTimeString();
                response.Imgs = shopImgs.OrderBy(o => o.Id).Where(r => r.Type == 1 || r.Type == 2 || r.Type == 4)
                    .Select(p => new ImgDTO() { Url = Domain + p.ImgUrl }).ToList();

                List<long> shopIds = new List<long>() { request.ShopId };
                var orderCommentTask =
                    _orderCommentClient.GetShopScore(new ShopScoreClientRequest() { ShopIdList = shopIds }); //评论
                var shopOrderCountTask =
                    _orderClient.BatchGetOrderCountByShopId(
                        new GetOrderCountByShopIdClientRequest() { ShopIds = shopIds }); //查询门店订单数量
                await Task.WhenAll(orderCommentTask, shopOrderCountTask);
                var orderComment = orderCommentTask.Result ?? new List<ShopScoreDto>();
                var shopOrderCount = shopOrderCountTask.Result ?? new List<GetOrderCountByShopIdClientResponse>();
                response.Score = orderComment.FirstOrDefault(_ => _.ShopId == request.ShopId)?.Score ?? 0;
                response.TotalComment = orderComment.FirstOrDefault(_ => _.ShopId == request.ShopId)?.TotalNum ?? 0;
                response.TotalOrder = shopOrderCount.FirstOrDefault(_ => _.ShopId == request.ShopId)?.OrderCount ?? 0;
            }

            return response;
        }

        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopDetailForAppResponse> GetShopDetailForApp(GetShopRequest request)
        {
            //获取门店详情
            var shopInfo = await _shopRepository.GetShopDetailForAppAsync(request.ShopId);

            GetShopDetailForAppResponse response = new GetShopDetailForAppResponse();
            if (shopInfo != null)
            {
                #region 设施配置
                //设施配置
                List<ShopSetDTO> shopSetDTO = new List<ShopSetDTO>();

                //基础设施
                List<IsSetDTO> ShopBaseSet = new List<IsSetDTO>();
                ShopBaseSet.Add(new IsSetDTO()
                {
                    Name = "免费WIFI",
                    IsConfigured = shopInfo.IsFreeWiFi
                });
                ShopBaseSet.Add(new IsSetDTO()
                {
                    Name = "休息室",
                    IsConfigured = shopInfo.IsLoungeRoom
                });
                ShopBaseSet.Add(new IsSetDTO()
                {
                    Name = "卫生间",
                    IsConfigured = shopInfo.IsRestRoom
                });
                shopSetDTO.Add(new ShopSetDTO()
                {
                    Name = "基础配备",
                    Items = ShopBaseSet
                });
                //硬件设施
                List<IsSetDTO> HardwareFacilities = new List<IsSetDTO>();
                HardwareFacilities.Add(new IsSetDTO()
                {
                    Name = "柱式举升机",
                    IsConfigured = shopInfo.IsPostLift
                });
                HardwareFacilities.Add(new IsSetDTO()
                {
                    Name = "汽车故障诊断仪",
                    IsConfigured = shopInfo.CarFaultDiagnosticTool
                });
                shopSetDTO.Add(new ShopSetDTO()
                {
                    Name = "硬件设施",
                    Items = HardwareFacilities
                });
                #endregion

                #region 获取门店专修品牌
                var brands = await _shopServiceRepository.GetShopServiceBrands(new List<long> { request.ShopId });
                if (brands.Any())
                {
                    response.BrandNames = _mapper.Map<List<ShopServiceBrandDTO>>(brands);
                }
                #endregion

                #region 获取门店账号信息
                var shopBackRes = await _shopBankCardRepository.GetShopBankCardByShopIdAsync(request.ShopId);
                if (shopBackRes != null)
                {
                    response.OpeningBank = shopBackRes.OpeningBank;
                    response.OpeningBranch = shopBackRes.OpeningBranch;
                    response.OpeningUserName = shopBackRes.OpeningUserName;
                    response.BankCardNo = shopBackRes.BankCardNo;
                }
                #endregion

                //获取门店图片
                var shopImgs = await _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
                List<string> Imgs = new List<string>();
                shopImgs.ForEach(a => { Imgs.Add(a.ImgUrl); });
                response.Contact = shopInfo.Contact;
                response.Telephone = shopInfo.Telephone;
                response.Head = shopInfo.Head;
                response.HeadPhone = shopInfo.HeadPhone;
                response.HeadEmail = shopInfo.HeadEmail;
                response.Address = shopInfo.Address;
                response.WorkTime = shopInfo.StartWorkTime.ToShortTimeString() + "-" + shopInfo.EndWorkTime.ToShortTimeString();
                response.HeadImg = Domain + shopInfo.Img;
                response.Description = shopInfo.Description;
                //response.AppletCode = shopInfo.AppletCode;
                response.OwnerName = shopInfo.OwnerName;
                response.OwnerPhone = shopInfo.OwnerPhone;
                response.ShopImgs = shopImgs.Where(o => o.Type == 2).Select(p => new ImgDTO() { Url = Domain + p.ImgUrl }).ToList();
                response.ShopProofImgs = shopImgs.Where(o => o.Type == 3).Select(p => new ImgDTO() { Url = Domain + p.ImgUrl }).ToList(); response.ShopFacility = shopSetDTO;
                response.AppletCode = Domain + shopInfo.AppletCode;//+ "?imageView2/1/w/200/h/200";
                response.ShopAppletCode = Domain + shopInfo.ShopAppletCode;
                response.TagNames = !string.IsNullOrWhiteSpace(shopInfo.TagName) ? shopInfo.TagName.Split(',').ToList() : new List<string>();
            }

            return response;
        }

        public async Task<List<GetVehicleBrandVo>> GetVehicleBrandList(GetVehicleBrandVo request)
        {
            var brandRes = await _vehicleClient.GetVehicleBrandList();
            if (brandRes.Any())
            {
                var brands = _mapper.Map<List<GetVehicleBrandVo>>(brandRes);
                return brands;
            }
            return new List<GetVehicleBrandVo>();
        }

        /// <summary>
        /// 新增门店注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<AddShopRegisterResponse>> AddShopRegister(ApiRequest<AddShopRegisterRequest> request)
        {
            var result = Result.Failed<AddShopRegisterResponse>("注册异常");
            try
            {
                #region 校验
                var requestData = request.Data;
                if (request.Data == null)
                {
                    throw new CustomException("请求参数异常");
                }
                //判断门店名称是否唯一
                var existShop = await _shopRepository.GetShopByNameAsync(requestData.SimpleName, 0);
                if (existShop != null)
                {
                    throw new CustomException("门店名称已存在");
                }
                var createBy = requestData.OwnerName;//操作人
                #endregion

                #region 创建门店
                LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                var shopRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                {
                    Identifier = string.Empty,
                    CreateBy = createBy,
                    UpdateBy = string.Empty,
                    Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                    Type2 = LoggerTypeTwoEnum.ShopMainCreate.GetEnumDescription(),
                    Filter1 = string.Empty,
                    Filter2 = string.Empty,
                    Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                });
                //类型为合作店，已上架，新建，正常营业
                var shop = _mapper.Map<ShopDO>(requestData);
                shop.FullName = requestData.SimpleName;
                shop.Type = requestData.Nature == ShopNatureEnum.MrApolloErp ? 16 : 1;
                shop.Online = 1;
                //shop.CheckStatus = 0;
                shop.CheckStatus = 2;
                shop.SystemType = shop.Type == 16 ? 2 : 0;
                shop.Status = 0;
                shop.CreateBy = createBy;
                shop.CreateTime = DateTime.Now;
                shop.Head = _configuration["ShopManageDefault:Name"];
                shop.HeadPhone = _configuration["ShopManageDefault:Tel"];
                shop.HeadEmail = _configuration["ShopManageDefault:Email"];
                shop.Contact = requestData.OwnerName;
                shop.Mobile = requestData.OwnerPhone;
                shop.Telephone = requestData.OwnerPhone;
                shop.OwnerName = requestData.OwnerName;
                shop.OwnerPhone = requestData.OwnerPhone;
                shop.CompanyId = requestData.CompanyId;
                shop.ShopCompanyName = requestData.ShopCompanyName ?? String.Empty;
                shop.Province = requestData.Province ?? string.Empty;
                shop.ProvinceId = requestData.ProvinceId ?? string.Empty;
                shop.City = requestData.City ?? string.Empty;
                shop.CityId = requestData.CityId ?? string.Empty;
                shop.District = requestData.District ?? string.Empty;
                shop.DistrictId = requestData.DistrictId ?? string.Empty;
                shop.Address = requestData.Address ?? string.Empty;
                shop.CompanyId = request.Data.CompanyId;
                var shopId = await shopRepository.AddShopAsync(shop);
                if (shopId <= 0)
                {
                    throw new CustomException("创建门店异常");
                }
                var response = new AddShopRegisterResponse() { ShopId = shopId };
                #endregion

                #region 生成排队码和门店码
                _ = Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        //排队码
                        var fastQueuingCodeResult = await GenerateShopAppletCode(shopId, "pages/fastQueuing/main");
                        if (fastQueuingCodeResult.IsNotNullSuccess())
                        {
                            await _shopRepository.UpdateQuickQueueAppletCode(new ShopDO
                            {
                                UpdateBy = createBy,
                                AppletCode = fastQueuingCodeResult.Data,
                                Id = shopId
                            });
                        }
                        //门店码
                        var shopCodeResult = await GenerateShopAppletCode(shopId, "pages/storeDetails/main");
                        if (shopCodeResult.IsNotNullSuccess())
                        {
                            await _shopRepository.UpdateShopAppletCode(new ShopDO
                            {
                                UpdateBy = createBy,
                                ShopAppletCode = shopCodeResult.Data,
                                Id = shopId
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("AddShopRegister_UpdateShopAppletCodeEx", ex);
                    }
                });
                #endregion

                #region 自动生成门店服务类型记录
                _ = Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var serviceTypes = await _baoYangClient.GetServiceTypeEnum();
                        if (serviceTypes != null && serviceTypes.Any())
                        {
                            foreach (var item in serviceTypes)
                            {
                                await _shopServiceTypeRepository.CreateShopServiceType(new ShopServiceTypeDO
                                {
                                    ShopId = shopId,
                                    IsDeleted = 0,
                                    CreateBy = "System",
                                    CreateTime = DateTime.Now,
                                    ServiceType = item.ServiceType
                                });
                            }
                        }
                        //_logger.Info($"AddShopRegister_CreateShopServiceType serviceTypes={JsonConvert.SerializeObject(serviceTypes)} ");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("AddShopRegister_CreateShopServiceTypeEx", ex);
                    }
                });
                #endregion

                result = Result.Success(response, "注册成功");

                var subject = string.Empty;

                if (_hostingEnvironment.IsProduction())
                {
                    subject = $"门店注册邮件 Prod 环境";
                }
                else if (_hostingEnvironment.IsStaging())
                {
                    subject = $"门店注册邮件 Staging 环境";
                }
                else
                {
                    subject = $"门店注册邮件 Dev 环境";
                }

                MailboxAddress to = new MailboxAddress(_configuration["ShopManageDefault:Email"]);
                List<MailboxAddress> tolist = new List<MailboxAddress>();
                tolist.Add(to);
                await _eMailClient.SendEMailAsync(subject, $"{requestData.OwnerName} 电话 {requestData.OwnerPhone} 在 {DateTime.Now.ToString()} 注册了名称为： { requestData.SimpleName} 门店,请登录boss系统查看详细信息", tolist);
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("AddShopRegisterEx", ex);
            }
            finally
            {
                //_logger.Info($"AddShopRegister request={JsonConvert.SerializeObject(request)} result={JsonConvert.SerializeObject(result)}");
            }


            return result;
        }

        #endregion


        /// <summary>
        /// 重新生成门店二维码
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddShopAppletCode()
        {
            var shops = await _shopRepository.GetAllShopAsync();
            if (shops != null && shops.Any())
            {
                foreach (var item in shops)
                {
                    //生成快速排队二维码
                    var res = await GenerateShopAppletCode(item.Id, "pages/fastQueuing/main");
                    if (res.Code == ResultCode.Success)
                    {
                        await _shopRepository.UpdateQuickQueueAppletCode(new ShopDO
                        {
                            UpdateBy = "SYSTEM",
                            AppletCode = res.Data,
                            Id = item.Id
                        });
                    }

                    //生成门店二维码
                    res = await GenerateShopAppletCode(item.Id, "pages/storeDetails/main");
                    if (res.Code == ResultCode.Success)
                    {
                        await _shopRepository.UpdateShopAppletCode(new ShopDO
                        {
                            UpdateBy = "SYSTEM",
                            ShopAppletCode = res.Data,
                            Id = item.Id
                        });
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 查询门店日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopLogDTO>>> GetShopLog(GetShopLogRequest request)
        {
            var getShops = await _shopLogRepository.GetShopLog(request.ShopId);
            return new ApiResult<List<ShopLogDTO>>()
            {
                Code = ResultCode.Success,
                Data = getShops

            };
        }



        /// <summary>
        /// 得到引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<object>> GetGuideData(GetGuideDataRequest request)
        {
            //门店信息
            var shopInfo = await _shopRepository.GetShopAsync(request.ShopId);

            var getGuideDataResponse = new Object();

            if (request.Step == OpeningGuideStepEnum.First)
            {
                if (shopInfo != null)
                {
                    var shopBaseInfo = _mapper.Map<ShopBaseInfoVO>(shopInfo);

                    if (shopBaseInfo != null)
                    {
                        shopBaseInfo.FrontImgs = new List<ImgVO>();
                        shopBaseInfo.EnvironmentImgs = new List<ImgVO>();
                        shopBaseInfo.ManagementLicenseImgs = new List<ImgVO>();
                        shopBaseInfo.OtherLicenseImgs = new List<ImgVO>();
                        shopBaseInfo.DoorHeaderImgs = new List<ImgVO>();
                        shopBaseInfo.BusinessLicenseImgs = new List<ImgVO>();

                        //获取门店图片
                        var imgs = await _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
                        imgs?.ForEach(_ =>
                        {

                            if (_.Type == ShopImgTypeEnum.DoorHeader.ToInt())
                            {
                                shopBaseInfo.DoorHeaderImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                            if (_.Type == ShopImgTypeEnum.Front.ToInt())
                            {
                                shopBaseInfo.FrontImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                            if (_.Type == ShopImgTypeEnum.Environment.ToInt())
                            {
                                shopBaseInfo.EnvironmentImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                            if (_.Type == ShopImgTypeEnum.ManagementLicense.ToInt())
                            {
                                shopBaseInfo.ManagementLicenseImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                            if (_.Type == ShopImgTypeEnum.OtherLicense.ToInt())
                            {
                                shopBaseInfo.OtherLicenseImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                            if (_.Type == ShopImgTypeEnum.BusinessLicense.ToInt())
                            {
                                shopBaseInfo.BusinessLicenseImgs.Add(new ImgVO()
                                {
                                    Url = $"{_configuration["QiNiuImageDomain"]}{ _.ImgUrl}"
                                });
                            }
                        });

                        if (!string.IsNullOrWhiteSpace(shopInfo.TagName))
                        {
                            var tags = shopInfo.TagName.Split(',').ToList();
                            shopBaseInfo.TagNameOne = tags[0];
                            if (tags.Count > 1)
                            {
                                shopBaseInfo.TagNameTwo = tags[1];
                            }
                            if (tags.Count > 2)
                            {
                                shopBaseInfo.TagNameThree = tags[2];
                            }
                        }
                        else
                        {
                            //if (shopInfo.Nature == 1)
                            //{
                            //    shopBaseInfo.TagNameOne = "上门服务";
                            //    shopBaseInfo.TagNameTwo = "1对1接待";
                            //}
                            //else
                            //{
                            //    shopBaseInfo.TagNameOne = "上门取车";
                            //    shopBaseInfo.TagNameTwo = "汽车消毒";
                            //}
                        }
                        shopBaseInfo.Head = shopBaseInfo.Head + " " + shopBaseInfo.HeadPhone;
                        var result = await _shopServiceRepository.GetShopServiceBrands(new List<long> { request.ShopId });
                        shopBaseInfo.Brands = _mapper.Map<List<GetVehicleBrandVo>>(result);

                        getGuideDataResponse = shopBaseInfo;

                    }
                }
            }

            if (request.Step == OpeningGuideStepEnum.Second)
            {
                if (shopInfo != null)
                {
                    var shopAddressInfo = _mapper.Map<ShopAddressInfoVO>(shopInfo);
                    getGuideDataResponse = shopAddressInfo;
                }
            }
            else if (request.Step == OpeningGuideStepEnum.Third)
            {
                var thirdStep = new ShopConfigInfoVO();
                //门店配置信息
                var shopConfigInfo = await _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
                if (shopConfigInfo != null)
                {
                    thirdStep.StartWorkTime = shopConfigInfo.StartWorkTime.ToString("HH:mm");
                    thirdStep.EndWorkTime = shopConfigInfo.EndWorkTime.ToString("HH:mm");
                    List<IsSetDTO> Items = new List<IsSetDTO>();
                    Items.Add(new IsSetDTO { Name = "休息室", IsConfigured = Convert.ToBoolean(shopConfigInfo.IsLoungeRoom) });
                    Items.Add(new IsSetDTO { Name = "卫生间", IsConfigured = Convert.ToBoolean(shopConfigInfo.IsRestRoom) });
                    Items.Add(new IsSetDTO { Name = "免费WiFi", IsConfigured = Convert.ToBoolean(shopConfigInfo.IsFreeWifi) });
                    Items.Add(new IsSetDTO { Name = "柱式举升机", IsConfigured = Convert.ToBoolean(shopConfigInfo.IsPostLift) });
                    Items.Add(new IsSetDTO { Name = "汽车故障诊", IsConfigured = Convert.ToBoolean(shopConfigInfo.CarFaultDiagnosticTool) });
                    thirdStep.Items = Items;

                }
                else
                {
                    thirdStep.StartWorkTime = "08:00";
                    thirdStep.EndWorkTime = "20:00";
                    List<IsSetDTO> Items = new List<IsSetDTO>();
                    Items.Add(new IsSetDTO { Name = "休息室", IsConfigured = true });
                    Items.Add(new IsSetDTO { Name = "卫生间", IsConfigured = true });
                    Items.Add(new IsSetDTO { Name = "免费WiFi", IsConfigured = true });
                    Items.Add(new IsSetDTO { Name = "柱式举升机", IsConfigured = true });
                    Items.Add(new IsSetDTO { Name = "汽车故障诊", IsConfigured = true });
                    thirdStep.Items = Items;
                }
                getGuideDataResponse = thirdStep;
            }
            else if (request.Step == OpeningGuideStepEnum.Fourth)
            {
                var fourthStep = new ShopBankInfoVO();
                //门店银行账户信息
                var shopBankCard = await _shopBankCardRepository.GetShopBankCardByShopIdAsync(request.ShopId);
                if (shopBankCard != null)
                {
                    fourthStep = _mapper.Map<ShopBankInfoVO>(shopBankCard);
                }
                if (shopInfo != null)
                {
                    fourthStep.IdCardFront = shopInfo.IdCardFront;
                    fourthStep.IdCardBack = shopInfo.IdCardBack;
                }
                getGuideDataResponse = fourthStep;
            }
            return new ApiResult<object>()
            {
                Code = ResultCode.Success,
                Data = getGuideDataResponse
            };
        }

        /// <summary>
        /// 保存引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveOpeningGuide(ApiRequest<SaveOpeningGuideRequest> request)
        {
            var requestData = request.Data;
            if (requestData == null)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "门店信息异常"
                };
            }

            //门店信息
            var shopInfo = await _shopRepository.GetShopAsync(requestData.ShopId);
            if (shopInfo == null)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "门店信息异常"
                };
            }
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (requestData.SaveStep == OpeningGuideStepEnum.First)
                {
                    var updateShopImgs = await _shopImgRepository.UpdateShopImgInfoForOpeningGuide(new List<int>()
                            {
                                ShopImgTypeEnum.Front.ToInt(),
                                ShopImgTypeEnum.Environment.ToInt(),
                                ShopImgTypeEnum.BusinessLicense.ToInt(),
                                ShopImgTypeEnum.DoorHeader.ToInt(),
                                ShopImgTypeEnum.ManagementLicense.ToInt(),
                                ShopImgTypeEnum.OtherLicense.ToInt()
                            }, requestData?.ShopId ?? 0, requestData.FirstStep?.UpdateBy ?? string.Empty);

                    List<ShopImgDO> shopImgDOs = new List<ShopImgDO>();

                    if (requestData.FirstStep != null)
                    {
                        if (shopInfo.Nature == 1)
                        {
                            //判断门店名称是否唯一
                            var existShop = await _shopRepository.GetShopByNameAsync(requestData.FirstStep.SimpleName, requestData.ShopId);
                            if (existShop != null)
                            {
                                throw new CustomException("门店名称已存在");
                            }
                        }
                        else if (shopInfo.Nature == 0)
                        {
                            if (requestData.FirstStep.ShopCompanyName.IsNullOrWhiteSpace())
                            {
                                throw new CustomException("门店公司名称不能为空");
                            }
                        }
                        requestData.FirstStep.DoorHeaderImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.DoorHeader.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });

                        });
                        requestData.FirstStep?.FrontImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.Front.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        requestData.FirstStep?.EnvironmentImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.Environment.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        requestData.FirstStep?.ManagementLicenseImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.ManagementLicense.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        requestData.FirstStep?.OtherLicenseImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.OtherLicense.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        requestData.FirstStep?.BusinessLicenseImgs?.ForEach(_ =>
                        {
                            shopImgDOs.Add(new ShopImgDO()
                            {
                                ImgUrl = _.Url.Substring(_.Url.IndexOf(".cn/") + 4, _.Url.Length - _.Url.IndexOf(".cn/") - 4),
                                ShopId = requestData.ShopId,
                                Type = (sbyte)ShopImgTypeEnum.BusinessLicense.ToInt(),
                                CreateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                UpdateBy = requestData.FirstStep?.UpdateBy ?? string.Empty,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        if (!string.IsNullOrWhiteSpace(requestData.FirstStep.TagNameTwo))
                        {
                            requestData.FirstStep.TagNameOne = string.Join(",", requestData.FirstStep.TagNameOne, requestData.FirstStep.TagNameTwo);
                        }
                        if (!string.IsNullOrWhiteSpace(requestData.FirstStep.TagNameThree))
                        {
                            requestData.FirstStep.TagNameOne = string.Join(",", requestData.FirstStep.TagNameOne, requestData.FirstStep.TagNameThree);
                        }
                        if (shopImgDOs?.Count > 0)
                            await _shopImgRepository.InsertBatchAsync(shopImgDOs);

                        if (requestData.FirstStep.Head.Trim() == _configuration["ShopManageDefault:Name"])
                        {
                            requestData.FirstStep.HeadPhone = _configuration["ShopManageDefault:Tel"];
                        }

                        var updateShopBaseInfo = await _shopRepository.UpdateShopBaseInfoForOpeningGuide(requestData.FirstStep, requestData.ShopId, shopInfo.Nature);

                        //修改门店专修品牌
                        if (requestData.FirstStep.Brands != null && requestData.FirstStep.Brands.Any())
                        {
                            List<string> brands = requestData.FirstStep.Brands.Select(o => o.BrandSuffix).ToList();
                            await ModifyShopBrand(brands, requestData.ShopId, requestData.UpdateBy);
                        }
                    }
                }
                else if (requestData.SaveStep == OpeningGuideStepEnum.Second)
                {
                    var updateShopAddress = await _shopRepository.UpdateShopAddressInfoForOpeningGuide(requestData.SecondStep, requestData.ShopId);
                }
                else if (requestData.SaveStep == OpeningGuideStepEnum.Third)
                {
                    if (shopInfo.Nature == 0)
                    {
                        if (requestData.ThirdStep.StartWorkTime.IsNullOrWhiteSpace())
                        {
                            throw new CustomException("营业开始时间不能为空");
                        }
                        if (requestData.ThirdStep.EndWorkTime.IsNullOrWhiteSpace())
                        {
                            throw new CustomException("营业结束时间不能为空");
                        }
                    }
                    var configRequest = new ModifyShopConfigInfoRequest() { Type = 1 };
                    var shopConfig = await _shopConfigRepository.GetShopConfigByShopIdAsync(requestData.ShopId);
                    LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
                    if (shopConfig != null)
                    {
                        var shopConfigRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                        {
                            Identifier = shopConfig?.Id.ToString(),
                            CreateBy = requestData.UpdateBy,
                            UpdateBy = requestData.UpdateBy,
                            Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.ModifyShopConfigInfo.GetEnumDescription(),
                            Filter1 = requestData.ShopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                        });
                        if (shopInfo.Nature == 0)
                        {
                            //营业时间
                            configRequest.StartWorkTime = DateTime.Parse("1900-01-01 " + requestData.ThirdStep.StartWorkTime);
                            configRequest.EndWorkTime = DateTime.Parse("1900-01-01 " + requestData.ThirdStep.EndWorkTime);
                        }
                        configRequest.UpdateBy = requestData.UpdateBy;
                        configRequest.ShopId = requestData.ShopId;
                        if (requestData.ThirdStep.Items != null && requestData.ThirdStep.Items.Any())
                        {
                            configRequest.IsLoungeRoom = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(0).IsConfigured);
                            configRequest.IsRestRoom = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(1).IsConfigured);
                            configRequest.IsFreeWifi = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(2).IsConfigured);
                            configRequest.IsPostLift = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(3).IsConfigured);
                            configRequest.CarFaultDiagnosticTool = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(4).IsConfigured);
                        }
                        var updateShopConfig = await shopConfigRepository.ModifyShopConfigInfoAsync(configRequest);
                    }
                    else
                    {
                        var shopConfigDo = new ShopConfigDO();
                        shopConfigDo.CreateTime = DateTime.Now;
                        shopConfigDo.ShopId = requestData.ShopId;
                        //营业时间
                        shopConfigDo.StartWorkTime = DateTime.Parse("1900-01-01 " + requestData.ThirdStep.StartWorkTime);
                        shopConfigDo.EndWorkTime = DateTime.Parse("1900-01-01 " + requestData.ThirdStep.EndWorkTime);
                        var shopConfigRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                        {
                            Identifier = string.Empty,
                            CreateBy = requestData.UpdateBy,
                            UpdateBy = string.Empty,
                            Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                            Type2 = LoggerTypeTwoEnum.ShopConfigCreate.GetEnumDescription(),
                            Filter1 = requestData.ShopId.ToString(),
                            Filter2 = string.Empty,
                            Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                        });
                        if (requestData.ThirdStep.Items != null && requestData.ThirdStep.Items.Any())
                        {
                            shopConfigDo.IsLoungeRoom = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(0).IsConfigured);
                            shopConfigDo.IsRestRoom = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(1).IsConfigured);
                            shopConfigDo.IsFreeWifi = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(2).IsConfigured);
                            shopConfigDo.IsPostLift = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(3).IsConfigured);
                            shopConfigDo.CarFaultDiagnosticTool = Convert.ToSByte(requestData.ThirdStep.Items.ElementAtOrDefault(4).IsConfigured);
                        }
                        var addShopConfig = await shopConfigRepository.AddAsync(shopConfigDo);
                    }
                }
                else if (requestData.SaveStep == OpeningGuideStepEnum.Fourth)
                {
                    var shopBankInfo = requestData.FourthStep;
                    var bankAccount = new ModifyShopBankAccountRequest()
                    {
                        OpeningBank = shopBankInfo.OpeningBank,
                        OpeningUserName = shopBankInfo.OpeningUserName,
                        BankCardNo = shopBankInfo.BankCardNo,
                        ShopId = requestData.ShopId,
                        UpdateBy = requestData.UpdateBy,
                        Source = 1
                    };
                    var isSuccess = ModifyShopBankAccountAsync(bankAccount);

                    var shopDo = new ShopDO()
                    {
                        Id = requestData.ShopId,
                        UpdateBy = requestData.UpdateBy,
                        IdCardFront = shopBankInfo.IdCardFront,
                        IdCardBack = shopBankInfo.IdCardBack
                    };
                    var modifyShopIDCard = ModifyShopIDCard(shopDo);
                    Task.WaitAll(isSuccess, modifyShopIDCard);
                }
                else if (requestData.SaveStep == OpeningGuideStepEnum.Fifth)
                {
                    //await _shopRepository.UpdateExaminedStatus(new UpdateExaminedStatusRequest
                    //{
                    //    ShopId = requestData.ShopId,
                    //    UpdateBy = requestData.UpdateBy,
                    //    CheckStatus = 0
                    //});
                }

                ts.Complete();

                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "保存成功"
                };
            }
        }

        public async Task<ApiResult<string>> UpdateExaminedStatus(UpdateExaminedStatusRequest request)
        {
            request.UpdateBy = this.identityService.GetUserName();
            var res = new ApiResult<string>();
            try
            {
                await _shopRepository.UpdateExaminedStatus(request);
                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdateExaminedStatus_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

        public async Task<ApiResult<List<string>>> ReGenerateAppletCode(GenerateAppletCodeRequest request)
        {
            var res = new ApiResult<List<string>>();
            try
            {
                var codeList = new List<string>();
                var result = new ApiResult<string>();
                try
                {
                    //生成快速排队二维码
                    result = await GenerateShopAppletCode(request.ShopId, "pages/fastQueuing/main");
                    if (result.Code == ResultCode.Success)
                    {
                        await _shopRepository.UpdateQuickQueueAppletCode(new ShopDO
                        {
                            UpdateBy = this.identityService.GetUserName(),
                            AppletCode = result.Data,
                            Id = request.ShopId
                        });
                    }
                    codeList.Add(_configuration["QiNiuImageDomain"] + result.Data);
                }
                catch (Exception ex)
                {
                    _logger.Error($"ReGenerateAppletCodeQuickQueue_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                }

                //生成门店二维码
                result = await GenerateShopAppletCode(request.ShopId, "pages/storeDetails/main");
                if (result.Code == ResultCode.Success)
                {
                    await _shopRepository.UpdateShopAppletCode(new ShopDO
                    {
                        UpdateBy = this.identityService.GetUserName(),
                        ShopAppletCode = result.Data,
                        Id = request.ShopId
                    });
                }
                codeList.Add(_configuration["QiNiuImageDomain"] + result.Data);
                res.Data = codeList;
                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"ReGenerateAppletCode_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
        /// <summary>
        /// 修改门店上下架状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateShopOnline(UpdateOnlineRequest request)
        {
            request.UpdateBy = identityService.GetUserName();
            var res = new ApiResult<string>();
            try
            {
                await _shopRepository.UpdateShopOnline(request);
                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdateShopOnline_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
        /// <summary>
        /// 修改门店老板身份证图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> ModifyShopIDCard(ShopDO request)
        {
            LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorShopHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
            var shopRepository = logInterceptorShopHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
            {
                Identifier = request.Id.ToString(),
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                Type2 = LoggerTypeTwoEnum.ModifyShopIDCard.GetEnumDescription(),
                Filter1 = request.Id.ToString(),
                Filter2 = string.Empty,
                Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
            });
            return await shopRepository.UpdateShopIDCard(request);
        }

        /// <summary>
        /// 获取门店审核信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopCheckInfoResponse>> GetShopCheckInfo(BaseGetShopRequest request)
        {
            var result = Result.Failed<GetShopCheckInfoResponse>("获取门店审核信息异常");
            var shopInfo = await _shopRepository.GetShopAsync(request.ShopId);
            if (shopInfo != null)
            {
                var response = new GetShopCheckInfoResponse()
                {
                    CheckStatus = shopInfo.CheckStatus,
                    FailedExaminedReason = shopInfo.FailedExaminedReason
                };
                result = Result.Success(response, "注册成功");
            }
            return result;
        }

        /// <summary>
        /// 查询银行列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankInformationDTO>> GetBankListAsync()
        {
            var items = await _bankInformationRepository.GetBankListAsync();
            return _mapper.Map<List<BankInformationDTO>>(items);
        }


        /// <summary>
        /// 根据服务PID查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopSimpleInfoForBOSSDTO>> GetShopListForServiceAsync(GetShopListForServiceRequest request)
        {
            //获取地址坐标
            var coordinate = await _basicDataClient.GetCoordinate(new GetCoordinateClientRequest { Address = request.Address });
            if (coordinate != null)
            {
                request.DistrictId = coordinate.DistrictId;
                request.Longitude = coordinate.Longitude;
                request.Latitude = coordinate.Latitude;
            }
            //门店信息列表
            var result = await _shopRepository.GetShopListForServiceAsync(request);
            return new ApiPagedResultData<ShopSimpleInfoForBOSSDTO>()
            {
                Items = _mapper.Map<List<ShopSimpleInfoForBOSSDTO>>(result.Items),
                TotalItems = result.TotalItems
            };
        }

        /// <summary>
        /// 门店列表查询（平台先生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopListPageResponse> GetShopListPage(ShopListPageRequest request)
        {
            var shopInfo = await _shopRepository.GetAsync<ShopDO>(request.ShopId);
            if (shopInfo == null)
            {
                throw new CustomException("当前门店不存在");
            }

            var serviceTypeEnumTask = _baoYangClient.GetServiceTypeEnum();

            var result = await GetShopListForType(request, shopInfo);

            List<ShopListPageVo> data = result.Select(_ => new ShopListPageVo
            {
                Id = _.Id,
                SimpleName = _.SimpleName,
                Status = _.Status,
                Telephone = _.Telephone,
                Address = $"{_.Province}{_.City}{_.District}{_.Address}",
                Distance = GetDistance(shopInfo.Latitude, shopInfo.Longitude, _.Latitude, _.Longitude),
                Score = (decimal)5.00,
                Description = _.Description
            }).OrderBy(_ => _.Distance).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .ToList();

            List<long> shopIdList = data.Select(_ => _.Id).ToList();
            var shopImageTask = _shopImgRepository.GetShopImagesByType(shopIdList, 1);
            //查询门店订单数量
            var shopOrderCountListTask =
                _orderClient.BatchGetOrderCountByShopId(new GetOrderCountByShopIdClientRequest()
                { ShopIds = shopIdList });
            await Task.WhenAll(shopImageTask, shopOrderCountListTask);
            var shopImage = shopImageTask.Result ?? new List<ShopImgDO>();
            var shopOrderCountList = shopOrderCountListTask.Result ?? new List<GetOrderCountByShopIdClientResponse>();
            data.ForEach(_ =>
            {
                var defaultImg = shopImage.FirstOrDefault(t => t.ShopId == _.Id)?.ImgUrl;
                string image = !string.IsNullOrWhiteSpace(defaultImg)
                    ? defaultImg
                    : "shopProductImage/202002251011076245.PNG";
                _.Img = $"{_configuration["QiNiuImageDomain"]}{image}";
                _.TotalOrder = shopOrderCountList.FirstOrDefault(t => t.ShopId == _.Id)?.OrderCount ?? 0;
            });

            var serviceTypeEnum = await serviceTypeEnumTask ?? new List<ServiceTypeDTO>();
            serviceTypeEnum.Insert(0, new ServiceTypeDTO()
            {
                ServiceType = "",
                DisplayName = "全部"
            });

            return new ShopListPageResponse()
            {
                ShopRegion = new ShopServiceRegion()
                {
                    ProvinceId = shopInfo.ProvinceId,
                    Province = shopInfo.Province,
                    CityId = shopInfo.CityId,
                    City = shopInfo.City,
                    DistrictId = shopInfo.DistrictId,
                    District = shopInfo.District
                },
                ShopList = new ApiPagedResultData<ShopListPageVo>()
                {
                    Items = data,
                    TotalItems = result.Count
                },
                ServiceType = serviceTypeEnum.Select(_ => new ServiceTypeVo
                {
                    ServiceType = _.ServiceType,
                    DisplayName = _.DisplayName
                }).ToList()
            };
        }

        /// <summary>
        /// 平台先生门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopDetailForShopResponse> GetShopDetailForShop(ShopDetailForShopRequest request)
        {
            var shopTask = _shopRepository.GetAsync<ShopDO>(request.ShopId);
            var currentShopTask = _shopRepository.GetAsync<ShopDO>(request.CurrentShopId);
            var shopImgTask = _shopImgRepository.GetImgsByShopIdAsync(request.ShopId);
            var shopConfigTask = _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
            var serviceTypeEnumTask = _baoYangClient.GetServiceTypeEnum();
            var shopServiceTypeTask = _shopServiceTypeRepository.GetShopServiceTypeListAsync(request.ShopId);
            var shopOrderCountTask = _orderClient.BatchGetOrderCountByShopId(new GetOrderCountByShopIdClientRequest()
            {
                ShopIds = new List<long>()
                {
                    request.ShopId
                }
            });
            await Task.WhenAll(shopTask, currentShopTask, shopImgTask, shopConfigTask, shopOrderCountTask,
                serviceTypeEnumTask, shopServiceTypeTask);
            var shop = shopTask.Result;
            var currentShop = currentShopTask.Result;
            var shopImg = shopImgTask.Result ?? new List<ShopImgDO>();
            var shopConfig = shopConfigTask.Result;
            var serviceTypeEnum = serviceTypeEnumTask.Result ?? new List<ServiceTypeDTO>();
            var shopServiceType = shopServiceTypeTask.Result ?? new List<ShopServiceTypeDO>();
            var shopOrderCount = shopOrderCountTask.Result ?? new List<GetOrderCountByShopIdClientResponse>();
            if (shop == null || currentShop == null)
            {
                throw new CustomException("门店Id输入有误");
            }

            List<int> type = new List<int>() { 1, 2, 4 };
            List<string> url = shopImg.Where(_ => type.Contains(_.Type)).Select(_ => Domain + _.ImgUrl).ToList();
            if (!url.Any())
            {
                url.Add(Domain + "shopProductImage/202002251011076245.PNG");
            }

            List<ServiceTypeVo> serviceType = new List<ServiceTypeVo>();
            serviceTypeEnum.ForEach(_ =>
            {
                var defaultType = shopServiceType.FirstOrDefault(x => x.ServiceType == _.ServiceType);
                if (defaultType != null)
                {
                    serviceType.Add(new ServiceTypeVo()
                    {
                        CategoryId = _.Id,
                        ServiceType = _.ServiceType,
                        DisplayName = _.DisplayName,
                        OrderType = _.Id
                    });
                }
            });

            var baoYangType = serviceType?.Where(_ => _.ServiceType == "BaoYang")?.FirstOrDefault();
            var tireType = serviceType?.Where(_ => _.ServiceType == "Tire")?.FirstOrDefault();
            var tireTypeId = tireType?.CategoryId;
            var baoYangTypeId = baoYangType?.CategoryId;
            if (baoYangType != null && tireType != null)
            {
                baoYangType.OrderType = tireTypeId ?? 0;
                tireType.OrderType = baoYangTypeId ?? 0;
            }


            ShopDetailForShopResponse result = new ShopDetailForShopResponse()
            {
                Id = shop.Id,
                SimpleName = shop.SimpleName,
                Status = shop.Status,
                Province = shop.Province,
                City = shop.City,
                District = shop.District,
                Address = shop.Address,
                Telephone = shop.Telephone,
                Score = (decimal)5.00,
                TotalOrder = shopOrderCount.FirstOrDefault(_ => _.ShopId == request.ShopId)?.OrderCount ?? 0,
                Distance = GetDistance(currentShop.Latitude, currentShop.Longitude, shop.Latitude, shop.Longitude),
                Longitude = shop.Longitude,
                Latitude = shop.Latitude,
                Description = shop.Description,
                Imgs = url,
                WorkTime = shopConfig.StartWorkTime.ToShortTimeString() + "-" +
                           shopConfig.EndWorkTime.ToShortTimeString(),
                TagNames = shop.TagName?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)?.ToList() ??
                           new List<string>(),
                Type = shop.Type,
                DepositAmount = shop.DepositAmount,
                ServiceType = serviceType
            };

            return result;
        }

        private async Task<List<ShopDO>> GetShopListForType(ShopListPageRequest request, ShopDO shopInfo)
        {
            List<ShopDO> result = new List<ShopDO>();
            string cityId = request.CityId;
            string districtId = request.DistrictId;

            if (string.IsNullOrEmpty(cityId) || string.IsNullOrEmpty(districtId))
            {
                cityId = shopInfo.CityId;
                districtId = shopInfo.DistrictId;
            }
            var shopList = await _shopRepository.GetShopsByType(16); //平台先生

            if (shopList.Any())
            {
                var provinceId =
                    await _basicDataClient.GetParentRegionId(new ParentRegionIdClientRequest() { RegionId = cityId });

                var shopIds = shopList.Select(_ => _.Id).ToList();
                var shopAreaList = await _shopServiceAreaRepository.GetShopServiceAreaByShopIds(shopIds);
                shopAreaList = shopAreaList.Where(_ => _.Type == 0).ToList();
                var configShopId = shopAreaList.Select(_ => _.ShopId).Distinct().ToList();

                var config = shopAreaList.Where(_ =>
                    _.ProvinceId == provinceId && (_.CityId == cityId || string.IsNullOrEmpty(_.CityId))).ToList();

                var notConfig = shopList.Where(_ => !configShopId.Contains(_.Id)).ToList();
                notConfig = notConfig.Where(_ => _.CityId == cityId).ToList();
                if (districtId != "0")
                {
                    config = config.Where(_ => _.DistrictId == districtId || string.IsNullOrEmpty(_.DistrictId))
                        .ToList();
                    notConfig = notConfig.Where(_ => _.DistrictId == districtId).ToList();
                }

                var shopId1 = config.Select(_ => _.ShopId).Distinct().ToList();
                var shopId2 = notConfig.Select(_ => _.Id).ToList();
                shopId1.AddRange(shopId2);
                //需要将全国的记录也拿出来
                var allConfigs = shopAreaList?.Where(r => r.CityId == "100100" && !r.IsDeleted)?.Select(r => r.ShopId).Distinct()?.ToList();
                if (allConfigs != null && allConfigs.Any())
                {
                    shopId1.AddRange(allConfigs);
                }

                if (!string.IsNullOrEmpty(request.ServiceType))
                {
                    var serviceType = await _shopServiceTypeRepository.GetServiceType(shopId1, request.ServiceType);

                    shopId1 = serviceType.Select(_ => _.ShopId).ToList();
                }

                result = shopList.Where(_ => shopId1.Contains(_.Id)).ToList();

            }
            return result;
        }

        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        private static decimal GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result =
                2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                                        Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) *
                EARTH_RADIUS;
            return (decimal)Math.Round(result / 1000, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        #region ServiceArea

        /// <summary>
        /// 更新门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateShopServiceArea(UpdateShopServiceAreaRequest request)
        {
            List<ShopServiceRegion> regionIdList = request.Regions ?? new List<ShopServiceRegion>();

            using (TransactionScope ts = new TransactionScope())
            {
                await _shopServiceAreaRepository.DeleteShopServiceAreaByShopId(request.ShopId, 0, request.SubmitBy);
                if (regionIdList.Any())
                {
                    List<ShopServiceAreaDO> batchData = regionIdList.Select(_ => new ShopServiceAreaDO
                    {
                        ShopId = request.ShopId,
                        Type = 0,
                        ProvinceId = _.ProvinceId,
                        Province = _.Province,
                        CityId = _.CityId,
                        City = _.City,
                        DistrictId = _.DistrictId,
                        District = _.District,
                        CreateBy = request.SubmitBy,
                        CreateTime = DateTime.Now
                    }).ToList();
                    await _shopServiceAreaRepository.InsertBatchAsync(batchData);
                }

                ts.Complete();
            }

            return true;
        }

        private List<ShopServiceAreaDO> TransformRegion(List<ProvinceRequest> provinces)
        {
            List<ShopServiceAreaDO> result = new List<ShopServiceAreaDO>();
            if (provinces != null && provinces.Any())
            {
                foreach (var _ in provinces)
                {
                    if (_.Cities != null && _.Cities.Any())
                    {
                        foreach (var x in _.Cities)
                        {
                            if (x.Districts != null && x.Districts.Any())
                            {
                                foreach (var f in x.Districts)
                                {
                                    result.Add(new ShopServiceAreaDO()
                                    {
                                        ProvinceId = _.ProvinceId,
                                        Province = _.Province,
                                        CityId = x.CityId,
                                        City = x.City,
                                        DistrictId = f.DistrictId,
                                        District = f.District
                                    });
                                }
                            }
                            else
                            {
                                result.Add(new ShopServiceAreaDO()
                                {
                                    ProvinceId = _.ProvinceId,
                                    Province = _.Province,
                                    CityId = x.CityId,
                                    City = x.City
                                });
                            }
                        }
                    }
                    else
                    {
                        result.Add(new ShopServiceAreaDO()
                        {
                            ProvinceId = _.ProvinceId,
                            Province = _.Province
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopServiceAreaResponse> GetShopServiceArea(ShopServiceAreaRequest request)
        {
            var serviceArea = await _shopServiceAreaRepository.GetShopServiceAreaByShopId(request.ShopId);
            serviceArea = serviceArea.Where(_ => _.Type == 0).ToList();
            return new ShopServiceAreaResponse()
            {
                Regions = serviceArea.Select(_ => new ShopServiceRegion
                {
                    ProvinceId = _.ProvinceId,
                    Province = _.Province,
                    CityId = _.CityId,
                    City = _.City,
                    DistrictId = _.DistrictId,
                    District = _.District
                }).ToList()
            };
        }

        #endregion

        #region  private 内部调用


        /// <summary>
        /// 获取门店类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ShopTypeDTO> GetShopTypes(int type)
        {
            List<ShopTypeDTO> shopTypes = new List<ShopTypeDTO>();
            ShopTypeDTO stCoop = new ShopTypeDTO();
            ShopTypeDTO stDirOp = new ShopTypeDTO();

            ShopTypeEnum shoptype = (ShopTypeEnum)type;
            stCoop.Code = shoptype.ToString();
            stCoop.Name = CommonHelper.GetDescriptionByEnum(shoptype);
            shopTypes.Add(stCoop);

            //if ((type & 1) == 1)
            //{
            //    stCoop.Code = ShopTypeEnum.CooperativeStore.ToString();
            //    stCoop.Name = CommonHelper.GetDescriptionByEnum(ShopTypeEnum.CooperativeStore);
            //    shopTypes.Add(stCoop);
            //}
            //if ((type & 2) == 2)
            //{
            //    stDirOp.Code = ShopTypeEnum.DirectlyOperatedStore.ToString();
            //    stDirOp.Name = CommonHelper.GetDescriptionByEnum(ShopTypeEnum.DirectlyOperatedStore);
            //    shopTypes.Add(stDirOp);
            //}
            //if ((type & 4) == 4)
            //{
            //    stDirOp.Code = ShopTypeEnum.UpperDoorMaintain.ToString();
            //    stDirOp.Name = CommonHelper.GetDescriptionByEnum(ShopTypeEnum.UpperDoorMaintain);
            //    shopTypes.Add(stDirOp);
            //}
            //if ((type & 8) == 8)
            //{
            //    stDirOp.Code = ShopTypeEnum.UpperDoorMaintain.ToString();
            //    stDirOp.Name = CommonHelper.GetDescriptionByEnum(ShopTypeEnum.UpperDoorMaintain);
            //    shopTypes.Add(stDirOp);
            //}

            return shopTypes;
        }
        private string GetShopTypeStr(int type)
        {
            string shopTypes = string.Empty;
            if(type == -1)
            {
                return CommonHelper.GetDescriptionByEnum(ShopTypeEnum.Warehouse);
            }
            if ((type & 1) == 1)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.CooperativeStore) + "/";
            }
            if ((type & 2) == 2)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.DirectlyOperatedStore) + "/";
            }
            if ((type & 4) == 4)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.UpperDoorMaintain) + "/";
            }

            if ((type & 8) == 8)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.CertificationStore) + "/";
            }

            if ((type & 16) == 16)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.ApolloErpMen) + "/";
            }

            if ((type & 32) == 32)
            {
                shopTypes += CommonHelper.GetDescriptionByEnum(ShopTypeEnum.FrontWarehouse) + "/";
            }

            if (shopTypes.EndsWith("/"))
            {
                shopTypes = shopTypes.Substring(0, shopTypes.Length - 1);
            }

            return shopTypes;
        }


        /// <summary>
        /// 修改门店专修品牌
        /// </summary>
        /// <param name="brandNames"></param>
        /// <param name="shopId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        private async Task<int> ModifyShopBrand(List<string> brandNames, long shopId, string updateBy)
        {
            #region 更新专修品牌记录
            int id = 0;
            //需要判断哪些是新添加  哪些是要删除的记录
            if (brandNames.Any())
            {
                //清除原来的记录
                await _shopServiceRepository.DeleteShopServiceBrand(new ShopServiceBrandDO
                {
                    ShopId = shopId,
                    UpdateBy = updateBy
                });

                //新增选中的记录
                var brandRes = await _vehicleClient.GetVehicleBrandList();
                if (brandRes.Any())
                {
                    var brands = brandNames;
                    foreach (var item in brands)
                    {
                        var vehicleBrand = brandRes.FirstOrDefault(r => r.BrandSuffix == item);
                        var brandDto = new ShopServiceBrandDO
                        {
                            ShopId = shopId,
                            CreateBy = updateBy,
                            CreateTime = DateTime.Now,
                            IsDeleted = 0,
                            Brand = item,
                            BrandUrl = vehicleBrand != null ? vehicleBrand.BrandUrl : string.Empty
                        };
                        id = await _shopServiceRepository.CreateShopServiceBrand(brandDto);
                    }
                }
            }
            else
            {
                //清除原来的记录
                id = await _shopServiceRepository.DeleteShopServiceBrand(new ShopServiceBrandDO
                {
                    ShopId = shopId,
                    UpdateBy = updateBy
                });
            }
            return id;
            #endregion
        }

        /// <summary>
        /// 修改门店配置信息
        /// </summary>
        /// <param name="shopConfigDTO"></param>
        /// <returns></returns>
        private async Task<bool> ModifyShopConfigAsyne(ShopConfigDTO shopConfigDTO)
        {
            var shopConfig = await _shopConfigRepository.GetShopConfigByShopIdAsync(shopConfigDTO.ShopId);
            if (shopConfig != null)
            {
                var shopConfigModify = _mapper.Map<ModifyShopConfigInfoRequest>(shopConfigDTO);
                LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterConfig = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
                var shopConfigRepository = logInterConfig.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                {
                    Identifier = shopConfigDTO.ShopId.ToString(),
                    CreateBy = shopConfigDTO.UpdateBy,
                    UpdateBy = shopConfigDTO.UpdateBy,
                    Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                    Type2 = LoggerTypeTwoEnum.ModifyShopConfigInfo.GetEnumDescription(),
                    Filter1 = shopConfigDTO.ShopId.ToString(),
                    Filter2 = string.Empty,
                    Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                });
                return await shopConfigRepository.ModifyShopConfigInfoAsync(shopConfigModify);
            }
            else
            {
                //新增门店配置
                DateTime dt = DateTime.Now;
                //营业时间
                ShopConfigDO shopConfigDo = _mapper.Map<ShopConfigDO>(shopConfigDTO);
                shopConfigDo.CreateTime = dt;
                LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorShopConfigHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
                var shopConfigRepository = logInterceptorShopConfigHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                {
                    Identifier = string.Empty,
                    CreateBy = shopConfigDTO.CreateBy,
                    UpdateBy = shopConfigDTO.CreateBy,
                    Type1 = LoggerTypeOneEnum.ShopCreate.GetEnumDescription(),
                    Type2 = LoggerTypeTwoEnum.ShopConfigCreate.GetEnumDescription(),
                    Filter1 = shopConfigDTO.ShopId.ToString(),
                    Filter2 = string.Empty,
                    Summary = LogTypeSummaryEnum.CreateShop.GetEnumDescription()
                });
                var id = await shopConfigRepository.AddAsync(shopConfigDo);
                return id > 0;
            }

        }

        /// <summary>
        /// 得到门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetNearCityShopInfoResponse>> GetNearCityShopInfo(GetNearCityShopInfoRequest request)
        {

            var shops = await _shopRepository.GetNearCityShopInfo(request);


            List<GetNearCityShopInfoResponse> data = _mapper.Map<List<GetNearCityShopInfoResponse>>(shops.Items);
            if (data != null && data.Any())
            {
                var shopImgs = await _shopImgRepository.GetListAsync("Where shop_id in @ShopIds and type=1", new { ShopIds = data.Select(_ => _.Id)?.ToList() });
                var shopImg = string.Empty;
                data?.ToList()?.ForEach(_ =>
                {
                    shopImg = shopImgs?.Where(item => item.ShopId == _.Id)?.FirstOrDefault()?.ImgUrl;
                    _.Img = !string.IsNullOrWhiteSpace(shopImg) ? shopImg : "shopProductImage/202002251011076245.PNG";
                });
            }
            return new ApiPagedResult<GetNearCityShopInfoResponse>()
            {
                Code = ResultCode.Success,
                Data = new ApiPagedResultData<GetNearCityShopInfoResponse>()
                {
                    Items = data,
                    TotalItems = shops.TotalItems
                }
            };

        }

        #endregion

        public async Task<ApiResult> UpdateShopDeposit(UpdateShopDepositRequest request)
        {
            var data = await _shopRepository.UpdateShopDeposit(request);
            if (data)
                return Result.Success();
            return Result.Failed();
        }

        public async Task<ApiResult<bool>> ModifyShopInfoAsync(AddShopRequest request)
        {
            var result = Result.Failed<bool>("修改门店信息异常");
            if (request.Shop == null)
            {
                return result;
            }
            //验证门店是否存在
            var ShopInfo = await _shopRepository.GetShopAsync(request.Shop.Id);
            if (ShopInfo == null)
            {
                return result;
            }
            var dt = DateTime.Now;
            request.Shop.TagName = request.Shop.TagNames.Any() ? string.Join(",", request.Shop.TagNames) : string.Empty;
            ShopDO shop = _mapper.Map<ShopDO>(request.Shop);
            int num = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    #region  修改门店信息

                    //修改门店信息
                    shop.UpdateTime = dt;
                    shop.UpdateBy = request.UserId;
                    LogInterceptorHelpher<IShopRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopRepository, ShopLogDO>();
                    var shopRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopRepository, new ShopLogDO()
                    {
                        Identifier = shop.Id.ToString(),
                        CreateBy = shop.UpdateBy,
                        UpdateBy = shop.UpdateBy,
                        Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                        Type2 = LoggerTypeTwoEnum.ModifyShopBaseInfo.GetEnumDescription(),
                        Filter1 = shop.Id.ToString(),
                        Filter2 = string.Empty,
                        Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                    });
                    num = await shopRepository.UpdateShopInfoAsync(shop);

                    #endregion

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new ApiResult<bool>
            {
                Data = true,
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };
        }

        public async Task<List<ShopGrouponProductDTO>> GetShopGrouponProduct(GetShopGrouponProductRequest request)
        {
            var getShopGroupProduct = await _shopGrouponProductRepository.GetShopGrouponProduct((int)request.ShopId, request.Status);

            List<ShopGrouponProductDTO> data = _mapper.Map<List<ShopGrouponProductDTO>>(getShopGroupProduct);

            return data;
        }
    }

}
