using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.AccountAuthority.Service.Client.Interface.ShopServer
{
    public interface ITestClient
    {
        Task<bool> UpdateShopInfo();

    }

}
