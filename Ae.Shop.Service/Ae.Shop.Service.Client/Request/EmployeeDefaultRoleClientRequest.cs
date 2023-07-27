using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class EmployeeDefaultRoleClientRequest
    {
        /// <summary>
        /// 操作者EmployeeId
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 操作者中文名称
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public int Type { get; set; }
    }
}
