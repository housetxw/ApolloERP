using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Model.ReceiveCheck;
using Ae.Receive.Service.Core.Request.ReceiveCheck;
using Ae.Receive.Service.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Core.Interfaces
{
    /// <summary>
    /// 检查报告
    /// </summary>
    public interface IReceiveCheckService
    {
        /// <summary>
        /// 检查项目首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckMainDataResponse> GetCheckMainData(CheckMainDataRequest request);

        /// <summary>
        /// 获取客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CustomerDescriptionResponse> GetCustomerDescription(CheckMainDataRequest request);

        /// <summary>
        /// 获取仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CarDashboardResponse> GetCarDashboard(CheckMainDataRequest request);

        /// <summary>
        /// 获取附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OtherInspectionResponse> GetOtherInspection(CheckMainDataRequest request);

        /// <summary>
        /// 获取内饰/外观
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<InOrOutlookInspectionResponse> GetInOrOutlookInspection(InOrOutlookInspectionRequest request);

        /// <summary>
        /// 保存客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveCustomerDescription(SaveCustomerDescriptionRequest request);

        /// <summary>
        /// 保存仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveCarDashboard(SaveCarDashboardRequest request);

        /// <summary>
        /// 保存附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveOtherInspection(SaveOtherInspectionRequest request);

        /// <summary>
        /// 保存外观/内饰
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveInOrOutlookInspection(SaveInOrOutlookInspectionRequest request);

        /// <summary>
        /// 升级项目查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UpgradeCheckItemResponse> GetUpgradeCheckItem(UpgradeCheckItemRequest request);

        /// <summary>
        /// 升级项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveUpgradeCheckItem(SaveUpgradeCheckItemRequest request);

        /// <summary>
        /// 检查项统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckStatisticsResultResponse> GetCheckStatisticsResult(CheckStatisticsResultRequest request);

        /// <summary>
        /// 根据到店记录查询检查报告统计
        /// </summary>
        /// <param name="recId"></param>
        /// <returns></returns>
        Task<CheckStatisticsVo> GetCheckStatisticsByRecId(long recId);

        /// <summary>
        /// 技师Or客户签字
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveSignatureData(SaveSignatureDataRequest request);

        /// <summary>
        /// 提交检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SubmitCheckResult(SubmitCheckResultRequest request);

        /// <summary>
        /// 计算规则结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ComputeRuleResultResponse> ComputeRuleResult(ComputeRuleResultRequest request);

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckReportResponse> GetCheckReport(CheckReportRequest request);

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<UserReceiveCheckVo>> GetUserReceiveCheckList(UserReceiveCheckListRequest request);

        /// <summary>
        /// 车辆历史检测报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CarReceiveCheckVo>> GetCarReceiveCheckList(CarReceiveCheckListRequest request);

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserVehicleFileResponse> GetUserVehicleFile(UserVehicleFileRequest request);

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CheckErrorDetailVo>> GetCheckErrorDetail(CheckErrorDetailRequest request);

        /// <summary>
        /// 根据到店记录批量查询检查报告
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        Task<List<ReceiveCheckListVo>> GetReceiveCheckListByRecIds(List<long> recIds);
    }
}
