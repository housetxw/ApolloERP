using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model.OpeningGuide;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopImgRepository: IRepository<ShopImgDO>
    {
        Task<List<ShopImgDO>> GetImgsByShopIdAsync(long shopId);
        /// <summary>
        /// 删除门店照片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<int> DeleteShopImg(long shopId, string updateBy);

        /// <summary>
        /// 更新门店图片信息For 开店指导
        /// </summary>
        /// <param name="shopBaseInfo"></param>
        /// <returns></returns>
        Task<int> UpdateShopImgInfoForOpeningGuide(List<int> shopImgsType,long shopId,string updateBy);

        Task<List<ShopImgDO>> GetShopImagesByType(List<long> shopId, int type);
    }
}
