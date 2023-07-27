using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Client.Interface;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Core.Interfaces.ShopManage;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Imp.Services.ShopManage
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
        public async Task<List<OrganizationVO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req)
        {
            var resDto = await empClient.GetEmpAndOrgListForLoginByAccountIdAsync(req);
            var res = mapper.Map<List<OrganizationVO>>(resDto);
            return res;
        }

        public async Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req)
        {
            var resDto = await empClient.GetEmpAndOrgPageForLoginByAccountIdAsync(req);
            return new ApiPagedResult<OrganizationVO>
            {
                Data = new ApiPagedResultData<OrganizationVO>
                {
                    Items = mapper.Map<List<OrganizationVO>>(resDto.Data?.Items) ?? new List<OrganizationVO>(),
                    TotalItems = resDto.Data?.TotalItems ?? 0
                },
                Code = resDto.Code,
                Message = resDto.Message
            };
        }

        [Obsolete("废弃")]
        public async Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            var resDto = await empClient.GetEmpAndOrgPageForLoginByEmpIdAsync(req);
            return new ApiPagedResult<OrganizationVO>
            {
                Data = new ApiPagedResultData<OrganizationVO>
                {
                    Items = mapper.Map<List<OrganizationVO>>(resDto.Data?.Items) ?? new List<OrganizationVO>(),
                    TotalItems = resDto.Data?.TotalItems ?? 0
                },
                Code = resDto.Code,
                Message = resDto.Message
            };
        }

        public async Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            var resDto = await empClient.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(req);
            return new ApiPagedResult<OrganizationVO>
            {
                Data = new ApiPagedResultData<OrganizationVO>
                {
                    Items = mapper.Map<List<OrganizationVO>>(resDto.Data?.Items) ?? new List<OrganizationVO>(),
                    TotalItems = resDto.Data?.TotalItems ?? 0
                },
                Code = resDto.Code,
                Message = resDto.Message
            };
        }

        public async Task<ApiPagedResult<OrganizationVO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req)
        {
            var resDto = await empClient.GetOrgRangePageForLoginByEmpIdExcCurOrgId(req);
            return new ApiPagedResult<OrganizationVO>
            {
                Data = new ApiPagedResultData<OrganizationVO>
                {
                    Items = mapper.Map<List<OrganizationVO>>(resDto.Data?.Items) ?? new List<OrganizationVO>(),
                    TotalItems = resDto.Data?.TotalItems ?? 0
                },
                Code = resDto.Code,
                Message = resDto.Message
            };
        }


    }
}
