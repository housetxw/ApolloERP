using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class OePartCodeDetailVo
    {
        /// <summary>
        /// OE件号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartType { get; set; }

        /// <summary>
        /// 车型品牌
        /// </summary>
        public List<string> VehicleBrand { get; set; } = new List<string>();

        /// <summary>
        /// 配件号
        /// </summary>
        public List<ParCodeDetailVo> PartCodes { get; set; } = new List<ParCodeDetailVo>();
    }

    /// <summary>
    /// 配件号
    /// </summary>
    public class ParCodeDetailVo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }
    }
}
