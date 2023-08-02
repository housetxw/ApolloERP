using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Clients;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Imp.Services
{
    public class InstallGuideService : IInstallGuideService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<InstallGuideService> logger;
        private readonly InstallGuideClient installGuideClient;
        private readonly IIdentityService identityService;


        public InstallGuideService(IMapper mapper, ApolloErpLogger<InstallGuideService> logger,
            IIdentityService identityService, InstallGuideClient installGuideClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.installGuideClient = installGuideClient;
        }

        public async Task<ApiResult<string>> CreateInstallGuide(InstallGuideDTO request)
        {
            var clientRequest = mapper.Map<InstallGuideClientDTO>(request);


            clientRequest.CreateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            if (clientRequest.FileList.Any())
            {
                foreach (var item in clientRequest.FileList)
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
            var res = await installGuideClient.CreateInstallGuide(clientRequest);

            return res;
        }

        public async Task<ApiResult<string>> DeleteInstallGuideFile(InstallGuideFileDTO request)
        {
            var clientRequest = mapper.Map<InstallGuideFileClientDTO>(request);
            clientRequest.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            var res = await installGuideClient.DeleteInstallGuideFile(clientRequest);

            return res;

        }

        public async Task<ApiResult<List<InstallGuideCategoryDTO>>> GetInstallGuideCategory()
        {
            var res = await installGuideClient.GetInstallGuideCategory();
            var vo = mapper.Map<ApiResult<List<InstallGuideCategoryDTO>>>(res);
            return vo;
        }

        public async Task<ApiResult<InstallGuideDTO>> GetInstallGuideDetail(GetInstallGuidePagesRequest request)
        {
            var clientRequest = mapper.Map<GetInstallGuidePagesClientRequest>(request);
            var res = await installGuideClient.GetInstallGuideDetail(clientRequest);

            var vo = mapper.Map<ApiResult<InstallGuideDTO>>(res);
            return vo;
        }

        public async Task<ApiPagedResult<InstallGuideDTO>> GetInstallGuidePages(GetInstallGuidePagesRequest request)
        {
            var clientRequest = mapper.Map<GetInstallGuidePagesClientRequest>(request);
            var res = await installGuideClient.GetInstallGuidePages(clientRequest);

            var vo = mapper.Map<ApiPagedResult<InstallGuideDTO>>(res);
            if (vo.Code == ResultCode.Success)
            {
                if (vo.Data.Items.Any())
                {
                    foreach (var item in vo.Data.Items)
                    {
                        item.ContentStr = item.Content.Length > 100 ? item.Content.Substring(0, 100) + "......" : item.Content;
                    }
                }
            }
            return vo;
        }

        public async Task<ApiResult<string>> UpdateInstallGuideInfo(InstallGuideDTO request)
        {
            var clientRequest = mapper.Map<InstallGuideClientDTO>(request);
            clientRequest.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            if (clientRequest.FileList.Any())
            {
                foreach (var item in clientRequest.FileList)
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
            var res = await installGuideClient.UpdateInstallGuideInfo(clientRequest);
            return res;
        }

        public async Task<ApiResult<string>> UpdateInstallGuideStatus(UpdateInstallGuideStatusRequest request)
        {
            var clientRequest = mapper.Map<UpdateInstallGuideStatusClientRequest>(request);
            clientRequest.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();

            var res = await installGuideClient.UpdateInstallGuideStatus(clientRequest);
            return res;
        }
    }
}
