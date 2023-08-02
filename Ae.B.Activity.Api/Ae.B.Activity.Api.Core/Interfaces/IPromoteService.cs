using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Core.Interfaces
{
   public interface IPromoteService
    {
        Task<ApiPagedResult<PromoteContentDTO>> GetPromoteContents(GetPromoteContentsRequest request);

        Task<ApiResult<string>> CreatePromoteContent(PromoteContentDTO request);

        Task<ApiResult<string>> UpdatePromoteContentOnline(PromoteContentDTO request);

        Task<ApiResult<string>> DeletePromoteContent(PromoteContentDTO request);

        Task<ApiResult<PromoteContentDTO>> GetPromoteContentDetail(GetPromoteContentsRequest request);

        Task<ApiResult<string>> UpdatePromoteContent(PromoteContentDTO request);

        Task<ApiResult<string>> DeletePromoteAttachment(PromoteContentAttachmentDTO request);

        Task<ApiResult<string>> UpdatePromoteStatus(UpdatePromoteStatusRequest request);

        Task<ApiResult<List<ActivityLogDTO>>> GetActivityLogs(GetActivityLogRequest request);

        Task<ApiResult<ActivityLogDTO>> GetActivityLogDetail(GetActivityLogRequest request);
    }
}
