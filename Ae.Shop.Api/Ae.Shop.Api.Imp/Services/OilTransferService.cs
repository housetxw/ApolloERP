using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class OilTransferService : IOilTransferService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<TransferTaskService> _logger;
        private readonly IIdentityService _identityService;
        private readonly IShopRepository _shopRepository;
        private readonly IStockManageService _stockManageService;

        public OilTransferService(IMapper _mapper, ApolloErpLogger<TransferTaskService> _logger,
            IIdentityService identityService, IShopRepository shopRepository,
            IStockManageService stockManageService)
        {
            this._mapper = _mapper;
            this._logger = _logger;
            this._identityService = identityService;
            this._shopRepository = shopRepository;
            this._stockManageService = stockManageService;
        }

        /// <summary>
        /// 扩展卖油 专用 
        /// 注意：返回库存数量  非可用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProductStockDTO>>> GetProductStocks(ProductStockDTO request)
        {
            var res = new ApiResult<List<ProductStockDTO>>();
            try
            {
                var productStocks = new List<ProductStockDTO>();
                //查询公司下每个门店的库存
                var shopRes = await _shopRepository.GetListAsync("where is_deleted=0 and check_status=2 and online =1 and status=0 and company_id =@company_id", new { company_id = request.LocationId });
                foreach (var item in shopRes)
                {
                    var productStocRes = await _stockManageService.GetAvailableBatchs(new GetAvailableBatchsRequest
                    {
                        ProductId = request.ProductId,
                        ShopId = item.Id
                    });
                    foreach (var productStock in productStocRes.Data.GroupBy(r => r.ProductId))
                    {
                        var shopInfo = productStock.FirstOrDefault();
                        var shop = await _shopRepository.GetAsync(shopInfo.ShopId);

                        productStocks.Add(new ProductStockDTO
                        {
                            ProductId = productStock.Key,
                            ProductName = shopInfo.ProductName,
                            LocationId = shopInfo.ShopId,
                            LocationName = shop.SimpleName,
                            Num = Convert.ToInt32(productStock.Sum(r => r.CanUseNum))  //  库存量  非可用！！！
                        });
                    }
                }
                res.Data  = productStocks;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetProductStocks_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }
    }
}
