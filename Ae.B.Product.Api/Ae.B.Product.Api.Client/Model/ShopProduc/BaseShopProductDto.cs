using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
    public class BaseShopProductDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 1 置顶  2 按照上架时间顺序
        /// </summary>
        public int SortType { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; }
        /// <summary>
        /// 上下架
        /// </summary>
        public int OnSale { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime OnSaleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 缩率图
        /// </summary>
        public string Icon { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Detail { get; set; } = string.Empty;
      
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { get; set; }
        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public sbyte AuditStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditOpinion { get; set; } = string.Empty;
    }
}
