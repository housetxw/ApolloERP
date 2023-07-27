using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Model.OpeningGuide
{
    public class ShopConfigInfoVO
    {
        /// <summary>
        /// 营业开始时间
        /// </summary>
        public string StartWorkTime { get; set; }
        /// <summary>
        /// 营业结束时间
        /// </summary>
        public string EndWorkTime { get; set; }
        /// <summary>
        /// 是否配置
        /// </summary>
        public List<IsSetDTO> Items { get; set; }
    }
}
