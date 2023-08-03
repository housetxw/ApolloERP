using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("fct_shop_product")]
    public class FctShopProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        ///  产品编码
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 市场价格
        /// </summary>
        [Column("standard_price")]
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [Column("sales_price")]
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 1 置顶  2 按照上架时间顺序
        /// </summary>
        [Column("sort_type")]
        public int SortType { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        [Column("is_top")]
        public int IsTop { get; set; }
        /// <summary>
        /// 上下架
        /// </summary>
        [Column("on_sale")]
        public int OnSale { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        [Column("on_sale_time")]
        public DateTime OnSaleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 缩率图
        /// </summary>
        [Column("icon")]
        public string Icon { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 引用服务PId
        /// </summary>
        [Column("ref_pid")]
        public int RefPid { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        [Column("detail")]
        public string Detail { get; set; } = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Column("apply_time")]
        public DateTime ApplyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("audit_time")]
        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        [Column("audit_status")]
        public sbyte AuditStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("audit_user")]
        public string AuditUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核意见
        /// </summary>
        [Column("audit_opinion")]
        public string AuditOpinion { get; set; } = string.Empty;
        /// <summary>
        /// 是否门店外采商品 1 是 0 否 默认 0
        /// </summary>
        [Column("is_storeoutside")]
        public int IsStoreoutside { get; set; }
    }
}