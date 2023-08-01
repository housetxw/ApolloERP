using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_brand")]
    public class VehicleBrandDO
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("brand")]
        public string Brand { get; set; }

        [Column("brand_prefix")]
        public string BrandPrefix { get; set; }

        [Column("brand_suffix")]
        public string BrandSuffix { get; set; }

        [Column("rank")]
        public int Rank { get; set; }

        [Column("hot_level")]
        public int HotLevel { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("create_by")]
        public string CreateBy { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("update_by")]
        public string UpdateBy { get; set; }

        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
