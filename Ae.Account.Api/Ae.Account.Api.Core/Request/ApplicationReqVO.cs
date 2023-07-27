using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Core.Request
{
    public class ApplicationReqVO { }

    public class AppListReqVO : BasePageRequest
    {
        public long Id { get; set; }

        //public string Name { get; set; }

        public string Initialism { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AppReqVO
    {
        [Range(1, int.MaxValue, ErrorMessage = "系统Id必须大于0")]
        public long Id { get; set; }

        [Required(ErrorMessage = "请输入系统名称")]
        [Display(Name = "系统名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入系统名称")]
        [Display(Name = "系统简称")]
        public string Initialism { get; set; }

        [Required(ErrorMessage = "请输入系统域名")]
        [Display(Name = "系统域名")]
        public string Route { get; set; }
    }


}
