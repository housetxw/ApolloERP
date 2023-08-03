using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ReceiveCheckService : IReceiveCheckService
    {
        private readonly IReceiveCheckClient receiveCheckClient;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public ReceiveCheckService(IReceiveCheckClient receiveCheckClient, IMapper mapper, IIdentityService identityService)
        {
            this.receiveCheckClient = receiveCheckClient;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckReportResponse> GetCheckReport(CheckReportRequest request)
        {
            var result = await receiveCheckClient.GetCheckReport(new CheckReportClientRequest
            {
                CheckId = request.CheckId
            });

            if (result != null)
            {
                return mapper.Map<CheckReportResponse>(result);
            }

            return null;
        }

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleFileResponse> GetUserVehicleFile(UserVehicleFileRequest request)
        {
            var result = await receiveCheckClient.GetUserVehicleFile(new UserVehicleFileClientRequest
            {
                UserId = identityService.GetUserId(),
                CarId = request.CarId
            });

            if (result != null)
            {
                return mapper.Map<UserVehicleFileResponse>(result);
            }

            return null;
        }

        /// <summary>
        /// 维修记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Maintenancerecord> GetMaintenancerecord(MaintenancerecordRequest request)
        {
            Maintenancerecord data = new Maintenancerecord();
            var result = await receiveCheckClient.GetMaintenancerecord(new MaintenancerecordClientRequest
            {
                CarId = request.CarId,
                ServiceCode = request.ServiceCode
            });
            if (result != null)
            {
                data = mapper.Map<Maintenancerecord>(result);
            }
            return data;
        }

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserReceiveCheckVo>> GetUserReceiveCheckList(UserReceiveCheckListRequest request)
        {
            ApiPagedResultData<UserReceiveCheckVo> data = new ApiPagedResultData<UserReceiveCheckVo>
            {
                Items = new List<UserReceiveCheckVo>()
            };
            var result = await receiveCheckClient.GetUserReceiveCheckList(new UserReceiveCheckListClientRequest
            {
                UserId = identityService.GetUserId(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null)
                {
                    data.Items = mapper.Map<List<UserReceiveCheckVo>>(result.Items);
                }
            }
            return data;
        }

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CheckErrorDetailVo>> GetCheckErrorDetail(CheckErrorDetailRequest request)
        {
            List<CheckErrorDetailVo> data = new List<CheckErrorDetailVo>();

            var result = await receiveCheckClient.GetCheckErrorDetail(new CheckErrorDetailClientRequest
            {
                CarId = request.CarId,
                KeyName = request.KeyName
            });

            if (result != null)
            {
                data = mapper.Map<List<CheckErrorDetailVo>>(result);
            }

            return data;
        }
    }
}
