using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IFAQService
    {
        /// <summary>
        /// 查询FAQ列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<FaqDTO>> GetFAQListAsync(GetFAQListRequest request);
        /// <summary>
        /// 查询FAQ详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<FaqDTO> GetFAQInfoAsync(GetFAQInfoRequest request);
        /// <summary>
        /// 查询FAQ渠道列表
        /// </summary>
        /// <returns></returns>
        Task<List<FaqChannelDTO>> GetFaqChannelListAsync();
        /// <summary>
        /// 查询FAQ分类列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<FaqCategoryDTO>> GetFaqCategoryListAsync(GetFaqCategoryListByIdRequest request);
        /// <summary>
        /// 新增/修改FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ModifyFAQAsync(ModifyFAQRequest request);
        /// <summary>
        /// 删除FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteFAQByIdAsync(GetFAQInfoRequest request);
    }
}
