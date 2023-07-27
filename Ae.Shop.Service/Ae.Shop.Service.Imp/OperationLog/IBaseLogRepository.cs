using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request;

namespace Ae.Shop.Service.Imp.OperationLog
{
    public interface IBaseLogRepository
    {
        string GetRegionChinaBeforeContent(LogOperationReqDTO logReq);
        string GetRegionChinaAfterContent(LogOperationReqDTO logReq);
        void InsertLog(LogOperationReqDTO logReq);
    }
}
