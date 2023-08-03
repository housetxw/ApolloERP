﻿using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IFranchisesConfigRepository : IRepository<FranchisesConfigDO>
    {
        Task<List<FranchisesConfigDO>> GetFranchises(GetFranchisesConfigRequest request);
    }
}
