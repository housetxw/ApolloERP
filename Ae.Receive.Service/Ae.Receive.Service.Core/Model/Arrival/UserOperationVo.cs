using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    public class UserOperationVo
    {
        /// <summary>
        /// 操作
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 用户可执行操作中文名
        /// </summary>
        public string ShowFunctionName { get; set; }

        /// <summary>
        /// 展示的序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否突出
        /// </summary>
        public bool IsImportance { get; set; }
    }
}
