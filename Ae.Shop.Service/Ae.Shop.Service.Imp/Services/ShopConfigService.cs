using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Common.Helper;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class ShopConfigService : IShopConfigService
    {
        private readonly IShopConfigRepository _shopConfigRepository;
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<ShopConfigService> _logger;
        private readonly IIdentityService identityService;

        public ShopConfigService(IMapper mapper,
            IShopConfigRepository shopConfigRepository, ApolloErpLogger<ShopConfigService> logger,
            IIdentityService identityService
            )
        {
            _mapper = mapper;
            _shopConfigRepository = shopConfigRepository;
            this._logger = logger;
            this.identityService = identityService;
        }


        /// <summary>
        /// 修改门店配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopConfigInfoAsync(ModifyShopConfigInfoRequest request)
        {
            //营业时间
            request.StartWorkTime = DateTime.Parse("1900-01-01 " + request.StartWorkTime.ToString("HH:mm"));
            request.EndWorkTime = DateTime.Parse("1900-01-01 " + request.EndWorkTime.ToString("HH:mm"));
            request.UpdateBy = identityService.GetUserName();

            LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
            var shopConfigRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
            {
                Identifier = string.Empty,
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                Type2 = LoggerTypeTwoEnum.ModifyShopConfigInfo.GetEnumDescription(),
                Filter1 = request.ShopId.ToString(),
                Filter2 = string.Empty,
                Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
            });

            var shopConfig =await _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
            if (shopConfig != null)
            {
                var result = await shopConfigRepository.ModifyShopConfigInfoAsync(request);
                return result;
            }
            else
            {
                //新增门店配置
                DateTime dt = DateTime.Now;
                //创建时间
                ShopConfigDO shopConfigDo = _mapper.Map<ShopConfigDO>(request);
                shopConfigDo.CreateTime = dt;
                shopConfigDo.CreateBy = request.UpdateBy;

                var id = await shopConfigRepository.AddAsync(shopConfigDo);
                return id > 0;
            }
        }
        /// <summary>
        /// 修改门店配置服务费用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopConfigExpenseAsync(ModifyShopConfigExpenseRequest request)
        {
            request.UpdateBy = identityService.GetUserName();

            LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
            var shopConfigRepository = logInterceptorHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
            {
                Identifier = string.Empty,
                CreateBy = request.UpdateBy,
                UpdateBy = request.UpdateBy,
                Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                Type2 = LoggerTypeTwoEnum.ModifyShopConfigExpense.GetEnumDescription(),
                Filter1 = request.ShopId.ToString(),
                Filter2 = string.Empty,
                Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
            });

            var shopConfig = await _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
            if (shopConfig != null)
            {                      
                var result = await shopConfigRepository.ModifyShopConfigExpenseAsync(request);
                return result;
            }
            else
            {
                //新增门店配置
                DateTime dt = DateTime.Now;
                //新增时间
                ShopConfigDO shopConfigDo = _mapper.Map<ShopConfigDO>(request);
                shopConfigDo.CreateTime = dt;
                shopConfigDo.CreateBy = request.UpdateBy;

                var id = await shopConfigRepository.AddAsync(shopConfigDo);
                return id > 0;
            }
        }

        public async Task<ApiResult<string>> ModifyShopSuspendTime(ModifyShopSuspendTimeRequest request)
        {
            request.UdpateBy = identityService.GetUserName();
            var res = new ApiResult<string>();
            try
            {
                if (!request.Times.Any())
                {
                    res.Code = ResultCode.Exception;
                    res.Message = "请选择暂停营业时间!";
                    return res;
                }

                var shopConfig = await _shopConfigRepository.GetShopConfigByShopIdAsync(request.ShopId);
                if (shopConfig == null)
                {
                    var shopConfigDo = new ShopConfigDO()
                    {
                        ShopId = request.ShopId,
                        CreateBy = request.UdpateBy,
                        CreateTime = DateTime.Now
                    };

                    LogInterceptorHelpher<IShopConfigRepository, ShopLogDO> logInterceptorShopConfigHelpher = new LogInterceptorHelpher<IShopConfigRepository, ShopLogDO>();
                    var shopConfigRepository = logInterceptorShopConfigHelpher.CreateInterfaceProxyWithTarget(_shopConfigRepository, new ShopLogDO()
                    {
                        Identifier = string.Empty,
                        CreateBy = shopConfigDo.CreateBy,
                        UpdateBy = shopConfigDo.CreateBy,
                        Type1 = LoggerTypeOneEnum.ShopEdit.GetEnumDescription(),
                        Type2 = LoggerTypeTwoEnum.ModifyShopConfigExpense.GetEnumDescription(),
                        Filter1 = request.ShopId.ToString(),
                        Filter2 = string.Empty,
                        Summary = LogTypeSummaryEnum.EditShop.GetEnumDescription()
                    });
                    await shopConfigRepository.AddAsync(shopConfigDo);
                }
                    
                await _shopConfigRepository.ModifyShopSuspendTime(request);
                res.Code = ResultCode.Success;
                res.Message = "操作成功!";
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"ModifyShopSuspendTime_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }
    }
}
