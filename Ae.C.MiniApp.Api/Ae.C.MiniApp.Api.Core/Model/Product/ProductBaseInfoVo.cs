using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    public class ProductBaseInfoVo
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } 
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 主图连接
        /// </summary>
        public string Image1 { get; set; }

        /// <summary>
        ///  市场价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentNumber { get; set; }

        /// <summary>
        ///  好评率
        /// </summary>
        public decimal FavorableRate { get; set; }

        /// <summary>
        /// 是否原厂
        /// </summary>
        public bool OriginalFactory { get; set; }

       
    }
}
