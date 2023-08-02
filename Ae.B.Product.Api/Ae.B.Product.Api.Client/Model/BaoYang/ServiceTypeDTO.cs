using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    public class ServiceTypeDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 显示中文
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// icon图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string RouteUrl { get; set; }
    }
}
