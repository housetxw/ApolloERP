using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;

namespace Ae.B.Activity.Api.Client.Clients
{
    public class PromoteClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public PromoteClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiPagedResult<PromoteContentClientDTO>> GetPromoteContents(GetPromoteContentsClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<GetPromoteContentsClientRequest, ApiPagedResult<PromoteContentClientDTO>>(configuration["ActivityServer:GetPromoteContents"], request);
            return result;
        }

        public async Task<ApiResult<string>> CreatePromoteContent(PromoteContentClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<PromoteContentClientDTO, ApiResult<string>>(configuration["ActivityServer:CreatePromoteContent"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdatePromoteContentOnline(PromoteContentClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<PromoteContentClientDTO, ApiResult<string>>(configuration["ActivityServer:UpdatePromoteContentOnline"], request);
            return result;
        }

        public async Task<ApiResult<string>> DeletePromoteContent(PromoteContentClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<PromoteContentClientDTO, ApiResult<string>>(configuration["ActivityServer:DeletePromoteContent"], request);
            return result;
        }

        public async Task<ApiResult<PromoteContentClientDTO>> GetPromoteContentDetail(GetPromoteContentsClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.GetAsJsonAsync<GetPromoteContentsClientRequest, ApiResult<PromoteContentClientDTO>>(configuration["ActivityServer:GetPromoteContentDetail"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdatePromoteContent(PromoteContentClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<PromoteContentClientDTO, ApiResult<string>>(configuration["ActivityServer:UpdatePromoteContent"], request);
            return result;
        }


        public async Task<ApiResult<string>> DeletePromoteAttachment(PromoteContentAttachmentClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<PromoteContentAttachmentClientDTO, ApiResult<string>>(configuration["ActivityServer:DeletePromoteAttachment"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdatePromoteStatus(UpdatePromoteStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.PostAsJsonAsync<UpdatePromoteStatusClientRequest, ApiResult<string>>(configuration["ActivityServer:UpdatePromoteStatus"], request);
            return result;
        }

        public async Task<ApiResult<List<ActivityLogClientDTO>>> GetActivityLogs(GetActivityLogClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.GetAsJsonAsync<GetActivityLogClientRequest,
                ApiResult<List<ActivityLogClientDTO>>>(configuration["ActivityServer:GetActivityLogs"], request);
            return result;
        }

        public async Task<ApiResult<ActivityLogClientDTO>> GetActivityLogDetail(GetActivityLogClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

            var result = await client.GetAsJsonAsync<GetActivityLogClientRequest,
                ApiResult<ActivityLogClientDTO>>(configuration["ActivityServer:GetActivityLogDetail"], request);
            return result;
        }
    }
}
