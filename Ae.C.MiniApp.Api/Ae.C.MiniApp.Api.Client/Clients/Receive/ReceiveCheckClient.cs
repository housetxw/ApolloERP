using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Client.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Client.Response.ReceiveCheck;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Receive
{
    public class ReceiveCheckClient: IReceiveCheckClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<ReceiveCheckClient> logger;

        public ReceiveCheckClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<ReceiveCheckClient> _logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this.logger = _logger;
        }

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckReportClientResponse> GetCheckReport(CheckReportClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            ApiResult<CheckReportClientResponse> result =
                await client.GetAsJsonAsync<CheckReportClientRequest, ApiResult<CheckReportClientResponse>>(
                    configuration["ReceiveServer:GetCheckReport"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetCheckReport_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleFileClientResponse> GetUserVehicleFile(UserVehicleFileClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            ApiResult<UserVehicleFileClientResponse> result =
                await client.GetAsJsonAsync<UserVehicleFileClientRequest, ApiResult<UserVehicleFileClientResponse>>(
                    configuration["ReceiveServer:GetUserVehicleFile"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetUserVehicleFile_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 维修记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MaintenancerecordDto> GetMaintenancerecord(MaintenancerecordClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            ApiResult<MaintenancerecordDto> result =
                await client.GetAsJsonAsync<MaintenancerecordClientRequest, ApiResult<MaintenancerecordDto>>(
                    configuration["ReceiveServer:GetMaintenancerecord"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetMaintenancerecord_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserReceiveCheckDto>> GetUserReceiveCheckList(UserReceiveCheckListClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            ApiPagedResult<UserReceiveCheckDto> result =
                await client.GetAsJsonAsync<UserReceiveCheckListClientRequest, ApiPagedResult<UserReceiveCheckDto>>(
                    configuration["ReceiveServer:GetUserReceiveCheckList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetUserReceiveCheckList_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CheckErrorDetailDto>> GetCheckErrorDetail(CheckErrorDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            ApiResult<List<CheckErrorDetailDto>> result =
                await client.GetAsJsonAsync<CheckErrorDetailClientRequest, ApiResult<List<CheckErrorDetailDto>>>(
                    configuration["ReceiveServer:GetCheckErrorDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<CheckErrorDetailDto>();
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetCheckErrorDetail_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }
    }
}
