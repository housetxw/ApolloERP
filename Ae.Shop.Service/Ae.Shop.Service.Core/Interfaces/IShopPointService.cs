using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    /// <summary>
    /// 门店积分
    /// </summary>
    public interface IShopPointService
    {
        /// <summary>
        /// 获取门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopPointVo> GetShopPoint(ShopPointRequest request);

        /// <summary>
        /// 操作门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddShopPointRecord(AddShopPointRecordRequest request);
    }
}
