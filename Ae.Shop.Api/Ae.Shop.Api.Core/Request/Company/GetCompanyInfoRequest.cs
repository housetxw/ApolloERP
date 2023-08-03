using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Company
{
    public class GetCompanyInfoRequest
    {
        /// <summary>
        /// 公司id
        /// </summary>
        [Required(ErrorMessage = "公司ID不能为空")]
        public long Id { get; set; }
    }
}
