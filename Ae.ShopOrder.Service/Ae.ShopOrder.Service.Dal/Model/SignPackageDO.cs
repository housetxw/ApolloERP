using System;
using System.Collections.Generic;
using System.Text;
using MySqlX.XDevAPI.Relational;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.ShopOrder.Service.Dal.Model
{
    public  class SignPackageDO
    {
        /// <summary>
        /// 关联单号
        /// </summary>
        public string RelationNo { get; set; }

        /// <summary>
        /// 签收状态
        /// </summary>
        public sbyte SignStatus { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        public string PackageNo { get; set; }

        
    }
}
