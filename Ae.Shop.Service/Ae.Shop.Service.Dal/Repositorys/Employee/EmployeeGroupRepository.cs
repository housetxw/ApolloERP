using Dapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Employee
{
    public class EmployeeGroupRepository : AbstractRepository<EmployeeGroupDO>, IEmployeeGroupRepository
    {
        private IConfiguration configuration;
        private readonly string qiNiuImageDomain;
        private readonly long technicanJobId;

        private readonly ApolloErpLogger<EmployeeGroupRepository> logger;
        private readonly string className;

        public EmployeeGroupRepository(ApolloErpLogger<EmployeeGroupRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;

            qiNiuImageDomain = configuration["QiNiuImageDomain"]?.ToString() ?? "";


        }

        public async Task<List<EmployeeGroupDTO>> GetEmployeeGroupList(GetEmployeeGroupListRequest request)
        {
            var res = new List<EmployeeGroupDTO>();


            var param = new DynamicParameters();


            var condition = new StringBuilder();



            if (!string.IsNullOrEmpty(request.Name))
            {
                condition.AppendLine(" And name=@Name");

                param.Add("@Name", request.Name);
            }

            if (!string.IsNullOrEmpty(request.Mobile))
            {
                condition.AppendLine(" And mobile=@Mobile");

                param.Add("@Mobile", request.Mobile);
            }

            if (!string.IsNullOrEmpty(request.GroupName))
            {
                condition.AppendLine(" And group_name=@GroupName");

                param.Add("@GroupName", request.GroupName);
            }

            if (!string.IsNullOrEmpty(request.GroupLeader))
            {
                condition.AppendLine(" And group_leader=@GroupLeader");

                param.Add("@GroupLeader", request.GroupName);
            }

            if (request.ShopId>0)
            {
                condition.AppendLine(" And shop_id=@ShopId");

                param.Add("@ShopId", request.ShopId);
            }

            var sql = @"select id Id, employee_id EmployeeId,`name` Name,mobile Mobile,group_name GroupName,group_leader GroupLeader,shop_id ShopId, create_by CreateBy,
                    create_time CreateTime,update_by UpdateBy,update_time UpdateTime

                    from employee_group where is_deleted=0 " + condition.ToString();




            await OpenSlaveConnectionAsync(async conn => res = (await conn.QueryAsync<EmployeeGroupDTO>(sql, param)).ToList());

            return res;
        }

    }
}
