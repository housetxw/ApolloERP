using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Account.Api.Core.Model
{
    public class ApplicationVO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "请输入系统名称")]
        [Display(Name = "系统名称")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "请输入系统简称")]
        [Display(Name = "系统简称")]
        public string Initialism { get; set; }

        [Required(ErrorMessage = "请输入系统域名")]
        [Display(Name = "系统域名")]
        public string Route { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
