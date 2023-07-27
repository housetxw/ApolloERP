using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Imp.OperationLog
{
    /// <summary>
    /// TODO: <ShopDO> 目前是随便写的，用到时，需要修改
    /// </summary>
    public class LogShopRepository : AbstractRepository<ShopDO>, IBaseLogRepository
    {
        private readonly ApolloErpLogger<OperationLogInterceptor> logger;
        private readonly string className;

        public LogShopRepository(ApolloErpLogger<OperationLogInterceptor> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        public string GetRegionChinaBeforeContent(LogOperationReqDTO logReq)
        {
            try
            {
                var res = logReq.LogId.Replace("，", ",").Contains(CommonConstant.SeparatorComma)
                                    || logReq.LogId.Contains(CommonConstant.SeparatorSingleQuotes)
                    ? GetList<ShopDO>($"WHERE id IN ({logReq.LogId}) ").ToList()
                    : GetList<ShopDO>("WHERE id = @Id ", new { Id = logReq.LogId }).ToList();
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
                    ? GetList<ShopDO>($"WHERE id IN ({logReq.LogId}) ").ToList()
                    : GetList<ShopDO>("WHERE id = @Id ", new { Id = logReq.LogId }).ToList();
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
                    Comment = logReq.Comment.IsNotNullOrWhiteSpace() ? $"Ae.Shop.Service: {logReq.Comment}" : "Ae.Shop.Service"
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBInsertException} SVC: {className}\n"), e);
            }
        }


    }
}
