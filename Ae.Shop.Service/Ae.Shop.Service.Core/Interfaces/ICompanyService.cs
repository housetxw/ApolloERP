using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Model.Company;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.Company;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Response.OpeningGuide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    /// <summary>
    /// 公司管理
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> AddCompanyAsync(CompanyDTO request);

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> AddCompanyReturnIdAsync(CompanyDTO request);

        /// <summary>
        /// 注册公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AddCompanyRegisterResponse> AddCompanyRegister(AddCompanyRegisterRequest request);

        /// <summary>
        /// 查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CompanyDTO> GetCompanyInfoByIdAsync(GetCompanyInfoByIdRequest request);

        /// <summary>
        /// 查询公司列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<CompanyPageListForShopDTO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request);
        /// <summary>
        /// 获取所以公司和门店简单信息
        /// </summary>
        /// <returns></returns>
        Task<GetAllUnitResponse> GetAllUnitAsync();
        /// <summary>
        /// 根据公司ID查询公司信息---微信小程序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CompanySimpleInfoDTO> MiniGetCompanyInfo(MiniGetCompanyInfoRequest request);

        /// <summary>
        /// 根据门店ID查询公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CompanyInfoDTO> GetCompanyInfoByShopId(GetCompanyByShopIdRequest request);
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCompanyAsync(CompanyDTO request);

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCompanyForSupplierAsync(CompanyDTO request);
        /// <summary>
        /// 查询公司下的子公司和门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CompanyLessInfoDTO>> GetCompanyAndShopByParentId(GetCompanyInfoByIdRequest request);
        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCompanyStatus(UpdateCompanyStatusRequest request);

        /// <summary>
        /// OperateCompanyDeposit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> OperateCompanyDeposit(OperateCompanyDepositRequest request);

        Task<List<VenderDTO>> GetVenders();

        Task<ApiResult<List<CompanyPageListForShopDTO>>> GetCompanySubs(GetPageCompanyListRequest request);

        Task<List<CompanyDTO>> GetFirstCompanyList(GetFirstCompanyListRequest request);
    }
}
