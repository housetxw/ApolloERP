using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 根据到店记录Id查询到店记录
    /// </summary>
    public class ReceiveByIdsRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [MaxLength(50, ErrorMessage = "批量查询最多支持50条")]
        [Required(ErrorMessage = "到店记录不能为空")]
        public List<long> ArriveIdList { get; set; }
    }
}
