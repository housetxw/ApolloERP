using AutoMapper;
using Ae.C.MiniApp.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ae.C.MiniApp.Api.Client.Interface;
using System.Threading.Tasks;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Response.Adaptation;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using Ae.C.MiniApp.Api.Client.Request.Adapter;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using VehicleClientRequest = Ae.C.MiniApp.Api.Client.Request.BaoYang.VehicleClientRequest;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class BaoYangService: IBaoYangService
    {
        private readonly IMapper _mapper;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IProductClient _productClient;
        private readonly string _channel = "MiniApp";
        private readonly IIdentityService _identityService;

        public BaoYangService(IMapper mapper, IBaoYangClient baoYangClient, IProductClient productClient,
            IIdentityService identityService)
        {
            _mapper = mapper;
            _baoYangClient = baoYangClient;
            _productClient = productClient;
            _identityService = identityService;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(GetBaoYangPackagesRequest request)
        {
            request.Channel = _channel;
            request.UserId = _identityService.GetUserId();
            var clientRequest = _mapper.Map<BaoYangPackagesClientRequest>(request);
            var result = await _baoYangClient.GetBaoYangPackagesAsync(clientRequest);

            var response = new ApiResult<List<BaoYangCategoryVO>>()
            {
                Code = result.Code,
                Message = result.Message
            };
            if (result.Data != null)
            {
                response.Data = _mapper.Map<List<BaoYangCategoryVO>>(result.Data);
            }

            return response;
        }

        /// <summary>
        /// 保养更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsWithCondition(
            SearchProductRequest request)
        {
            SearchProductModel<BaoYangPackageProductModel>
                result = new SearchProductModel<BaoYangPackageProductModel>();
            request.Channel = _channel;
            request.UserId = _identityService.GetUserId();
            var clientRequest = _mapper.Map<SearchProductClientRequest>(request);
            var products =
                await _baoYangClient.SearchPackageProductsWithConditionAsync(clientRequest);
            result.Products = _mapper.Map<List<BaoYangPackageProductModel>>(products);
            return result;
        }

        /// <summary>
        /// 轮胎服务页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetTireServiceListResponse> GetTireCategoryList(TireCategoryListRequest request)
        {
            request.UserId = _identityService.GetUserId();
            var clientRequest = _mapper.Map<TireCategoryListClientRequest>(request);
            var result = await _baoYangClient.GetTireCategoryListAsync(clientRequest);
            return _mapper.Map<GetTireServiceListResponse>(result);
        }

        /// <summary>
        /// 轮胎适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<TireProductVO>> GetTireListAsync(GetTireListRequest request)
        {
            request.UserId = _identityService.GetUserId();
            var clientRequest = _mapper.Map<TireListClientRequest>(request);
            var products = await _baoYangClient.GetTireListAsync(clientRequest);
            return new ApiPagedResultData<TireProductVO>()
            {
                Items = _mapper.Map<List<TireProductVO>>(products),
                TotalItems = products.Count
            };
        }


        /// <summary>
        /// 校验商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyAdaptiveProductResponse> VerifyAdaptiveProduct(VerifyAdaptiveProductRequest request)
        {
            var product = (await _productClient.ProductDetail(new ProductDetailSearchClientRequest()
            {
                ProductCodes = new List<string>() {request.Pid}
            }))?.FirstOrDefault();
            if (product == null)
            {
                throw new CustomException("商品不存在");
            }

            if (product.Product.MainCategoryId == 1)
            {
                var baoYangResult = await _baoYangClient.BaoYangAdaptiveProduct(new BaoYangAdaptiveProductRequest
                {
                    Vehicle = _mapper.Map<VehicleClientRequest>(request.Vehicle),
                    Products = new List<ProductClientRequest>()
                    {
                        new ProductClientRequest()
                        {
                            Pid = request.Pid,
                            CategoryId = product.Product.ChildCategoryId.ToString()
                        }
                    }
                });
                if (baoYangResult != null && baoYangResult.Any())
                {
                    return new VerifyAdaptiveProductResponse()
                    {
                        Result = 1,
                        Tip = "此商品适配车型"
                    };
                }
                else
                {
                    return new VerifyAdaptiveProductResponse()
                    {
                        Result = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }
            else if (product.Product.MainCategoryId == 4)
            {
                var tireResult = await _baoYangClient.TireAdaptiveProduct(new TireAdaptiveProductClientRequest()
                {
                    VehicleId = request.Vehicle.VehicleId,
                    Tid = request.Vehicle.Tid,
                    TireSize = request.Vehicle.TireSize,
                    PidList = new List<string>() {request.Pid}
                });
                if (tireResult != null && tireResult.Any())
                {
                    return new VerifyAdaptiveProductResponse()
                    {
                        Result = 1,
                        Tip = "此商品适配车型"
                    };
                }
                else
                {
                    return new VerifyAdaptiveProductResponse()
                    {
                        Result = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }
            else
            {
                return new VerifyAdaptiveProductResponse()
                {
                    Result = 2,
                    Tip = "此商品不确认是否适配车型，请仔细选购"
                };
            }
        }

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VerifyAdaptiveProductForBuyResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyRequest request)
        {
            var product = (await _productClient.ProductDetail(new ProductDetailSearchClientRequest()
            {
                ProductCodes = new List<string>() {request.Pid}
            }))?.FirstOrDefault();
            if (product == null)
            {
                throw new CustomException("商品不存在");
            }

            if (product.Product.MainCategoryId == 1)
            {
                var baoYangResult = await _baoYangClient.VerifyAdaptiveProductForBuy(
                    new VerifyAdaptiveProductForBuyClientRequest()
                    {
                        Pid = request.Pid,
                        CategoryId = product.Product.ChildCategoryId,
                        ShopId = request.ShopId,
                        Vehicle = _mapper.Map<VehicleClientRequest>(request.Vehicle)
                    });
                if (baoYangResult != null)
                {
                    return _mapper.Map<VerifyAdaptiveProductForBuyResponse>(baoYangResult);
                }
                else
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }
            else if (product.Product.MainCategoryId == 4)
            {
                var tireResult = await _baoYangClient.TireAdaptiveProduct(new TireAdaptiveProductClientRequest()
                {
                    VehicleId = request.Vehicle.VehicleId,
                    Tid = request.Vehicle.Tid,
                    TireSize = request.Vehicle.TireSize,
                    PidList = new List<string>() {request.Pid}
                });
                if (tireResult != null && tireResult.Any())
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 1,
                        Tip = "此商品适配车型"
                    };
                }
                else
                {
                    return new VerifyAdaptiveProductForBuyResponse()
                    {
                        IsAdaptive = 0,
                        Tip = "此商品不匹配车型"
                    };
                }
            }
            else
            {
                return new VerifyAdaptiveProductForBuyResponse()
                {
                    IsAdaptive = 2,
                    Tip = "此商品不确认是否适配车型，请仔细选购"
                };
            }
        }
    }
}
