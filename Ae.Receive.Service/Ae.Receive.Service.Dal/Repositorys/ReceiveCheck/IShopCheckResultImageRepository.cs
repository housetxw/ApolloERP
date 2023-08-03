using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopCheckResultImageRepository:IRepository<ShopCheckResultImageDo>
    {
        /// <summary>
        /// 根据checkId查询图片
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopCheckResultImageDo>> GetCheckResultImageByCheckId(long checkId, int categoryId);
    }
}
