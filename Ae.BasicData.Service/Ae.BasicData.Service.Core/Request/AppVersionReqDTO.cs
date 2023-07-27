using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BasicData.Service.Core.Request
{
    public class AppVersionReqDTO { }

    public class AppVersionEntityReqDTO
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [Required(ErrorMessage = "请输入APP版本号")]
        public long VersionCode { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Required(ErrorMessage = "请输入门店Id")]
        public long ShopId { get; set; }
    }


}
