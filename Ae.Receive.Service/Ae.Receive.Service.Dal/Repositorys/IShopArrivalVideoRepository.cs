using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IShopArrivalVideoRepository : IRepository<ShopArrivalVideoDO>
    {
        Task<List<ShopArrivalVideoDO>> GetShopArrivalVideos(ShopArrivalVideoRequest request);

        Task<bool> UpdateShopArrivalVideo(DeleteShopArrivalVideoRequest request);
    }
}
