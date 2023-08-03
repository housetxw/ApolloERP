using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    public enum AccountCheckResultEnum
    {
        已核对 = 1,
        未核对 = 2,
        核对异常 = 3
    }

    public enum AccountCheckRelationTypeEnum
    {
        总部 = 1,
        门店,
        结算单=3
    }

    public enum AccountCheckWithdrawStatus
    {
        未申请=1,
        已申请 = 2,
        申请失败=3,
        已结算,
        申请成功
    }

    public enum AccountCheckExceptionStatus { 
        新建=1,
        已处理
    }

    public enum AccountCheckExceptionType {
        其他=1,
        需要给门店补服务费,
        订单服务费计算错误
    }
}
