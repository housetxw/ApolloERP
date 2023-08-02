using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Model;
using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Filters;

namespace Ae.B.Activity.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(InstallGuideController))]
    [ApiController]
    public class InstallGuideController : Controller
    {
        private readonly IInstallGuideService _installGuideService;

        public InstallGuideController(IInstallGuideService installGuideService)
        {
            this._installGuideService = installGuideService;
        }

        [HttpGet]
        public async Task<ApiPagedResult<InstallGuideDTO>> GetInstallGuidePages([FromQuery]GetInstallGuidePagesRequest request)
        {
            var result = await _installGuideService.GetInstallGuidePages(request);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateInstallGuideStatus([FromBody]BaseEntityPostRequest<UpdateInstallGuideStatusRequest> request)
        {
            var result = await _installGuideService.UpdateInstallGuideStatus(request.Data);
            return result;
        }

        [HttpGet]
        public async Task<ApiResult<InstallGuideDTO>> GetInstallGuideDetail([FromQuery]GetInstallGuidePagesRequest request)
        {
            var result = await _installGuideService.GetInstallGuideDetail(request);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> DeleteInstallGuideFile([FromBody]BaseEntityPostRequest<InstallGuideFileDTO> request)
        {
            var result = await _installGuideService.DeleteInstallGuideFile(request.Data);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreateInstallGuide([FromBody]BaseEntityPostRequest<InstallGuideDTO> request)
        {
            var result = await _installGuideService.CreateInstallGuide(request.Data);
            return result;
        }

        [HttpGet]
        public async Task<ApiResult<List<InstallGuideCategoryDTO>>> GetInstallGuideCategory()
        {
            var result = await _installGuideService.GetInstallGuideCategory();
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateInstallGuideInfo([FromBody]BaseEntityPostRequest<InstallGuideDTO> request)
        {
            var result = await _installGuideService.UpdateInstallGuideInfo(request.Data);
            return result;
        }
    }
}