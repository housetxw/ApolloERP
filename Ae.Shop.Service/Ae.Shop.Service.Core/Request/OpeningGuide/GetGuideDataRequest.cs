using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.OpeningGuide
{
    /// <summary>
    /// 引导页请求
    /// </summary>
    public class GetGuideDataRequest : BaseGetRequest
    {
        /// <summary>
        /// 不需要传输
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 保存步骤 
        /// </summary>
        [Required]
        public OpeningGuideStepEnum Step { get; set; }
    }
}
