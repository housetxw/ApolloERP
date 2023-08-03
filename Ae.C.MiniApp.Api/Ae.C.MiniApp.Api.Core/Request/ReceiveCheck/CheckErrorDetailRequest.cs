using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ReceiveCheck
{
    public class CheckErrorDetailRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Required(ErrorMessage = "车辆Id不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 部件Code
        /// </summary>
        [Required(ErrorMessage = "部件Code不能为空")]
        public string KeyName { get; set; }
    }
}
