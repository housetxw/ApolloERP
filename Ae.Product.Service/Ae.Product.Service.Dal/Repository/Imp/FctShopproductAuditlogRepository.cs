﻿using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class FctShopproductAuditlogRepository : AbstractRepository<FctShopproductAuditlogDO>, IFctShopproductAuditlogRepository
    {
        public FctShopproductAuditlogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }
    }
}
