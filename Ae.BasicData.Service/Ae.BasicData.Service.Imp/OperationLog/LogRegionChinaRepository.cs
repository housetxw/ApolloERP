using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;

namespace Ae.BasicData.Service.Imp.OperationLog
{
    public class LogRegionChinaRepository : AbstractRepository<RegionChinaDO>, IBaseLogRepository
    {
        private readonly ApolloErpLogger<OperationLogInterceptor> logger;
        private readonly string className;

        public LogRegionChinaRepository(ApolloErpLogger<OperationLogInterceptor> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("BasicDataSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        public string GetRegionChinaBeforeContent(LogOperationReqDTO logReq)
        {
            try
            {
                var res = logReq.LogId.Replace("，", ",").Contains(CommonConstant.SeparatorComma)
                                    || logReq.LogId.Contains(CommonConstant.SeparatorSingleQuotes)
                    ? GetList<RegionChinaDO>($"WHERE id IN ({logReq.LogId}) ").ToList()
                    : GetList<RegionChinaDO>("WHERE id = @Id ", new { Id = logReq.LogId }).ToList();
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBQueryException} SVC: {className}\n"), e);
                return "";
            }
        }

        public string GetRegionChinaAfterContent(LogOperationReqDTO logReq)
        {
            try
            {
                var res = logReq.LogId.Replace("，", ",").Contains(CommonConstant.SeparatorComma)
                          || logReq.LogId.Contains(CommonConstant.SeparatorSingleQuotes)
                    ? GetList<RegionChinaDO>($"WHERE id IN ({logReq.LogId}) ").ToList()
                    : GetList<RegionChinaDO>("WHERE id = @Id ", new { Id = logReq.LogId }).ToList();
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBQueryException} SVC: {className}\n"), e);
                return "";
            }
        }

        public void InsertLog(LogOperationReqDTO logReq)
        {
            try
            {
                InsertAsync(new LogOperationDO
                {
                    LogId = logReq.LogId,
                    LogType = logReq.LogType,
                    BizType = logReq.BizType,
                    ReqParam = logReq.ReqParam,
                    OperatedBeforeContent = logReq.OperatedBeforeContent,
                    OperatedAfterContent = logReq.OperatedAfterContent,
                    Operator = logReq.Operator,
                    Comment = logReq.Comment.IsNotNullOrWhiteSpace() ? $"Ae.BasicData.Service: {logReq.Comment}" : "Ae.BasicData.Service"
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBInsertException} SVC: {className}\n"), e);
            }
        }


    }
}
