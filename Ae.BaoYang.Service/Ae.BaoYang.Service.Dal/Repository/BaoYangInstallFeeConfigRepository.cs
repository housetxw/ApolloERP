using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Condition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangInstallFeeConfigRepository : AbstractRepository<BaoYangInstallFeeConfigDO>,
        IBaoYangInstallFeeConfigRepository
    {
        public BaoYangInstallFeeConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 查询安装服务费加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<BaoYangInstallFeeConfigDO>> GetBaoYangInstallFeeConfigPageList(
            BaoYangInstallFeeConfigPageListCondition request)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE 0 = 0");
            if (!string.IsNullOrEmpty(request.ServiceId))
            {
                condition.Append(" AND service_id = @serviceId");
                para.Add("@serviceId", request.ServiceId);
            }

            if (request.GuidePrice.HasValue)
            {
                condition.Append(" AND car_min_price <= @guidePrice AND car_max_price > @guidePrice");
                para.Add("@guidePrice", request.GuidePrice.Value);
            }

            var result = await GetListPagedAsync<BaoYangInstallFeeConfigDO>(request.PageIndex, request.PageSize,
                condition.ToString(), "`service_id` ASC,additional_price", para);

            return result;
        }

        /// <summary>
        /// pid查询安装服务配置
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<BaoYangInstallFeeConfigDO>> GetInstallFeeConfigByPid(string serviceId,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@serviceId", serviceId);
            var result = await GetListAsync("WHERE service_id = @serviceId", para, !readOnly);
            return result?.AsList() ?? new List<BaoYangInstallFeeConfigDO>();
        }

        /// <summary>
        /// pidList查询安装服务配置
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="guidPrice"></param>
        /// <returns></returns>
        public async Task<List<BaoYangInstallFeeConfigDO>> GetInstallFeeConfigByPidList(List<string> serviceId,
            decimal guidPrice)
        {
            var para = new DynamicParameters();
            para.Add("@serviceId", serviceId);
            para.Add("@guidePrice", guidPrice);
            var result =
                await GetListAsync(
                    "WHERE service_id IN @serviceId AND car_min_price <= @guidePrice AND car_max_price > @guidePrice",
                    para);
            return result?.AsList() ?? new List<BaoYangInstallFeeConfigDO>();
        }
    }
}
