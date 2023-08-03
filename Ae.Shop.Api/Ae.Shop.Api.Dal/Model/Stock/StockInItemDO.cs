using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("stock_in_item")]
    public class StockInItemDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 入库单Id
        /// </summary>
        [Column("stock_in_id")]
        public long StockInId { get; set; }
        /// <summary>
        /// 外联批次单号
        /// </summary>
        [Column("ref_batch_no")]
        public string RefBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的Code
        /// </summary>
        [Column("brand_code")]
        public string BrandCode { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的名称
        /// </summary>
        [Column("brand_name")]
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类Code
        /// </summary>
        [Column("first_category_code")]
        public string FirstCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类名称
        /// </summary>
        [Column("first_category_name")]
        public string FirstCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类Code
        /// </summary>
        [Column("second_category_code")]
        public string SecondCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类名称
        /// </summary>
        [Column("second_category_name")]
        public string SecondCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类Code
        /// </summary>
        [Column("third_category_code")]
        public string ThirdCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类名称
        /// </summary>
        [Column("third_category_name")]
        public string ThirdCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类Code
        /// </summary>
        [Column("four_category_code")]
        public string FourCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类名称
        /// </summary>
        [Column("four_category_name")]
        public string FourCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类code
        /// </summary>
        [Column("five_category_code")]
        public string FiveCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类名称
        /// </summary>
        [Column("five_category_name")]
        public string FiveCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 供应商Id
        /// </summary>
        [Column("supplier_id")]
        public string SupplierId { get; set; } = string.Empty;
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 货主id
        /// </summary>
        [Column("own_id")]
        public long OwnId { get; set; }
        /// <summary>
        /// 货主类型 (仓库、门店、供应商(Company,Shop))
        /// </summary>
        [Column("own_type")]
        public string OwnType { get; set; } = string.Empty;
        /// <summary>
        /// 周期
        /// </summary>
        [Column("week_year")]
        public string WeekYear { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        [Column("num")]
        public long Num { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        [Column("freight")]
        public decimal Freight { get; set; }
        /// <summary>
        /// 入库成本
        /// </summary>
        [Column("cost")]
        public decimal Cost { get; set; }
        /// <summary>
        /// 单位id
        /// </summary>
        [Column("uom_id")]
        public string UomId { get; set; } = string.Empty;
        /// <summary>
        /// 单位文本
        /// </summary>
        [Column("uom_text")]
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 入库产品的属性（良品，不良品）
        /// </summary>
        [Column("product_attr_type")]
        public string ProductAttrType { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }

}
