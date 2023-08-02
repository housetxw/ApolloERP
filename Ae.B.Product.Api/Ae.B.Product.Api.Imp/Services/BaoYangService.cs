using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Imp.Services
{
    public class BaoYangService : IBaoYangService
    {
        private readonly IBaoYangClient _baoYangClient;
        private readonly string _channel = "BOSS";
        private readonly IMapper _mapper;

        public BaoYangService(IMapper mapper, IBaoYangClient baoYangClient)
        {
            _mapper = mapper;
            _baoYangClient = baoYangClient;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(GetBaoYangPackagesRequest request)
        {
            request.Channel = _channel;
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
            var clientRequest = _mapper.Map<SearchProductClientRequest>(request);
            var products =
                await _baoYangClient.SearchPackageProductsWithConditionAsync(clientRequest);
            result.Products = _mapper.Map<List<BaoYangPackageProductModel>>(products);
            return result;
        }
    }
}
