using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Sign
{
    /// <summary>
    /// 签收请求对象
    /// </summary>
    public class SignRequest
    {
        /// <summary>
        /// 签收的内容
        /// </summary>
        [Required]
        public List<string> Content { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        [Required]
        public string SignUser { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 签收类型(0 默认 1：门店向小仓下单签收)
        /// </summary>
        public int SignType { get; set; }
    }


}
