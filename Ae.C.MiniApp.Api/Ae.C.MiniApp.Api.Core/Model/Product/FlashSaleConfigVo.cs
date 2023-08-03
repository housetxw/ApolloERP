using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class FlashSaleConfigVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime ActiveStartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime ActiveEndTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        public string Image1 { get; set; }

        public decimal SalesPrice { get; set; } = 0;

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        public string Description { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

    }
}
