using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AuthorityService : IAuthorityService
    {
        #region 变量和构造器

        private readonly IMapper mapper;

        private readonly IAuthorityRepository authorityRepo;

        public AuthorityService(IMapper mapper, IAuthorityRepository authorityRepo)
        {
            this.mapper = mapper;
            this.authorityRepo = authorityRepo;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateAuthority(AuthorityDTO req)
        {
            var reqDo = mapper.Map<AuthorityDO>(req);
            var res = await authorityRepo.CreateAuthority(reqDo, req.ParentIdList);
            return res;
        }

        public async Task<bool> UpdateAuthorityById(AuthorityDTO req)
        {
            var reqDo = mapper.Map<AuthorityDO>(req);
            var res = await authorityRepo.UpdateAuthorityById(reqDo);
            return res;
        }

        public async Task<bool> DeleteAuthorityById(AuthorityDTO req)
        {
            var reqDo = mapper.Map<AuthorityDO>(req);
            var res = await authorityRepo.DeleteAuthorityById(reqDo, req.ParentIdList);
            return res;
        }

        public async Task<AuthorityPageListResDTO> GetPagedAuthorityList(AuthorityListReqDTO req)
        {
            var res = new AuthorityPageListResDTO();

            var resDo = await authorityRepo.GetPagedAuthorityList(req);

            if (null == resDo)
            {
                return res;
            }

            res.AuthorityList = mapper.Map<List<AuthorityPageResDTO>>(resDo.Items);
            res.TotalItems = resDo.TotalItems;
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAllAuthorityList()
        {
            var resDo = await authorityRepo.GetAllAuthorityList();
            var res = mapper.Map<List<AuthorityDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListByApplicationIdAsync(req);
            var res = mapper.Map<List<AuthorityDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListByApplicationIdExceptIdAsync(req);
            var res = mapper.Map<List<AuthorityDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAuthorityListByIsDeleted(AuthorityListReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListByIsDeleted(req);
            var res = mapper.Map<List<AuthorityDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListAnyCondition(req);
            var res = mapper.Map<List<AuthorityPageResDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityPageResDTO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListWithApplicationInfo(req);
            var res = mapper.Map<List<AuthorityPageResDTO>>(resDo);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req)
        {
            var resDo = await authorityRepo.GetAuthorityListByApplicationIdAsync(req);
            var resDto = mapper.Map<List<AuthorityDTO>>(resDo);
            var result = new List<AuthorityDTO>();
            GetParentAuthorityList(req.ParentId, resDto, ref result);
            return result;
        }

        #endregion 接口实现

        #region 私有方法

        private void GetParentAuthorityList(long pId, IReadOnlyCollection<AuthorityDTO> authorityList, ref List<AuthorityDTO> result)
        {
            var curAuthority = authorityList.FirstOrDefault(f => f.Id == pId);
            result.Add(curAuthority);

            var parAuthority = authorityList.FirstOrDefault(f => f.Id == curAuthority?.ParentId);

            if (null != parAuthority)
            {
                result.Add(parAuthority);
                if (parAuthority?.ParentId > 0)
                {
                    GetParentAuthorityList(parAuthority.ParentId, authorityList, ref result);
                }
            }
        }


        #endregion 私有方法

    }
}
