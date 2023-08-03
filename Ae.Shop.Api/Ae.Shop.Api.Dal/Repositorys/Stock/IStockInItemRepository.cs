using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public interface IStockInItemRepository : IRepository<StockInItemDO>
    {

    }
}