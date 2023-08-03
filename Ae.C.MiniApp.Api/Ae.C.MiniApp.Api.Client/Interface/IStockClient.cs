using Ae.C.MiniApp.Api.Client.Model.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IStockClient
    {
        /// <summary>
        /// 大仓库存查询
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="pidList"></param>
        /// <returns></returns>
        Task<List<WarehouseStockDto>> GetWarehouseStockForAdaptation(string provinceId,string cityId, List<string> pidList);
    }
}
