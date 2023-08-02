using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    public class BaoYangPartProductDto
    {
        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartType { get; set; }

        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 关联时间
        /// </summary>
        public string RelateTime { get; set; }

        /// <summary>
        /// 关联人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
    }
}
