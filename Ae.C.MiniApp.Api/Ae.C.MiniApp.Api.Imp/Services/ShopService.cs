using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Clients.Shop;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using ApolloErp.Login.Auth;
using Newtonsoft.Json;
using ApolloErp.Common.GuidHelper;
using Ae.C.MiniApp.Api.Client.Request.Activity;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Common.Extension;
using System.Linq;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ShopService : IShopService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ShopService> logger;
        private readonly IShopClient shopClient;
        private readonly IIdentityService identityService;
        private readonly IShopCustomerClient shopCustomerClient;
        private readonly IActivityClient activityClient;

        public ShopService(IMapper mapper, ApolloErpLogger<ShopService> logger,
            IShopClient shopClient,
            IIdentityService identityService
, IShopCustomerClient shopCustomerClient, IActivityClient activityClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.shopClient = shopClient;
            this.identityService = identityService;
            this.shopCustomerClient = shopCustomerClient;
            this.activityClient = activityClient;
        }
        /// <summary>
        /// 加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> JoinInAsync(JoinInRequest request)
        {
            var clientRequest = mapper.Map<JoinInClientRequest>(request);
            clientRequest.CreateBy = identityService.GetUserName();
            return await shopClient.JoinInAsync(clientRequest);
        }
        /// <summary>
        /// 查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<NearShopInfoVO>> GetNearShopListAsync(GetNearShopListRequest request)
        {
            var clientRequest = mapper.Map<GetNearShopListClientRequest>(request);
            var clientResponse = await shopClient.GetNearShopListAsync(clientRequest);
            var responseList = mapper.Map<List<NearShopInfoVO>>(clientResponse.Items);
            if (responseList != null && responseList.Any())
            {
                responseList.ForEach(r =>
                {
                    r.WorkTime = r.Status==1?"停止服务": r.Status==2? "筹备中": "营业时间：" + r.StartWorkTime.ToString("t")+" - "+r.EndWorkTime.ToString("t");
                    r.Reservation = "已排队0人，最快"+ DateTime.Now.ToString("M/d  H:mm") +"为您服务";
                });
            }

            var result = new ApiPagedResultData<NearShopInfoVO>()
            {
                Items = responseList,
                TotalItems = clientResponse.TotalItems
            };
            return result;
        }
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopDetailResponse> GetShopDetailAsync(GetShopDetailRequest request)
        {
            var clientRequest = mapper.Map<GetShopDetailClientRequest>(request);
            clientRequest.CheckStatus = 2;
            var clientResponse = await shopClient.GetShopDetailAsync(clientRequest);
            //  if (clientResponse != null)
            var typeText = ((ShopTypeEnum)clientResponse.Type).GetDescription();
            var response = mapper.Map<GetShopDetailResponse>(clientResponse);
            response.Type = typeText;

            AddShopUserRelationRequest addShopUserRelationRequest = null;
            ApiResult<bool> addShopUserRelationResult = null;
            try
            {
                var userId = identityService.GetUserId();
                var userName = identityService.GetUserName();
                //如果推广门店与当前打开门店相同
                if (!string.IsNullOrWhiteSpace(request.SceneId) && !string.IsNullOrWhiteSpace(userId))
                {
                    var getWxacodeSceneResult = await activityClient.GetWxacodeScene(new GetWxacodeSceneClientRequest()
                    {
                        SceneId = request.SceneId
                    });
                    var sceneJsonObject = getWxacodeSceneResult.GetSuccessData();
                    if (sceneJsonObject != null)
                    {
                        var sceneJson = JsonConvert.SerializeObject(sceneJsonObject);
                        dynamic scene = JsonConvert.DeserializeObject<object>(sceneJson);
                        var promoteContentType = scene.promoteContentType;
                        var shopId = scene.shopId;
                        var employeeId = scene.employeeId;
                        if (shopId != null && response != null && shopId == response.Id)
                        {
                            //新增门店用户
                            addShopUserRelationRequest = new AddShopUserRelationRequest()
                            {
                                ShopId = shopId,
                                UserId = userId,
                                SubmitBy = string.IsNullOrWhiteSpace(userName) ? "扫小程序码" : userName,
                                ReferrerShopId = shopId,
                                ReferrerUserId = employeeId ?? string.Empty,
                                ReferrerType = (Client.Model.ReferrerType)promoteContentType,
                                Channel = Client.Model.ChannelType.Employee
                            };
                            addShopUserRelationResult = await shopCustomerClient.AddShopUserRelation(addShopUserRelationRequest);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AddShopUserRelationEx", ex);
            }
            finally
            {
                logger.Info($"AddShopUserRelation request={JsonConvert.SerializeObject(request)} addShopUserRelationRequest={JsonConvert.SerializeObject(addShopUserRelationRequest)} addShopUserRelationResult={JsonConvert.SerializeObject(addShopUserRelationResult)}");
            }

            return response;
        }

        /// <summary>
        /// 获取城市下的区县门店数量
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopLocationVO>> GetRegionByCityId(GetRegionByCityIdRequest request)
        {
            var clientRequest = mapper.Map<GetRegionByCityIdClientRequest>(request);
            var clientResponse = await shopClient.GetRegionByCityId(clientRequest);
            var response = mapper.Map<List<ShopLocationVO>>(clientResponse);
            return response;
        }
        /// <summary>
        /// 获取公司简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCompanyInfoResponse> GetCompanyInfo(GetCompanyInfoRequest request)
        {
            var clientRequest = mapper.Map<GetCompanyInfoClientRequest>(request);
            var clientResponse = await shopClient.MiniGetCompanyInfo(clientRequest);
            var response = mapper.Map<GetCompanyInfoResponse>(clientResponse);
            return response;
        }

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public async Task<GetAllCitysResponse> GetAllCitys()
        {
            var clientResponse = await shopClient.GetChinaCitys();
            var response = mapper.Map<GetAllCitysResponse>(clientResponse);
            return response;
        }

        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceProjectVO>> GetShopServiceProject(BaseShopRequest request)
        {
            var clientRequest = new BaseShopClientRequest() { ShopId = request.ShopId };
            var clientResponse = await shopClient.GetShopServiceProject(clientRequest);
            var response = mapper.Map<List<ShopServiceProjectVO>>(clientResponse);
            return response;
        }
        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetOptimalShopResponse> GetOptimalShop(GetOptimalShopRequest request)
        {
            var clientRequest = mapper.Map<GetOptimalShopClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await shopClient.GetOptimalShop(clientRequest);
            var response = mapper.Map<GetOptimalShopResponse>(clientResponse);
            return response;
        }
    }
}
