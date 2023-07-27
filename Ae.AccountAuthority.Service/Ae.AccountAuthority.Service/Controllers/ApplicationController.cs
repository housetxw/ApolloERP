using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Filters;

namespace Ae.AccountAuthority.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(ApplicationController))]
    public class ApplicationController : Controller
    {
        #region 变量和构造器

        private readonly IApplicationService appSvc;

        public ApplicationController(IApplicationService appSvc)
        {
            this.appSvc = appSvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 新增应用
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateApplication([FromBody] ApplicationDTO req)
        {
            var res = await appSvc.CreateApplication(req);
            return res;
        }

        /// <summary>
        /// 根据Id更新应用信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateApplicationById([FromBody] ApplicationDTO req)
        {
            var res = await appSvc.UpdateApplicationById(req);
            return res;
        }

        /// <summary>
        /// 根据Id删除应用
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteApplicationById([FromBody] ApplicationDTO req)
        {
            var res = await appSvc.DeleteApplicationById(req);
            return res;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppListResDTO> GetPagedApplicationList([FromQuery] AppListReqDTO req)
        {
            var res = await appSvc.GetPagedApplicationList(req);
            return res;
        }

        /// <summary>
        /// 根据Id获取应用详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppResDTO> GetApplicationById([FromQuery] long id)
        {
            var res = await appSvc.GetApplicationById(id);
            return res;
        }

        /// <summary>
        /// 根据Name获取应用详情
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppResDTO> GetApplicationByName([FromQuery] string name)
        {
            var res = await appSvc.GetApplicationByName(name);
            return res;
        }

        /// <summary>
        /// 根据Initialism获取应用详情
        /// </summary>
        /// <param name="initialism"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppResDTO> GetApplicationByInitialism([FromQuery] string initialism)
        {
            var res = await appSvc.GetApplicationByInitialism(initialism);
            return res;
        }

        /// <summary>
        /// 根据Route获取应用详情
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppResDTO> GetApplicationByRoute([FromQuery] string route)
        {
            var res = await appSvc.GetApplicationByRoute(route);
            return res;
        }

        /// <summary>
        /// 获取应用详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AppResDTO> GetApplication([FromQuery] AppReqDTO req)
        {
            var res = await appSvc.GetApplication(req);
            return res;
        }

        /// <summary>
        /// 根据任意条件获取应用信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AppResDTO>> GetApplicationListAnyCondition([FromQuery] AppReqDTO req)
        {
            var res = await appSvc.GetApplicationListAnyCondition(req);
            return res;
        }

        /// <summary>
        /// 获取所有应用信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AppResDTO>> GetApplicationList()
        {
            var res = await appSvc.GetApplicationList();
            return res;
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法

    }
}