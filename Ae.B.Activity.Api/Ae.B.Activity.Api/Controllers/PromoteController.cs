using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Filters;

namespace Ae.B.Activity.Api.Controllers
{
    /// <summary>
    ///财务相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(PromoteController))]
    [ApiController]
    public class PromoteController : ControllerBase
    {
        private readonly IPromoteService promoteService;

        public PromoteController(IPromoteService promoteService)
        {
            this.promoteService = promoteService;
        }

        [HttpPost]
        public async Task<ApiPagedResult<PromoteContentDTO>> GetPromoteContents([FromBody]BaseEntityPostRequest<GetPromoteContentsRequest> request)
        {
            var result = await promoteService.GetPromoteContents(request.Data);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreatePromoteContent([FromBody]BaseEntityPostRequest<PromoteContentDTO> request)
        {
            var result = await promoteService.CreatePromoteContent(request.Data);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdatePromoteContentOnline([FromBody]BaseEntityPostRequest<PromoteContentDTO> request)
        {
            var result = await promoteService.UpdatePromoteContentOnline(request.Data);
            return result;
        }


        [HttpPost]
        public async Task<ApiResult<string>> DeletePromoteContent([FromBody]BaseEntityPostRequest<PromoteContentDTO> request)
        {
            var result = await promoteService.DeletePromoteContent(request.Data);
            return result;
        }

        [HttpGet]
        public async Task<ApiResult<PromoteContentDTO>> GetPromoteContentDetail([FromQuery]GetPromoteContentsRequest request)
        {
            var result = await promoteService.GetPromoteContentDetail(request);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdatePromoteContent([FromBody]BaseEntityPostRequest<PromoteContentDTO> request)
        {
            var result = await promoteService.UpdatePromoteContent(request.Data);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> DeletePromoteAttachment([FromBody]BaseEntityPostRequest<PromoteContentAttachmentDTO> request)
        {
            var result = await promoteService.DeletePromoteAttachment(request.Data);
            return result;
        }


        [HttpPost]
        public async Task<ApiResult<string>> UpdatePromoteStatus([FromBody]BaseEntityPostRequest<UpdatePromoteStatusRequest> request)
        {
            var result = await promoteService.UpdatePromoteStatus(request.Data);
            return result;
        }

        [HttpGet]
        public async Task<ApiResult<List<ActivityLogDTO>>> GetActivityLogs([FromQuery]GetActivityLogRequest request)
        {
            return await promoteService.GetActivityLogs(request);
        }

        [HttpGet]
        public async Task<ApiResult<ActivityLogDTO>> GetActivityLogDetail([FromQuery]GetActivityLogRequest request)
        {
            return await promoteService.GetActivityLogDetail(request);
        }
    }
}