using Ae.Product.Service.Client.Model.BaoYang;
using Ae.Product.Service.Client.Request.BaoYang;
using Ae.Product.Service.Client.Response.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Client.Interface
{
    public interface IBaoYangClient
    {
        /// <summary>
        /// 轮胎适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TireServiceListResponse> GetTireCategoryListAllProductAsync(TireCategoryListRequest request);

        /// <summary>
        /// 保养适配首页接口 - 返回所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageModelDto>> GetBaoYangPackagesAllProductAsync(
            GetBaoYangPackagesRequest request);

        /// <summary>
        /// 获取GetBaoYangTypeConfig
        /// </summary>
        Task<List<BaoYangTypeConfigDto>> GetBaoYangTypeConfig();

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationClientRequest request);
    }
}
