using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("bao_yang_parts_liyang")]
    public class BaoYangPartsLiyangDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("level_id")]
        public string LevelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("part_name")]
        public string PartName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("part_code")]
        public string PartCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("product_series")]
        public string ProductSeries { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("product_model")]
        public string ProductModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("product_remark")]
        public string ProductRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("vehicle_remark")]
        public string VehicleRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("installation_site")]
        public string InstallationSite { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("drying_time")]
        public string DryingTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("recommend_term")]
        public string RecommendTerm { get; set; }
    }
}
