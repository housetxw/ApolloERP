using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class TechnicianRepository : AbstractRepository<TechnicianDO>, ITechnicianRepository
    {
        private IConfiguration configuration;

        private readonly ApolloErpLogger<TechnicianRepository> logger;
        private readonly string className;

        public TechnicianRepository(ApolloErpLogger<TechnicianRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;


        }

        public async Task<TechnicianDO> GetTechnicianInfo(string accountId)
        {
            TechnicianDO res = new TechnicianDO();
            try
            {
                string conditions = " where is_deleted =0 and account_id = @AccountId ";

                var param = new DynamicParameters();
                param.Add("@AccountId", accountId);

                var employees = await GetListAsync(conditions, param);

                res = employees.FirstOrDefault();
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.DBQueryException}\n{CommonConstant.ParameterReqDetail}";
                logger.Error(msg, e);
            }
            return res;
        }

        public async Task<bool> UpdateTechnicianById(TechnicianDO req)
        {
            bool res;
            try
            {
                var param = new List<string>
                {
                    "Avatar", "Name", "OrganizationId", "DepartmentId", "Type", "Identity", "Number", "Mobile", "Email", "Gender", "WeChat", "QQ",
                    "Level", "WorkKindId", "JobId", "QualificationCertificate", "Description", "IsDeleted", "UpdateBy", "UpdateTime"
                };

                res = await UpdateAsync(req, param) > 0;
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            return res;
        }
    }
}
