using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// FrontCategoryPageListRequest
    /// </summary>
    public class FrontCategoryPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType? TerminalType { get; set; }
    }
}
