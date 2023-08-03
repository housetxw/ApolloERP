using System;
using System.Collections.Generic;
using System.Text;
using RedGoose.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Rg.C.MiniApp.Api.Dal.Model
{
    [Table("ShopPurchase")]
    public class PurchaseInfo
    {
        [Key]
        public int PKID { get; set; }
        [Column("shop_id")]
        public int ShopID { get; set; }
        public string Supplier { get; set; }
    }
}
