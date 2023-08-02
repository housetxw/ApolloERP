using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    public class OePartCodeDetailDto
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
        public List<ParCodeDetailDto> PartCodes { get; set; } = new List<ParCodeDetailDto>();
    }

    public class ParCodeDetailDto
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
