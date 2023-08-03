using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
   public class GetAccountCheckLogRequest
    {
        /// <summary>
        /// 关联单号
        /// </summary>
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联类型
        /// </summary>
        public string RelationType { get; set; } = string.Empty;
    }
}
