using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface IEmployeeRepository : IRepository<EmployeeDO>
    {
        #region  ！！！登录接口相关，请勿轻易对其修改！！！

        Task<List<EmployeeListDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdExcCurOrgIdAsync(EmployeePageForLoginListReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req);

        Task<List<OrgRangeRoleListForLoginResDTO>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req);

        Task<bool> EditEmployeeRangeRole(EmployeeRangeRoleSaveReqDO req);


        #endregion  ！！！登录接口相关，请勿轻易对其修改！！！

        Task<List<EmployeeDO>> GetAllEmployeeListAsync();

        Task<List<EmployeeDO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req);

        Task<PagedEntity<EmployeePageDTO>> GetEmployeePage(EmployeePageReqDTO req);

        Task<EmployeeCustomDO> GetEmployeeInfo(EmployeeInfoReqDTO req);

        Task<List<OrgRangeRoleListForLoginResDTO>> GetOrgRangeRoleListByEmpId(OrgRangeRoleListForLoginReqDTO req);

        Task<List<EmployeeDO>> GetEmployeeListByShopIdAsync(long organizationId, int type);

        Task<List<EmployeeDO>> GetEmployeeListByEmpId(List<string> empId);

        Task<EmployeeDO> EmployeeVerify(EmployeeEntityReqDTO req);

        /// <summary>
        /// 获取门店员工列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopEmployeeDO>> GetShopEmployeePagedListAsync(BasePageShopRequest req);

        Task<bool> UpdateEmployeeById(EmployeeDO req);

        Task<bool> DeleteEmployeeById(EmployeeDeleteReqByIdDTO req);

        Task<bool> BatchDeleteEmployeeById(EmployeeBatchDeleteReqByIdDTO req);

        Task<PagedEntity<EmployeeCustomDO>> GetTechnicianPage(TechnicanPageReqDTO req);

        Task<List<EmployeeDO>> GetEmployeesByShopIds(List<long> shopIds);

    }
}
