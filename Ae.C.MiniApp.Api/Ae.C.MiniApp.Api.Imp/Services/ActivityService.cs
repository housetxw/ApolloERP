using AutoMapper;
using ApolloErp.Common.GuidHelper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Activity;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Activity;
using Ae.C.MiniApp.Api.Core.Response.Activity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ActivityService> logger;
        private readonly IActivityClient activityClient;
        private readonly IShopClient shopClient;
        private readonly IIdentityService identityService;

        public ActivityService(IMapper mapper, ApolloErpLogger<ActivityService> logger, IActivityClient activityClient, IShopClient shopClient, IIdentityService identityService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.activityClient = activityClient;
            this.shopClient = shopClient;
            this.identityService = identityService;
        }

        public async Task<ApiResult<object>> GetWxacodeScene(GetWxacodeSceneRequest request)
        {
            var result = Result.Failed(ResultCode.Failed, "解析异常", new object());
            try
            {
                var getWxacodeSceneResult = await activityClient.GetWxacodeScene(new GetWxacodeSceneClientRequest()
                {
                    SceneId = request.SceneId
                });
                if (getWxacodeSceneResult.IsNotNullSuccess())
                {
                    result = Result.Success(getWxacodeSceneResult.GetSuccessData(), "解析成功");
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetWxacodeSceneEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<H5PromoteArticleQueryResponse>> H5PromoteArticleQuery(H5PromoteArticleQueryRequest request)
        {
            var result = Result.Failed<H5PromoteArticleQueryResponse>("您好，该文章链接已经失效，无法查看");
            try
            {
                //文章
                var getPromoteContentResult = await activityClient.GetPromoteContent(new GetPromoteContentRequest()
                {
                    Id = request.Id,
                    IsPreview = request.IsPreview
                });
                var promoteContent = getPromoteContentResult.GetSuccessData();
                if (promoteContent == null)
                {
                    throw new CustomException("该文章已被作者下架");
                }
                var response = new H5PromoteArticleQueryResponse()
                {
                    Id = promoteContent.Id,
                    Title = promoteContent.Title,
                    Content = promoteContent.Content
                };

                //门店
                var shop = await shopClient.GetShopDetailAsync(new GetShopDetailClientRequest() { ShopId = request.ShopId });
                if (shop == null)
                {
                    throw new CustomException("门店信息异常，请重试");
                }
                response.ShopId = shop.Id;
                response.ShopName = shop.SimpleName;
                response.Address = string.IsNullOrWhiteSpace(shop.Address) ? "门店地址进入引导补充完整后展示" : shop.Address;
                response.Telephone = shop.Telephone;

                //服务项目
                var shopServiceProjects = await shopClient.GetShopServiceProject(new BaseShopClientRequest() { ShopId = request.ShopId });
                if (shopServiceProjects != null && shopServiceProjects.Any())
                {
                    foreach (var item in shopServiceProjects)
                    {
                        response.ShopServiceProjectNames.Add(item.DisplayName);
                    }
                }

                //小程序码
                if (!Guid.TryParse(request.EmployeeId, out Guid guidEmployeeId))
                {
                    throw new CustomException("用户信息异常，请重试");
                }
                var clientRequest = new GenMinAppCodeClientRequest
                {
                    Scene = new
                    {
                        PromoteContentType = (int)PromoteContentTypeEnum.Article,
                        ShopId = request.ShopId,
                        EmployeeId = guidEmployeeId
                    }
                };
                var genMinAppCodeResult = await activityClient.GenMinAppCode(clientRequest);
                var genMinAppCodeResponse = genMinAppCodeResult.GetSuccessData();
                if (genMinAppCodeResponse == null || genMinAppCodeResponse.CodeBytes == null || genMinAppCodeResponse.CodeBytes.Length <= 0)
                {
                    throw new CustomException("码信息异常，请重试");
                }
                response.EmployeeId = request.EmployeeId;
                response.CodeBase64String = $"data:image/png;base64,{Convert.ToBase64String(genMinAppCodeResponse.CodeBytes)}";

                result = Result.Success(response, "加载成功");
            }
            catch (CustomException)
            {
                throw new CustomException("您好，该文章链接已经失效，无法查看");
            }
            catch (Exception ex)
            {
                logger.Error("H5PromoteArticleQueryEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> SharePromotionContent(ApiRequest<SharePromotionContentRequest> request)
        {
            var result = Result.Failed("操作异常");
            try
            {
                var clientRequest = mapper.Map<ApiRequest<SharePromotionContentClientRequest>>(request);
                var userId = identityService.GetUserId();
                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new CustomException("登录异常，请重新登录");
                }
                clientRequest.Data.PromoterType = 2;
                clientRequest.Data.PromoterId = userId;
                clientRequest.Data.PromoterName = identityService.GetUserName();
                var apiResult = await activityClient.SharePromotionContent(clientRequest);
                if (apiResult.IsNotNullSuccess())
                {
                    result = Result.Success("操作成功");
                }
                else
                {
                    result = Result.Failed("操作失败");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("SharePromotionContentEx", ex);
            }
            return result;
        }
    }
}
