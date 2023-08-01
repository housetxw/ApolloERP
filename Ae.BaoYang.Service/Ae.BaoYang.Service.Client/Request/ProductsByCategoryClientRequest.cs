using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Request
{
    public class ProductsByCategoryClientRequest
    {
        public int CategoryId { get; set; }

        public bool HasAttribute { get; set; }

        public int? IsOnSale { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 是否包含安装服务
        /// </summary>
        public bool HasInstallService { get; set; } = false;
    }
}
