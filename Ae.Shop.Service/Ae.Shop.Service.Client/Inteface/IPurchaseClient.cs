using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IPurchaseClient
    {
        Task<List<VenderDTO>> GetVenders();
    }
}
