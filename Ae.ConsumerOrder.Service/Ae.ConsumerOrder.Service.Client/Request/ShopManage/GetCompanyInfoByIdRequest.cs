using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class GetCompanyInfoByIdRequest
    {
        /// <summary>
        /// 公司id
        /// </summary>
        [Required(ErrorMessage = "公司ID不能为空")]
        public long Id { get; set; }
    }
}
