using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class ScanCodeRequest
    {
        /// <summary>
        /// 核销码或安装码字符串
        /// </summary>
        [Required(ErrorMessage = "核销码或安装码不可为空")]
        public string Code { get; set; }
        /// <summary>
        /// 选中的码渠道键
        /// </summary>
        public string ChannelKey { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
    }
}
