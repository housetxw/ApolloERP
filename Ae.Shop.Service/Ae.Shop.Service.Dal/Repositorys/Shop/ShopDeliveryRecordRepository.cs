﻿using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class ShopDeliveryRecordRepository : AbstractRepository<ShopDeliveryRecordDO>, IShopDeliveryRecordRepository
    {
        public ShopDeliveryRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
    }
}
