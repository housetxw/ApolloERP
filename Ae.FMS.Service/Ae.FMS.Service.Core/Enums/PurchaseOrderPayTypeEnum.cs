using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    public enum PurchaseOrderPayTypeEnum
    {
        [Description("现金")]
        Money=1,
        [Description("账期")]
        AccountPeriod = 2,
        [Description("月结")]
        Month =3

    }
}
