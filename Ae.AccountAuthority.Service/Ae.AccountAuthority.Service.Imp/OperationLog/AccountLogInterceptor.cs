using System;
using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Imp.Services;

namespace Ae.AccountAuthority.Service.Imp.OperationLog
{
    public class AccountLogInterceptor : AbstractRepository<AccountDO>, IInterceptor
    {
        private readonly ApolloErpLogger<AccountService> logger;
        private readonly LogOperationReqDTO logReq;
        private readonly string className;

        public AccountLogInterceptor(ApolloErpLogger<AccountService> logger, LogOperationReqDTO logReq)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
            this.logReq = logReq;
            className = this.GetType().Name;
        }

        public void Intercept(IInvocation invocation)
        {
            logReq.OperatedBeforeContent = GetAccountBeforeContent();

            invocation.Proceed();

            logReq.OperatedAfterContent = GetAccountAfterContent();
            InsertAccountLog();
        }

        private string GetAccountBeforeContent()
        {
            try
            {
                var res = logReq.LogId.Replace("，", ",").Contains(CommonConstant.SeparatorComma)
                    ? GetList<AccountDO>($"where id IN ({logReq.LogId}) ").ToList()
                    : GetList<AccountDO>("where id = @Id ", new { Id = logReq.LogId }).ToList();
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBQueryException} SVC: {className}\n"), e);
                return "";
            }
        }

        private string GetAccountAfterContent()
        {
            try
            {
                var res = logReq.LogId.Replace("，", ",").Contains(CommonConstant.SeparatorComma)
                    ? GetList<AccountDO>($"where id IN ({logReq.LogId}) ").ToList()
                    : GetList<AccountDO>("where id = @Id ", new { Id = logReq.LogId }).ToList();
                return JsonConvert.SerializeObject(res);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBQueryException} SVC: {className}\n"), e);
                return "";
            }
        }

        private void InsertAccountLog()
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
                    Comment = logReq.Comment.IsNotNullOrWhiteSpace() ? $"Ae.AccountAuthority.Service: {logReq.Comment}" : "Ae.AccountAuthority.Service"
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBInsertException} SVC: {className}\n"), e);
            }
        }

    }
}
