using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;

namespace Ae.AccountAuthority.Service.Imp.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper mapper;

        private readonly IRoleRepository roleRepo;

        private readonly IRoleAuthorityRepository roleAuthorityRepo;

        public RoleService(IMapper mapper, IRoleRepository roleRepo, IRoleAuthorityRepository roleAuthorityRepo)
        {
            this.mapper = mapper;
            this.roleRepo = roleRepo;
            this.roleAuthorityRepo = roleAuthorityRepo;
        }
        

        public async Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req)
        {
            var res = await roleRepo.GetRoleListByOrgIds(req);
            return res;
        }

        public async Task<bool> CreateRole(RoleDTO req)
        {
            var reqDo = mapper.Map<RoleDO>(req);
            var res = await roleRepo.CreateRole(reqDo);
            return res;
        }

        public bool SaveRoleAuthority(RoleAuthorityReqDTO req)
        {
            var reqDo = mapper.Map<RoleAuthorityReqDO>(req);
            return roleAuthorityRepo.SaveRoleAuthority(reqDo);
        }

        public async Task<bool> UpdateRoleById(RoleDTO req)
        {
            var reqDo = mapper.Map<RoleDO>(req);
            var res = await roleRepo.UpdateRoleById(reqDo);
            return res;
        }

        public async Task<bool> DeleteRoleById(RoleDTO req)
        {
            var reqDo = mapper.Map<RoleDO>(req);
            var res = await roleRepo.DeleteRoleById(reqDo);
            return res;
        }

        public async Task<RolePageListResDTO> GetPagedRoleList(RoleListReqDTO req)
        {
            var res = new RolePageListResDTO();

            var resDo = await roleRepo.GetPagedRoleList(req);

            if (null == resDo)
            {
                return res;
            }

            res.RoleList = mapper.Map<List<RolePageResDTO>>(resDo.Items);
            res.TotalItems = resDo.TotalItems;
            return res;
        }

        public async Task<List<RoleDTO>> GetAllRoleList()
        {
            var resDo = await roleRepo.GetAllRoleList();
            var res = mapper.Map<List<RoleDTO>>(resDo);
            return res;
        }

        public async Task<List<RoleDTO>> GetRoleListByName(List<string> roleName)
        {
            var resDo = await roleRepo.GetRoleListByName(roleName);
            var res = mapper.Map<List<RoleDTO>>(resDo);
            return res;
        }

        public async Task<List<RoleDTO>> GetRoleListByOrgIdAsync(RoleListReqDTO req)
        {
            var resDo = await roleRepo.GetRoleListByOrgIdAsync(req);
            var res = mapper.Map<List<RoleDTO>>(resDo);
            return res;
        }

        public async Task<List<RolePageResDTO>> GetRoleListAnyCondition(RoleListInternalReqDTO req)
        {
            var resDo = await roleRepo.GetRoleListAnyCondition(req);
            var res = mapper.Map<List<RolePageResDTO>>(resDo);
            return res;
        }

        public async Task<List<RoleAuthorityDTO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req)
        {
            var resDo = await roleAuthorityRepo.GetRoleAuthorityListByRoleId(req);
            var res = mapper.Map<List<RoleAuthorityDTO>>(resDo);
            return res;
        }



    }
}
