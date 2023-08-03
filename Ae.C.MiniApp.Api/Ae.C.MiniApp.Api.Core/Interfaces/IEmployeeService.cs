using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 获取员工基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmployeeInfoVO> GetEmployeeInfo(GetEmployeeInfoRequest request);
    }
}
