using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.Company;

namespace Ae.Shop.Service.Dal.Repositorys.Company
{
    public interface ICompanyRepository : IRepository<CompanyDO>
    {
        Task<int> AddCompanyAsync(CompanyDO CompanyDO);
        /// <summary>
        /// 查询公司简单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CompanySimpleInfoDO> GetCompanyInfo(long companyId);
        /// <summary>
        /// 查询公司列表-分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<CompanyDO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request);
        /// <summary>
        /// 获取所有公司数据
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<CompanyDO>> GetAllCompanyAsync();
        /// <summary>
        /// 根据门店ID查询公司信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<CompanySimpleInfoDO> GetCompanyInfoByShopId(long shopId);
        /// <summary>
        /// 根据公司名称查询公司简单信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        Task<CompanySimpleInfoDO> GetCompanyByName(string name, string simpleName);

        /// <summary>
        /// 名称模糊查询公司
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<CompanyDO>> GetCompanyByName(string name);

        /// <summary>
        /// 查询公司下的子公司和门店
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<CompanyLessInfoDO>> GetCompanyAndShopByParentId(long parentId);
        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateCompanyStatus(UpdateCompanyStatusRequest request);

        /// <summary>
        /// 根据名称查公司数量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        Task<int> GetCompanyTotalByName(string simpleName, long id);

        /// <summary>
        /// 更新押金
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depositAmount"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> UpdateCompanyDeposit(long companyId, decimal depositAmount, string updateBy);
    }
}
