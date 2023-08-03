using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Dal.Model.Company;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Dal.Repositorys.Company
{
    public interface ICompanyRepository : IRepository<CompanyDO>
    {
        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="CompanyDO"></param>
        /// <returns></returns>
        Task<int> AddCompanyAsync(CompanyDO CompanyDO);
        /// <summary>
        /// 查询公司列表-分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<CompanyDO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request);
        /// <summary>
        /// 根据公司名称查询公司简单信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        Task<CompanySimpleInfoDO> GetCompanyByName(string name, string simpleName);

        /// <summary>
        /// 查询公司下属门店数量
        /// </summary>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        Task<List<CompanyContainShopsDO>> GetShopCountUnderTheCompanyAsync(List<long> companyIds);
        /// <summary>
        /// 根据名称查公司数量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        Task<int> GetCompanyTotalByName(string simpleName, long id);
        Task<List<ShopSimpleInfoDO>> GetShopWareHouseListAsync(GetShopListRequest request);
    }
}
