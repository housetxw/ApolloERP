using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Filters;

namespace Ae.AccountAuthority.Service.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AuthorityController))]
    public class AuthorityController : Controller
    {
        #region 变量和构造器

        private readonly IAuthorityService authoritySvc;

        public AuthorityController(IAuthorityService authoritySvc)
        {
            this.authoritySvc = authoritySvc;
        }


        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateAuthority([FromBody] AuthorityDTO req)
        {
            var res = await authoritySvc.CreateAuthority(req);
            return res;
        }

        /// <summary>
        /// 根据Id更新权限信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateAuthorityById([FromBody] AuthorityDTO req)
        {
            var res = await authoritySvc.UpdateAuthorityById(req);
            return res;
        }

        /// <summary>
        /// 根据Id删除权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteAuthorityById([FromBody] AuthorityDTO req)
        {
            var res = await authoritySvc.DeleteAuthorityById(req);
            return res;
        }

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AuthorityPageListResDTO> GetPagedAuthorityList([FromQuery] AuthorityListReqDTO req)
        {
            var res = await authoritySvc.GetPagedAuthorityList(req);
            return res;
        }

        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityDTO>> GetAllAuthorityList()
        {
            var res = await authoritySvc.GetAllAuthorityList();
            return res;
        }

        /// <summary>
        /// 根据ApplicationId获取系统下的所有权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req)
        {
            var res = await authoritySvc.GetAuthorityListByApplicationIdAsync(req);
            return res;
        }

        /// <summary>
        /// 编辑权限时，根据ApplicationId和排除当前AuthorityId，获取系统下的所有权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req)
        {
            var res = await authoritySvc.GetAuthorityListByApplicationIdExceptIdAsync(req);
            return res;
        }

        /// <summary>
        /// 获取有效的权限信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityDTO>> GetAuthorityListByIsDeleted(AuthorityListReqDTO req)
        {
            var res = await authoritySvc.GetAuthorityListByIsDeleted(req);
            return res;
        }

        /// <summary>
        /// 根据任意条件获取权限信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition([FromQuery] AuthorityListInternalReqDTO req)
        {
            var res = await authoritySvc.GetAuthorityListAnyCondition(req);
            return res;
        }

        /// <summary>
        /// 获取有效权限的权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityPageResDTO>> GetAuthorityListWithApplicationInfo([FromQuery] AuthorityListInternalReqDTO req)
        {
            var res = await authoritySvc.GetAuthorityListWithApplicationInfo(req);
            return res;
        }

        /// <summary>
        /// 根据AuthorityId和ApplicationId，获取系统下当前权限及其所有上级
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req)
        {
            var res = await authoritySvc.GetParentAuthorityListByAppIdAndAuthIdAsync(req);
            return res;
        }

        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法

    }
}