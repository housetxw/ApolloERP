using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Api.Dal.Model;
using ApolloErp.Data.DapperExtensions;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IStockOutItemRepository : IRepository<StockOutItemDO>
    {

    }
}