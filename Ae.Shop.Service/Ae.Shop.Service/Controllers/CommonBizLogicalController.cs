using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.Company;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class CommonBizLogicalController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<CommonBizLogicalController> logger;
        private readonly string className;

        private readonly IMapper mapper;
        private readonly IEmployeeService empSvc;
        private readonly ICompanyService compSvc;

        public CommonBizLogicalController(ApolloErpLogger<CommonBizLogicalController> logger,
            IMapper mapper,
            IEmployeeService empSvc,
            ICompanyService compSvc)
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.mapper = mapper;
            this.empSvc = empSvc;
            this.compSvc = compSvc;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 获取技师分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<TechnicianPageDTO>> GetTechnicianPage([FromQuery] TechnicanPageReqDTO req)
        {
            var res = await empSvc.GetTechnicianPage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 获取员工分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<TechnicianPageDTO>> GetShopEmployeeByJobIdPage([FromBody] TechnicanPageReqDTO req)
        {
            var res = await empSvc.GetTechnicianPage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }


        /// <summary>
        /// 根据员工类型，获取所有公司和门店简单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UnitDTO>>> GetUnitsByType([FromQuery] CompanyListReqByType req)
        {
            var resDto = await compSvc.GetAllUnitAsync();
            var res = resDto?.List.FindAll(f => f.Type == (int)req.Type).ToList();
            return Result.Success(res);
        }

        /// <summary>
        /// 根据员工类型，获取所有公司和门店简单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UnitSelDTO>>> GetUnitsForSelByType([FromQuery] CompanyListReqByType req)
        {
            var resDto = await compSvc.GetAllUnitAsync();
            int type = (int)req.Type;
            List<UnitDTO> res;
            //if (type == 0)
            //{
            //    res= resDto?.List.FindAll(f => f.Type !=1&&f.Type!=2).ToList();
            //}
            //else
            //{
            //    res = resDto?.List.FindAll(f => f.Type == (int)req.Type).ToList();
            //}

            res = resDto?.List.FindAll(f => f.Type == (int)req.Type).ToList();


            return Result.Success(mapper.Map<List<UnitSelDTO>>(res));
        }


    }
}