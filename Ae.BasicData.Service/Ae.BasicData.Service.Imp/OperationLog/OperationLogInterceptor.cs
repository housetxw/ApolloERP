using System;
using System.Linq;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Enums;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;
using Ae.BasicData.Service.Imp.Services;

namespace Ae.BasicData.Service.Imp.OperationLog
{
    public class OperationLogInterceptor : IInterceptor
    {
        private readonly ApolloErpLogger<OperationLogInterceptor> logger;
        private readonly IBaseLogRepository logRepo;
        private readonly LogOperationReqDTO logReq;

        public OperationLogInterceptor(ApolloErpLogger<OperationLogInterceptor> logger,
            IBaseLogRepository logRepo,
            LogOperationReqDTO logReq)
        {
            this.logger = logger;
            this.logRepo = logRepo;
            this.logReq = logReq;
        }

        public void Intercept(IInvocation invocation)
        {
            logReq.OperatedBeforeContent = logRepo.GetRegionChinaBeforeContent(logReq);

            invocation.Proceed();

            logReq.OperatedAfterContent = logRepo.GetRegionChinaAfterContent(logReq);
            logRepo.InsertLog(logReq);
        }

    }
}
