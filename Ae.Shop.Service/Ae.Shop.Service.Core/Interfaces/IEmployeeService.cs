using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IEmployeeService
    {
        #region ！！！登录接口相关，请勿轻易对其修改！！！

        Task<List<EmployeeResDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req);

        Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req);

        Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<EmployeePageResDTO> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<EmployeePageResDTO> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req);

        Task<List<long>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req);


        #endregion ！！！登录接口相关，请勿轻易对其修改！！！


        Task<List<EmployeeResDTO>> GetAllEmployeeListAsync();

        Task<List<EmployeeResDTO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req);

        Task<EmployeePageResDTO> GetEmployeePage(EmployeePageReqDTO req);

        Task<EmployeeInfoDTO> GetEmployeeInfo(EmployeeInfoReqDTO reqDto);
        Task<EmployeeSimpleInfoResponse> GetEmployeeSimpleInfo(EmployeeInfoReqDTO reqDto);

        Task<List<EmployeeInfoDTO>> GetEmployeeListByShopIdAsync(GetEmployeeListByOrganizationIdRequest request);

        Task<List<EmployeeInfoDTO>> GetEmployeeListByEmpId(List<string> empId);

        Task<ApiResult<List<RoleListResVO>>> GetRoleListByOrgIdAndType(RoleListReqVO req);

        Task<List<OrgRangeRolesVO>> GetRoleListByOrgId(long orgId);

        Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName(AccountEntityReqByNameDTO req);

        Task<ApiResult> CreateOrUpdateEmployee(EmployeeEntityReqDTO req);

        Task<ApiResult> CreateEmployee(EmployeeEntityReqDTO req, bool isCreateShop = false, bool isSendMessage = false,string roleDefaultName="");

        /// <summary>
        /// 获取门店员工列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopEmployeeDTO>> GetShopEmployeePagedListAsync(BasePageShopRequest request);

        List<EmployeeLevelListResDTO> GetEmployeeLevelList();

        Task<bool> DeleteEmployeeById(EmployeeDeleteReqByIdDTO req);

        Task<bool> BatchDeleteEmployeeById(EmployeeBatchDeleteReqByIdDTO req);

        Task<ApiPagedResultData<TechnicianPageDTO>> GetTechnicianPage(TechnicanPageReqDTO req);

        Task<ApiResult> AddOrEidtEmployeeGroup(EmployeeGroupDTO req);

        Task<ApiResult<List<EmployeeGroupDTO>>> GetEmployeeGroupList(GetEmployeeGroupListRequest request);

        Task<ApiResult> AddOrEidtTechnician(EmployeeEntityReqDTO req);


    }
}
