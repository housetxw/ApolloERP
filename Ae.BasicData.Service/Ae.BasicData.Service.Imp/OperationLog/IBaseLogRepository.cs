using System;
using System.Collections.Generic;
using System.Text;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Request;

namespace Ae.BasicData.Service.Imp.OperationLog
{
    public interface IBaseLogRepository
    {
        string GetRegionChinaBeforeContent(LogOperationReqDTO logReq);
        string GetRegionChinaAfterContent(LogOperationReqDTO logReq);
        void InsertLog(LogOperationReqDTO logReq);
    }
}
