using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class ReservedListRequest : BasePageRequest
    {
        /// <summary>
        /// 类型  1已预约  2已完成
        /// </summary>
        public int Type { get; set; }
    }
}
