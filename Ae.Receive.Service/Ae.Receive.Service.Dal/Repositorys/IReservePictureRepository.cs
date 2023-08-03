using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IReservePictureRepository:IRepository<ReservePictureDO>
    {
        Task<List<ReservePictureDO>> GetReservePictureList(long reserveId);
    }
}
