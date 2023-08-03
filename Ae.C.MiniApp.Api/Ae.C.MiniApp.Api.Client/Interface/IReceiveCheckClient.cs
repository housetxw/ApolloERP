using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Client.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Client.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IReceiveCheckClient
    {
        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckReportClientResponse> GetCheckReport(CheckReportClientRequest request);

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleFileClientResponse> GetUserVehicleFile(UserVehicleFileClientRequest request);

        /// <summary>
        /// 维修记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MaintenancerecordDto> GetMaintenancerecord(MaintenancerecordClientRequest request);

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserReceiveCheckDto>> GetUserReceiveCheckList(UserReceiveCheckListClientRequest request);

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CheckErrorDetailDto>> GetCheckErrorDetail(CheckErrorDetailClientRequest request);
    }
}
