using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.BaoYang
{
    /// <summary>
    /// 服务项目Enum
    /// </summary>
    public class ServiceTypeEnumVo
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 显示中文
        /// </summary>
        public string DisplayName { get; set; }

        ///// <summary>
        ///// 描述
        ///// </summary>
        //public string Description { get; set; }

        ///// <summary>
        ///// icon图片地址
        ///// </summary>
        //public string ImageUrl { get; set; }

        ///// <summary>
        ///// 跳转路由
        ///// </summary>
        //public string RouteUrl { get; set; }
    }
}
