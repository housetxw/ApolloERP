﻿using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Employee
{
    public interface IEmployeeGroupRepository : IRepository<EmployeeGroupDO>
    {
        Task<List<EmployeeGroupDTO>> GetEmployeeGroupList(GetEmployeeGroupListRequest request);
    }
}
