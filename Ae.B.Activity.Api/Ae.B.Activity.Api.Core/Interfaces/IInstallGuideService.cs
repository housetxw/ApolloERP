using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Core.Interfaces
{
    public interface IInstallGuideService
    {
        Task<ApiPagedResult<InstallGuideDTO>> GetInstallGuidePages(GetInstallGuidePagesRequest request);

        Task<ApiResult<string>> UpdateInstallGuideStatus(UpdateInstallGuideStatusRequest request);

        Task<ApiResult<InstallGuideDTO>> GetInstallGuideDetail(GetInstallGuidePagesRequest request);


        Task<ApiResult<string>> CreateInstallGuide(InstallGuideDTO request);

        Task<ApiResult<string>> DeleteInstallGuideFile(InstallGuideFileDTO request);

        Task<ApiResult<List<InstallGuideCategoryDTO>>> GetInstallGuideCategory();

        Task<ApiResult<string>> UpdateInstallGuideInfo(InstallGuideDTO request);

    }
}
