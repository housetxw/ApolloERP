using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Enums;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;

namespace Ae.AccountAuthority.Service.Imp.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private IConfiguration configuration;
        private readonly List<string> roleNames = new List<string>();

        private readonly IEmployeeRoleRepository empRoleRepo;
        private readonly IRoleService roleSvc;
        private readonly IEmployeeRangeRoleRespository iEmployeeRangeRoleRespository;

        public EmployeeRoleService(IMapper mapper, IConfiguration configuration,
            IEmployeeRoleRepository empRoleRepo, IRoleService roleSvc, IEmployeeRangeRoleRespository iEmployeeRangeRoleRespository)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.iEmployeeRangeRoleRespository = iEmployeeRangeRoleRespository;
            this.empRoleRepo = empRoleRepo;
            this.roleSvc = roleSvc;
        }

        // ---------------------------------- 授权接口实现 --------------------------------------

        /// <summary>
        /// 授权接口：根据EmployeeId，OrganizationId和EmployeeType，获取APP其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<AuthorizationAPPResDTO> GetEmpAuthorityListForAPPByEmpIdAsync(AuthorizationReqDTO req)
        {
            var res = new AuthorizationAPPResDTO();

            var resDo = await empRoleRepo.GetEmpAuthorityListByEmpIdAsync(req);
            List<AuthorizationDO> rangRight = new List<AuthorizationDO>();
            //查询范围角色
            //类型；0：公司；1：门店；2：仓库；
            int type = (int)req.EmployeeType;
            var rangDo = await iEmployeeRangeRoleRespository.GetRangeRoleListByIds(req.EmployeeId, req.OrganizationId, type);
            if (rangDo != null && !string.IsNullOrEmpty(rangDo.RoleIds))
            {
                RangeRoleAuthorityReqDTO dt = new RangeRoleAuthorityReqDTO();
                dt.EmployeeId = req.EmployeeId;
                List<String> orders = rangDo.RoleIds?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                dt.RoleIds = new List<long>();
                orders.ForEach(o => dt.RoleIds.Add(Convert.ToInt32(o)));
                rangRight = await empRoleRepo.GetRangeRoleAuthorityListByIds(dt);
            }
            
            //把范围管辖确定的权限加到返回中
            foreach (var r in rangRight)
            {
                if (!resDo.Contains(r))
                {
                    resDo.Add(r);
                }
            }


            var moduleList = resDo.FindAll(f => f.AuthorityType == AuthorityType.Module);
            var pageList = resDo.FindAll(f => f.AuthorityType == AuthorityType.Page);
            //var btnList = resDo.FindAll(f => f.AuthorityType == AuthorityType.Button);

            moduleList.ForEach(m =>
            {
                if (m == null || m.AuthorityId <= 0) return;

                var pageChildList = pageList.FindAll(p => p.ParentId == m.AuthorityId);
                switch (m.AuthorityName)
                {
                    case nameof(res.TopMenuList):
                        //get menu authority
                        //res.TopMenuList.AddRange(mapper.Map<List<AuthorizationAuthorityAPPResDTO>>(pageChildList));
                        pageChildList.ForEach(pc =>
                        {
                            var appResDto = mapper.Map<AuthorizationAuthorityAPPResDTO>(pc);
                            res.TopMenuList.Add(pc.RouteParameter.IsNullOrWhiteSpace() ? appResDto : GenRouteParam(pc, appResDto));
                        });

                        //get button authority
                        //pageChildList.ForEach(pc =>
                        //{
                        //    if (pc != null && pc.AuthorityId > 0)
                        //    {
                        //        var btnChildList = btnList.FindAll(b => b.ParentId == pc.AuthorityId);
                        //        res.TopMenuList.AddRange(mapper.Map<List<AuthorizationAuthorityAPPResDTO>>(btnChildList));
                        //    }
                        //    res.TopMenuList.Add(mapper.Map<AuthorizationAuthorityAPPResDTO>(pc));
                        //});

                        break;
                    case nameof(res.CenterMenuList):
                        //get menu authority
                        //res.CenterMenuList.AddRange(mapper.Map<List<AuthorizationAuthorityAPPResDTO>>(pageChildList));
                        pageChildList.ForEach(pc =>
                        {
                            var appResDto = mapper.Map<AuthorizationAuthorityAPPResDTO>(pc);
                            res.CenterMenuList.Add(pc.RouteParameter.IsNullOrWhiteSpace() ? appResDto : GenRouteParam(pc, appResDto));
                        });

                        break;
                    case nameof(res.ShopManagerList):
                        //get menu authority
                        //res.CenterMenuList.AddRange(mapper.Map<List<AuthorizationAuthorityAPPResDTO>>(pageChildList));
                        pageChildList.ForEach(pc =>
                        {
                            var appResDto = mapper.Map<AuthorizationAuthorityAPPResDTO>(pc);
                            res.ShopManagerList.Add(pc.RouteParameter.IsNullOrWhiteSpace() ? appResDto : GenRouteParam(pc, appResDto));
                        });

                        break;
                }
            });

            return res;
        }
        private AuthorizationAuthorityAPPResDTO GenRouteParam(AuthorizationDO pc, AuthorizationAuthorityAPPResDTO appResDto)
        {
            appResDto.RouteParam = new Dictionary<string, string>();

            var routeList = pc.RouteParameter.Replace("，", CommonConstant.SeparatorComma)
                .Split(CommonConstant.SeparatorComma).ToList();
            routeList.ForEach(r =>
            {
                var paramList = r.Trim().Split("=").ToList();

                var paramKey = paramList.FirstOrDefault()?.Trim();

                if (paramKey.IsNullOrWhiteSpace()) return;

                if (!appResDto.RouteParam.TryAdd(paramKey, paramList.LastOrDefault()?.Trim()))
                {
                    appResDto.RouteParam[paramKey] = paramList.LastOrDefault()?.Trim();
                }
            });

            return appResDto;
        }

        /// <summary>
        /// 授权通用接口：根据EmployeeId，EmployeeType和OrganizationId，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req)
        {
            var resDo = await empRoleRepo.GetEmpAuthorityListByEmpIdAsync(req);
            //List<AuthorizationDO> rangRight=new List<AuthorizationDO>();
            ////查询范围角色
            ////类型；0：公司；1：门店；2：仓库；
            //int type = (int)req.EmployeeType;
            //var rangDo = await iEmployeeRangeRoleRespository.GetRangeRoleListByIds(req.EmployeeId,req.OrganizationId,type);
            //if(rangDo!=null && !string.IsNullOrEmpty(rangDo.RoleIds))
            //{
            //    RangeRoleAuthorityReqDTO dt = new RangeRoleAuthorityReqDTO();
            //    dt.EmployeeId = req.EmployeeId;
            //    List<String> orders = rangDo.RoleIds?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //    orders.ForEach(o => dt.RoleIds.Add(Convert.ToInt32(o)));
            //    rangRight = await empRoleRepo.GetRangeRoleAuthorityListByIds(dt);
            //}
            var res = mapper.Map<List<AuthorizationResDTO>>(resDo);
            //var raes = mapper.Map<List<AuthorizationResDTO>>(rangRight);
            ////把范围管辖确定的权限加到返回中
            //foreach(var r in raes)
            //{
            //    if(!res.Contains(r))
            //    {
            //        res.Add(r);
            //    }
            //}
            return res;
        }

        /// <summary>
        /// 授权通用接口：根据EmployeeId和RoleIds，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req)
        {
            var resDo = await empRoleRepo.GetRangeRoleAuthorityListByIds(req);
            var res = mapper.Map<List<AuthorizationResDTO>>(resDo);
            return res;
        }


        // ---------------------------------- 对外接口 --------------------------------------
        public async Task<bool> EditEmployeeRoleWithRoleId(EmployeeRoleSaveReqWithRoleIdDTO req)
        {
            var opr = string.Join("|", req.Operator, req.OperatorId);
            var reqDto = new EmployeeRoleSaveReqDO
            {
                EmployeeId = req.EmployeeId,
                EmployeeRoleList = new List<EmployeeRoleDO>(),
                Operator = opr
            };

            req.RoleIds.Distinct().ToList().ForEach(f =>
            {
                reqDto.EmployeeRoleList.Add(new EmployeeRoleDO
                {
                    EmployeeId = req.EmployeeId,
                    RoleId = f,
                    CreateBy = opr
                });
            });

            var res = await empRoleRepo.EditEmployeeRole(reqDto);
            return res;
        }

        public async Task<(bool flag, string message)> AddShopEmployeeDefaultRole(EmployeeDefaultRoleReqDTO req)
        {
            var roleList = await roleSvc.GetRoleListByName(GetDefaultRoleNames(req.Type));

            if (roleList == null || !roleList.Any()) return (true, "没有配置默认角色");

            var reqDto = mapper.Map<EmployeeRoleSaveReqWithRoleIdDTO>(req);
            reqDto.RoleIds.AddRange(roleList.Select(s => s.Id).Distinct());

            var flag = await EditEmployeeRoleWithRoleId(reqDto);
            return (flag, flag ? CommonConstant.OperateSuccess : CommonConstant.OperateFailure);
        }

        public async Task<List<EmployeeRolesDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req)
        {
            var res = await empRoleRepo.GetEmployeeRoleListByEmpIds(req);

            if (res == null || !res.Any()) return new List<EmployeeRolesDTO>();

            return res
                .GroupBy(g => g.EmployeeId)
                .Select(s => new EmployeeRolesDTO
                {
                    EmployeeId = s.Key,
                    Roles = s.ToList()
                })
                .ToList();
        }


        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req)
        {
            var resDo = await empRoleRepo.GetEmployeeRoleListByEmpIdAsync(req);
            var res = mapper.Map<List<EmployeeRoleListDTO>>(resDo);
            return res;
        }

        public async Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDTO req)
        {
            var reqDto = mapper.Map<EmployeeRoleSaveReqDO>(req);
            var res = await empRoleRepo.EditEmployeeRole(reqDto);
            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private List<string> GetDefaultRoleNames(int type)
        {
            RoleShopFeatures shoptype = (RoleShopFeatures)type;
            string configdic = shoptype.GetEnumDisplayName();
            var tmp = configuration[$"AccountAuthorityBiz:EmployeeRole:{configdic}"];
            if (tmp.IsNotNullOrWhiteSpace())
            {
                if (tmp.Replace("，", CommonConstant.SeparatorComma).Contains(CommonConstant.SeparatorComma))
                {
                    roleNames.AddRange(tmp.Split(CommonConstant.SeparatorComma).ToList());
                }
                else
                {
                    roleNames.Add(tmp);
                }
            }
            return roleNames;
        }

        private List<EmployeeRoleListDTO> GetEmployeeRoleListDTO(IGrouping<string, EmployeeRoleListDTO> req)
        {
            return req.ToList();
        }

    }
}
