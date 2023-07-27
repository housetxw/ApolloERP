using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Imp.Services
{
    public class EmployeeService : IEmployeeService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;

        private readonly IEmployeeClient empClient;

        public EmployeeService(IMapper mapper, IEmployeeClient empClient)
        {
            this.mapper = mapper;
            this.empClient = empClient;
        }
        
        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<EmployeeResVO>> GetAllEmployeeListAsync()
        {
            var resDto = await empClient.GetAllEmployeeListAsync();
            var res = mapper.Map<List<EmployeeResVO>>(resDto);
            res.ForEach(f => f.TypeName = f.Type.GetEnumDescription());
            return res;
        }

        public async Task<List<EmployeeResVO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqVO req)
        {
            var reqDto = mapper.Map<EmployeeListReqDTO>(req);
            var resDto = await empClient.GetEmployeeListByOrgIdAndTypeAsync(reqDto);
            var res = mapper.Map<List<EmployeeResVO>>(resDto);
            res.ForEach(f => f.TypeName = f.Type.GetEnumDescription());
            return res;
        }

        public async Task<ApiPagedResultData<EmployeePageResVO>> GetEmployeePage(EmployeePageReqVO req)
        {
            var reqDto = mapper.Map<EmployeePageReqDTO>(req);
            var resDto = await empClient.GetEmployeePage(reqDto);
            var res = new ApiPagedResultData<EmployeePageResVO>
            {
                Items = mapper.Map<ICollection<EmployeePageResVO>>(resDto?.Items),
                TotalItems = resDto?.TotalItems ?? 0
            };
            return res;
        }


        // ---------------------------------- 私有方法 --------------------------------------

    }
}
