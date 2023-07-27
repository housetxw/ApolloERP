using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using ApolloErp.Login.Auth;

namespace Ae.Account.Api.Imp.Services
{
    public class AuthorityService : IAuthorityService
    {
        #region 变量和构造器

        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IAuthorityClient authorityClient;

        public AuthorityService(IMapper mapper,
            IIdentityService identitySvc, IAuthorityClient authorityClient)
        {
            this.mapper = mapper;
            this.identitySvc = identitySvc;
            this.authorityClient = authorityClient;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateAuthority(AuthorityVO req)
        {
            var reqDto = mapper.Map<AuthorityDTO>(req);
            var res = await authorityClient.CreateAuthority(reqDto);
            return res;
        }

        public async Task<bool> UpdateAuthorityById(AuthorityVO req)
        {
            var reqDto = mapper.Map<AuthorityDTO>(req);
            var res = await authorityClient.UpdateAuthorityById(reqDto);
            return res;
        }

        public async Task<bool> DeleteAuthorityById(AuthorityVO req)
        {
            var reqDto = mapper.Map<AuthorityDTO>(req);
            var res = await authorityClient.DeleteAuthorityById(reqDto);
            return res;
        }

        public async Task<AuthorityPageListResVO> GetPagedAuthorityList(AuthorityListReqVO req)
        {
            var reqDto = mapper.Map<AuthorityListReqDTO>(req);
            var resDto = await authorityClient.GetPagedAuthorityList(reqDto);
            var res = mapper.Map<AuthorityPageListResVO>(resDto);
            return res;
        }

        public async Task<List<AuthoritySelectResVO>> GetAllAuthoritySelectList()
        {
            var resDto = await authorityClient.GetAllAuthorityList();
            var res = mapper.Map<List<AuthoritySelectResVO>>(resDto);
            return res;
        }

        public async Task<List<AuthoritySelectResVO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqVO req)
        {
            var reqDto = mapper.Map<AuthorityListReqDTO>(req);
            var resDto = await authorityClient.GetAuthorityListByApplicationIdAsync(reqDto);
            var res = mapper.Map<List<AuthoritySelectResVO>>(resDto);
            return res;
        }

        public async Task<List<AuthoritySelectResVO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqVO req)
        {
            var reqDto = mapper.Map<AuthorityListReqDTO>(req);
            var resDto = await authorityClient.GetAuthorityListByApplicationIdExceptIdAsync(reqDto);
            var res = mapper.Map<List<AuthoritySelectResVO>>(resDto);
            return res;
        }

        public async Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req)
        {
            var res = await authorityClient.GetAuthorityListAnyCondition(req);
            return res;
        }

        public async Task<List<AuthorityTreeVO>> GetAuthorityTreeWithApplicationInfo()
        {
            Int32.TryParse(identitySvc.GetOrganizationId(), out var shopId);
            var orgType = identitySvc.GetOrgType();//0-公司，1-门店

            var req = new AuthorityListInternalReqDTO();
            if (shopId <= 1)
            {
            }
            else
            {
                req.Initialism = orgType == "0" ? "Company" : "Shop";
            }
            var resDto = await authorityClient.GetAuthorityListWithApplicationInfo(req);
            var res = mapper.Map<List<AuthorityTreeVO>>(resDto);
            return BuildAuthorityTree(res);
        }

        public async Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req)
        {
            var reqDto = mapper.Map<AuthorityListReqDTO>(req);
            var res = await authorityClient.GetParentAuthorityListByAppIdAndAuthIdAsync(reqDto);
            return res;
        }


        #endregion 接口实现

        #region 私有方法

        #region 构建前端权限树相关的方法

        /// <summary>
        /// 构建前端所需要树结构
        /// TODO: 前期由开发人员配置角色权限，暂时不区分所属单位，权限；
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<AuthorityTreeVO> BuildAuthorityTree(List<AuthorityTreeVO> list)
        {
            var tree = new List<AuthorityTreeVO>();

            var topAuthorityList = list.FindAll(f => f.ParentId == 0);

            var appGroupList = topAuthorityList.Distinct().GroupBy(g => g.ApplicationId).Select(g => g.ToList()).ToList();

            //遍历所有系统下的所有权限
            appGroupList?.ForEach(f =>
            {
                AuthorityTreeVO topTreeAppInfo = new AuthorityTreeVO();

                //遍历同一系统下的所有权限
                for (var i = 0; i < f.Count; i++)
                {
                    var authVo = f[i];

                    //确保系统信息只加入权限树中一次
                    if (i == 0)
                    {
                        topTreeAppInfo.Id = authVo.ApplicationId;
                        topTreeAppInfo.Name = authVo.ApplicationName;
                    }

                    if (authVo.ParentId == 0)
                    {
                        RecursionAuthority(list, authVo);

                        topTreeAppInfo.Children.Add(authVo);
                    }
                }

                tree.Add(topTreeAppInfo);
            });

            return tree;
        }

        private void RecursionAuthority(List<AuthorityTreeVO> authorityList, AuthorityTreeVO authority)
        {
            //获取子一级列表
            var childrenList = GetChildrenAuthorityList(authorityList, authority);
            authority.Children = childrenList;

            childrenList.ForEach(a =>
            {
                if (HasChildren(authorityList, a.Id))
                {
                    RecursionAuthority(authorityList, a);
                }
            });
        }

        /// <summary>
        /// 得到子节点列表
        /// </summary>
        /// <param name="authorityList"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        private List<AuthorityTreeVO> GetChildrenAuthorityList(List<AuthorityTreeVO> authorityList, AuthorityTreeVO authority)
        {
            return authorityList.FindAll(f => f.ParentId == authority.Id);
        }

        /// <summary>
        /// 判断是否有子节点
        /// </summary>
        /// <param name="authorityList"></param>
        /// <param name="authorityId"></param>
        /// <returns></returns>
        private bool HasChildren(List<AuthorityTreeVO> authorityList, long authorityId)
        {
            return authorityList.Any(a => a.ParentId == authorityId);
        }

        #endregion 构建前端权限树相关的方法


        #endregion 私有方法

    }
}
