using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_check_property")]
    public class ShopCheckPropertyDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("key_name")]
        public string KeyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("display_des")]
        public string DisplayDes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("rank")]
        public decimal Rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("condition")]
        public string Condition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("version_num")]
        public int VersionNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("function_des")]
        public string FunctionDes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("is_check_item_main")]
        public bool IsCheckItemMain { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Column("is_require")]
        public bool IsRequire { get; set; }

        /// <summary>
        /// 车辆对应部位
        /// </summary>
        [Column("car_components")]
        public string CarComponents { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
