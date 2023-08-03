using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 车系搜搜
    /// </summary>
    public class VehicleSearchByNameRequest : BaseGetRequest
    {
        /// <summary>
        /// 搜索名
        /// </summary>
        [Required(ErrorMessage = "搜索条件不能为空")]
        public string Name { get; set; }
    }
}
