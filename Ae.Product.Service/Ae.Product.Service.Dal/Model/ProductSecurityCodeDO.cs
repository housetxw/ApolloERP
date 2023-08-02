using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{
    [Table("product_security_code")]
    public class ProductSecurityCodeDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("security_code")]
        public string SecurityCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("route_url")]
        public string RouteUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("search_count")]
        public int SearchCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("first_search_time")]
        public DateTime? FirstSearchTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
