using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Clients;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Login.Auth;
using System.Linq;

namespace Ae.B.Activity.Api.Imp.Services
{
    public class PromoteService : IPromoteService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<PromoteService> logger;
        private readonly PromoteClient promoteClient;
        private readonly IIdentityService identityService;

        public PromoteService(IMapper mapper, ApolloErpLogger<PromoteService> logger, PromoteClient promoteClient,
            IIdentityService identityService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.promoteClient = promoteClient;
            this.identityService = identityService;
        }

        public async Task<ApiResult<string>> CreatePromoteContent(PromoteContentDTO request)
        {
            var vo = mapper.Map<PromoteContentClientDTO>(request);
            vo.CreateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            #region 图片格式处理
            if(request.Type == 2)
            {
                if (vo.Content.IndexOf(".cn/") != -1) {
                    var newUrl = request.Content.Substring(request.Content.IndexOf(".cn/") + 4, 
                        request.Content.Length - request.Content.IndexOf(".cn/") - 4);
                    vo.Content = newUrl;

                }
            }
            if (!string.IsNullOrWhiteSpace(request.ThumbnailUrl) && request.ThumbnailUrl.IndexOf(".cn/") != -1)
            {
                //需要处理缩略图的格式
                var newUrl = request.ThumbnailUrl.Substring(request.ThumbnailUrl.IndexOf(".cn/") + 4, request.ThumbnailUrl.Length - request.ThumbnailUrl.IndexOf(".cn/") - 4);
                vo.ThumbnailUrl = newUrl;
            }

            if (vo.Attachments.Any())
            {
                foreach (var item in vo.Attachments)
                {
                    if (!string.IsNullOrWhiteSpace(item.Url) &&
                        item.Url.IndexOf(".cn/") != -1)
                    {
                        //需要处理缩略图的格式
                        var newUrl = item.Url.Substring(item.Url.IndexOf(".cn/") + 4, 
                            item.Url.Length - item.Url.IndexOf(".cn/") - 4);
                        item.Url = newUrl;
                    }
                }
            }
            #endregion
            var result = await promoteClient.CreatePromoteContent(vo);
            return result;
        }

        public async  Task<ApiResult<string>> DeletePromoteAttachment(PromoteContentAttachmentDTO request)
        {
            var vo = mapper.Map<PromoteContentAttachmentClientDTO>(request);
            vo.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            var result = await promoteClient.DeletePromoteAttachment(vo);
            return result;
        }

        public async Task<ApiResult<string>> DeletePromoteContent(PromoteContentDTO request)
        {
            var clientRequest = mapper.Map<PromoteContentClientDTO>(request);
            clientRequest.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();

            var result = await promoteClient.DeletePromoteContent(clientRequest);
            return result;
        }

        public async  Task<ApiResult<ActivityLogDTO>> GetActivityLogDetail(GetActivityLogRequest request)
        {
            var clientRequest = mapper.Map<GetActivityLogClientRequest>(request);
            var result = await promoteClient.GetActivityLogDetail(clientRequest);
            var vo = mapper.Map<ApiResult<ActivityLogDTO>>(result);
            return vo;
        }

        public async Task<ApiResult<List<ActivityLogDTO>>> GetActivityLogs(GetActivityLogRequest request)
        {
            var clientRequest = mapper.Map<GetActivityLogClientRequest>(request);
            var result = await promoteClient.GetActivityLogs(clientRequest);
            var vo = mapper.Map<ApiResult<List<ActivityLogDTO>>>(result);
            return vo;
        }

        public async Task<ApiResult<PromoteContentDTO>> GetPromoteContentDetail(GetPromoteContentsRequest request)
        {
            var clientRequest = mapper.Map<GetPromoteContentsClientRequest>(request);
            var result = await promoteClient.GetPromoteContentDetail(clientRequest);
            var vo = mapper.Map<ApiResult<PromoteContentDTO>>(result);
            return vo; ;
        }

        public async Task<ApiPagedResult<PromoteContentDTO>> GetPromoteContents(GetPromoteContentsRequest request)
        {           
            var clientRequest = mapper.Map<GetPromoteContentsClientRequest>(request);
            if (request.Times != null && request.Times.Any())
            {
                clientRequest.StartTime = Convert.ToDateTime(request.Times[0]);
                clientRequest.EndTime = Convert.ToDateTime(request.Times[1]);
            }
            var result = await promoteClient.GetPromoteContents(clientRequest);
            var vo = mapper.Map<ApiPagedResult<PromoteContentDTO>>(result);

            if (vo.Data != null && vo.Data.Items.Count > 0)
            {
                foreach (var item in vo.Data.Items)
                {
                    if (item.Type == 3 && item.Content.Length > 100)
                    {
                        item.ContentStr = item.Content.Substring(0, 100) + "......";
                    }
                    else
                    {
                        item.ContentStr = item.Content;
                    }
                }
            }

            return vo;
        }

        public async  Task<ApiResult<string>> UpdatePromoteContent(PromoteContentDTO request)
        {
            var vo = mapper.Map<PromoteContentClientDTO>(request);
            vo.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            #region 图片格式处理
            if (!string.IsNullOrWhiteSpace(request.ThumbnailUrl) && request.ThumbnailUrl.IndexOf(".cn/") != -1)
            {
                //需要处理缩略图的格式
                var newUrl = request.ThumbnailUrl.Substring(request.ThumbnailUrl.IndexOf(".cn/") + 4, request.ThumbnailUrl.Length - request.ThumbnailUrl.IndexOf(".cn/") - 4);
                vo.ThumbnailUrl = newUrl;
            }

            if (vo.Attachments.Any())
            {
                foreach (var item in vo.Attachments)
                {
                    if (!string.IsNullOrWhiteSpace(item.Url) &&
                        item.Url.IndexOf(".cn/") != -1)
                    {
                        //需要处理缩略图的格式
                        var newUrl = item.Url.Substring(item.Url.IndexOf(".cn/") + 4,
                            item.Url.Length - item.Url.IndexOf(".cn/") - 4);
                        item.Url = newUrl;
                    }
                }
            }

            #endregion
            var result = await promoteClient.UpdatePromoteContent(vo);
            return result;
        }

        public async Task<ApiResult<string>> UpdatePromoteContentOnline(PromoteContentDTO request)
        {
            var vo = mapper.Map<PromoteContentClientDTO>(request);
            vo.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            var result = await promoteClient.UpdatePromoteContentOnline(vo);
            return result;

        }

        public async  Task<ApiResult<string>> UpdatePromoteStatus(UpdatePromoteStatusRequest request)
        {
            var vo = mapper.Map<UpdatePromoteStatusClientRequest>(request);
            vo.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            var result = await promoteClient.UpdatePromoteStatus(vo);
            return result;

        }
    }
}
