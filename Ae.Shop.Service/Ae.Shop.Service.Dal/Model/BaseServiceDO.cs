using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("base_service")]
    public class BaseServiceDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 产品id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 英文CODE
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 默认销售价
        /// </summary>
        [Column("default_sale_price")]
        public decimal DefaultSalePrice { get; set; } 
        /// <summary>
        /// 默认成本价
        /// </summary>
        [Column("default_cost_price")]
        public decimal DefaultCostPrice { get; set; } 
        /// <summary>
        /// 默认销售价格限制
        /// </summary>
        [Column("default_sale_price_limit")]
        public decimal DefaultSalePriceLimit { get; set; } 
        /// <summary>
        /// 成本价限制
        /// </summary>
        [Column("default_cost_price_limit")]
        public decimal DefaultCostPriceLimit { get; set; } 
        /// <summary>
        /// 分类id
        /// </summary>
        [Column("category_id")]
        public long CategoryId { get; set; } 
        /// <summary>
        /// 分类类型
        /// </summary>
        [Column("category_type")]
        public string CategoryType { get; set; } = string.Empty;
        /// <summary>
        /// 服务备注
        /// </summary>
        [Column("service_remark")]
        public string ServiceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 价格备注
        /// </summary>
        [Column("sale_price_remark")]
        public string SalePriceRemark { get; set; } = string.Empty;
        /// <summary>
        /// 配件名称
        /// </summary>
        [Column("accessories_name")]
        public string AccessoriesName { get; set; } = string.Empty;
        /// <summary>
        /// 服务费
        /// </summary>
        [Column("service_charge")]
        public decimal ServiceCharge { get; set; } 
        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        public int IsDeleted { get; set; } 
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