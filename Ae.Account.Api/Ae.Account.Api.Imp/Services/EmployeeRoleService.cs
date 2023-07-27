using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Interface.AccountAuthorityServer;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Imp.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;

        private readonly IEmployeeRoleClient empRoleClient;
        private readonly IRoleClient roleClient;

        private readonly IEmployeeClient empClient;

        public EmployeeRoleService(IMapper mapper,
            IEmployeeRoleClient empRoleClient,
            IEmployeeClient empClient,
            IRoleClient roleClient)
        {
            this.mapper = mapper;

            this.empRoleClient = empRoleClient;
            this.roleClient = roleClient;

            this.empClient = empClient;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        #region ！！！授权接口实现！！！

        /// <summary>
        /// 授权接口：根据EmployeeId，EmployeeType和OrganizationId，获取Web端其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<AuthorizationWebResDTO>> GetEmpAuthorityListForWebByEmpIdAsync(AuthorizationReqVO req)
        {
            var res = new List<AuthorizationWebResDTO>();

            var reqDto = mapper.Map<AuthorizationReqDTO>(req);

            var resDto = req.IsOrganizationRange
                ? await GetRangeRoleAuthorityListByIdAndOrgId(reqDto)
                : await GetEmpAuthorityListByEmpIdAsync(reqDto);

            if (resDto == null || !resDto.Any()) { return res; }

            res = mapper.Map<List<AuthorizationWebResDTO>>(resDto);
            res.ForEach(f =>
            {
                var obj = resDto.FirstOrDefault(r => r.AuthorityId.Equals(f.AuthorityId));
                f.Meta.Add(new MenuMeta
                {
                    Title = obj?.AuthorityName,
                    Icon = obj?.MenuIcon
                });
            });
            return res;
        }

        /// <summary>
        /// 授权接口：根据EmployeeId，EmployeeType和OrganizationId，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req)
        {
            var res = await empRoleClient.GetEmpAuthorityListByEmpIdAsync(req);
            return res;
        }
        /// <summary>
        /// 授权接口：根据EmployeeId和RoleIds，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIdAndOrgId(AuthorizationReqDTO req)
        {
            var res = new List<AuthorizationResDTO>();

            var roleList = await empClient.GetOrgRangeRoleIdList(new OrgRangeRoleListForLoginReqDTO
            {
                EmployeeId = req.EmployeeId,
                EmployeeType = req.EmployeeType,
                OrganizationId = req.OrganizationId
            });

            if (!roleList.Any()) return res;

            res = await empRoleClient.GetRangeRoleAuthorityListByIds(new RangeRoleAuthorityReqDTO
            {
                EmployeeId = req.EmployeeId,
                RoleIds = roleList
            });
            
            return res;
        }


        #endregion ！！！授权接口实现！！！

        public async Task<List<EmployeeRoleLisVO>> GetRoleListByOrgIdAsync(EmployeeRoleDialogListReqVO req)
        {
            var reqDto = mapper.Map<RoleListReqDTO>(req);
            var resDto = await roleClient.GetRoleListByOrgIdAsync(reqDto);
            var res = mapper.Map<List<EmployeeRoleLisVO>>(resDto);
            return res;
        }

        public async Task<List<EmployeeRoleLisVO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqVO req)
        {
            var reqDto = mapper.Map<EmployeeRoleListReqDTO>(req);
            var resDto = await empRoleClient.GetEmployeeRoleListByEmpIdAsync(reqDto);
            var res = mapper.Map<List<EmployeeRoleLisVO>>(resDto);
            return res;
        }

        public async Task<bool> EditEmployeeRole(EmployeeRoleSaveReqVO req)
        {
            var reqDto = new EmployeeRoleSaveReqDTO
            {
                EmployeeRoleList = new List<EmployeeRoleEntityDTO>(),
                EmployeeId = req.EmployeeId,
                Operator = req.Operator
            };

            req.RoleIdList.ForEach(f =>
            {
                reqDto.EmployeeRoleList.Add(new EmployeeRoleEntityDTO
                {
                    EmployeeId = req.EmployeeId,
                    RoleId = f,
                    CreateBy = req.Operator
                });
            });

            var res = await empRoleClient.EditEmployeeRole(reqDto);
            return res;
        }

    }
}
