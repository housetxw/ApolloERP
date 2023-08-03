using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model
{
    /// <summary>
    /// 配件类型对应适配商品
    /// </summary>
    public class PartProductRefDTO
    {
        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public List<string> BrandList { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string Pid { get; set; }
    }
}
