using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("tbl_dictionary")]
    public class TblDictionaryDO
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Column("dic_type")]
        public string DicType { get; set; } = string.Empty;
        /// <summary>
        /// key
        /// </summary>
        [Column("dic_key")]
        public string DicKey { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Column("dic_value")]
        public string DicValue { get; set; } = string.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        [Column("row_versions")]
        public DateTime RowVersions { get; set; } = new DateTime(1900, 1, 1);
    }
}