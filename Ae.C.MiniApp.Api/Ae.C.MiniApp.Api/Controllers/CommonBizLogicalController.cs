using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Util.TypeConverters;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Model.BaoYang;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(CommonBizLogicalController))]
    public class CommonBizLogicalController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------


        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 获取所有的服务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<ServiceTypeEnumVo>> GetServiceTypeList()
        {
            return Result.Success((from OrderTypeEnum item in Enum.GetValues(typeof(OrderTypeEnum))
                                   select new ServiceTypeEnumVo
                                   {
                                       Id = Convert.ToInt32(item),
                                       DisplayName = item.GetDescription(),
                                       ServiceType = item.ToString()
                                   }).ToList());
        }



        // ---------------------------------- 私有方法 --------------------------------------


    }
}