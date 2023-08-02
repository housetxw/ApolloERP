using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Request.Product;
using Ae.B.Product.Api.Common.Util;
using Ae.B.Product.Api.Core.Model.Product;

namespace Ae.B.Product.Api.Imp.Services
{
    public class PageConfigService : IPageConfigService
    {
        private readonly IMapper _mapper;
        private readonly IPageConfigClient _pageConfigClient;
        private readonly IIdentityService _identityService;
        private readonly IProductClient _productClient;


        public PageConfigService(IMapper mapper, IPageConfigClient pageConfigClient,
            IIdentityService identityService, IProductClient productClient)
        {
            _mapper = mapper;
            _pageConfigClient = pageConfigClient;
            _identityService = identityService;
            _productClient = productClient;
        }

        public async Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request)
        {
            request.CreateBy = _identityService.GetUserName();
            if (request.Times != null && request.Times.Any())
            {
                request.StartTime = Convert.ToDateTime(request.Times[0]);
                request.EndTime = Convert.ToDateTime(request.Times[1]);
            }
            return await _pageConfigClient.AddConfigAdvertisement(request);
        }

        public async Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request)
        {
            request.UpdateBy = _identityService.GetUserName();
            return await _pageConfigClient.DeleteConfigAdvertisement(request);
        }

        public async Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request)
        {
            var result = await _pageConfigClient.GetConfigAdvertisement(request);
            if (result.Code == ResultCode.Success)
            {
                var vo = result.Data;
                vo.Times.Add(vo.StartTime.ToString());
                vo.Times.Add(vo.EndTime.ToString());
                result.Data = vo;
            }
            return result;
        }

        public async Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request)
        {
            return await _pageConfigClient.GetConfigAdvertisements(request);
        }

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductVo>> GetGrouponProductPageList(
            GrouponProductPageListRequest request)
        {
            ApiPagedResultData<GrouponProductVo> response = new ApiPagedResultData<GrouponProductVo>()
            {
                Items = new List<GrouponProductVo>()
            };
            var result = await _productClient.GetGrouponProductPageList(new GrouponProductPageListClientRequest()
            {
                OnSale = request.OnSale,
                Key = request.Key,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<GrouponProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductVo>> SearchProductForGroupon(
            SearchProductForGrouponRequest request)
        {
            ApiPagedResultData<GrouponProductVo> response = new ApiPagedResultData<GrouponProductVo>()
            {
                Items = new List<GrouponProductVo>()
            };
            var result = await _productClient.SearchProductForGroupon(new SearchProductForGrouponClientRequest()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue()
            });

            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<GrouponProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddGrouponProduct(AddGrouponProductRequest request)
        {
            var result = await _productClient.AddGrouponProduct(new AddGrouponProductClientRequest()
            {
                GrouponProduct = request.GrouponProduct?.Select(_ => new GrouponProductClientRequest
                {
                    Pid = _.Pid,
                    MinPrice = _.MinPrice,
                    MaxPrice = _.MaxPrice
                })?.ToList(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditGrouponProduct(EditGrouponProductRequest request)
        {
            var result = await _productClient.EditGrouponProduct(new EditGrouponProductClientRequest()
            {
                Id = request.Id,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                IsDeleted = request.IsDeleted,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        public async Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request)
        {
            request.UpdateBy = _identityService.GetUserName();

            if (request.Times != null && request.Times.Any())
            {
                request.StartTime = Convert.ToDateTime(request.Times[0]);
                request.EndTime = Convert.ToDateTime(request.Times[1]);
            }
            request.UpdateTime = DateTime.Now;
            return await _pageConfigClient.UpdateConfigAdvertisement(request);
        }
    }
}
