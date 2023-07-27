using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.EmployeePerformance
{
   public interface IEmployeePerformanceReportRepository : 
        IRepository<EmployeePerformanceReportDO>
    {
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeePerformanceDto>> GetEmployeePerformanceList(EmployeePerformanceRequest request);

        /// <summary>
        /// 获取员工绩效详情
        /// </summary>
        /// <returns></returns>
        Task<EmployeePerformanceDetialDto> GetEmployeePerformanceDetial(EmployeePerformanceDetialRequest request);

        /// <summary>
        /// 获取技师绩效列表V2
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDto>> GetTechPerformanceList(TechPerformanceRequest request);

        /// <summary>
        /// 获取技师绩效明细V2
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetail(TechPerformanceDetailRequest request);

        /// <summary>
        /// 获取技师绩效列表V3
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDto>> GetTechPerformanceListV3(TechPerformanceRequest request);

        /// <summary>
        /// 获取技师绩效明细V3
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetailV3(TechPerformanceDetailRequest request);

        /// <summary>
        /// 获取技师绩效列表V4
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDto>> GetTechPerformanceListV4(TechPerformanceRequest request);

        /// <summary>
        /// 获取技师绩效明细V4
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetailV4(TechPerformanceDetailRequest request);
    }
}
