using AutoMapper;
using ApolloErp.Log;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeClient employeeClient;
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<EmployeeService> logger;

        public EmployeeService(IEmployeeClient employeeClient, ApolloErpLogger<EmployeeService> logger, IMapper mapper)
        {
            this.employeeClient = employeeClient;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// 获取员工基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmployeeInfoVO> GetEmployeeInfo(GetEmployeeInfoRequest request)
        {
            var clientRequest = mapper.Map<GetEmployeeInfoClientRequest>(request);
            var clientResponse = await employeeClient.GetEmployeeSimpleInfo(clientRequest);
            var response = mapper.Map<EmployeeInfoVO>(clientResponse);
            response.Level = !string.IsNullOrEmpty(clientResponse.WorkKindName) ? clientResponse.WorkKindName : clientResponse.JobName +
                 (clientResponse.Level > 0 ? "-" + EnumExtension.GetDescription((EmployeeLevelEnum)clientResponse.Level) : "");
            return response;

        }
    }
}
