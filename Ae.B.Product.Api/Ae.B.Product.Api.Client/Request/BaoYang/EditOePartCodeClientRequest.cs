using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class EditOePartCodeClientRequest
    {
        /// <summary>
        /// Oe件号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 车型品牌
        /// </summary>
        public List<string> VehicleBrand { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public List<ParCodeDetailClientRequest> PartCode { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
