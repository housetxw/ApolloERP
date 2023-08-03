using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IReceiveCheckService
    {
        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckReportResponse> GetCheckReport(CheckReportRequest request);

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleFileResponse> GetUserVehicleFile(UserVehicleFileRequest request);

        /// <summary>
        /// 维修记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Maintenancerecord> GetMaintenancerecord(MaintenancerecordRequest request);

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserReceiveCheckVo>> GetUserReceiveCheckList(UserReceiveCheckListRequest request);

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CheckErrorDetailVo>> GetCheckErrorDetail(CheckErrorDetailRequest request);
    }
}
