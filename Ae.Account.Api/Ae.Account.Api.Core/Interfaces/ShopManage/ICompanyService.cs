using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Core.CommonModel;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<List<OrganizationSel>> GetAllOrganizationSelectListAsync();

        Task<List<OrganizationSel>> GetAllOrganizationExceptShopSelectListAsync();

    }
}
