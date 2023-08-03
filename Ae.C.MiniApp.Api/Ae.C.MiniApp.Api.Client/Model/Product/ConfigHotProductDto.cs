using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Product
{
    public class ConfigHotProductDto : ProductBaseInfoDto
    {
        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 热门产品主键
        /// </summary>
        public long HotProductId { get; set; }
    }
}
