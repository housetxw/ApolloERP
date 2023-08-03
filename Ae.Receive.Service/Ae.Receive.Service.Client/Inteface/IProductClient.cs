using Ae.Receive.Service.Client.Model.Product;
using Ae.Receive.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IProductClient
    {
        Task<List<ProductActivityDTO>> GetProductActivityByOrderNos(ProductActivityByOrderNosClientRequest request);
    }
}
