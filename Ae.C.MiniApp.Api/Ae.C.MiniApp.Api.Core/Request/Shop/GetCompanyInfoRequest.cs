using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class GetCompanyInfoRequest : BaseGetRequest
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "公司ID必须大于0")]
        public long CompanyId { get; set; }
    }
}
