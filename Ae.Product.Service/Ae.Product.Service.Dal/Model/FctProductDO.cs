using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{

    [Table("fct_product")]
    public class FctProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 类别（ 2子产品，4 父产品）
        /// </summary>
        [Column("class_type")]
        public int ClassType { get; set; }
        /// <summary>
        ///  父产品的Id
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        ///  产品编码
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 促销广告语
        /// </summary>
        [Column("advertisement")]
        public string Advertisement { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        ///  税率
        /// </summary>
        [Column("tax_rate")]
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 关联类目Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
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
        /// 批发价
        /// </summary>
        [Column("wholesale_price")]
        public decimal WholesalePrice { get; set; }

        /// <summary>
        /// 返现价
        /// </summary>
        [Column("return_cash")]
        public decimal ReturnCash { get; set; }

        /// <summary>
        /// 门店结算价
        /// </summary>
        [Column("settlement_price")]
        public decimal SettlementPrice { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        ///  长
        /// </summary>
        [Column("length")]
        public decimal Length { get; set; }
        /// <summary>
        ///  宽
        /// </summary>
        [Column("width")]
        public decimal Width { get; set; }
        /// <summary>
        ///  高
        /// </summary>
        [Column("height")]
        public decimal Height { get; set; }
        /// <summary>
        ///  产品重量
        /// </summary>
        [Column("weight")]
        public decimal Weight { get; set; }
        /// <summary>
        ///  颜色
        /// </summary>
        [Column("color")]
        public string Color { get; set; } = string.Empty;
        /// <summary>
        /// 主图连接
        /// </summary>
        [Column("image1")]
        public string Image1 { get; set; } = string.Empty;
        /// <summary>
        /// 图片链接2
        /// </summary>
        [Column("image2")]
        public string Image2 { get; set; } = string.Empty;
        /// <summary>
        /// 图片链接3
        /// </summary>
        [Column("image3")]
        public string Image3 { get; set; } = string.Empty;
        /// <summary>
        /// 图片链接4
        /// </summary>
        [Column("image4")]
        public string Image4 { get; set; } = string.Empty;
        /// <summary>
        /// 图片链接5
        /// </summary>
        [Column("image5")]
        public string Image5 { get; set; } = string.Empty;
        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        [Column("part_code")]
        public string PartCode { get; set; } = string.Empty;
        /// <summary>
        /// 通用 - 配件编号 （用于保养品）
        /// </summary>
        [Column("part_no")]
        public string PartNo { get; set; } = string.Empty;
        /// <summary>
        ///  推荐位置（填写>=1的整数，0为不推荐），用于搜索结果
        /// </summary>
        [Column("product_refer")]
        public int ProductRefer { get; set; }
        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        [Column("product_attribute")]
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        ///  上下架
        /// </summary>
        [Column("on_sale")]
        public sbyte OnSale { get; set; }
        /// <summary>
        /// 是否缺货
        /// </summary>
        [Column("stockout")]
        public sbyte Stockout { get; set; }
        /// <summary>
        ///  是否显示  -  0 否 1 是 用于控制产品是否在搜索及产品列表页显示。
        /// </summary>
        [Column("is_show")]
        public sbyte IsShow { get; set; }
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        [NotGenWhereCondition]
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 是否门店外采商品 1 是 0 否 默认 0
        /// </summary>
        [Column("is_storeoutside")]
        public sbyte IsStoreoutside { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_number")]
        public string ShopNumber { get; set; } = string.Empty;
        /// <summary>
        ///  是否零售 0 否 1 是
        /// </summary>
        [Column("is_retail")]
        public sbyte IsRetail { get; set; }
        /// <summary>
        ///  是否有保险 0 否 1 是
        /// </summary>
        [Column("is_insurance")]
        public sbyte IsInsurance { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 评分
        /// </summary>
        [Column("score")]
        public decimal Score { get; set; }
        /// <summary>
        ///  销量
        /// </summary>
        [Column("sales")]
        public int Sales { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        [Column("favorable_rate")]
        public decimal FavorableRate { get; set; }
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
    }
}