using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class RefreshRedisRequest
    {
        /// <summary>
        /// Key
        /// Rg:BaoYang:Service:BaoYang:*   保养配置缓存
        /// Rg:BaoYang:Service:ProductClient:*   产品缓存
        /// Rg:BaoYang:Service:BaoYang:AllOnLineService:*   门店上架服务缓存
        /// </summary>
        public string Key { get; set; }
    }
}
