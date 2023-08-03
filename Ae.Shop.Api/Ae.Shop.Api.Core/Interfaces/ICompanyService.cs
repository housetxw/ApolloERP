using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.Company;
using Ae.Shop.Api.Core.Request.Company;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddCompanyAsync(AddCompanyRequest request);
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCompanyAsync(AddCompanyRequest request);
        /// <summary>
        /// 根据公司ID查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CompanyDTO> GetCompanyInfoAsync(GetCompanyInfoRequest request);

        /// <summary>
        /// 查询二级公司列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request);

        /// <summary>
        /// 查询区域合伙列表 - 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPartnershipCompanyListAsync(GetPageCompanyListRequest request);


    }
}
