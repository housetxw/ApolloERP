using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Dal.Model;
using Ae.B.Login.Api.Imp.Services;

namespace Ae.B.Login.Api.Imp.OperationLog
{
    public class AccountLogInterceptor : AbstractRepository<AccountDO>, IInterceptor
    {
        private readonly ApolloErpLogger<AccountService> logger;
        private readonly LogOperationReqDTO logReq;
        private readonly string className;

        public AccountLogInterceptor(ApolloErpLogger<AccountService> logger, LogOperationReqDTO logReq)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSqlReadOnly");

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
                var res = GetList<AccountDO>("where id = @Id ", new { Id = logReq.LogId }).ToList();
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
                var res = GetList<AccountDO>("where id = @Id ", new { Id = logReq.LogId }).ToList();
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
                    Comment = logReq.Comment.IsNotNullOrWhiteSpace() ? $"Ae.B.Login.Api: {logReq.Comment}" : "Ae.B.Login.Api"
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(logReq).GenDBExceptionInfo($"{CommonConstant.DBInsertException} SVC: {className}\n"), e);
            }
        }

    }
}
