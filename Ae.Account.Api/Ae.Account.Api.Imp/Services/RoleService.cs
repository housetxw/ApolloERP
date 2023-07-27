using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Imp.Services
{
    public class RoleService : IRoleService
    {
        #region 变量和构造器

        private readonly IMapper mapper;

        private readonly ICompanyClient companyClient;

        private readonly IRoleClient roleClient;

        public RoleService(IMapper mapper, IRoleClient roleClient, ICompanyClient companyClient)
        {
            this.mapper = mapper;
            this.roleClient = roleClient;
            this.companyClient = companyClient;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateRole(RoleVO req)
        {
            var reqDto = mapper.Map<RoleDTO>(req);
            var res = await roleClient.CreateRole(reqDto);
            return res;
        }

        public async Task<bool> SaveRoleAuthority(RoleAuthorityReqVO req)
        {
            var roleId = req.RoleId;
            var crtBy = req.CreateBy;
            var now = DateTime.Now;

            //req.DeleteList.ForEach(f =>
            //{
            //    f.RoleId = roleId;
            //    f.UpdateBy = udBy;
            //});
            //req.AddList.ForEach(f =>
            //{
            //    f.RoleId = roleId;
            //    f.CreateBy = crtBy;
            //});

            req.HalfCheckedList.ForEach(f =>
            {
                req.AddList.Add(new RoleAuthorityVO
                {
                    RoleId = roleId,
                    AuthorityId = f,
                    HalfCheck = true,
                    CreateBy = crtBy,
                    CreateTime = now
                });
            });
            req.CheckedList.ForEach(f =>
            {
                req.AddList.Add(new RoleAuthorityVO
                {
                    RoleId = roleId,
                    AuthorityId = f,
                    HalfCheck = false,
                    CreateBy = crtBy,
                    CreateTime = now
                });
            });

            var reqDto = mapper.Map<RoleAuthorityReqDTO>(req);
            return await roleClient.SaveRoleAuthority(reqDto);
        }

        public async Task<bool> UpdateRoleById(RoleVO req)
        {
            var reqDto = mapper.Map<RoleDTO>(req);
            var res = await roleClient.UpdateRoleById(reqDto);
            return res;
        }

        public async Task<bool> DeleteRoleById(RoleVO req)
        {
            var reqDto = mapper.Map<RoleDTO>(req);
            var res = await roleClient.DeleteRoleById(reqDto);
            return res;
        }

        public async Task<RolePageListResVO> GetPagedRoleList(RoleListReqVO req)
        {
            var reqDto = mapper.Map<RoleListReqDTO>(req);
            var resDto = await roleClient.GetPagedRoleList(reqDto);
            var res = mapper.Map<RolePageListResVO>(resDto);
            return res;
        }

        public async Task<List<RoleSelectResVO>> GetAllRoleSelectList()
        {
            var resDto = await roleClient.GetAllRoleList();
            var res = mapper.Map<List<RoleSelectResVO>>(resDto);
            return res;
        }

        public async Task<List<RoleSelectResVO>> GetRoleListByOrgIdAsync(RoleListReqVO req)
        {
            var reqDto = mapper.Map<RoleListReqDTO>(req);
            var resDto = await roleClient.GetRoleListByOrgIdAsync(reqDto);
            var res = mapper.Map<List<RoleSelectResVO>>(resDto);
            return res;
        }

        public async Task<List<RolePageResVO>> GetRoleListAnyCondition(RoleListInternalReqDTO req)
        {
            var resDto = await roleClient.GetRoleListAnyCondition(req);
            var res = mapper.Map<List<RolePageResVO>>(resDto);
            return res;
        }

        public async Task<List<RoleAuthorityVO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdVO req)
        {
            var reqDto = mapper.Map<RoleAuthorityListReqByRoleIdDTO>(req);
            var resDto = await roleClient.GetRoleAuthorityListByRoleId(reqDto);
            var res = mapper.Map<List<RoleAuthorityVO>>(resDto);
            return res;
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}
