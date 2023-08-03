using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model.Shop
{
    public class BaseServiceDTO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 服务PID
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 英文CODE
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
