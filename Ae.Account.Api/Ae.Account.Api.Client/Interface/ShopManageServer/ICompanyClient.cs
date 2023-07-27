using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface
{
    public interface ICompanyClient
    {
        Task<List<UnitDTO>> GetAllOrganizationListAsync();

    }

}
